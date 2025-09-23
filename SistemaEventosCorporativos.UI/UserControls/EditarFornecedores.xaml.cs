using SistemaEventosCorporativos.DATA;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class EditarFornecedores : UserControl
    {
        private int fornecedorId;

        public event Action? OnVoltar;

        public EditarFornecedores(int fornecedorId)
        {
            InitializeComponent();
            this.fornecedorId = fornecedorId;
            CarregarDados();
        }

        private void CarregarDados()
        {
            using (var context = new AppDbContext())
            {
                var fornecedor = context.Fornecedores
                                        .FirstOrDefault(f => f.Id == fornecedorId);

                if (fornecedor != null)
                {
                    fornecedor_txtNome.Text = fornecedor.NomeServico;
                    fornecedor_txtNome.Foreground = Brushes.Black;

                    fornecedor_txtValor.Text = fornecedor.Valor.ToString("F2", CultureInfo.InvariantCulture);
                    fornecedor_txtValor.Foreground = Brushes.Black;

                    fornecedor_txtCnpj.Text = fornecedor.CNPJ;
                    fornecedor_txtCnpj.Foreground = Brushes.Black;
                }
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var fornecedor = context.Fornecedores
                                            .FirstOrDefault(f => f.Id == fornecedorId);

                    if (fornecedor != null)
                    {
                        fornecedor.NomeServico = fornecedor_txtNome.Text;

                        if (decimal.TryParse(fornecedor_txtValor.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
                            fornecedor.Valor = valor;

                        fornecedor.CNPJ = fornecedor_txtCnpj.Text;

                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Fornecedor atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                OnVoltar?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o fornecedor: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 🔹 Métodos genéricos de placeholder
        private void Placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && tb.Text == tb.Tag?.ToString())
            {
                tb.Text = "";
                tb.Foreground = Brushes.Black;
            }
        }

        private void Placeholder_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = tb.Tag?.ToString() ?? "";
                tb.Foreground = Brushes.Gray;
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            OnVoltar?.Invoke();
        }
    }
}
