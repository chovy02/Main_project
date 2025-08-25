# Project Instructions

This README file explains how to **compile and run the project** using Unity.

---

## Requirements
Before you begin, ensure that you have:
- [Unity Hub](https://unity.com/download) installed.  
- The Unity Editor version specified in `ProjectSettings/ProjectVersion.txt`.  
- (Optional) An IDE such as **Visual Studio** or **Rider** for editing scripts.  

---

## How to Open the Project
1. Open **Unity Hub**.  
2. Click **Add Project from Disk**.  
3. Select the project’s root folder (the one containing `Assets/`, `Packages/`, and `ProjectSettings/`).  
4. If Unity Hub asks for a specific Unity version, install it.  
5. Open the project through Unity Hub.

---

## How to Compile the Project
Unity automatically compiles all scripts when they are saved.  
To create a standalone build of the project:
1. In Unity, go to **File > Build Settings**.  
2. Choose your target platform (Windows, macOS, Linux, Android, iOS, etc.).  
3. Add the scene(s) you want to include by clicking **Add Open Scenes**.  
4. Click **Build** (or **Build and Run** to test immediately).  
5. Select an output folder. Unity will generate the executable files there.  

---

## How to Run the Project
- **From the Unity Editor:** Press the **Play** button in the toolbar.  
- **From a Compiled Build:** Run the generated file (`.exe` for Windows, `.app` for macOS, `.apk` for Android, etc.).  

---

## Troubleshooting
- **Script errors:** Open the **Console** window (`Ctrl + Shift + C` / `Cmd + Shift + C`) to view error messages.  
- **Wrong Unity version:** Install the Unity version listed in `ProjectVersion.txt`.  
- **Missing packages:** Open **Window > Package Manager** in Unity to re-import dependencies.  
