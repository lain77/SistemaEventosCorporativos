using SistemaEventosCorporativos.DATA;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class EditarEvento : UserControl
    {
        private int eventoId;

        // 🔹 Evento que o pai (ConsultaEventos) vai assinar
        public event Action? OnVoltar;

        public EditarEvento(int eventoId)
        {
            InitializeComponent();
            this.eventoId = eventoId;
            CarregarDados();
        }

        private void CarregarDados()
        {
            using (var context = new AppDbContext())
            {
                var evento = context.Eventos.Find(eventoId);
                if (evento != null)
                {
                    txtNome.Text = evento.Nome;
                    dpDataInicio.SelectedDate = evento.DataInicio.ToDateTime(TimeOnly.MinValue);
                    dpDataFim.SelectedDate = evento.DataFim.ToDateTime(TimeOnly.MinValue);
                    txtLocal.Text = evento.Local;
                    txtObservacoes.Text = evento.Observacoes;
                    txtLotacao.Text = evento.LotacaoMaxima.ToString();
                    txtOrcamento.Text = evento.OrcamentoMaximo.ToString("F2", CultureInfo.InvariantCulture);
                }
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var evento = context.Eventos.Find(eventoId);
                    if (evento != null)
                    {
                        evento.Nome = txtNome.Text;
                        evento.DataInicio = DateOnly.FromDateTime(dpDataInicio.SelectedDate ?? DateTime.Now);
                        evento.DataFim = DateOnly.FromDateTime(dpDataFim.SelectedDate ?? DateTime.Now);
                        evento.Local = txtLocal.Text;
                        evento.Observacoes = txtObservacoes.Text;

                        if (int.TryParse(txtLotacao.Text, out int lotacao))
                            evento.LotacaoMaxima = lotacao;

                        if (decimal.TryParse(txtOrcamento.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal orcamento))
                            evento.OrcamentoMaximo = orcamento;

                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Evento atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                // 🔹 Ao salvar, volta para a lista
                OnVoltar?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o evento: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtNome_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text == "Nome do Evento")
            {
                txtNome.Text = string.Empty;
                txtNome.Foreground = Brushes.Black;
            }
        }

        private void TxtNome_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                txtNome.Text = "Nome do Evento";
                txtNome.Foreground = Brushes.Gray;
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            // 🔹 dispara o evento para o pai cuidar
            OnVoltar?.Invoke();
        }
    }
}
