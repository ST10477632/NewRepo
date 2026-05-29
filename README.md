# NewRepo
# 🛡️ CyberSafe AI – Cybersecurity Awareness Chatbot

## Overview

**CyberSafe AI** is a desktop chatbot application built using **C# and WPF (Windows Presentation Foundation)**. The chatbot is designed to educate users about **cybersecurity awareness**, helping them learn how to stay safe online through interactive conversations.

The application provides guidance on cybersecurity topics such as:

* Password safety
* Phishing attacks
* Malware
* Firewalls
* VPNs
* Two-Factor Authentication (2FA)
* Data breaches
* Privacy protection
* Social engineering
* Online scams and fraud

The chatbot also includes **basic memory features**, **sentiment detection**, and **conversation flow handling** for a more interactive user experience.

---

# Features

## 1. User Authentication / Greeting

* Users enter their name before starting a conversation.
* Returning users are recognized using local text file storage.
* Personalized greetings are displayed.

### New User Example

> Hey John, welcome to CyberSafe AI! 🛡️ I'm here to help you with cybersecurity. Ask me anything!

### Returning User Example

> Welcome back, John! Great to see you again.

---

## 2. Cybersecurity Knowledge Base

The chatbot provides educational responses on various cybersecurity topics using **keyword recognition**.

### Supported Topics

* Passwords
* Phishing
* Malware
* VPN
* Firewalls
* Privacy
* Encryption
* Backups
* Data Breaches
* Fraud
* Scams
* Software Updates
* Social Engineering
* Malicious Chatbots
* Two-Factor Authentication (2FA)

The system returns **randomized responses** for each topic to avoid repetitive conversations.

---

## 3. Sentiment Detection

CyberSafe AI can detect user emotions and respond empathetically.

### Supported Emotions

* Frustrated
* Confused
* Worried
* Scared
* Happy
* Excited
* Sad
* Angry
* Curious
* Overwhelmed

### Example

**User:**

> I'm worried about phishing emails.

**Bot:**

> I understand your concern. Let's make sure your information is secure.

---

## 4. Conversation Flow / Follow-Up Support

The chatbot remembers the **last cybersecurity topic discussed** and supports follow-up prompts.

### Example

**User:**

> Tell me about phishing

**Bot:**

> Phishing uses fake messages or websites to trick you into revealing sensitive data.

**User:**

> Tell me more

**Bot:**

> Look out for urgent language, misspelled domains, and suspicious attachments.

Supported follow-up phrases include:

* Tell me more
* Explain more
* Give me another tip
* More info
* Elaborate
* Continue
* What else

---

## 5. User Interest Memory

CyberSafe AI remembers topics users are interested in.

### Example

**User:**

> I like VPN security

The chatbot stores this information and recalls it during future sessions.

### Example Recall

> Last time you mentioned you were interested in: VPN security.

---

## 6. Modern WPF User Interface

The application uses a **modern dark-themed UI** built with WPF.

### UI Components

#### Welcome Screen

* Logo branding
* Username input
* Start Chat button

#### Chat Screen

* Chat bubbles
* Bot avatar
* Header with user status
* Message input area
* Send button
* Auto-scrolling messages

### Design Features

* Dark mode aesthetic
* Rounded corners
* Styled buttons and textboxes
* Responsive chat layout
* User/bot message differentiation

---

# Technologies Used

* **Language:** C#
* **Framework:** WPF (.NET)
* **UI Markup:** XAML
* **Architecture:** Event-driven desktop application
* **Storage:** Local text files

---

# Project Structure

```text
CyberSecurityAwareness_pp2/
│
├── MainWindow.xaml
│   ├── UI layout
│   ├── Styles
│   └── Chat interface
│
├── MainWindow.xaml.cs
│   ├── User input handling
│   ├── Chat flow logic
│   ├── Username management
│   ├── Memory system
│   └── Chat bubble rendering
│
├── Respond.cs
│   ├── Knowledge base
│   ├── Sentiment detection
│   ├── Keyword matching
│   ├── Stop words
│   └── Randomized responses
│
├── user_names.txt
│   └── Stores usernames
│
├── interested_topic.txt
│   └── Stores user interests
│
└── README.md
```

---

# How the Application Works

## Step 1: User Registration

The user enters their name on the welcome page.

The system:

1. Checks if the username exists in `user_names.txt`
2. Saves new users
3. Displays a personalized greeting

---

## Step 2: User Sends a Message

The user types a cybersecurity-related question.

Example:

> What is phishing?

---

## Step 3: Input Processing

The chatbot processes the input in this order:

### 1. Follow-up Detection

Checks for continuation phrases such as:

> Tell me more

### 2. Interest Recognition

Detects phrases like:

* I like
* I love
* Interested

### 3. Sentiment Detection

Detects emotional cues:

> worried, confused, frustrated

### 4. Keyword Matching

Matches cybersecurity topics.

### 5. Default Response

If no match is found:

> I'm not sure I understand. Could you try rephrasing that?

---

# Installation

## Prerequisites

Before running the project, ensure you have:

* **Visual Studio 2022 or later**
* **.NET Desktop Development workload**
* **WPF support installed**

---

## Steps to Run

1. Clone or download the repository.

```bash
git clone https://github.com/your-username/CyberSecurityAwareness_pp2.git
```

2. Open the solution in **Visual Studio**.

3. Build the project:

```bash
Build → Build Solution
```

4. Run the application:

```bash
F5
```

---

# File Storage

The chatbot stores user information locally.

## `user_names.txt`

Stores usernames for returning user recognition.

Example:

```text
John
Sarah
Mike
```

## `interested_topic.txt`

Stores user interests.

Example:

```text
John: vpn, cybersecurity
Sarah: phishing, malware
```

---

# Example Questions to Ask

Try asking:

* What is phishing?
* How do I create a strong password?
* Tell me about malware
* What is a VPN?
* Explain social engineering
* How can I stay safe online?
* What should I do if I get hacked?
* Tell me more

---

# Future Improvements

Potential enhancements for the project:

* Database integration instead of text files
* AI/NLP improvements
* Better keyword matching
* Voice interaction
* Chat history persistence
* User authentication system
* Typing animation
* Theme switching (light/dark mode)
* Machine learning sentiment analysis

---

# Known Limitations

* Uses simple keyword matching instead of advanced AI.
* Limited cybersecurity knowledge base.
* Stores data in text files only.
* Cannot understand complex sentence structures.
* No internet connectivity or real-time threat updates.

---

# Author

Developed as a **Cybersecurity Awareness Chatbot project** using **C#, WPF, and XAML**.

---

# License

This project is for **educational purposes only**.
