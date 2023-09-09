using System;
using System.Linq;
using Exiled.API.Features;
using HarmonyLib;
using MERRoomReplacement.Events.Handlers;
using MERRoomReplacement.Events.Interfaces;
using MERRoomReplacement.Features.Configuration;

namespace MERRoomReplacement
{
    public class MERRoomReplacement : Plugin<Config>
    {
        private IEventHandler _replacementHandler;

        private Harmony _harmony;
            
        public override string Name => "MERRoomReplacement";
        
        public override string Author => "FakeMan";

        public override string Prefix => "room_replacement";

        public override Version Version => new(1, 3, 1);
        
        public override void OnEnabled()
        {
            base.OnEnabled();

            _replacementHandler = new ReplacementHandler(
                Config.ReplacementOptions.Where(x => x.IsEnabled),
                Config.PreventScp079OnRoleChange);

            _replacementHandler.SubscribeEvents();

            if (Config.RemoveScp079FromSpawnQueue) 
                return;
            
            _harmony = new Harmony("fakeman.merroomreplacement.patcher");

            // RemoveScp079FromSpawnQueue.PatchSpawnQueue(_harmony);
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            
            _replacementHandler?.UnsubscribeEvents();
            _harmony?.UnpatchAll(_harmony.Id);
        }
    }
}