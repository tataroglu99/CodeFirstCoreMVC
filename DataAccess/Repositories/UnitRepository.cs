using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UnitRepository : BaseRepository<Unit>
    {
        public UnitRepository(DbContext baseContext) : base(baseContext)
        {
        }
    }
}
