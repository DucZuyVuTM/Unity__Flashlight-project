# 🔦 Flashlight Game

[![Vercel Deployment](https://img.shields.io/badge/Play_Now-flashlight--game.vercel.app-brightgreen?style=flat&logo=vercel&logoColor=white)](https://flashlight-game.vercel.app)
[![Unity Version](https://img.shields.io/badge/Unity-2022.3.x-orange?logo=unity)](https://unity.com/)
[![Netcode](https://img.shields.io/badge/Netcode-for%20GameObjects-2b5797)](https://docs-multiplayer.unity3d.com/netcode/current/about)

**A first-person shooter where you defend your flashlight from relentless robots — mind the ricochet, and don’t fall off the floating platform!**

[![Game Screenshot](https://via.placeholder.com/800x400.png?text=Flashlight+Game+-+Shoot+Robots+in+First+Person)](https://flashlight-game.vercel.app)

## 🎮 About the Game

In *Flashlight Game*, you are trapped on a levitating platform in the sky, holding only a flashlight. Your mission is to survive waves of robots that attack from both sides. Hit their armor at the wrong angle and your bullet will bounce away. Stay aware of your surroundings — one wrong step and you fall into the void.

> **Play the live WebGL build at [flashlight-game.vercel.app](https://flashlight-game.vercel.app)**

### Key Features

- **First-person shooter** with a unique flashlight‑centric gameplay.
- **Ricochet mechanic** – bullets bounce if the impact angle is too steep.
- **Multiplayer support** – play cooperatively with a friend (Host / Client).
- **Floating arena** – navigate a platform high above the ground (don't fall!).
- **WebGL & Desktop builds** – play directly in your browser or download for Linux/Windows.

## 🕹️ Controls

| Action | Key |
|--------|-----|
| Move | `W` `A` `S` `D` |
| Rotate / Look | `←` `→` |
| Shoot | `↑` (Up Arrow) |
| Pause | `↓` (Down Arrow) |
| Jump | `Space` |
| Exit Gameplay | `P` |

> **Tip:** `ESC` toggles full‑screen mode in the browser.

## 🧱 Project Structure

Unity__Flashlight-project/
├── Assets/          # Game scripts, scenes, prefabs, and assets
├── ProjectSettings/ # Unity project configuration
├── Packages/        # Package manifest
├── WebBuild/        # WebGL build (deployed to Vercel)
├── LinuxBuild/      # Linux standalone build
├── vercel.json      # Vercel deployment configuration (Unity WebGL headers)
└── .gitattributes   # Git LFS & line‑ending settings

## 🚀 Getting Started (Development)

### Prerequisites
- [Unity 2022.3 LTS](https://unity.com/releases/editor/whats-new/2022.3.0) or newer.
- **C# 9.0** and **.NET Standard 2.1**

### 1. Clone the Repository

Open a terminal and run:

```bash
git clone https://github.com/DucZuyVuTM/Unity__Flashlight-project.git
```

### 2. Open the Project in Unity

1. Launch **Unity Hub**.
2. Click **Add** → select the cloned `Unity__Flashlight-project` folder.
3. Open the project with Unity 2022.3 LTS or newer.

### 3. Build the Game

1. Go to `File → Build Settings`.
2. Choose your target platform (WebGL, Linux, or Windows).
3. Click **Build** and select an empty output folder.

### 4. Run the WebGL Build Locally

To test the WebGL build on your computer, you need a local web server. For example, using Python:

```bash
# Python 3
python -m http.server 8000

# Python 2
python -m SimpleHTTPServer 8000
```

Then open `http://localhost:8000` in your browser.

### 5. Deploy to Vercel (for WebGL)

1. Ensure the vercel.json file is present in your build output folder (it sets correct Content-Encoding: br headers).
2. Install the [Vercel CLI](https://vercel.com/cli) and run:

```bash
vercel --prod
```

## 🧠 Technical Highlights

- **Unity Netcode for GameObjects** – handles host/client synchronization.
- **XR Device Simulator** – used to debug VR/AR interactions.
- **Increased Runtime Speed** – maximizes execution performance.
- **Vercel Brotli compression** – speeds up WebGL asset delivery.

## 🤝 Contributing

This is a student / personal project, but issues and suggestions are welcome! Open a discussion on GitHub or submit a pull request.

## 📄 License

MIT – feel free to use the code as a reference for your own Unity multiplayer or WebGL experiments.

## 🙏 Credits

- Author: **Vu Duc Zuy**
- Group: IKBO-10-23
- University: RTU MIREA, 2026

- Email: <duczuyvu12@gmail.com>
- GitHub: [DucZuyVuTM](https://github.com/DucZuyVuTM)

- Special thanks to the Unity community for Netcode and XR toolkits.
