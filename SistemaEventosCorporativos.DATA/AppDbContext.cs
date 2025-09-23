using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.CORE;


namespace SistemaEventosCorporativos.DATA
{
    public class AppDbContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Participante> Participantes { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<TipoEvento> TiposEventos { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<ParticipanteEvento> ParticipanteEvento { get; set; }

        public DbSet<FornecedorEvento> FornecedorEvento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=EventosDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParticipanteEvento>().ToTable("ParticipanteEvento");

            modelBuilder.Entity<Evento>()
                .Property(e => e.DataInicio)
                .HasConversion(
                    d => d.ToDateTime(new TimeOnly(0, 0)),
                    d => DateOnly.FromDateTime(d)
                );

            modelBuilder.Entity<Evento>()
                .Property(e => e.DataFim)
                .HasConversion(
                    d => d.ToDateTime(new TimeOnly(0, 0)),
                    d => DateOnly.FromDateTime(d)
                );
        }
    }
}
