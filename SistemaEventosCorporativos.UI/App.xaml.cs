using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using System.Linq;
using System.Windows;

namespace SistemaEventosCorporativos.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Cria a janela de login
            var login = new LoginWindow();

            // Define que essa será a MainWindow temporária do app
            this.MainWindow = login;

            // Mostra a janela
            login.Show();
        }
    }
}
