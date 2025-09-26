using SistemaEventosCorporativos.UI.LoginUserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace SistemaEventosCorporativos.UI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Sr71_Click(object sender, MouseButtonEventArgs e)
        {
            ContentArea.Content = new LoginUserControl();

            if (sender is Border border && border.Effect is DropShadowEffect shadow)
            {
                shadow.BlurRadius = 50; 
                shadow.Opacity = 0.7;
            }
        }
    }
}
