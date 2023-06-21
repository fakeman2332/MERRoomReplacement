using Exiled.API.Enums;
using Exiled.API.Features;
using MapEditorReborn.API.Features;
using UnityEngine;

namespace MERRoomReplacement.Api;

public static class RoomReplacer
{
    public static void ReplaceRoom(RoomType roomType, string schematicName)
    {
        var room = Room.Get(roomType);

        Log.Debug($"Schematic `{schematicName}` spawned");
        ObjectSpawner.SpawnSchematic(schematicName, room.Position, room.Rotation);
        
        Log.Debug($"Destroying `{roomType}` room");
        MonoBehaviour.Destroy(room.gameObject);
        
        Log.Debug($"Room `{roomType}` should to be destroyed");
    }
}