using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using MapEditorReborn.API;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using MapEditorReborn.API.Features.Serializable;
using MEC;
using MERRoomReplacement.Api.Structures;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace MERRoomReplacement.Api;

public static class RoomReplacer
{
    private static readonly IDictionary<RoomType, CachedRoom> RoomsTransformDataCache;

    static RoomReplacer()
    {
        RoomsTransformDataCache = new Dictionary<RoomType, CachedRoom>();
    }

    /// <summary>
    ///     Replaces room with MapEditorReborn schematic
    /// </summary>
    /// <param name="roomType">Room that should be replaced</param>
    /// <param name="roomSchematic">Replacement options</param>
    /// <returns>
    ///     <see cref="SchematicObject" />
    /// </returns>
    public static SchematicObject ReplaceRoom(RoomType roomType, RoomSchematic roomSchematic)
    {
        var room = Room.Get(roomType);

        var schematicPosition = roomSchematic.PositionOffset.ToUnityEngineVector();
        var schematicRotation = roomSchematic.RotationOffset.ToUnityEngineVector();

        if (room == null)
        {
            return TryReplaceCachedRoom(roomType, roomSchematic, schematicPosition, schematicRotation,
                out var cachedRoomData)
                ? null
                : cachedRoomData.Schematic;
        }

        DestroyRoom(room);

        
        var schematicSerializable = new SchematicSerializable(roomSchematic.SchematicName);
        var rotation = Quaternion.Euler(schematicRotation + room.transform.localRotation.eulerAngles);
        var schematic = ObjectSpawner.SpawnSchematic(schematicSerializable, schematicPosition + room.Position, rotation);
        
        API.SpawnedObjects.Add(schematic);

        var roomDetails = new CachedRoom(room.Position, room.Rotation.eulerAngles, schematic);

        if (RoomsTransformDataCache.ContainsKey(roomType))
        {
            RoomsTransformDataCache[roomType] = roomDetails;
            return schematic;
        }

        RoomsTransformDataCache.Add(roomType, roomDetails);

        return schematic;
    }

    /// <summary>
    ///     Replaces room with MapEditorReborn schematic
    /// </summary>
    /// <param name="roomType">Room that should be replaced</param>
    /// <param name="roomSchematic">Replacement options</param>
    /// <param name="delay">Delay in seconds until replacement</param>
    public static void ReplaceRoom(RoomType roomType, RoomSchematic roomSchematic, float delay)
    {
        Timing.CallDelayed(delay, () =>
        {
            _ = ReplaceRoom(roomType, roomSchematic);
        });
    }
    
    /// <summary>
    ///     Destroys the specified room
    /// </summary>
    /// <remarks>
    ///     This method will not destroy the 079 components (cameras, speakers) in room
    /// </remarks>
    /// <param name="room">Room that should be destroyed</param>
    public static void DestroyRoom(Room room)
    {
        foreach (var component in room.gameObject.GetComponentsInChildren<Component>())
            try
            {
                if (component.name.Contains("SCP-079") || component.name.Contains("CCTV"))
                {
                    Log.Debug(
                        $"Prevent from destroying: {component.name} {component.tag} {component.GetType().FullName}");
                    continue;
                }

                if (component.GetComponentsInParent<Component>()
                    .Any(c => c.name.Contains("SCP-079") || c.name.Contains("CCTV")))
                {
                    Log.Debug(
                        $"Prevent from destroying: {component.name} {component.tag} {component.GetType().FullName}");
                    continue;
                }

                Log.Debug($"Destroying component: {component.name} {component.tag} {component.GetType().FullName}");

                Object.Destroy(component);
            }
            catch
            {
                // ignored
            }
    }

    private static bool TryReplaceCachedRoom(RoomType roomType, RoomSchematic roomSchematic, Vector3 schematicPosition,
        Vector3 schematicRotation, out CachedRoom cachedRoomData)
    {
        if (!RoomsTransformDataCache.TryGetValue(roomType, out cachedRoomData))
            return true;

        schematicPosition += cachedRoomData.Position;
        schematicRotation += cachedRoomData.Rotation;

        cachedRoomData.Schematic.Destroy();
        API.SpawnedObjects.Remove(cachedRoomData.Schematic);
        
        var schematic = new SchematicSerializable(roomSchematic.SchematicName);
        
        cachedRoomData.Schematic = ObjectSpawner.SpawnSchematic(schematic,
            schematicPosition, Quaternion.Euler(schematicRotation));
        API.SpawnedObjects.Add(cachedRoomData.Schematic);

        return false;
    }
}