using SistemaEventosCorporativos.UI.UserControls;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace SistemaEventosCorporativos.UI
{
    public partial class MainWindow : Window
    {

        private DispatcherTimer _loadingTimer;

        public MainWindow()
        {
            InitializeComponent();
            StartLoadingAnimation();
            StopLoadingAnimation();
            ShowLoading();
            HideLoading();
        }

        // Método helper para trocar de tela com loading
        private async Task AbrirTelaAsync(UserControl tela)
        {
            LoadingOverlay.Visibility = Visibility.Visible;

            // Espera um instante para garantir que o overlay apareça
            await Task.Delay(100);

            // Troca de conteúdo
            ContentArea.Content = tela;

            LoadingOverlay.Visibility = Visibility.Collapsed;
        }

        private async void BtnAbrirCadastroEvento_Click(object sender, RoutedEventArgs e)
        {
            await AbrirTelaAsync(new CadastroEvento());
        }

        private async void BtnAbrirConsultaEventos_Click(object sender, RoutedEventArgs e)
        {
            var consultaEventos = new ConsultaEventos();
            await AbrirTelaAsync(consultaEventos);

            consultaEventos.OnVoltar += () => ContentArea.Content = null;
        }

        private async void BtnAbrirCadastroTipoEventos_Click(object sender, RoutedEventArgs e)
        {
            await AbrirTelaAsync(new CadastrarTipoEvento());
        }

        private async void BtnAbrirCadastroFornecedor_Click(object sender, RoutedEventArgs e)
        {
            await AbrirTelaAsync(new CadastrarFornecedor());
        }

        private async void BtnAbrirCadastroParticipante_Click(object sender, RoutedEventArgs e)
        {
            await AbrirTelaAsync(new CadastrarParticipante());
        }

        private async void BtnAbrirEditarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            var consultarFornecedor = new ConsultarFornecedor();
            await AbrirTelaAsync(consultarFornecedor);
            consultarFornecedor.OnVoltar += () => ContentArea.Content = null;
        }

        private async void BtnAbrirEditarParticipante_Click(object sender, RoutedEventArgs e)
        {
            var consultarParticipante = new ConsultarParticipante();
            await AbrirTelaAsync(consultarParticipante);
            consultarParticipante.OnVoltar += () => ContentArea.Content = null;
        }

        private async void BtnAbrirRelatorios_Click(object sender, RoutedEventArgs e)
        {
            var relatorios = new Relatórios();
            await AbrirTelaAsync(relatorios);
            relatorios.OnVoltar += () => ContentArea.Content = null;
        }

        private void StartLoadingAnimation()
        {
            LoadingOverlay.Visibility = Visibility.Visible;

            _loadingTimer = new DispatcherTimer();
            _loadingTimer.Interval = TimeSpan.FromMilliseconds(20);
            double angle = 0;

            _loadingTimer.Tick += (s, e) =>
            {
                angle += 5;
                if (angle >= 360) angle = 0;
                SR71RotateTransform.Angle = angle;
            };

            _loadingTimer.Start();
        }

        private void StopLoadingAnimation()
        {
            LoadingOverlay.Visibility = Visibility.Collapsed;
            if (_loadingTimer != null)
            {
                _loadingTimer.Stop();
                _loadingTimer = null;
            }
        }

        public void ShowLoading()
        {
            LoadingOverlay.Visibility = Visibility.Visible;

            // Fade-in
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            LoadingOverlay.BeginAnimation(OpacityProperty, fadeIn);

            // Rotação SR-71
            _loadingTimer = new DispatcherTimer();
            _loadingTimer.Interval = TimeSpan.FromMilliseconds(20);
            double angle = 0;
            _loadingTimer.Tick += (s, e) =>
            {
                angle += 5;
                if (angle >= 360) angle = 0;
                SR71RotateTransform.Angle = angle;
            };
            _loadingTimer.Start();
        }

        public void HideLoading()
        {
            if (_loadingTimer != null)
            {
                _loadingTimer.Stop();
                _loadingTimer = null;
            }

            // Fade-out
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(300));
            fadeOut.Completed += (s, e) => LoadingOverlay.Visibility = Visibility.Collapsed;
            LoadingOverlay.BeginAnimation(OpacityProperty, fadeOut);
        }
    }
}
