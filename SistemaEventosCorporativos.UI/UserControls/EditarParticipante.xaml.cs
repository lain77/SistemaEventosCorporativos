using SistemaEventosCorporativos.DATA;
using SistemaEventosCorporativos.Core;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class EditarParticipante : UserControl
    {
        private int participanteId;

        public event Action? OnVoltar;

        public EditarParticipante(int participanteId)
        {
            InitializeComponent();
            this.participanteId = participanteId;
            CarregarDados();
        }

        private void CarregarDados()
        {
            using (var context = new AppDbContext())
            {
                var participante = context.Participantes
                                          .FirstOrDefault(p => p.Id == participanteId);

                if (participante != null)
                {
                    txtNome.Text = participante.Nome;
                    txtNome.Foreground = Brushes.Black;

                    txtCpf.Text = participante.CPF;
                    txtCpf.Foreground = Brushes.Black;

                    txtTelefone.Text = participante.Telefone;
                    txtTelefone.Foreground = Brushes.Black;

                    txtTipo.Text = participante.Tipo;
                    txtTipo.Foreground = Brushes.Black;
                }
            }
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var participante = context.Participantes
                                              .FirstOrDefault(p => p.Id == participanteId);

                    if (participante != null)
                    {
                        participante.Nome = txtNome.Text;
                        participante.CPF = txtCpf.Text;
                        participante.Telefone = txtTelefone.Text;
                        participante.Tipo = txtTipo.Text;

                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Participante atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                OnVoltar?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o participante: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Placeholders simulados
        private void TxtNome_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text == "Nome do Participante")
            {
                txtNome.Text = "";
                txtNome.Foreground = Brushes.Black;
            }
        }

        private void TxtNome_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                txtNome.Text = "Nome do Participante";
                txtNome.Foreground = Brushes.Gray;
            }
        }
        #endregion

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            OnVoltar?.Invoke();
        }
    }
}
