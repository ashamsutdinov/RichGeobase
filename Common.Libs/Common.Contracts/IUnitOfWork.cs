namespace Common.Contracts
{
    public interface IUnitOfWork
    {
        ITransactionResult Commit();
    }
}
