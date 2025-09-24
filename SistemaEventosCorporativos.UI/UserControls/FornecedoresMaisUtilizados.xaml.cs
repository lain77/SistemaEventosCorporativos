using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using SistemaEventosCorporativos.CORE.DTO;
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
    /// Interação lógica para FornecedoresMaisUtilizados.xam
    /// </summary>
    public partial class FornecedoresMaisUtilizados : UserControl
    {
        public FornecedoresMaisUtilizados()
        {
            InitializeComponent();
            CarregarDados();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow main)
            {
                main.ContentArea.Content = new Relatórios();
            }
        }

        private void CarregarDados()
        {
            using (var context = new AppDbContext())
            {
                var dados = context.FornecedorEvento
                    .GroupBy(fe => fe.Fornecedor)
                    .Select(g => new FornecedorRelatorioDTO
                    {
                        NomeFornecedor = g.Key.NomeServico,
                        TotalQuantidade = g.Count(),
                        ValorTotal = g.Sum(x => x.Fornecedor.Valor) 
                    })
                    .OrderByDescending(x => x.TotalQuantidade)
                    .Take(10)
                    .ToList();

                dataGridFornecedores.ItemsSource = dados;
            }
        }
    }
}
