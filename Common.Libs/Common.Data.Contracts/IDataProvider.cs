using System;
using Common.Contracts;

namespace Common.Data.Contracts
{
    public interface IDataProvider<TObjectInterface> :
        IEventDispatcher<IObjectUpdated<TObjectInterface>>,
        IEventDispatcher<IObjectDeleted<TObjectInterface>>
        where TObjectInterface : ITransientObject
    {
        ISaveResult<TObjectInterface> Save(TObjectInterface obj);

        IDeleteResult<TObjectInterface> Delete(TObjectInterface obj);

        IMultyQueryResult<TObjectInterface> Select(Func<TObjectInterface, bool> selector, int skip, int take);
    }

    public interface IDataProvider<in TKey, TObjectInterface> :
        IDataProvider<TObjectInterface>
        where TObjectInterface : ITransientObject<TKey>
        where TKey : IComparable<TKey>
    {
        ISingleQueryResult<TObjectInterface> GetById(TKey id);

        IDeleteResult<TObjectInterface> Delete(TKey id);
    }
}
