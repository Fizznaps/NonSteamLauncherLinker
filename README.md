My goal for this project is to extend OSOL by Wambatfromhell into a more universal and less fussy solution that uses some extra config files to automate creating the ini for each launcher.  The major benefits would be so that I can launch all games, regardless of platform, from steam by using a uri where available to take full advantage of each launchers features, such as cloud saves, synced progress, achievements, etc.  It also makes it so my in home streaming and living room PC can have a single front end because I somehow find a steam front end less fussy than playnite.

I would also like to implement a way for it to autmatically add the games as nonsteam games in steam.

The ultimate solution would be to turn this into a standalone program where you can specify your launcher paths, URI information, and game install path and it would read from those, add an OSOL to each install path, and add them to steam.  THat is probably never going to happen but, that is the dream.  I want to rename the project since it is moving to a more universal approach.

# OSOL (O.rigin S.team O.verlay L.auncher)

Why should I use OSOL
=====================
If you've tried using the Steam Controller (or any other Steam Input supported device) from a couch with a third-party non-Steam game (like with games on Battle.net, Origin, or UPlay) then you know how annoying this combination can be. OSOL was created to make this process significantly more user-friendly while also providing additional functionality not typically available using other tools (CPU affinity, process priority, and pre/post launch command execution).

Aside from the most common mainstream launchers mentioned previously OSOL is also compatible with the vast majority of emulators and even some MMORPG/MMOFPS launchers. See the [OSOL project wiki](https://github.com/WombatFromHell/OriginSteamOverlayLauncher/wiki) for more details about this and other application specific notes.

**OSOL currently supports the following launchers:** _Battle.net, UPlay, Origin, GOG Galaxy, Epic Games Launcher, Steam, and most Emulators/Master Launchers that call a game's executable file._


How To Install/Uninstall
========================
This wrapper requires no installation other than copying it into the directory of the game executable you wish to run through Steam BPM/Overlay. It also can simply be deleted if you wish to uninstall it at any time. If you have trouble running it, or are running Windows 8 or earlier, you may need to download and install the [.NET Framework Runtime v4.7.1](https://www.microsoft.com/en-us/download/details.aspx?id=56115).


How To Use
==========
* Unpack the OSOL .exe file from the OSOL archive into the game directory (where the game's executable is located for example).
* Run the OSOL .exe file from this directory and it will prompt you to choose the path to your game executable (required) and the game launcher (which is optional).
* Add the OSOL .exe to Steam as a non-Steam game by clicking the "Add a game" button on the bottom left of the Steam window, clicking "Add a Non-Steam Game" and selecting the OSOL .exe file in the path chooser.
* Name this new non-Steam game shortcut of OSOL (in Steam) whatever you like (such as your game's name).
* Run this non-Steam game shortcut from the Steam library as any other Steam game and the Steam overlay and third-party overlay should show up in-game (if enabled).
* **Optional:** _for advanced functionality or compatibility options for particular launchers see the OSOL project [Wiki](https://github.com/WombatFromHell/OriginSteamOverlayLauncher/wiki)._

NEW STUFF
* OSOL supports an external JSON file named launcherinfo.json placed alongside the game install folder. This file defines settings for different launchers and helps OSOL automatically configure launch parameters.

| Field                 | Description                                                                                                  |
| --------------------- | ------------------------------------------------------------------------------------------------------------ |
| **Launcher**          | The active launcher name (case-insensitive), e.g., `"Epic"` or `"Legendary"`.                                |
| **LauncherPath**      | Full path to the launcher executable for that platform.                                                      |
| **LauncherUriPrefix** | URI prefix for launching games via the launcher (optional). Used only if `UseUri` is `true`.                 |
| **GameInstallPath**   | Path where the game is installed (optional, can be blank).                                                   |
| **UseUri**            | *Optional.* Boolean flag indicating whether the launcher uses URI launching. Defaults to `false` if omitted. |

Notes
=====
__If you're looking for specific instructions on getting OSOL working with a particular launcher or are having issues with behavior you believe is related to OSOL please read the [project Wiki](https://github.com/WombatFromHell/OriginSteamOverlayLauncher/wiki) before making an issue ticket.__

If you experience crashes when starting OSOL and are running Windows 7, make sure you install the .NET 4.7.1 Redistributable found [here](https://www.microsoft.com/en-us/download/details.aspx?id=56115).

If you have difficulty getting the Steam overlay to hook into your game when launching with OSOL please follow [these instructions](https://support.steampowered.com/kb_article.php?ref=9828-SFLZ-9289), and make sure OSOL and Steam are both running with the appropriate permissions (if Steam is running as Admin, make sure to run OSOL as Admin as well so that all processes spawned from it can be hooked by Steam). **This is important for older games (circa <2007).**

If you have issues with games not launching with the Steam Overlay and are using a recent AMD graphics device you may need to disable the "AMD External Events Utility" service by following the instructions below:

* Run "services.msc".
* Browse down to the "AMD External Events" service.
* Double-click it, change the startup type to "Disabled", and click "Stop" to disable the service, then click "OK" and exit the dialog.

**NOTE:** _This will break FreeSync functionality, but allow the Steam Overlay to hook into Origin games._

If you find a launcher that doesn't work with OSOL please [report it](https://github.com/WombatFromHell/OriginSteamOverlayLauncher/issues/new) so I can consider adding it to the supported launchers list.


How To Compile
==============
If you wish to compile this project from Github source, you'll need Visual Studio v14+ or Visual Studio Community (targetting the .NET Framework v4.7.1 for C#). There are no external libraries required except for the .NET v4.7.1 framework package. The source code can be modified freely under the MIT license as long as the contributers and creator are given credit.

If you'd like to contribute please make sure to comment your code thoroughly and try to split major features up into their own PRs when possible.


## Development Notes

### Project Objective

Create a unified launcher management system supporting multiple launchers (Steam, Epic, Legendary, GOG Galaxy, etc.) with flexible configuration via `launcherinfo.json`.

### Current Status

* `launcherinfo.json` file defined with multiple launcher configurations.
* `LauncherInfoManager` implemented to parse the JSON and provide launcher info.
* Base project refactored to prepare for launcher abstraction.

### Last Completed Step

* Step 2: Created and tested `LauncherInfoManager` to reliably parse launcherinfo.json and expose current launcher data.

### Next Steps

* Step 3: Integrate launcherinfo.json data into `LaunchLogic` class.
* Implement logic to detect launcher type from game path and auto-build launch URIs.
* Support additional launchers beyond Epic, Legendary, and GOG Galaxy.
* Refactor code to replace hardcoded paths and values with config-driven data.

### Epic Launcher Integration Details

* The Epic launcher URI requires the correct game identifier.
* To get this, the Legendary launcher executable path (defined in the JSON) will be used to execute a JSON export of installed games.
* This export contains the official game names and metadata.
* The Epic manifests (found in the Epic Games install folder) are then parsed to determine the install paths for each game.
* By matching the install paths from the manifests with the game data from Legendary’s export, the code can resolve the correct game identifier.
* This game identifier is appended to the `launcherURIPrefix` from `launcherinfo.json` to build the full launch URI.
* This approach avoids hardcoding Epic game IDs and allows for dynamic and flexible launch handling.

### Notes / Blockers

* Need to confirm consistent naming conventions across JSON, INI, and code.
* Confirm error handling for missing or malformed launcherinfo.json.
* Investigate adding support for other launchers like GOG Galaxy.

Credits
=======
Special thanks to CriticalComposer for his art/icon contribution to the OSOL project.

Thanks to Dafzor and his bnetlauncher wrapper (http://madalien.com/stuff/bnetlauncher/) for giving me the idea to make this.

Thanks to WombatFromHell for the OriginSteamOverlayLauncher (https://github.com/WombatFromHell/OriginSteamOverlayLauncher)

Donations <a href="https://www.buymeacoffee.com/wombatfromhell" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="21" width="87"></a>
=========
If you find this project useful and you would like to donate toward on-going development you can use the link above. Any and all donations are much appreciated!
