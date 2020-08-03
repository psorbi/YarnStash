using Microsoft.EntityFrameworkCore;
using YarnStash.Models;

namespace YarnStash.Data
{
    public class YarnContext : DbContext  //database
    {
        public YarnContext(DbContextOptions<YarnContext> options) : base(options)
        {

        }

        public DbSet<YarnModel> Yarn { get; set; } //table of yarn model - data

        /*
        private DbSet<YarnModel> yarn;
        public DbSet<YarnModel> GetYarnModel()
        {
            return yarn;
        }
        public void SetYarnModel(DbSet<YarnModel> model)
        {
            yarn = model;
        }
        */
    }
}
