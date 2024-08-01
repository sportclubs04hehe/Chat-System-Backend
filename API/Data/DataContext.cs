using API.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext(DbContextOptions options): DbContext(options)
    {
        public DbSet<AppUser> Users => Set<AppUser>();
    }
}
