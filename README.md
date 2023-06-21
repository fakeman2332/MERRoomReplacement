# MERRoomReplacement
An **[EXILED](https://github.com/Exiled-Team/EXILED)** Plugin that can replace basegame room with MER's schematic 
# ⚠️ NOTE ⚠️
+ **Not all rooms able to be replaced due to basegame features**

## Requirements
+ EXILED
+ MapEditorReborn

## Installation
1. Go to [releases page](https://github.com/FakeMan2332/MERRoomReplacement/releases/latest) and download dll.
2. Put downloaded plugin to your `EXILED/Plugins` directory

## Configration

```yml
room_replacement:
# Indicates plugin enabled or not
  is_enabled: true
  # Indicates debug mode enabled or not
  debug: false
  # Options for replacement. Will be applied on MapGenerated event
  replacement_options:
  - is_enabled: false
    target_room_type: HczTestRoom
    schematic_name: 'AwsomeSchematic'
```

## Command
```
REPLACEROOM <ROOM_TYPE> <SCHEMATIC_NAME>
```

# Depends on <3
**[EXILED](https://github.com/Exiled-Team/EXILED)** & **[MapEditorReborn](https://github.com/Michal78900/MapEditorReborn)**
