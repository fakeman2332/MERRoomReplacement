using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Handlers;
using MERRoomReplacement.Api;
using MERRoomReplacement.Api.Structures;
using MERRoomReplacement.Events.Interfaces;
using PlayerRoles;
using Random = UnityEngine.Random;

namespace MERRoomReplacement.Events.Handlers;

public class ReplacementHandler : IEventHandler
{
    private readonly IEnumerable<RoomSchematic> _replacementOptions;

    private readonly bool _scp079ShouldReplaced;

    private static readonly IEnumerable<RoleTypeId> ScpsRoleTypes;

    static ReplacementHandler()
    {
        ScpsRoleTypes = Enum.GetValues(typeof(RoleTypeId))
            .ToArray<RoleTypeId>()
            .Where(roleType => RoleExtensions.GetTeam(roleType) == Team.SCPs && 
                roleType is not RoleTypeId.Scp079 and not RoleTypeId.Scp0492);
    }
    
    public ReplacementHandler(IEnumerable<RoomSchematic> replacementOptions, bool scp079ShouldReplaced)
    {
        _replacementOptions = replacementOptions;
        _scp079ShouldReplaced = scp079ShouldReplaced;
    }

    public void SubscribeEvents()
    {
        Server.WaitingForPlayers += OnWaitingForPlayers;
        Player.ChangingRole += OnPlayerChangingRole;
    }

    private void OnPlayerChangingRole(ChangingRoleEventArgs ev)
    {
        if (ev.NewRole != RoleTypeId.Scp079)
            return;
        
        if (!_scp079ShouldReplaced)
            return;

        ev.NewRole = GetFreeScp();
    }

    
    private void OnWaitingForPlayers()
    {
        foreach (var roomSchematic in _replacementOptions)
        {
            if (roomSchematic.SpawnChance > Random.Range(0, 101))
                continue;
            
            RoomReplacer.ReplaceRoom(roomSchematic.TargetRoomType, roomSchematic);
        }
    }

    public void UnsubscribeEvents()
    {
        Player.ChangingRole -= OnPlayerChangingRole;
        Server.WaitingForPlayers -= OnWaitingForPlayers;
    }
    
    private static RoleTypeId GetFreeScp()
    {
        var scpPlayers = Exiled.API.Features.Player.Get(Team.SCPs);
        var possibleScps = ScpsRoleTypes
            .Where(scpRoleType => scpPlayers.All(s => s.Role.Type != scpRoleType))
            .ToList();

        return possibleScps.Count == 0 ? 
            RoleTypeId.ClassD : 
            possibleScps[Random.Range(0, possibleScps.Count)];
    }
}