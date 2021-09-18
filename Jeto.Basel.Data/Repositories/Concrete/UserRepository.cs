using Jeto.Basel.Data.Context;
using Jeto.Basel.Data.Repositories.Abstract;
using Jeto.Basel.Domain.Models;

namespace Jeto.Basel.Data.Repositories.Concrete
{
    public class UserRepository  : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
            
        }
    }
}