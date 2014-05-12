
using GeoLib;

namespace RichGeobase.Account.Models.Interface
{
    public interface IUser : IDto
    {
        int Id { get; }
        string Login { get; set; }
        string Password { get; set; }
        string UserName { get; set; }

    }
}
