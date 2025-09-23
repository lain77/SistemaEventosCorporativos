using SistemaEventosCorporativos.DATA;
using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.UI.UserControls;
using System.Windows;

namespace SistemaEventosCorporativos.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAbrirCadastroEvento_Click(object sender, RoutedEventArgs e)
        {
            var cadastroEvento = new CadastroEvento(); // UserControl
            ContentArea.Content = cadastroEvento;
        }

        private void BtnAbrirConsultaEventos_Click(object sender, RoutedEventArgs e)
        {
            var consultaEventos = new ConsultaEventos();
            ContentArea.Content = consultaEventos;

            consultaEventos.OnVoltar += () =>
            {
                ContentArea.Content = null;
            };
        }

        private void BtnAbrirCadastroTipoEventos_Click(object sender, RoutedEventArgs e)
        {
            var cadastrarTipoEvento = new CadastrarTipoEvento();
            ContentArea.Content = cadastrarTipoEvento;
        }

        private void BtnAbrirCadastroFornecedor_Click(object sender, RoutedEventArgs e)
        {
            var cadastrarFornecedor = new CadastrarFornecedor();
            ContentArea.Content = cadastrarFornecedor;
        }

        private void BtnAbrirCadastroParticipante_Click(object sender, RoutedEventArgs e)
        {
            var cadastrarParticipante = new CadastrarParticipante();
            ContentArea.Content = cadastrarParticipante;
        }

        private void BtnAbrirEditarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            var consultarFornecedor = new ConsultarFornecedor();
            ContentArea.Content = consultarFornecedor;

            consultarFornecedor.OnVoltar += () =>
            {
                ContentArea.Content = null;
            };
        }

        private void BtnAbrirEditarParticipante_Click(object sender, RoutedEventArgs e)
        {
            var consultarParticipante = new ConsultarParticipante();
            ContentArea.Content = consultarParticipante;

            consultarParticipante.OnVoltar += () =>
            {
                ContentArea.Content = null;
            };
        }

        private void BtnAbrirRelatorios_Click(object sender, RoutedEventArgs e)
        {
            var relatorios = new Relatórios();
            ContentArea.Content = relatorios;

            relatorios.OnVoltar += () =>
            {
                ContentArea.Content = null;
            };
        }
    }
}
