using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using MapEditorReborn.API.Features;
using MapGeneration;
using MERRoomReplacement.Features.Configuration.Structures;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace MERRoomReplacement.Api;

public static class RoomReplacer
{
    private static readonly IDictionary<RoomType, (Vector3 Position, Vector3 Rotation)> RoomsTransformDataCache;

    static RoomReplacer()
    {
        RoomsTransformDataCache = new Dictionary<RoomType, (Vector3 Position, Vector3 Rotation)>();
    }
    
    public static void ReplaceRoom(RoomType roomType, RoomSchematic roomSchematic)
    {
        var room = Room.Get(roomType);

        var roomPosition = roomSchematic.PositionOffset.ToUnityEngineVector();
        var roomRotation = roomSchematic.RotationOffset.ToUnityEngineVector();
        
        if (room == null)
        {
            if (!RoomsTransformDataCache.TryGetValue(roomType, out var roomData))
                return;

            roomPosition += roomData.Position;
            roomRotation += roomData.Rotation;
        }
        else
        {
            if (RoomsTransformDataCache.ContainsKey(roomType)) 
                return;

            roomPosition += room.Position;
            roomRotation += room.Rotation.eulerAngles;
            
            RoomsTransformDataCache.Add(roomType, (roomPosition, roomRotation));
        }
        
        Log.Debug($"Spawning `{roomSchematic.SchematicName}` schematic");
        
        ObjectSpawner.SpawnSchematic(roomSchematic.SchematicName, 
            roomPosition, Quaternion.Euler(roomRotation));
        
        Log.Debug($"Schematic `{roomSchematic.SchematicName}` spawned");

        Log.Debug($"Destroying `{roomType}` room");
        Object.Destroy(room.gameObject);
        Log.Debug($"Room `{roomType}` should to be destroyed");
    }
}