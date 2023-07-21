using System;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.Permissions.Extensions;
using MapEditorReborn.API.Features;
using MERRoomReplacement.Api;
using MERRoomReplacement.Api.Structures;

namespace MERRoomReplacement.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class ReplaceRoomCommand : ICommand, IUsageProvider
{
    public string Command => "replaceroom";

    public string[] Usage { get; } =
    {
        "room_type",
        "schematic_name",

        "(offset_pos_x)",
        "(offset_pos_y)",
        "(offset_pos_z)",

        "(offset_rot_x)",
        "(offset_rot_y)",
        "(offset_rot_z)",
    };

    public string[] Aliases { get; } =
    {
        "roomreplace"
    };

    public string Description => "Replace specified room with schematic";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission("mp.roomreplacement"))
        {
            response = "You must have 'mp.roomreplacement' permission to execute this command";
            return false;
        }

        if (arguments.Count < 2)
        {
            response = $"Wrong arguments! Command usage: {this.DisplayCommandUsage()}";
            return false;
        }

        var roomName = arguments.At(0);

        if (!Enum.TryParse(roomName, out RoomType roomType))
        {
            response = "Room not found";
            return false;
        }

        var schematicName = arguments.At(1);

        var isSchematicExists = MapUtils.GetSchematicDataByName(schematicName) != null;

        if (!isSchematicExists)
        {
            response = "Schematic not found!";
            return false;
        }
        
        var positionOffsetX = 0f;
        var positionOffsetY = 0f;
        var positionOffsetZ = 0f;
        
        var rotationOffsetX = 0f;
        var rotationOffsetY = 0f;
        var rotationOffsetZ = 0f;

        if (arguments.Count > 2 && !float.TryParse(arguments.At(2), out positionOffsetX))
        {
            response = "Failed to parse a position offset X";
            return false;
        }

        if (arguments.Count > 3 && !float.TryParse(arguments.At(3), out positionOffsetY))
        {
            response = "Failed to parse a position offset Y";
            return false;
        }

        if (arguments.Count > 4 && !float.TryParse(arguments.At(4), out positionOffsetZ))
        {
            response = "Failed to parse a position offset Z";
            return false;
        }

        if (arguments.Count > 5 && !float.TryParse(arguments.At(2), out rotationOffsetX))
        {
            response = "Failed to parse a rotation offset X";
            return false;
        }

        if (arguments.Count > 6 && !float.TryParse(arguments.At(3), out rotationOffsetY))
        {
            response = "Failed to parse a rotation offset Y";
            return false;
        }

        if (arguments.Count > 7 && !float.TryParse(arguments.At(4), out rotationOffsetZ))
        {
            response = "Failed to parse a rotation offset Z";
            return false;
        }

        _ = RoomReplacer.ReplaceRoom(roomType, new RoomSchematic()
        {
            SchematicName = schematicName,
            PositionOffset = new Vector3(positionOffsetX, positionOffsetY, positionOffsetZ),
            RotationOffset = new Vector3(rotationOffsetX, rotationOffsetY, rotationOffsetZ)
        });

        response = "Room replaced!";
        return true;
    }
}