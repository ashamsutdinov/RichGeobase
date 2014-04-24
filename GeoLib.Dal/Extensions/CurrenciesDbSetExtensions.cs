using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;

namespace GeoLib.Dal.Extensions
{
    public static class CurrenciesDbSetExtensions
    {
        public static Currency SaveCurrency(string id, string name, GeoContext context)
        {
            var ctx = context ?? new GeoContext();

            var currency = ctx.Currencies.GetOrCreate(id);
            currency.Entity.Id = id;
            currency.Entity.Name = name;

            ctx.Currencies.PrepareToSave(currency);

            if (context == null)
            {
                ctx.SaveChanges();
                ctx.Dispose();
            }

            return currency.Entity;
        }
    }
}