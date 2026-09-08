# Changelog

## 0.2
### 0.2.0
Features:
- Team Only Sync mode (only sync with teammates)
- Silk skills will automatically equip for players when received if they don't have a silk skill equipped

New Syncs:
- Flea Caravan location
- The check for the Flea Caravan being able to move to Pale Lake
- Songclave's state (active, NPCs, etc)
- Completion of the act 2 start cutscene with Lace
- Bellhome key

Bugfixes:
- Items could duplicate on bad network connections
- Items in cages would despawn upon the cage breaking
- The icon for needle strike was huge
- Elegy of the Deep was not synced

## 0.1

### 0.1.4
- Increased compatibility with SSMP standalone server

### 0.1.3
- Updated the SSMP addon version since I forgot in 0.1.2... oops!

### 0.1.2
- Added `/sync-ui` for easier access to settings
- Added mod settings (ModMenu/BepInEx) for log levels
- Fixed a bug where sliding platforms in Whispering Vaults would reset upon leaving the room
- Fixed a bug that prevented syncing relic pickups
- Fixed map marker icons in lower left notifications

### 0.1.1
- Fixed sending and receiving some persistent objects (i.e. shortcuts)
- Better alerts for some items/upgrades
- Improved(?) networking
- Currency deductions can be synced (i.e. spending in shops/quests)
- Edge cases with Bell Beast and Silk Heart settings handled
- Cursed and cloakless crests no longer sync
- Arenas and boss fights are disabled properly when other players are in the scene but not in the fight
- Silk skills are auto-equipped if no silk skill is equipped

### 0.1.0
Initial release
- Basic syncing
- Settings command
- Most bosses deaths are synced when multiple players are in fight