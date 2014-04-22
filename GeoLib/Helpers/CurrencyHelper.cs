using GeoLib.Model;
using GeoLib.Model.Entities;

namespace GeoLib.Helpers
{
    public static class CurrencyHelper
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