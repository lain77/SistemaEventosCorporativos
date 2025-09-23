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
                    Fornecedor fornecedor = new Fornecedor
                    {
                        NomeServico = txtNomeServico.Text,
                        CNPJ = txtCnpj.Text,
                        Valor = decimal.Parse(txtValor.Text),
                        Tipo = txtTipo.Text
                    };

                    context.Fornecedores.Add(fornecedor);
                    context.SaveChanges(); 

                    FornecedorEvento fornecedorEvento = new FornecedorEvento
                    {
                        FornecedorId = fornecedor.Id,
                        EventoId = (int)cbEvento.SelectedValue
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
