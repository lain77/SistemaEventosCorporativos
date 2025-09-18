using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SistemaEventosCorporativos.UI.UserControls
{
    /// <summary>
    /// Interação lógica para CadastrarTipoEvento.xaml
    /// </summary>
    public partial class CadastrarTipoEvento : UserControl
    {
        public event Action? OnVoltar; // evento para voltar quando salvar ou cancelar

        public CadastrarTipoEvento()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    TipoEvento tipo = new TipoEvento
                    {
                        Descricao = txtDescricao.Text
                    };

                    context.TiposEventos.Add(tipo);
                    context.SaveChanges();
                }

                MessageBox.Show("Cadastrado com sucesso");

                // dispara evento de voltar para a tela anterior
                OnVoltar?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            OnVoltar?.Invoke();
        }
    }
}
