namespace GeoLib.Specification
{
    /// <summary>
    /// An abstraction for unit of work pattern.
    /// </summary>
    /// <remarks>
    /// The main goal of unit of work is to separate one set of changes from another. Each unit of work represents a single transaction.
    /// </remarks>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits any changes made during current unit of work scope.
        /// </summary>
        /// <remarks>
        /// A note to implementers: no secondary commit should be allowed!
        /// </remarks>
        void Commit();
    }
}