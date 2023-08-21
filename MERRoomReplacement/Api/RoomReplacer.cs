using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using MERRoomReplacement.Api.Structures;
using UnityEngine;

namespace MERRoomReplacement.Api;

public static class RoomReplacer
{
    private static readonly IDictionary<RoomType, CachedRoom> RoomsTransformDataCache;

    static RoomReplacer()
    {
        RoomsTransformDataCache = new Dictionary<RoomType, CachedRoom>();
    }

    /// <summary>
    /// Replaces room with MapEditorReborn schematic
    /// </summary>
    /// <param name="roomType">Room that should be replaced</param>
    /// <param name="roomSchematic">Replacement options</param>
    /// <returns><see cref="SchematicObject"/></returns>
    public static SchematicObject ReplaceRoom(RoomType roomType, RoomSchematic roomSchematic)
    {
        var room = Room.Get(roomType);

        var schematicPosition = roomSchematic.PositionOffset.ToUnityEngineVector();
        var schematicRotation = roomSchematic.RotationOffset.ToUnityEngineVector();

        if (room == null)
        {
            if (!RoomsTransformDataCache.TryGetValue(roomType, out var cachedRoomData))
                return null;

            schematicPosition += cachedRoomData.Position;
            schematicRotation += cachedRoomData.Rotation;

            cachedRoomData.Schematic.Destroy();

            cachedRoomData.Schematic = ObjectSpawner.SpawnSchematic(roomSchematic.SchematicName,
                schematicPosition, Quaternion.Euler(schematicRotation));

            return cachedRoomData.Schematic;
        }

        Object.Destroy(room.gameObject);

        var schematic = ObjectSpawner.SpawnSchematic(roomSchematic.SchematicName, schematicPosition + room.Position,
            Quaternion.Euler(schematicRotation + room.Rotation.eulerAngles));

        var roomDetails = new CachedRoom(room.Position, room.Rotation.eulerAngles, schematic);

        if (RoomsTransformDataCache.ContainsKey(roomType))
        {
            RoomsTransformDataCache[roomType] = roomDetails;
            return schematic;
        }

        RoomsTransformDataCache.Add(roomType, roomDetails);

        return schematic;
    }
}