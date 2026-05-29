using System;
using System.Collections.Generic;

namespace CyberSecurityAwareness_pp2
{
    internal class Respond
    {
        // Random number generator
        private readonly Random _rng = new Random();

        // Topic -> responses (keyword recognition + random responses)
        private readonly Dictionary<string, List<string>> _responses
            = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        // Sentiment -> responses
        private readonly Dictionary<string, List<string>> _sentiments
            = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        // Stop words
        private readonly HashSet<string> _stopWords
            = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Default fallback responses
        private readonly List<string> _defaults = new List<string>
        {
            "I'm not sure I understand. Could you try rephrasing that?",
            "Hmm, I didn't quite catch that. Try asking about phishing, passwords, firewalls, or malware.",
            "I'm still learning! Could you rephrase or ask about a cybersecurity topic?",
            "That's outside my knowledge for now. Ask me about online safety, scams, VPNs, or password tips!"
        };

        public Respond()
        {
            LoadResponses();
            LoadSentiments();
            LoadStopWords();
        }

        // ── PUBLIC API ────────────────────────────────────────────────────

        // Scan input for a known keyword and return a random matching response.
        // Also outputs the matched topic name for conversation-flow follow-ups.
        public string MatchKeyword(string input, out string matchedTopic)
        {
            matchedTopic = string.Empty;

            string[] words = input.ToLower().Split(
                new[] { ' ', ',', '.', '?', '!' },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in words)
            {
                if (_stopWords.Contains(word)) continue;

                foreach (var entry in _responses)
                {
                    if (input.Contains(entry.Key) || entry.Key.Contains(word))
                    {
                        matchedTopic = entry.Key;
                        return PickRandom(entry.Value);
                    }
                }
            }

            return string.Empty;
        }

        // Return a random response for a known topic (used for follow-ups)
        public string GetResponse(string topic)
        {
            if (_responses.TryGetValue(topic, out List<string> list))
                return PickRandom(list);
            return string.Empty;
        }

        // Check input for sentiment words and return empathetic reply
        public string DetectSentiment(string input)
        {
            foreach (var entry in _sentiments)
                if (input.Contains(entry.Key))
                    return PickRandom(entry.Value);
            return string.Empty;
        }

        // Return a random fallback for unrecognised input
        public string GetDefault() => PickRandom(_defaults);

        // Check if a word is a stop word
        public bool IsStopWord(string word) => _stopWords.Contains(word);

        // ── PRIVATE HELPER ────────────────────────────────────────────────

        private string PickRandom(List<string> list)
        {
            if (list == null || list.Count == 0) return string.Empty;
            return list[_rng.Next(list.Count)];
        }

        // ── RESPONSES ─────────────────────────────────────────────────────

