using Exiled.API.Enums;

namespace MERRoomReplacement.Features.Configuration.Structures;

public struct RoomSchematic
{
    public bool IsEnabled { get; set; }
    
    public RoomType TargetRoomType { get; set; } 
    
    public string SchematicName { get; set; }
}