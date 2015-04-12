using System;

namespace Common.Data.Contracts
{
    public interface ITransientObject
    {
        
    }

    public interface ITransientObject<TKey> :
        ITransientObject
        where TKey : IComparable<TKey>
    {
        TKey Id { get; }
    }
}