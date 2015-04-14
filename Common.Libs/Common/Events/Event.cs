using Common.Contracts;

namespace Common.Events
{
    public abstract class Event :
        IEvent
    {
        protected Event(object sender)
        {
            Sender = sender;
        }

        public object Sender { get; private set; }
    }
}
