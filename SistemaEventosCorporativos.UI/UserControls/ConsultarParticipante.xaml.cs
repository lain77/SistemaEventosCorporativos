using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.CORE;
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
    /// <summary>
    /// Interação lógica para ConsultarParticipante.xam
    /// </summary>
    public partial class ConsultarParticipante : UserControl
    {

        public event Action? OnVoltar;

        public ConsultarParticipante()
        {
            InitializeComponent();
            CarregarParticipante();
        }
        private void CarregarParticipante()
        {
            using (var context = new AppDbContext())
            {
                var participantes = context.Participantes
                                           .AsNoTracking()
                                           .ToList();

                dataGridParticipantes.ItemsSource = participantes;
            }
        }



        private void BtnExcluirParticipantes_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridParticipantes.SelectedItem is Participante participanteSelecionado)
            {
                var resultado = MessageBox.Show(
                    $"Deseja mesmo excluir o participante '{participanteSelecionado.Nome}'?",
                    "Confirmar?",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                    );
                if( resultado == MessageBoxResult.Yes )
                {
                    using ( var context = new AppDbContext())
                    {
                        context.Participantes.Remove(participanteSelecionado);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione um participante");
            }
        }

        private void DataGridParticipantes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridParticipantes.SelectedItem is Participante participanteSelecionado)
            {
                var editar = new EditarParticipante(participanteSelecionado.Id);
                ContentArea.Content = editar;

                editar.OnVoltar += () =>
                {
                    ContentArea.Content = null;
                    CarregarParticipante();
                };
            }
        }


    }
}
