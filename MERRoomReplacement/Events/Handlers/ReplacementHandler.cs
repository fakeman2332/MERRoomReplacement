using System.Collections.Generic;
using MERRoomReplacement.Api;
using MERRoomReplacement.Events.Interfaces;
using MERRoomReplacement.Features.Configuration.Structures;
using Map = Exiled.Events.Handlers.Map;

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
        Map.Generated += OnMapGenerated;
    }

    private void OnMapGenerated()
    {
        foreach (var roomSchematic in _replacementOptions)
        {
            RoomReplacer.ReplaceRoom(roomSchematic.TargetRoomType, roomSchematic.SchematicName);
        }
    }

    public void UnsubscribeEvents()
    {
        Map.Generated -= OnMapGenerated;
    }
}