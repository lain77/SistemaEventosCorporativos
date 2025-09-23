using System;
using System.Windows;
using System.Windows.Controls;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class Relatórios : UserControl
    {
        public event Action? OnVoltar;

        public Relatórios()
        {
            InitializeComponent();
        }

        private void BtnAgendaParticipantes_Click(object sender, RoutedEventArgs e)
        {
            var uc = new AgendaParticipantes();
            AbrirUC(uc);
        }

        private void BtnFornecedoresMaisUtilizados_Click(object sender, RoutedEventArgs e)
        {
            var uc = new FornecedoresMaisUtilizados();
            AbrirUC(uc);
        }

        private void BtnTiposParticipantes_Click(object sender, RoutedEventArgs e)
        {
            var uc = new TipoParticipante();
            AbrirUC(uc);
        }

        private void BtnOrcamentoEventos_Click(object sender, RoutedEventArgs e)
        {
            var uc = new OrcamentoEventos();
            AbrirUC(uc);
        }

        private void AbrirUC(UserControl uc)
        {
            if (Application.Current.MainWindow is MainWindow main)
            {
                main.ContentArea.Content = uc;
            }
        }
    }
}
