using Common.Components;
using Common.Contracts;
using Common.Events;
using Common.Utils;

namespace Common.Commands
{
    public class CommandFactory : 
        Component,
        ICommandFactory
    {
        #region Private

        private readonly IEventDispatcher<ICommandExecuted> _eventDispatcher = new EventDispatcher<ICommandExecuted>(); 

        #endregion

        #region Implementation of IEventDispatcher

        public void Dispatch(ICommandExecuted evt)
        {
            _eventDispatcher.Dispatch(evt);
        }

        public void Subscribe(IEventHandler<ICommandExecuted> handler)
        {
            _eventDispatcher.Subscribe(handler);
        }

        public void Unsubscribe(IEventHandler<ICommandExecuted> handler)
        {
            _eventDispatcher.Unsubscribe(handler);
        }

        #endregion

        #region Implementation of ICommandFactory

        public TCommand Create<TCommand, TInputData, TResult>(TInputData input) 
            where TCommand : ICommand<TInputData, TResult>
            where TInputData : ICommandInput 
            where TResult : ICommandResult
        {
            return ObjectFactory<TCommand, ICommandFactory>.CreateObject.Invoke(this);
        }

        #endregion
    }
}