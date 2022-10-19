using Microsoft.EntityFrameworkCore;
using NotesApplication.Models.Enums;

namespace NotesApplication.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Note>()
                .Property(e => e.Color)
                .HasConversion(
                    v => v.ToString(),
                    v => (Color)Enum.Parse(typeof(Color), v));  
            
            base.OnModelCreating(modelBuilder);
        }    
        
    }
}
