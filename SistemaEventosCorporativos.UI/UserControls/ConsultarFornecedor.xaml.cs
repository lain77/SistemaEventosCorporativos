using SistemaEventosCorporativos.Core;
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
    /// Interação lógica para ConsultarFornecedor.xam
    /// </summary>
    public partial class ConsultarFornecedor : UserControl
    {

        public event Action? OnVoltar; 

        public ConsultarFornecedor()
        {
            InitializeComponent();
            CarregarFornecedor();
        }

        private void CarregarFornecedor()
        {
            using (var context = new AppDbContext())
            {
                var fornecedores = context.Fornecedores.ToList();
                dataGridFornecedores.ItemsSource = fornecedores;
            }
        }

        private void BtnExcluirFornecedor_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridFornecedores.SelectedItem is Fornecedor fornecedorSelecionado)
            {
                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir o participante '{fornecedorSelecionado.NomeServico}'?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                    );

                if( resultado == MessageBoxResult.Yes )
                {
                    using ( var context = new AppDbContext())
                    {
                        context.Fornecedores.Remove( fornecedorSelecionado );
                        context.SaveChanges();
                    }

                    MessageBox.Show("Fornecedor excluido.");
                    CarregarFornecedor();
                }
                else
                {
                    MessageBox.Show("Selecione um fornecedor para excluir.");
                }
            }
        }

        private void DataGridFornecedores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(dataGridFornecedores.SelectedItem is Fornecedor fornecedorSelecionado)
            {
                var editar = new EditarFornecedores(fornecedorSelecionado.Id);

                ContentArea.Content = editar;

                editar.OnVoltar += () =>
                {
                    ContentArea.Content = null;
                    CarregarFornecedor();
                };
            }
        }

    }
}
