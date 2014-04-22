using System;
using System.Linq.Expressions;
using RichGeobase.Query.Interface;

namespace RichGeobase.Query.Criterions
{
    public class BaseGridCriterion<TBaseEntity> : ICriterion
    {
        public int PageNumber { get;  set; }
        public int RowsPerPage { get;  set; }
        public bool IsAscending { get;  set; }
        public string SortColumn { get;  set; }
        public int CountRows { get;  set; }
        public Expression<Func<TBaseEntity, bool>> ExpressionForWhere { get; set; }
    }
}
