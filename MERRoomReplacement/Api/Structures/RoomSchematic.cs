using System.ComponentModel;
using Exiled.API.Enums;

namespace MERRoomReplacement.Api.Structures;

public struct RoomSchematic
{
    [Description("Indicates replacement is enabled or not")]
    public bool IsEnabled { get; set; }
    
    [Description("Room for replacement")]
    public RoomType TargetRoomType { get; set; } 
    
    [Description("Schematic that will be used")]
    public string SchematicName { get; set; }
    
    [Description("Chance for replace room with described schematic")]
    public int SpawnChance { get; set; }
    
    public Vector3 PositionOffset { get; set; }
    
    public Vector3 RotationOffset { get; set; }
}