        private void LoadResponses()
        {
            Add("greeting",
                "I'm doing well, thanks for asking! How are you doing today?",
                "Hey there! Great to chat with you. How can I help you stay safe online?",
                "Hi! I'm CyberSafe AI — ready to answer your cybersecurity questions.");

            Add("hello",
                "Hello! Welcome to CyberSafe AI. What cybersecurity topic can I help with?",
                "Hey! Ask me anything about online safety.",
                "Hi there! I'm here to help you stay secure online.");

            Add("hi",
                "Hi! What can I help you with today?",
                "Hello! Ask me about passwords, phishing, malware — anything cybersecurity!",
                "Hey! Great to see you. What's on your mind?");

            Add("purpose",
                "My purpose is to educate you on how to stay safe online.",
                "I help users understand online safety and digital protection.",
                "I assist with cybersecurity awareness and safety guidance.");

            Add("cybersecurity",
                "Cybersecurity is about protecting systems and networks from digital threats.",
                "It involves protecting devices and online accounts from malicious attacks.",
                "It focuses on securing digital information and preventing unauthorised access.");

            Add("password",
                "Make sure to use strong, unique passwords for each account. Avoid using personal details.",
                "A strong password should be at least 12 characters long with letters, numbers, and symbols.",
                "Avoid reusing passwords — a password manager can help you keep track of unique ones.",
                "Never share your passwords with anyone, even people you trust.");

            Add("phishing",
                "Be cautious of emails asking for personal information. Scammers disguise themselves as trusted organisations.",
                "Phishing uses fake messages or websites to trick you into revealing sensitive data.",
                "Look out for urgent language, misspelled domains, and unexpected attachments.",
                "When in doubt, go directly to the official website instead of clicking email links.");

            Add("scam",
                "Scammers often create a false sense of urgency — slow down and verify before acting.",
                "If an offer sounds too good to be true, it probably is.",
                "Report scams to your local cybercrime authority to help protect others.",
                "Never transfer money or share banking details based on an unsolicited request.");

            Add("privacy",
                "Review your social media privacy settings regularly.",
                "Avoid sharing personal details like your address or ID number online unnecessarily.",
                "Use a VPN when accessing sensitive information on public networks.",
                "Read app permissions carefully — many apps request more access than they need.");

            Add("malware",
                "Malware is malicious software designed to damage or gain unauthorised access to systems.",
                "Keep your antivirus software up to date to detect and remove malware.",
                "Avoid downloading software from untrusted sources — always use official stores.",
                "Regular backups are your best defence against ransomware.");

            Add("firewall",
                "A firewall controls network traffic based on security rules to block unauthorised access.",
                "It acts as a protective barrier between trusted and untrusted networks.",
                "Enable your operating system's built-in firewall for a basic layer of protection.",
                "Both hardware and software firewalls work together to keep your network secure.");

            Add("vpn",
                "A VPN encrypts your internet traffic and hides your IP address.",
                "Use a VPN when connecting to public Wi-Fi to prevent eavesdropping.",
                "Choose a reputable, no-logs VPN provider.",
                "A VPN protects your data in transit but doesn't make you fully anonymous online.");

            Add("two factor",
                "Two-factor authentication (2FA) adds an extra layer of security beyond your password.",
                "Enable 2FA on all important accounts — it stops attackers even if your password is stolen.",
                "Use an authenticator app rather than SMS for stronger 2FA protection.");

            Add("2fa",
                "2FA requires a second verification step, making it much harder for attackers to access your accounts.",
                "Even if your password leaks, 2FA can prevent unauthorised login.",
                "Set up 2FA on email, banking, and social media accounts as a priority.");

            Add("hacked",
                "If your account is hacked, change your password immediately and log out of all devices.",
                "Contact the platform's support team if you suspect your account has been compromised.",
                "Enable two-factor authentication after recovering a hacked account.",
                "Check your account's login history for suspicious activity.");

            Add("fraud",
                "Contact your bank immediately if you suspect financial fraud on your account.",
                "Report suspicious financial activity to your country's cybercrime authority.",
                "Monitor your bank statements regularly for unusual transactions.",
                "Protect your ID number and banking details carefully to prevent identity theft.");

            Add("social engineering",
                "Social engineering manipulates people into revealing confidential information.",
                "Attackers may impersonate IT support or authority figures to gain your trust.",
                "Always verify a caller's identity through an official channel before sharing anything.");

            Add("data breach",
                "A data breach exposes sensitive information — change passwords for affected accounts immediately.",
                "Use HaveIBeenPwned to check whether your email has appeared in a known breach.",
                "After a breach, monitor your credit report for signs of identity theft.");

            Add("encryption",
                "Encryption scrambles your data so only authorised parties can read it.",
                "Always use HTTPS websites — the padlock icon means your connection is encrypted.",
                "Encrypt sensitive files on your device to protect them if it is lost or stolen.");

            Add("backup",
                "Back up your important data using the 3-2-1 rule: 3 copies, 2 different media, 1 offsite.",
                "Cloud backups protect you from ransomware and hardware failure.",
                "Test your backups periodically to make sure they can actually be restored.");

            Add("update",
                "Keep your software and operating system updated — patches fix known security vulnerabilities.",
                "Enable automatic updates where possible so you never miss a critical security fix.",
                "Outdated software is one of the most common ways attackers gain access to systems.");

            Add("malicious chatbot",
                "Malicious bots often create urgency to trick users into sharing sensitive information.",
                "Fake chatbots may ask for passwords or payment details — legitimate services never do this.",
                "Be cautious if a bot pressures you for personal data or makes unusual requests.");
        }

        // ── SENTIMENTS ────────────────────────────────────────────────────

