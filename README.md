# SCP-1162
[![GitHub release](https://flat.badgen.net/github/release/MrAfitol/SCP-1162)](https://github.com/MrAfitol/SCP-1162/releases/)
![GitHub downloads](https://flat.badgen.net/github/assets-dl/MrAfitol/SCP-1162)


A plugin that adds SCP-1162
## How download ?
   - *1. Find the SCP SL server config folder*
   
   *("C:\Users\(user name)\AppData\Roaming\SCP Secret Laboratory\" for windows, "/home/(user name)/.config/SCP Secret Laboratory/" for linux)*
  
   - *2. Find the "PluginAPI" folder there, it contains the "plugins" folder.*
  
   - *3. Select either the port of your server to install the same on that server or the "global" folder to install the plugin for all servers*
  
  ***Or***
  
   - *Run the command in console `p install MrAfitol/SCP-1162`*
## Config
```yml
# What message will be displayed when using SCP-1162?
item_drop_message: <b>You dropped a <color=green>{dropitem}</color> through <color=yellow>SCP-1162</color>, and received a <color=red>{giveitem}</color></b>
# From what distance can SCP-1162 be used?
s_c_p1162_distance: 2
# Will the hands be cut off if the item is not in the hands?
cutting_hands: true
# What is the chance that the hands will be cut off if the item is not in the hands
chance_cutting: 40
# List of items that may drop from SCP-1162
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
- KeycardGuard
- Ammo762x39
- Ammo556x45
- GrenadeFlash
- KeycardScientist
- KeycardJanitor
- Coin
- Flashlight
```

**ItemType**
```
KeycardJanitor,
KeycardScientist,
KeycardResearchCoordinator,
KeycardZoneManager,
KeycardGuard,
KeycardNTFOfficer,
KeycardContainmentEngineer,
KeycardNTFLieutenant,
KeycardNTFCommander,
KeycardFacilityManager,
KeycardChaosInsurgency,
KeycardO5,
Radio,
GunCOM15,
Medkit,
Flashlight,
MicroHID,
SCP500,
SCP207,
Ammo12gauge,
GunE11SR,
GunCrossvec,
Ammo556x45,
GunFSP9,
GunLogicer,
GrenadeHE,
GrenadeFlash,
Ammo44cal,
Ammo762x39,
Ammo9x19,
GunCOM18,
SCP018,
SCP268,
Adrenaline,
Painkillers,
Coin,
ArmorLight,
ArmorCombat,
ArmorHeavy,
GunRevolver,
GunAK,
GunShotgun,
SCP330,
SCP2176,
SCP244a,
SCP244b,
SCP1853,
ParticleDisruptor,
GunCom45
```
