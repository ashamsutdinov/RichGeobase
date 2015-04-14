using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Common.Contracts;

namespace Common.Events
{
    public class EventDispatcher<TEvent> :
        IEventDispatcher<TEvent>
        where TEvent : IEvent
    {
        #region Private

        private readonly IDictionary<Type, IList<IEventHandler<TEvent>>> _handlers = new ConcurrentDictionary<Type, IList<IEventHandler<TEvent>>>();

        private readonly Type _eventType = typeof(TEvent);

        #endregion

        #region Implementation of IEventDispatcher

        public void Dispatch(TEvent evt)
        {
            TryDoWithHandlers(handlers =>
            {
                foreach (var handler in handlers)
                {
                    handler.Handle(evt);
                }
            });
        }

        public void Subscribe(IEventHandler<TEvent> handler)
        {
            TryDoWithHandlers(handlers =>
            {
                if (!handlers.Contains(handler))
                {
                    handlers.Add(handler);
                }
            });
        }

        public void Unsubscribe(IEventHandler<TEvent> handler)
        {
            TryDoWithHandlers(handlers =>
            {
                if (handlers.Contains(handler))
                {
                    handlers.Remove(handler);
                }
            });
        }

        #endregion

        #region Utils

        private void TryDoWithHandlers(Action<IList<IEventHandler<TEvent>>> action)
        {
            IList<IEventHandler<TEvent>> handlers;
            _handlers.TryGetValue(_eventType, out handlers);
            if (handlers == null)
            {
                handlers = new List<IEventHandler<TEvent>>();
                _handlers.Add(_eventType, handlers);
            }
            try
            {
                action(handlers);
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }
}