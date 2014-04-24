using System.ComponentModel.DataAnnotations;
using GeoLib.Specification;

namespace GeoLib
{
    /// <summary>
    /// A basic abstraction for all entity types.
    /// </summary>
    public abstract class Entity : 
        IEntity
    {
    }

    /// <summary>
    /// A basic abstraction for all entity types with identification.
    /// </summary>
    /// <typeparam name="T">Entity identifier type.</typeparam>
    public abstract class Entity<T> : 
        Entity, 
        IEntity<T>
    {
        protected Entity()
        {
            Id = default(T);
        }

        #region Implementation of IEntity<T>

        [Key]
        public T Id { get; set; }

        #endregion
    }
}
