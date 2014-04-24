using System;
using System.Collections.Generic;
using GeoLib.Dal.Model;
using RichGeobase.Query.Interface;

namespace RichGeobase.Query.Dal
{
    public class GeoQueryEntityContext :
        GeoContext,
        IEntityQueryContext
    {
        public int QueryCount<TCriterion>(TCriterion criterion) where TCriterion : ICriterion
        {
            throw new NotImplementedException();
        }

        public TResult QuerySingle<TCriterion, TResult>(TCriterion criterion) where TCriterion : ICriterion
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TResult> QueryMultiple<TCriterion, TResult>(TCriterion criterion) where TCriterion : ICriterion
        {
            throw new NotImplementedException();
        }
    }
}
