[![Downloads](https://img.shields.io/github/downloads/FakeMan2332/MERRoomReplacement/total?style=for-the-badge&color=blue)](https://github.com/FakeMan2332/MERRoomReplacement/releases/latest)

# MERRoomReplacement
An **[EXILED](https://github.com/Exiled-Team/EXILED)** Plugin that can replace basegame room with MER's schematic.

# ⚠️ IMPORTANT NOTE ⚠️
+ **Not all rooms able to be replaced due to basegame features.**
+ ⚠️ **Replacing rooms are breaking SCP-079! I should recommend you use `scp079_should_replaced` option in configuration (disabled by default).**

## Requirements
+ EXILED
+ MapEditorReborn

## Installation
1. Go to [releases page](https://github.com/FakeMan2332/MERRoomReplacement/releases/latest) and download dll.
2. Put downloaded plugin to your `EXILED/Plugins` directory.

## Configuration

```yml
room_replacement:
# Indicates plugin enabled or not
  is_enabled: true
  # Indicates debug mode enabled or not
  debug: false
  # Should SCP-079 be replaced with another free SCP
  scp079_should_replaced: false
  # Options for replacement. Will be applied o****n WaitingForPlayers event
  replacement_options:
  - is_enabled: false
    target_room_type: HczTestRoom
    schematic_name: 'AwesomeSchematic'
    position_offset:
      x: 0
      y: 0
      z: 0
    rotation_offset:
      x: 0
      y: 0
      z: 0
```

## Command
Requires `mp.roomreplacement` permission
```
REPLACEROOM <ROOM_TYPE> <SCHEMATIC_NAME> (OFFSET_POS_X) (OFFSET_POS_Y) (OFFSET_POS_Z) (OFFSET_ROT_X) (OFFSET_ROT_Y) (OFFSET_ROT_Z)
```
\* offsets are optional and by default equal to `0`.

### Usage example
```
replaceroom Lcz330 AwesomeSchematicName
```
```
replaceroom Lcz330 AwesomeSchematicName 0 0.15 0
```
```
replaceroom Lcz330 AwesomeSchematicName 0.1 0.15 -0.5 0 180 0
```

# API
+ You can use **`MERRoomReplacement.Api.RoomReplacer.ReplaceRoom(RoomType roomType, RoomSchematic roomSchematic)`** method to replace room from your plugin.


+ You also can manually remove SCP-079 from spawn queue by using `MERRoomReplacement.Patches.RemoveScp079FromSpawnQueue.PatchSpawnQueue(Harmony harmony)` method. (If you not using the `scp079_should_replaced` option).

# Depends on 💖
> **[EXILED](https://github.com/Exiled-Team/EXILED)** & **[MapEditorReborn](https://github.com/Michal78900/MapEditorReborn)**
