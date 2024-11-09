using Microsoft.EntityFrameworkCore;
using Resume.Domain.Entities.User;

namespace Resume.Domain.Context
{
    public class DatabaseContext : DbContext
    {
        #region User

        public DbSet<User> Users { get; set; }

        #endregion
    }
}
