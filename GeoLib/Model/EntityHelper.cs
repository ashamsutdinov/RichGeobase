namespace GeoLib.Model
{
    public class EntityHelper<TEntity>
    {
        public TEntity Entity { get; set; }

        public bool IsNew { get; set; }
    }
}