using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SistemaEventosCorporativos.UI
{
    public partial class CadastroEvento : UserControl
    {
        public event Action? OnVoltar;

        public CadastroEvento()
        {
            InitializeComponent();
            CarregarTiposEvento();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    Evento evento = new Evento
                    {
                        Nome = txtNome.Text,
                        DataInicio = DateOnly.FromDateTime(dpDataInicio.SelectedDate ?? DateTime.Now),
                        DataFim = DateOnly.FromDateTime(dpDataFim.SelectedDate ?? DateTime.Now),
                        Local = txtLocal.Text,
                        Observacoes = txtObservacoes.Text,
                        LotacaoMaxima = int.Parse(txtLotacao.Text),
                        OrcamentoMaximo = decimal.Parse(txtOrcamento.Text),
                        TipoEventoId = (int)cbTipoEvento.SelectedValue
                    };

                    context.Eventos.Add(evento);
                    context.SaveChanges();
                }

                MessageBox.Show("Evento cadastrado com sucesso");
                OnVoltar?.Invoke(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}");
            }
        }

        private void CarregarTiposEvento()
        {
            using (var context = new AppDbContext())
            {
                var tipos = context.TiposEventos.ToList();
                cbTipoEvento.ItemsSource = tipos;
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            OnVoltar?.Invoke();
        }
    }
}
