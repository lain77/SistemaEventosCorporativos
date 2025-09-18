using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using System.Linq;
using System.Windows;

namespace SistemaEventosCorporativos.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e); 

            using (var context = new AppDbContext())
            {
                if (!context.TiposEventos.Any())
                {
                    context.TiposEventos.AddRange(
                        new TipoEvento { Descricao = "Treinamento" },
                        new TipoEvento { Descricao = "Reunião" },
                        new TipoEvento { Descricao = "Conferência" },
                        new TipoEvento { Descricao = "AirShow" }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
