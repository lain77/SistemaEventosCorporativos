using System.Windows;

namespace SistemaEventosCorporativos.UI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow() 
        {
            InitializeComponent();
 
        }

        private void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();

            this.Close();
        }
    }
}
