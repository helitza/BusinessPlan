using Microsoft.EntityFrameworkCore;

namespace Models{

    public class SajamContext : DbContext
    {
        public DbSet<Sajam> Sajmovi { get; set; }
        public DbSet<Mentor> Mentori { get; set; }
        public DbSet<Kompanija> Kompanije { get; set; }
        public DbSet<Sala> Sale { get; set; }
        public DbSet<Prezentovanje> Prezentovanja { get; set; }
        public DbSet<Task> Taskovi { get; set; }

        public SajamContext(DbContextOptions options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Kompanija>()
                        .HasMany<Task>(p => p.Taskovi)
                        .WithOne(p => p.Kompanija);
        }
    }
}