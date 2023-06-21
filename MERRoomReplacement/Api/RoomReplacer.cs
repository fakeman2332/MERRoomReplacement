using Exiled.API.Enums;
using Exiled.API.Features;
using MapEditorReborn.API.Features;
using MERRoomReplacement.Features.Configuration.Structures;
using UnityEngine;

namespace MERRoomReplacement.Api;

public static class RoomReplacer
{
    public static void ReplaceRoom(RoomType roomType, RoomSchematic roomSchematic)
    {
        var room = Room.Get(roomType);

        Log.Debug($"Schematic `{roomSchematic.SchematicName}` spawned");
        ObjectSpawner.SpawnSchematic(roomSchematic.SchematicName, 
            room.Position + roomSchematic.PositionOffset.ToUnityEngineVector(),
            Quaternion.Euler(room.Rotation.eulerAngles + roomSchematic.RotationOffset.ToUnityEngineVector()));
        
        Log.Debug($"Destroying `{roomType}` room");
        MonoBehaviour.Destroy(room.gameObject);
        
        Log.Debug($"Room `{roomType}` should to be destroyed");
    }
}