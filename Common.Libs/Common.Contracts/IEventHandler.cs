namespace Common.Contracts
{
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        void Handle(TEvent evt);

        void Subscribe(IEventDispatcher<TEvent> dispatcher);

        void Unsubscribe(IEventDispatcher<TEvent> dispatcher);
    }
}