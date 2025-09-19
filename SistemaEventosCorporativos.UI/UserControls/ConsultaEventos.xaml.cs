using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class ConsultaEventos : UserControl
    {
        public event Action? OnVoltar; // caso queira voltar ao menu

        public ConsultaEventos()
        {
            InitializeComponent();
            CarregarEventos();
        }

        public void CarregarEventos()
        {
            using (var context = new AppDbContext())
            {
                var eventos = context.Eventos.ToList();
                dataGridEventos.ItemsSource = eventos;
            }
        }

        private void BtnExcluirEventos_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridEventos.SelectedItem is Evento eventoSelecionado)
            {
                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir o evento '{eventoSelecionado.Nome}'?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (resultado == MessageBoxResult.Yes)
                {
                    using (var context = new AppDbContext())
                    {
                        context.Eventos.Remove(eventoSelecionado);
                        context.SaveChanges();
                    }

                    CarregarEventos();
                }
            }
            else
            {
                MessageBox.Show("Selecione um evento para excluir");
            }
        }

        private void DataGridEventos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridEventos.SelectedItem is Evento eventoSelecionado)
            {
                var editar = new EditarEvento(eventoSelecionado.Id);

                ContentArea.Content = editar;

                editar.OnVoltar += () =>
                {
                    ContentArea.Content = null;
                    CarregarEventos();
                };
            }
        }

    }
}
