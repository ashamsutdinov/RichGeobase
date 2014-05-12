using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RichGeobase.Account.Models.Interface;

namespace RichGeobase.Account.Dal.Dal
{
    class UserDto : IUser
    {
        internal int id;

        public int Id
        {
            get { return id; }
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
