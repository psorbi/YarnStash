using Microsoft.EntityFrameworkCore;
using YarnStash.Models;

namespace YarnStash.Data
{
    public class YarnContext : DbContext
    {
        public YarnContext(DbContextOptions<YarnContext> options) : base(options)
        {

        }

        public DbSet<YarnModel> Yarn { get; set; }
    }
}
