using System.Diagnostics;
using JetBrains.Annotations;

namespace RichGeobase.Query
{
    /// <summary>
    /// Represents a base class for all Query descendant classes, providing the reference to the <see cref="WriteWrightDbContext"/> property.
    /// </summary>
    public abstract class BaseQuery
    {
        private IDbContext _queryProvider;

        /// <summary>
        /// Gets a <see>
        ///         <cref>IDbContext</cref>
        ///     </see>
        ///     instance to perform a query.
        /// </summary>
        [NotNull]
        [UsedImplicitly]
        public IDbContext QueryProvider
        {
            get { return _queryProvider; }
            set
            {
                Debug.Assert(_queryProvider == null, "QueryProvider can be set only once.");
                _queryProvider = value;
            }
        }
    }
}