using System;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.Permissions.Extensions;
using MERRoomReplacement.Api;

namespace MERRoomReplacement.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class ReplaceRoomCommand : ICommand, IUsageProvider
{
    public string Command => "replaceroom";

    public string[] Usage { get; } =
    {
        "room_type",
        "schematic_name"
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
        
        RoomReplacer.ReplaceRoom(roomType, schematicName);

        response = "Room replaced!";
        return true;
    }

}