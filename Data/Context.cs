using Microsoft.EntityFrameworkCore;
 
namespace Vocabulary
{
    public class Context : DbContext
    {
        public DbSet<Word> Words { get; set; }
        
        public Context(DbContextOptions<Context> options) : base(options)
        { }
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=vocabulary;Username=postgres;Password=postgres");
        }
    }
}