using SistemaEventosCorporativos.DATA;
using SistemaEventosCorporativos.Core;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;


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
                    txtCpf.Text = participante.CPF;
                    txtTelefone.Text = participante.Telefone;
                    txtTipo.Text = participante.Tipo;

                    // Carrega eventos
                    var eventos = context.ParticipanteEvento
                                         .Where(pe => pe.ParticipanteId == participanteId)
                                         .Include(pe => pe.Evento)
                                         .ToList();

                    dataGridEventos.ItemsSource = eventos;
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
                        MessageBox.Show("Participante atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        OnVoltar?.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o participante: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            OnVoltar?.Invoke();
        }
    }
}
