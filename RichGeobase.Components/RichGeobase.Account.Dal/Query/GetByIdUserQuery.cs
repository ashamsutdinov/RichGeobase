using System.Linq;
using RichGeobase.Account.Dal.model;
using RichGeobase.Account.Models.Interface;
using RichGeobase.Query;
using RichGeobase.Query.Criterions;
using RichGeobase.Query.Interface;

namespace RichGeobase.Account.Dal.Query
{
    internal class GetByIdUserQuery : ISingleSelectionQuery<GetByIdCriterion, IUser>
    {
        private readonly EfDbContext _dbcontext;

        public GetByIdUserQuery()
        {
            _dbcontext = new EfDbContext();
        }

        public IUser Execute(GetByIdCriterion criterion)
        {
            User u = _dbcontext.Users.FirstOrDefault(user => user.Id == criterion.Id);
            return (IUser)Mappers.GetDto<User>(u);
        }
    }
}
