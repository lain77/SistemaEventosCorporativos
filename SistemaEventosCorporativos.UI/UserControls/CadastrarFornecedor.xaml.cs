using SistemaEventosCorporativos.Core;
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
                        Tipo = txtTipo.Text,
                        EventoId = (int)cbEvento.SelectedValue
                    };
                    context.Fornecedores.Add(fornecedor);
                    context.SaveChanges();
                }
                MessageBox.Show("Fornecedor cadastrado.");
                OnVoltar?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
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
