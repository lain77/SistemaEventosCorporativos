using SistemaEventosCorporativos.DATA;
using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.CORE;
using System.Linq;
using System.Windows.Controls;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class TipoParticipante : UserControl
    {
        public TipoParticipante()
        {
            InitializeComponent();
            CarregarEventos();
        }

        private void CarregarEventos()
        {
            using (var context = new AppDbContext())
            {
                var eventos = context.Eventos
                    .Select(ev => new { ev.Id, ev.Nome })
                    .ToList();

                listEventos.ItemsSource = eventos;
            }
        }

        private void ListEventos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listEventos.SelectedValue == null) return;

            int eventoId = (int)listEventos.SelectedValue;

            using (var context = new AppDbContext())
            {
                var tiposComContagem = context.ParticipanteEvento
                    .Where(pe => pe.EventoId == eventoId)
                    .Select(pe => pe.Participante)
                    .GroupBy(p => p.Tipo.ToUpper())
                    .Select(g => new
                    {
                        Tipo = g.Key,
                        Quantidade = g.Count()
                    })
                    .OrderByDescending(x => x.Quantidade)
                    .ToList();

                dataGridTipos.ItemsSource = tiposComContagem;
            }
        }
    }
}
