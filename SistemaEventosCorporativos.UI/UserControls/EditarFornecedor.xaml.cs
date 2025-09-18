using SistemaEventosCorporativos.DATA;
using SistemaEventosCorporativos.Core;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class EditarFornecedor : UserControl
    {
        private int fornecedorId;

        public event Action? OnVoltar;

        public EditarFornecedor(int fornecedorId)
        {
            InitializeComponent();
            this.fornecedorId = fornecedorId;
            CarregarDados();
        }

        private void CarregarDados()
        {
            using (var context = new AppDbContext())
            {
                var fornecedor = context.Fornecedores.Find(fornecedorId);
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
                    var fornecedor = context.Fornecedores.Find(fornecedorId);
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

        #region Placeholders simulados
        private void TxtNome_GotFocus(object sender, RoutedEventArgs e)
        {
            if (fornecedor_txtNome.Text == "Nome do Serviço")
            {
                fornecedor_txtNome.Text = "";
                fornecedor_txtNome.Foreground = Brushes.Black;
            }
        }

        private void TxtNome_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fornecedor_txtNome.Text))
            {
                fornecedor_txtNome.Text = "Nome do Serviço";
                fornecedor_txtNome.Foreground = Brushes.Gray;
            }
        }

        private void TxtValor_GotFocus(object sender, RoutedEventArgs e)
        {
            if (fornecedor_txtValor.Text == "0.00")
            {
                fornecedor_txtValor.Text = "";
                fornecedor_txtValor.Foreground = Brushes.Black;
            }
        }

        private void TxtValor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fornecedor_txtValor.Text))
            {
                fornecedor_txtValor.Text = "0.00";
                fornecedor_txtValor.Foreground = Brushes.Gray;
            }
        }

        private void TxtCnpj_GotFocus(object sender, RoutedEventArgs e)
        {
            if (fornecedor_txtCnpj.Text == "CNPJ")
            {
                fornecedor_txtCnpj.Text = "";
                fornecedor_txtCnpj.Foreground = Brushes.Black;
            }
        }

        private void TxtCnpj_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fornecedor_txtCnpj.Text))
            {
                fornecedor_txtCnpj.Text = "CNPJ";
                fornecedor_txtCnpj.Foreground = Brushes.Gray;
            }
        }
        #endregion

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            OnVoltar?.Invoke();
        }
    }
}
