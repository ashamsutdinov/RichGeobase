using System.Data.Entity;

namespace GeoLib.Model
{
    public static class DbSetExtensions
    {
        public static TEntity GetById<TEntity>(this DbSet<TEntity> dbset, params object[] key) 
            where TEntity : class
        {
            return dbset.Find(key);
        }

        public static EntityHelper<TEntity> GetOrCreate<TEntity>(this DbSet<TEntity> dbset, params object[] key) 
            where TEntity : class, new()
        {
            var found = dbset.GetById(key);
            var helper = new EntityHelper<TEntity> {IsNew = found == null, Entity = found ?? new TEntity()};
            return helper;
        }

        public static void PrepareToSave<TEntity>(this DbSet<TEntity> dbset, EntityHelper<TEntity> entity) 
            where TEntity : class
        {
            if (entity.IsNew)
            {
                dbset.Add(entity.Entity);
            }
        }
    }
}