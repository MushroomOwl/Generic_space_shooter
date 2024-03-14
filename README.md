# Simple Top-Down Space Shooter

> [!WARNING]
> The code in this project is not optimal and, due to the use of singletons, is highly coupled. This is merely a prototype and will not be developed further. For cleaner code examples, please check [Generic Tower Defense](https://github.com/MushroomOwl/Generic_tower_defence).

## Project Overview

**Simple Top-Down Space Shooter** is a straightforward prototype featuring three different ships, two levels, and various pickups to enhance gameplay. Designed as a top-down space shooter, it allows players to experience basic shooting mechanics and level progression.

## Features Implemented

- **Control Input Sources:** Supports both mobile and keyboard controls, accommodating various player preferences.
- **Dynamic Object Spawning:** Implements spontaneous generation of objects to maintain engaging gameplay.
- **Physics-Based Controls:** Utilizes physics for more natural and responsive movement and interaction.
- **Pickups and Bonuses:** Integrates mechanics for pickups and bonuses, adding depth to the gameplay.
- **Configurable Ships and Weapons:** Facilitates easy addition and reconfiguration of ships and weapons via scriptable objects.
- **Simple Win and Lose Conditions:** Employs an easily updatable system for defining game outcomes, enhancing the gameplay structure.

## Build Instructions

### For Desktop:

1. Open the project in Unity.
2. Navigate to `File > Build Settings`.
3. Select your desired platform (Windows, Mac, Linux).
4. Click `Build and Run`, and choose a location for the compiled game.
5. Once built, the game should start automatically.

### For Mobile:

1. Go to `Assets/ScriptableObjects` and select the `ControlScheme` scriptable object.
2. In the Inspector, switch the control scheme to 'Mobile'.
3. Navigate to `File > Build Settings` and select your mobile platform (iOS or Android).
4. Click `Build and Run`, making sure your mobile device is connected and selected for the build destination.
5. Follow the on-screen prompts to complete the build and install the game on your device.

## Warning

This project serves as a basic prototype to showcase specific game mechanics. The codebase may exhibit tight coupling and suboptimal patterns due to the use of singletons. This prototype is not intended for further development. For examples of more advanced and cleaner code, please refer to [Generic Tower Defense](https://github.com/MushroomOwl/Generic_tower_defence).
