using Microsoft.EntityFrameworkCore;
using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.CORE;
using SistemaEventosCorporativos.DATA;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SistemaEventosCorporativos.UI.UserControls
{
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
                var fornecedores = context.FornecedorEvento
                    .Include(fe => fe.Fornecedor)
                    .Include(fe => fe.Evento)    
                    .ToList();

                dataGridFornecedores.ItemsSource = fornecedores;
            }
        }

        private void BtnExcluirFornecedor_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridFornecedores.SelectedItem is FornecedorEvento fornecedorEventoSelecionado)
            {
                var resultado = MessageBox.Show(
                    $"Deseja realmente excluir o fornecedor '{fornecedorEventoSelecionado.Fornecedor.NomeServico}'?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (resultado == MessageBoxResult.Yes)
                {
                    using (var context = new AppDbContext())
                    {
                        // Remove primeiro a relação FornecedorEvento
                        context.FornecedorEvento.Remove(fornecedorEventoSelecionado);

                        // Opcional: também remover o fornecedor (caso não seja usado em outros eventos)
                        // context.Fornecedores.Remove(fornecedorEventoSelecionado.Fornecedor);

                        context.SaveChanges();
                    }

                    MessageBox.Show("Fornecedor excluído com sucesso!");
                    CarregarFornecedor();
                }
            }
            else
            {
                MessageBox.Show("Selecione um fornecedor para excluir.");
            }
        }

        private void DataGridFornecedores_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridFornecedores.SelectedItem is FornecedorEvento fornecedorEventoSelecionado)
            {
                var editar = new EditarFornecedores(fornecedorEventoSelecionado.Fornecedor.Id);

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