        private void LoadSentiments()
        {
            AddSentiment("frustrated",
                "I understand you're frustrated. Let's work through this step by step — I'm here to help.",
                "It's completely normal to feel that way. Take a breath; we'll sort this out together.",
                "Frustration is understandable! Let me try to make this clearer for you.");

            AddSentiment("confused",
                "No worries — confusion is totally normal. Let me break it down more clearly.",
                "Let me explain that step by step so it makes sense.",
                "That's okay! I'll help you understand it better. What part is unclear?");

            AddSentiment("worried",
                "It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe.",
                "Don't panic — most cybersecurity issues can be fixed. Let's go through it together.",
                "I understand your concern. Let's make sure your information is secure.");

            AddSentiment("scared",
                "It's okay to feel scared. Let me help you take control of the situation step by step.",
                "You're not alone — I'm here to guide you through this.",
                "Being cautious online is healthy. Let me give you some practical tips.");

            AddSentiment("happy",
                "That's great to hear! Let me know if you need any cybersecurity tips.",
                "Awesome! I'm glad things are going well.",
                "Love the positive energy! Ask me anything about staying safe online.");

            AddSentiment("excited",
                "Love the enthusiasm! Let's channel that into learning how to stay safer online.",
                "That's great! Curiosity is the first step to better cybersecurity awareness.",
                "Awesome! Ask me anything you'd like to know about cybersecurity.");

            AddSentiment("sad",
                "I'm sorry you're feeling this way. I'm here for you — let's focus on what we can fix.",
                "That sounds tough. Take things one step at a time — I'll help where I can.",
                "I hope things improve soon. Feel free to talk to me anytime.");

            AddSentiment("angry",
                "I understand you're angry. Let's work through the issue together calmly.",
                "Your frustration is valid — I'll do my best to resolve this for you.",
                "Take your time. I'm here to help you sort it out.");

            AddSentiment("curious",
                "Curiosity is the best starting point! What would you like to know more about?",
                "Great question mindset! Ask me anything cybersecurity-related.",
                "I love curious users — let's dive into whatever topic you're wondering about.");

            AddSentiment("overwhelmed",
                "It can feel like a lot at first. Let's take it one topic at a time — no rush.",
                "Cybersecurity can seem complex, but I'll break it down simply for you.",
                "Don't worry — I'll guide you through the important stuff step by step.");
        }

        // ── STOP WORDS ────────────────────────────────────────────────────

        private void LoadStopWords()
        {
            string[] stops =
            {
                "a","about","above","across","after","again","against","all","almost","alone",
                "along","already","also","although","always","am","among","an","and","another",
                "any","are","around","as","at","back","be","became","because","been","before",
                "being","below","beside","between","beyond","both","but","by","can","cannot",
                "could","did","do","does","doing","done","down","during","each","either","else",
                "enough","even","ever","every","everyone","everything","except","few","for",
                "from","further","had","has","have","having","he","her","here","him","himself",
                "his","how","however","i","if","in","indeed","inside","instead","into","is",
                "it","its","itself","last","later","least","less","lot","many","may","me",
                "meanwhile","might","more","most","mostly","much","must","my","myself","neither",
                "never","next","no","nobody","none","nor","not","nothing","now","nowhere","of",
                "off","often","on","once","one","only","or","other","others","otherwise","our",
                "ours","out","over","own","part","per","perhaps","please","put","rather","re",
                "same","see","seem","seemed","seems","several","she","should","show","since",
                "so","some","somehow","someone","something","sometime","sometimes","somewhere",
                "still","such","take","than","that","the","their","them","then","there","these",
                "they","this","those","though","through","thus","to","together","too","toward",
                "under","unless","until","up","upon","us","used","very","via","was","we","well",
                "were","what","whatever","when","whenever","where","whether","which","while",
                "who","whoever","whole","whom","whose","why","will","with","within","without",
                "would","yes","yet","you","your","yours","yourself","yourselves","tell","give",
                "me","help","explain","know","want","need","think","feel","ask"
            };

            foreach (string s in stops)
                _stopWords.Add(s);
        }

        // ── CONVENIENCE ADDERS ────────────────────────────────────────────

        private void Add(string keyword, params string[] answers)
        {
            _responses[keyword] = new List<string>(answers);
        }

        private void AddSentiment(string emotion, params string[] answers)
        {
            _sentiments[emotion] = new List<string>(answers);
        }
    }
}