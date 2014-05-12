using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoLib;
using RichGeobase.Query.Interface;

namespace RichGeobase.Account.Dal.model
{
    internal class User: Entity<int>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
