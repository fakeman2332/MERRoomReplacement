using System.Collections.Generic;
using Exiled.API.Features;
using MERRoomReplacement.Api;
using MERRoomReplacement.Api.Structures;
using MERRoomReplacement.Events.Interfaces;
using UnityEngine;
using Server = Exiled.Events.Handlers.Server;

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
            Log.Debug($"Schematic name: {roomSchematic.SchematicName} | Room: {roomSchematic.TargetRoomType} | Chance: {roomSchematic.SpawnChance}%");

            if (roomSchematic.SpawnChance >= 100)
            {
                Log.Debug("Spawn chance is more or equal to 100, starting replacing");
            }
            else if (Random.Range(0, 101) is var chance && roomSchematic.SpawnChance >= chance)
            {
                Log.Debug($"Schematic chance {roomSchematic.SpawnChance} >= generated {chance}, starting replacing");
            }
            else
            {
                Log.Debug($"Generated chance {chance} is less then {roomSchematic}, skipping..");
                continue;
            }
            
            RoomReplacer.ReplaceRoom(roomSchematic.TargetRoomType, roomSchematic, roomSchematic.SpawnDelay);
        }
    }

    public void UnsubscribeEvents()
    {
        Server.WaitingForPlayers -= OnWaitingForPlayers;
    }
}