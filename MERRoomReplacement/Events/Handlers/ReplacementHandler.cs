﻿using System.Collections.Generic;
using Exiled.Events.Handlers;
using MERRoomReplacement.Api;
using MERRoomReplacement.Api.Structures;
using MERRoomReplacement.Events.Interfaces;

namespace MERRoomReplacement.Events.Handlers;

public class ReplacementHandler : IEventHandler
{
    private readonly IEnumerable<RoomSchematic> _replacementOptions;

    public ReplacementHandler(IEnumerable<RoomSchematic> replacementOptions)
    {
        _replacementOptions = replacementOptions;
    }

    public void SubscribeEvents()
    {
        Server.WaitingForPlayers += OnWaitingForPlayers;
    }

    private void OnWaitingForPlayers()
    {
        foreach (var roomSchematic in _replacementOptions)
        {
            RoomReplacer.ReplaceRoom(roomSchematic.TargetRoomType, roomSchematic);
        }
    }

    public void UnsubscribeEvents()
    {
        Server.WaitingForPlayers -= OnWaitingForPlayers;
    }
}