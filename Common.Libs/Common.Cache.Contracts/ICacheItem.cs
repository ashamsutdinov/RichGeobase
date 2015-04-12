using System;

namespace Common.Cache.Contracts
{
    public interface ICacheItem
    {
        TObject Data<TObject>();

        string[] Tags { get; }

        DateTime DateCreated { get; }
        
        TimeSpan Lifetime { get; }
    }
}