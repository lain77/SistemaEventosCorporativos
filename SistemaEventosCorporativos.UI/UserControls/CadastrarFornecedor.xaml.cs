using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.CORE;
using SistemaEventosCorporativos.DATA;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class CadastrarFornecedor : UserControl
    {
        public event Action? OnVoltar;

        public CadastrarFornecedor()
        {
            InitializeComponent();
            CarregarEventos();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Busca o evento selecionado
                    int eventoId = (int)cbEvento.SelectedValue;
                    var evento = context.Eventos.FirstOrDefault(ev => ev.Id == eventoId);

                    if (evento == null)
                    {
                        MessageBox.Show("Evento não encontrado.");
                        return;
                    }

                    Fornecedor fornecedor = new Fornecedor
                    {
                        NomeServico = txtNomeServico.Text,
                        CNPJ = txtCnpj.Text,
                        Valor = decimal.Parse(txtValor.Text),
                        Tipo = txtTipo.Text
                    };

                    decimal totalFornecedores = context.FornecedorEvento
                        .Where(fe => fe.EventoId == eventoId)
                        .Join(context.Fornecedores,
                              fe => fe.FornecedorId,
                              f => f.Id,
                              (fe, f) => f.Valor)
                        .Sum();

                    if (totalFornecedores + fornecedor.Valor > evento.OrcamentoMaximo)
                    {
                        MessageBox.Show("Você não pode ultrapassar o orçamento do evento.");
                        return;
                    }

                    context.Fornecedores.Add(fornecedor);
                    context.SaveChanges();

                    FornecedorEvento fornecedorEvento = new FornecedorEvento
                    {
                        FornecedorId = fornecedor.Id,
                        EventoId = eventoId
                    };

                    context.FornecedorEvento.Add(fornecedorEvento);
                    context.SaveChanges();

                    MessageBox.Show("Fornecedor cadastrado no evento com sucesso!");
                    OnVoltar?.Invoke();
                }
            }
            catch (Exception ex)
            {
                string mensagemErro = ex.Message;
                if (ex.InnerException != null)
                    mensagemErro += "\nInnerException: " + ex.InnerException.Message;

                MessageBox.Show("Erro ao cadastrar fornecedor: " + mensagemErro);
            }
        }


        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            OnVoltar?.Invoke();
        }

        private void CarregarEventos()
        {
            using (var context = new AppDbContext())
            {
                var eventos = context.Eventos.ToList();
                cbEvento.ItemsSource = eventos;
                cbEvento.DisplayMemberPath = "Nome";
                cbEvento.SelectedValuePath = "Id";
            }
        }
    }
}
