# SCP-1162
[![GitHub release](https://flat.badgen.net/github/release/MrAfitol/SCP-1162)](https://github.com/MrAfitol/SCP-1162/releases/)
![GitHub downloads](https://flat.badgen.net/github/assets-dl/MrAfitol/SCP-1162)


A plugin that adds SCP-1162

The idea is taken from the [plugin](https://github.com/SynapseSL/Scp1162)
## How download ?
   - *1. Find the SCP SL server config folder*
   
   *("C:\Users\(user name)\AppData\Roaming\SCP Secret Laboratory\" for windows, "/home/(user name)/.config/SCP Secret Laboratory/" for linux)*
  
   - *2. Find the "PluginAPI" folder there, it contains the "plugins" folder.*
  
   - *3. Select either the port of your server to install the same on that server or the "global" folder to install the plugin for all servers*
  
  ***Or***
  
   - *Run the command in console `p install MrAfitol/SCP-1162`*
## Config
```yml
# What message will be displayed when using SCP-1162? ({dropitem} - Thrown or dropped item. {giveitem} - Changed item. {dropstatus} - Replaces the text written in the points below in dependence on the drop status.)
item_drop_message:
  message: <b>You {dropstatus} a <color=green>{dropitem}</color> through <color=yellow>SCP-1162</color>, and received a <color=red>{giveitem}</color></b>
  drop_text: dropped
  throw_text: throwed
# What message will be displayed when item are deleted? ({dropitem} - Thrown or dropped item. {dropstatus} - Replaces the text written in the points below in dependence on the drop status.)
item_delete_message:
  message: <b>You {dropstatus} a <color=green>{dropitem}</color> through <color=yellow>SCP-1162</color>, and got <color=red>nothing</color></b>
  drop_text: dropped
  throw_text: throwed
# From what distance can SCP-1162 be used?
s_c_p1162_distance: 2
# What is the chance that the hands will be cut off if the item is not in the hands. (Set to 0 to disable)
cutting_chance: 30
# What is the chance that the item will be deleted. (Set to 0 to disable)
delete_chance: 10
# If this item is enabled, the hands will not be cut off only when the player threw item.
ignore_throw: true
# List of items that may drop from SCP-1162.
dropping_items:
- SCP500
- KeycardContainmentEngineer
- GunCOM15
- GrenadeFlash
- SCP207
- Adrenaline
- GunCOM18
- Medkit
- KeycardGuard
- Ammo9x19
- KeycardZoneManager
- KeycardResearchCoordinator
- Ammo762x39
- Ammo556x45
- KeycardScientist
- KeycardJanitor
- Coin
- Flashlight
# List of items and their name when using SCP-1162, if the item is not in the list the name will be the default.
items_name:
  SCP500: SCP-500
  KeycardContainmentEngineer: Containment Engineer Keycard
  GunCOM15: COM-15
  GrenadeFlash: Flashbang Grenade
  SCP207: SCP-207
  Adrenaline: Adrenaline
  GunCOM18: COM-18
  Medkit: Medkit
  KeycardGuard: Guard Keycard
  Ammo9x19: Ammo9x19
  KeycardZoneManager: Zone Manager Keycard
  KeycardResearchCoordinator: Research Supervisor Keycard
  Ammo762x39: Ammo762x39
  Ammo556x45: Ammo556x45
  KeycardScientist: Scientist Keycard
  KeycardJanitor: Janitor Keycard
  Coin: Coin
  Flashlight: Flashlight
```
## Wiki
**Be sure to check out the [Wiki](https://github.com/MrAfitol/SCP-1162/wiki)**