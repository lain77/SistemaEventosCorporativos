using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Text.Json;

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

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTipoEvento.SelectedValue == null)
                {
                    MessageBox.Show("Selecione um tipo de evento.");
                    return;
                }

                using (var context = new AppDbContext())
                {
                    var endereco = new Endereco
                    {
                        Rua = txtRua.Text.Trim(),
                        Numero = txtNumero.Text.Trim(),
                        Bairro = txtBairro.Text.Trim(),
                        Cidade = txtCidade.Text.Trim(),
                        Estado = txtEstado.Text.Trim(),
                        CEP = txtCep.Text.Trim()
                    };

                    await context.Enderecos.AddAsync(endereco);
                    await context.SaveChangesAsync();

                    var evento = new Evento
                    {
                        Nome = txtNome.Text.Trim(),
                        DataInicio = DateOnly.FromDateTime(dpDataInicio.SelectedDate ?? DateTime.Now),
                        DataFim = DateOnly.FromDateTime(dpDataFim.SelectedDate ?? DateTime.Now),
                        Local = txtCidade.Text.Trim(),
                        Observacoes = txtObservacoes.Text.Trim(),
                        LotacaoMaxima = int.TryParse(txtLotacao.Text, out var l) ? l : 0,
                        OrcamentoMaximo = decimal.TryParse(txtOrcamento.Text, out var o) ? o : 0,
                        TipoEventoId = Convert.ToInt32(cbTipoEvento.SelectedValue),
                        EnderecoId = endereco.Id
                    };

                    await context.Eventos.AddAsync(evento);
                    await context.SaveChangesAsync();
                }

                MessageBox.Show("Evento cadastrado com sucesso!");
                OnVoltar?.Invoke();
            }
            catch (Exception ex)
            {
                string mensagem = ex.Message;

                if (ex.InnerException != null)
                {
                    mensagem += "\nDetalhes: " + ex.InnerException.Message;
                }

                MessageBox.Show($"Erro ao salvar: {mensagem}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private async void BtnBuscarCep_Click(object sender, RoutedEventArgs e)
        {
            string cep = txtCep.Text.Trim().Replace("-", "");
            if (string.IsNullOrEmpty(cep))
            {
                MessageBox.Show("Informe um CEP válido.");
                return;
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://viacep.com.br/ws/{cep}/json/";
                    var response = await client.GetStringAsync(url);

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var endereco = JsonSerializer.Deserialize<ViaCepResponse>(response, options);

                    if (endereco != null && !string.IsNullOrEmpty(endereco.Logradouro))
                    {
                        txtRua.Text = endereco.Logradouro;
                        txtBairro.Text = endereco.Bairro;
                        txtCidade.Text = endereco.Localidade;
                        txtEstado.Text = endereco.Uf;
                    }
                    else
                    {
                        MessageBox.Show("CEP não encontrado.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao consultar o CEP: {ex.Message}");
                }
            }
        }

        public class ViaCepResponse
        {
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
        }
    }
}
