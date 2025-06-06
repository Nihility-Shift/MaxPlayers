[![](https://img.shields.io/badge/-Nihility_Shift-111111?style=just-the-label&logo=github&labelColor=24292f)](https://github.com/Nihility-Shift)
![](https://img.shields.io/badge/Game%20Version-[GameVersion]-111111?style=flat&labelColor=24292f&color=111111)
[![](https://img.shields.io/discord/1180651062550593536.svg?&logo=discord&logoColor=ffffff&style=flat&label=Discord&labelColor=24292f&color=111111)](https://discord.gg/g2u5wpbMGu "Void Crew Modding Discord")

# [UserModName]

Version [ModVersion]  
For Game Version [GameVersion]  
Developed by [Authors]  
Requires: [Dependencies]


---------------------

### 💡 Function(s)

- Sets default player limit to 8.
- Configures slider limit to 8.
- Defaults customizable via F5 menu or config file.

### 🎮 Client Usage

- Simply install. Player limit will default to 20.
- Control slider from any vanilla player limit slider.
- Configure settings at F5 > Mod Settings > Max Players.

#### 💻 Commands

- PlayerCount, pc
  - Returns the current player count and limit
- SetPlayerLimit, spl
  - Assigns the player limit with a given value
- StartQuest, sq
  - Starts the currently selected quest. Add argument "now" to skip the countdown. Ex: /startquest now

### 👥 Multiplayer Functionality

- ✅ All
  - All players must have this mod installed.

---------------------

## 🔧 Install Instructions - **Install following the normal BepInEx procedure.**

Ensure that you have [BepInEx 5](https://thunderstore.io/c/void-crew/p/BepInEx/BepInExPack/) (stable version 5 **MONO**) and [VoidManager](https://thunderstore.io/c/void-crew/p/VoidCrewModdingTeam/VoidManager/) installed.

#### ✔️ Mod installation - **Unzip the contents into the BepInEx plugin directory**

Drag and drop `[ModName].dll` into `Void Crew\BepInEx\plugins`