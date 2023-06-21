# MERRoomReplacement
An **[EXILED](https://github.com/Exiled-Team/EXILED)** Plugin that can replace basegame room with MER's schematic 

# Configration

```yml
room_replacement:
# Indicates plugin enabled or not
  is_enabled: true
  # Indicates debug mode enabled or not
  debug: false
  # Options for replacement. Will be applied on MapGenerated event
  replacement_options:
  - is_enabled: true
    target_room_type: HczTestRoom
    schematic_name: 'android_lab'
```

## Command
```
REPLACEROOM <ROOM_TYPE> <SCHEMATIC_NAME>
```

## Depends on <3
**[EXILED](https://github.com/Exiled-Team/EXILED)** & **[MapEditorReborn](https://github.com/Michal78900/MapEditorReborn)**
