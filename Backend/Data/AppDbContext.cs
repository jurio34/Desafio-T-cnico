using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Reserva> Reservas {get; set;} = null!;
        public DbSet<Sala> Salas {get; set;} = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Sala>().HasData(
                new Sala { Id = 1, Nome = "Sala Inovação" },
                new Sala { Id = 2, Nome = "Sala Foco" },
                new Sala { Id = 3, Nome = "Auditório Central" }
            );
        }
    }

}