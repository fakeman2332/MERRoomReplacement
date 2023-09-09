using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using MERRoomReplacement.Api.Structures;

namespace MERRoomReplacement.Features.Configuration
{
    public class Config : IConfig
    {
        [Description("Indicates plugin enabled or not")]
        public bool IsEnabled { get; set; } = true;

        [Description("Indicates debug mode enabled or not")]
        public bool Debug { get; set; } = false;

        [Description("Should SCP-079 be removed from spawn queue")]
        public bool RemoveScp079FromSpawnQueue { get; set; } = false;
        
        [Description("Should SCP-079 be replaced with another free SCP")]
        public bool PreventScp079OnRoleChange { get; set; } = false;

        [Description("Options for replacement")]
        public List<RoomSchematic> ReplacementOptions { get; set; } = new()
        {
            new RoomSchematic()
            {
                IsEnabled = false,
                TargetRoomType = RoomType.HczTestRoom,
                SchematicName = "AwesomeSchematic",
                SpawnChance = 50,
                PositionOffset = new Vector3(0, 0, 0),
                RotationOffset = new Vector3(0, 0, 0)
            }
        };
    }
}