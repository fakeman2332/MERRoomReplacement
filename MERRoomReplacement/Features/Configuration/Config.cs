using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Enums;
using Exiled.API.Interfaces;
using MERRoomReplacement.Features.Configuration.Structures;

namespace MERRoomReplacement.Features.Configuration
{
    public class Config : IConfig
    {
        [Description("Indicates plugin enabled or not")]
        public bool IsEnabled { get; set; } = true;

        [Description("Indicates debug mode enabled or not")]
        public bool Debug { get; set; } = false;

        [Description("Options for replacement")]
        public List<RoomSchematic> ReplacementOptions { get; set; } = new()
        {
            new RoomSchematic()
            {
                IsEnabled = false,
                TargetRoomType = RoomType.HczTestRoom,
                SchematicName = "AwsomeSchematic"
            }
        };
    }
}