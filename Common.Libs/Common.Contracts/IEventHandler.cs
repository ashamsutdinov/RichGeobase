namespace Common.Contracts
{
    public interface IEventHandler<TEvent>
        where TEvent : IEvent
    {
        void Handle(TEvent evt);
    }
}