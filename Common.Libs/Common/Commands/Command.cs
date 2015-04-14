using System;
using Common.Contracts;

namespace Common.Commands
{
    public abstract class Command<TInputData, TResult> : 
        ICommand<TInputData, TResult>
        where TInputData : CommandInput
        where TResult : CommandResult, new()
    {
        #region ctor

        protected Command(ICommandFactory factory)
        {
            _factory = factory;
        }

        #endregion

        #region Private

        private readonly ICommandFactory _factory;

        #endregion

        #region Protected

        protected TResult Result { get; private set; }

        protected abstract void ExecuteCore();

        #endregion

        #region Implementation of ICommand

        public TInputData Input { get; set; }

        public TResult Execute()
        {
            Result = new TResult();
            try
            {
                ExecuteCore();
            }
            catch (Exception ex)
            {
                Result.TransactionResult = new TransactionResult(false, ex);
            }
            _factory.Dispatch(new CommandExecuted(_factory, this, Result));
            return Result;
        }

        #endregion
    }
}
