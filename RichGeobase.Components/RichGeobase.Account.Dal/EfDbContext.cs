using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RichGeobase.Account.Dal.model;

namespace RichGeobase.Account.Dal
{
    class EfDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
