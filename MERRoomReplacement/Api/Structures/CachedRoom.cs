using MapEditorReborn.API.Features.Objects;

namespace MERRoomReplacement.Api.Structures;

public class CachedRoom
{
    public CachedRoom(UnityEngine.Vector3 position, UnityEngine.Vector3 rotation, SchematicObject schematic)
    {
        Position = position;
        Rotation = rotation;
        Schematic = schematic;
    }
    
    public UnityEngine.Vector3 Position { get; internal set; }
    
    public UnityEngine.Vector3 Rotation { get; internal set; }
    
    public SchematicObject Schematic { get; internal set; }
}