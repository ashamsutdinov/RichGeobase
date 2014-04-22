using System.ComponentModel.DataAnnotations;

namespace RichGeobase.Query.Interface
{
    /// <summary>
    /// A contract for all entity types with identification.
    /// </summary>
    /// <typeparam name="T">Entity identifier type.</typeparam>
    public interface IEntity<T>
    {
        [Key]
        T Id { get; set; }
    }
}