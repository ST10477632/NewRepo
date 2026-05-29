# NewRepo
# # CyberSafe AI – Cybersecurity Awareness Chatbot

## Overview

CyberSafe AI is a WPF-based cybersecurity awareness chatbot developed in C#.
The application helps users learn about online safety, cybersecurity threats, and protection methods through an interactive chat interface.

The chatbot provides:

* Cybersecurity education
* Sentiment-aware responses
* Conversation memory
* Randomised responses
* Topic recognition
* Voice greeting
* ASCII art display
* Modern WPF user interface

---

# Features

## User Registration

* Users enter their name before accessing the chatbot
* Returning users are recognised
* Usernames are stored locally in a text file

---

## Cybersecurity Awareness

The chatbot provides information on:

* Password safety
* Phishing
* Malware
* VPNs
* Firewalls
* Identity theft
* Public Wi-Fi safety
* Authentication
* Online scams
* Privacy protection
* Safe browsing
* Ransomware

---

## Sentiment Detection

The chatbot detects emotions such as:

* Worried
* Frustrated
* Confused
* Angry
* Curious
* Happy
* Excited
* Overwhelmed

It responds with supportive and educational feedback.

---

## Memory and Personalisation

The chatbot:

* Remembers usernames
* Stores user interests
* Recalls previous interests for returning users
* Provides personalised responses

---

## Conversation Flow

The chatbot supports follow-up requests such as:

* “Tell me more”
* “Explain more”
* “Another tip”
* “Continue”

---

## Voice Greeting

A `.wav` audio greeting plays when the application starts.

---

## ASCII Art

CyberSafe AI displays cybersecurity-themed ASCII art when launched.

---

# Technologies Used

* C#
* WPF (Windows Presentation Foundation)
* .NET Framework
* XAML
* File Handling
* Object-Oriented Programming (OOP)

---

# Project Structure

```text
CyberSecurityAwareness_pp2
│
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── Respond.cs
├── george.wav
├── user_names.txt
├── interested_topic.txt
└── README.md
```

---

# How to Run the Project

## Requirements

* Visual Studio 2022
* .NET Framework
* Windows OS

---

## Steps

1. Open Visual Studio
2. Open the project solution
3. Build the project
4. Run the application using:

```text
Ctrl + F5
```

OR click:

```text
Start
```

---

# Example Questions

Users can ask:

* “Tell me about phishing”
* “How do I create a strong password?”
* “What is malware?”
* “What does a VPN do?”
* “I am worried about scams”
* “Tell me more”

---

# Input Validation

The chatbot validates:

* Empty messages
* Unknown inputs
* Conversation flow
* Username input

---

# File Storage

The application stores:

* Usernames in `user_names.txt`
* User interests in `interested_topic.txt`

---

# Future Improvements

Possible future upgrades:

* Database integration
* AI/NLP support
* Speech recognition
* Dark/light mode toggle
* User authentication
* More cybersecurity topics

---

# Author

Developed for the Programming POE Project using C# and WPF.

---
