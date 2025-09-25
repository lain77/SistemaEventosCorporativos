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
    /// Interação lógica para OrcamentoEventos.xam
    /// </summary>
    public partial class OrcamentoEventos : UserControl
    {
        public OrcamentoEventos()
        {
            InitializeComponent();
            CarregarOrcamentos();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow main)
            {
                main.ContentArea.Content = new Relatórios();
            }
        }

        private void CarregarOrcamentos()
        {
            using (var context = new AppDbContext())
            {
                var dados = context.Eventos
                    .Select(ev => new
                    {
                        Nome = ev.Nome,
                        OrcamentoMax = ev.OrcamentoMaximo,
                        ValorTotalFornecedores = context.FornecedorEvento
                            .Where(fe => fe.EventoId == ev.Id)
                            .Sum(fe => fe.Fornecedor.Valor),
                        Saldo = ev.OrcamentoMaximo - context.FornecedorEvento
                            .Where(fe => fe.EventoId == ev.Id)
                            .Sum(fe => fe.Fornecedor.Valor)
                    })
                    .ToList();

                dataGridOrcamento.ItemsSource = dados;
            }
        }
    }
}
