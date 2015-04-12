namespace Common.Contracts
{
    public interface IEventDispatcher<TEvent>
        where TEvent : IEvent
    {
        void Dispatch(TEvent evt);

        void Subscribe(IEventHandler<TEvent> handler);

        void Unsubscribe(IEventHandler<TEvent> handler);
    }
}