namespace MERRoomReplacement.Events.Interfaces;

public interface IEventHandler
{
    void SubscribeEvents();
    
    void UnsubscribeEvents();
}