using Exiled.API.Enums;

namespace MERRoomReplacement.Features.Configuration.Structures;

public struct RoomSchematic
{
    public bool IsEnabled { get; set; }
    
    public RoomType TargetRoomType { get; set; } 
    
    public string SchematicName { get; set; }
    
    public Vector3 PositionOffset { get; set; }
    
    public Vector3 RotationOffset { get; set; }
}