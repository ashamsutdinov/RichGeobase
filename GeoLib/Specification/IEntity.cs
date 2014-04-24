using System.ComponentModel.DataAnnotations;

namespace GeoLib.Specification
{
    public interface IEntity
    {
    }

    /// <summary>
    /// A contract for all entity types with identification.
    /// </summary>
    /// <typeparam name="T">Entity identifier type.</typeparam>
    public interface IEntity<T> :
        IEntity
    {
        [Key]
        T Id { get; set; }
    }
}
