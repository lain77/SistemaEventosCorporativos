using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.CORE.DTO;
using SistemaEventosCorporativos.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class AgendaParticipantes : UserControl
    {
        public AgendaParticipantes()
        {
            InitializeComponent();
            CarregarParticipantes();
        }

        private void CarregarParticipantes()
        {
            using (var context = new AppDbContext())
            {
                var participantes = context.Participantes
                    .OrderBy(p => p.Nome)
                    .ToList();

                listParticipantes.ItemsSource = participantes;
            }
        }

        private void ListParticipantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listParticipantes.SelectedItem is Participante participante)
            {
                using (var context = new AppDbContext())
                {
                    var eventos = context.ParticipanteEvento
                        .Where(pe => pe.ParticipanteId == participante.Id)
                        .Select(pe => new EventoParticipanteDTO
                        {
                            NomeEvento = pe.Evento.Nome,
                            DataInicio = pe.Evento.DataInicio,
                            DataFim = pe.Evento.DataFim
                        })
                        .OrderBy(ev => ev.DataInicio)
                        .ToList();

                    dataGridEventos.ItemsSource = eventos;
                }
            }
        }
    }
}
