using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberSecurityAwareness_pp2
{
    public partial class MainWindow : Window
    {
        // Chatbot engine
        private readonly Respond _bot = new Respond();

        // User memory
        private string _username = string.Empty;
        private string _lastTopic = string.Empty;

        // File paths
        private const string UsersFile = "user_names.txt";
        private const string InterestsFile = "interested_topic.txt";
        private const string VoiceFile = "voice_greeting";


        public MainWindow()
        {
            InitializeComponent();
            EnsureFiles();

            // ✅ Play voice greeting when the app opens
            PlayVoiceGreeting();

            // ✅ Show ASCII art in the chat on startup
            DisplayAsciiArt();

        }
        // Make sure storage files exist
        private void EnsureFiles()
        {
            if (!File.Exists(UsersFile))
                File.WriteAllText(UsersFile, string.Empty);

            if (!File.Exists(InterestsFile))
                File.WriteAllText(InterestsFile, string.Empty);
        }

        // ── VOICE GREETING ────────────────────────────────────────────────

        private void PlayVoiceGreeting()
        {
            // Get the project root folder (two levels up from \bin\Debug)
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = System.IO.Path.GetFullPath(
                                       System.IO.Path.Combine(baseDirectory, @"..\..\"));
            string voiceFile = System.IO.Path.Combine(projectRoot, "george.wav");

            // ✅ Actually call PlayVoice with the resolved path
            PlayVoice(voiceFile);
        }

        public static void PlayVoice(string voice)
        {
            try
            {
                if (!File.Exists(voice))
                {
                    MessageBox.Show(
                        "Voice file not found at:\n" + voice +
                        "\n\nMake sure george.wav is in the project root folder.",
                        "CyberSafe AI – Audio",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                using (SoundPlayer player = new SoundPlayer(voice))
                {
                    player.Load();
                    player.Play(); // ✅ Use Play() instead of PlaySync() so the UI doesn't freeze
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Audio error: " + ex.Message,
                                "CyberSafe AI – Audio",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        // ── ASCII ART ─────────────────────────────────────────────────────

        private void DisplayAsciiArt()
        {
            // ✅ Show in the chat ListView instead of Console.WriteLine
            string art =
                "==================================================\n" +
                "   ____       _                 _   _            \n" +
                "  / ___|  ___| |__   ___   ___ | |_(_) ___  _ __ \n" +
                "  \\___ \\ / __| '_ \\ / _ \\ / _ \\| __| |/ _ \\| '_ \\\n" +
                "   ___) | (__| | | | (_) | (_) | |_| | (_) | | | |\n" +
                "  |____/ \\___|_| |_|\\___/ \\___/ \\__|_|\\___/|_| |_|\n" +
                "\n" +
                "   ****** CYBERSECURITY AWARENESS BOT ******\n" +
                "==================================================";

            AddAsciiArtBubble(art);
        }

        // Special bubble for ASCII art — uses a monospace font so the art aligns correctly
        private void AddAsciiArtBubble(string art)
        {
            var border = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(13, 17, 23)),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(16, 12, 16, 12),
                Margin = new Thickness(0, 8, 0, 8),
                BorderBrush = new SolidColorBrush(Color.FromRgb(0, 200, 150)),
                BorderThickness = new Thickness(1)
            };

            border.Child = new TextBlock
            {
                Text = art,
                FontFamily = new FontFamily("Courier New"),  // ✅ Monospace so ASCII art aligns
                FontSize = 11,
                Foreground = new SolidColorBrush(Color.FromRgb(0, 200, 150)),
                TextWrapping = TextWrapping.NoWrap
            };

            chats.Items.Add(border);
        }

        // ── USERNAME PAGE ────────────────────────────────────────────────

        // Allow Enter key to submit username
        private void UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                submit_name(sender, e);
        }

        // Validate and register the username, then open chat
        private void submit_name(object sender, RoutedEventArgs e)
        {
            string name = user_name.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter your name before continuing.",
                                "CyberSafe AI",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            _username = name;
            bool returning = CheckName(name);

            if (!returning)
                SaveName(name);

            // Switch views
            name_grid.Visibility = Visibility.Hidden;
            chats_grid.Visibility = Visibility.Visible;
            header_user.Text = "👤 " + _username;

            // Welcome message
            string greeting = returning
                ? "Welcome back, " + _username + "! Great to see you again. How can I help you stay safe online today?"
                : "Hey " + _username + ", welcome to CyberSafe AI! 🛡️ I'm here to help you with cybersecurity. Ask me anything!";

            AddBotBubble(greeting);

            // Recall stored interests for returning users
            if (returning)
                RecallInterests();
        }

        
        // ── CHAT PAGE ────────────────────────────────────────────────────

        // Allow Enter key to send message
        private void Question_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                send(sender, e);
        }

        // Main send handler
        private void send(object sender, RoutedEventArgs e)
        {
            string input = question.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                AddBotBubble("Please type a message before sending.");
                return;
            }

            AddUserBubble(input);
            question.Clear();

            string response = ProcessInput(input);
            AddBotBubble(response);

            // Auto-scroll to latest message
            if (chats.Items.Count > 0)
                chats.ScrollIntoView(chats.Items[chats.Items.Count - 1]);
        }

        // ── INPUT PROCESSING ─────────────────────────────────────────────

        private string ProcessInput(string input)
        {
            string lower = input.ToLower();

            // 1. Conversation flow: follow-up requests
            if (IsFollowUp(lower))
            {
                if (!string.IsNullOrEmpty(_lastTopic))
                {
                    string follow = _bot.GetResponse(_lastTopic);
                    return string.IsNullOrEmpty(follow)
                        ? "I've shared what I know about '" + _lastTopic + "'. Try asking about phishing, passwords, or VPNs!"
                        : follow;
                }
                return "Sure! What topic would you like me to elaborate on?";
            }

            // 2. Memory: user expresses interest
            if (lower.Contains("interested") || lower.Contains("i like") || lower.Contains("i love"))
                return HandleInterest(input);

            // 3. Sentiment detection
            string sentimentResponse = _bot.DetectSentiment(lower);
            if (!string.IsNullOrEmpty(sentimentResponse))
            {
                string topicResponse = _bot.MatchKeyword(lower, out string topicFound);
                if (!string.IsNullOrEmpty(topicResponse))
                {
                    _lastTopic = topicFound;
                    return sentimentResponse + "\n\n" + topicResponse;
                }
                return sentimentResponse;
            }

            // 4. Keyword matching
            string keywordResponse = _bot.MatchKeyword(lower, out string topic);
            if (!string.IsNullOrEmpty(keywordResponse))
            {
                _lastTopic = topic;
                return keywordResponse;
            }

            // 5. Default fallback
            return _bot.GetDefault();
        }

        // ── INTEREST / MEMORY ────────────────────────────────────────────

        private string HandleInterest(string input)
        {
            string[] words = input.ToLower().Split(' ');
            var topics = new List<string>();

            foreach (string w in words)
            {
                string clean = w.Trim(',', '.', '!', '?');
                if (!_bot.IsStopWord(clean) &&
                    clean != "interested" && clean != "like" && clean != "love" &&
                    clean.Length > 2)
                {
                    topics.Add(clean);
                }
            }

            if (topics.Count == 0)
                return "That's great! Could you tell me a bit more about what you're interested in?";

            string topicList = string.Join(", ", topics);
            File.AppendAllText(InterestsFile, _username + ": " + topicList + "\n");

            return "Great, I'll remember that you're interested in " + topicList + "! " +
                   "It's a crucial part of staying safe online. Feel free to ask me anything about it.";
        }

        private void RecallInterests()
        {
            if (!File.Exists(InterestsFile)) return;

            string[] lines = File.ReadAllLines(InterestsFile);
            foreach (string line in lines)
            {
                if (line.StartsWith(_username + ":", StringComparison.OrdinalIgnoreCase))
                {
                    string interests = line.Substring(_username.Length + 1).Trim();
                    AddBotBubble("By the way, last time you mentioned you were interested in: " + interests +
                                 ". Feel free to ask me more about those topics!");
                    break;
                }
            }
        }

        // ── FOLLOW-UP DETECTION ──────────────────────────────────────────

        private static bool IsFollowUp(string lower)
        {
            string[] followPhrases =
            {
                "tell me more", "explain more", "give me another tip",
                "more info", "elaborate", "go on", "continue",
                "what else", "say more", "another tip", "more details"
            };

            foreach (string phrase in followPhrases)
                if (lower.Contains(phrase)) return true;

            return false;
        }

        // ── CHAT BUBBLE BUILDERS ─────────────────────────────────────────

        private void AddUserBubble(string message)
        {
            var panel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right
            };

            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(0, 200, 150)),
                CornerRadius = new CornerRadius(14, 14, 2, 14),
                Padding = new Thickness(12, 8, 12, 8),
                Margin = new Thickness(80, 4, 0, 4),
                MaxWidth = 520
            };

            bubble.Child = new TextBlock
            {
                Text = message,
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 13,
                Foreground = new SolidColorBrush(Color.FromRgb(13, 17, 23)),
                TextWrapping = TextWrapping.Wrap
            };

            panel.Children.Add(bubble);
            chats.Items.Add(panel);
        }

        private void AddBotBubble(string message)
        {
            var panel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            // Bot avatar
            var avatar = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(22, 27, 34)),
                CornerRadius = new CornerRadius(20),
                Width = 34,
                Height = 34,
                Margin = new Thickness(0, 4, 8, 4),
                VerticalAlignment = VerticalAlignment.Top
            };
            avatar.Child = new TextBlock
            {
                Text = "🛡",
                FontSize = 16,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            var bubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(22, 27, 34)),
                CornerRadius = new CornerRadius(14, 14, 14, 2),
                Padding = new Thickness(12, 8, 12, 8),
                Margin = new Thickness(0, 4, 80, 4),
                MaxWidth = 520,
                BorderBrush = new SolidColorBrush(Color.FromRgb(33, 38, 45)),
                BorderThickness = new Thickness(1)
            };

            bubble.Child = new TextBlock
            {
                Text = message,
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 13,
                Foreground = new SolidColorBrush(Color.FromRgb(230, 237, 243)),
                TextWrapping = TextWrapping.Wrap
            };

            panel.Children.Add(avatar);
            panel.Children.Add(bubble);
            chats.Items.Add(panel);
        }

        // ── FILE HELPERS ─────────────────────────────────────────────────

        private bool CheckName(string name)
        {
            string[] names = File.ReadAllLines(UsersFile);
            foreach (string n in names)
                if (n.Trim().Equals(name.Trim(), StringComparison.OrdinalIgnoreCase))
                    return true;
            return false;
        }

        private void SaveName(string name)
        {
            File.AppendAllText(UsersFile, name.Trim() + "\n");
        }
    }
}

