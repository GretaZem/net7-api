using Microsoft.EntityFrameworkCore;
using net7_api.Models;

namespace net7_api.Context
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<PollutionPointModel> PollutionPoints { get; set; } = null!;
    }
}
