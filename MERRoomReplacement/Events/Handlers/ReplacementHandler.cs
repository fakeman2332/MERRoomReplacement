using System.Collections.Generic;
using Exiled.Events.Handlers;
using MERRoomReplacement.Api;
using MERRoomReplacement.Api.Structures;
using MERRoomReplacement.Events.Interfaces;
using UnityEngine;

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
            if (roomSchematic.SpawnChance >= 100 || Random.Range(0, 100) > roomSchematic.SpawnChance)
                RoomReplacer.ReplaceRoom(roomSchematic.TargetRoomType, roomSchematic, roomSchematic.SpawnDelay);
        }
    }

    public void UnsubscribeEvents()
    {
        Server.WaitingForPlayers -= OnWaitingForPlayers;
    }
}