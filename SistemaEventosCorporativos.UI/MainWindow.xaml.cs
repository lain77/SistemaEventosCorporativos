using SistemaEventosCorporativos.DATA;
using System.Windows;
using SistemaEventosCorporativos.Core;

namespace SistemaEventosCorporativos.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAbrirCadastroEvento_Click(object sender, RoutedEventArgs e)
        {
            CadastroEvento janela = new CadastroEvento();
            janela.ShowDialog();

        }
        private void BtnAbrirConsultaEventos_Click(object sender, RoutedEventArgs e)
        {
           ConsultaEventos janela = new ConsultaEventos();
           janela.ShowDialog();
        }
        private void BtnAbrirCadastroTipoEventos_Click(object sender, RoutedEventArgs e)
        {
            var cadastrarTipoEvento = new UserControls.CadastrarTipoEvento();
            ContentArea.Content = cadastrarTipoEvento;
        }

    }
}