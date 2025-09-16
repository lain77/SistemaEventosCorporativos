using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Core;


namespace SistemaEventosCorporativos.DATA
{
    public class AppDbContext : DbContext
    {
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Participante> Participantes { get; set; }  

        public DbSet<Fornecedor> Fornecedores { get; set; } 

        public DbSet<TipoEvento> TiposEventos { get; set; } 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=EventosDB;Trusted_Connection=True;TrustServerCertificate=True;");    
        }
    }
}
