using System;
using System.Linq;
using Exiled.API.Features;
using MERRoomReplacement.Events.Handlers;
using MERRoomReplacement.Events.Interfaces;
using MERRoomReplacement.Features.Configuration;

namespace MERRoomReplacement
{
    public class MERRoomReplacement : Plugin<Config>
    {
        private IEventHandler _replacementHandler;

            
        public override string Name => "MERRoomReplacement";
        
        public override string Author => "FakeMan";

        public override string Prefix => "room_replacement";

        public override Version Version => new(1, 3, 1);
        
        public override void OnEnabled()
        {
            base.OnEnabled();

            _replacementHandler = new ReplacementHandler(
                Config.ReplacementOptions.Where(x => x.IsEnabled));

            _replacementHandler.SubscribeEvents();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            
            _replacementHandler?.UnsubscribeEvents();
        }
    }
}