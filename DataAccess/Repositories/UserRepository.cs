using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DbContext baseContext) : base(baseContext)
        {
        }
    }
}
