# 🎰 Slot Machine Game

A simple **3-reel Slot Machine game built with Unity and C#**, featuring animated reels, interactive UI, virtual currency, winning detection, and WebGL browser support.

## 🎮 Game Overview

The player spins three reels and tries to match symbols on the winning line.

**Gameplay:**

* Start with a virtual coin balance.
* Place a bet.
* Click **Spin** to start the reels.
* Reels stop independently.
* Matching symbols result in a payout.
* Balance is updated after each spin.

## ▶️ Run the WebGL Build

### Using Unity

1. Open the project in Unity.
2. Go to **File → Build Settings**.
3. Select **WebGL**.
4. Ensure the main game scene is included in **Scenes In Build**.
5. Click **Build and Run**.

### Using a Local Server

Unity WebGL builds must be served through HTTP and should **not** be opened directly with `file://`.

From the WebGL build directory, run:

Url:https://siddharth1134.itch.io/sloty


For this project, the project directory is:

## ✨ Bonus Features

* Custom slot-machine visual design
* Animated reel spinning and stopping
* Interactive UI
* Virtual balance and betting
* Winning combination detection
* WebGL browser support

## 🧠 Thought Process / Approach

The project was structured with a focus on **separating game logic, reel behaviour, and UI**.

The main approach was:


Slot Machine
├── Reels
├── Game Logic
│   ├── Spin
│   ├── Result
│   ├── Win Detection
│   └── Payout
└── UI
    ├── Balance
    ├── Bet
    └── Spin Button
```

Each reel operates independently, allowing the spinning and stopping animations to feel more natural. The UI is managed separately from the core game logic so the visual design can be changed without significantly affecting gameplay functionality.

The project was also prepared for **WebGL deployment**, allowing the game to run directly in a browser through a local or hosted web server.

## 🛠️ Technologies

* Unity
* C#
* Unity UI
* WebGL
* Git / GitHub

## 📌 Note

This is a game-development project using **virtual currency only**. It does not implement real-money gambling.
