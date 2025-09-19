using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.DATA;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SistemaEventosCorporativos.UI.UserControls
{
    public partial class CadastrarParticipante : UserControl
    {
        public event Action? OnVoltar;

        public CadastrarParticipante()
        {
            InitializeComponent();
            CarregarEventos();

        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    Participante participante = new Participante
                    {
                        Nome = txtNome.Text,
                        CPF = txtCpf.Text,
                        Telefone = txtTelefone.Text,
                        Tipo = txtTipo.Text,
                        EventoId = (int)cbEvento.SelectedValue
                    };

                    context.Participantes.Add(participante);
                    context.SaveChanges();
                }

                MessageBox.Show("Participante cadastrado.");
                OnVoltar?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar participante: " + ex.Message);
            }
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            OnVoltar?.Invoke();
        }

        private void CarregarEventos()
        {
            using (var context = new AppDbContext())
            {
                var eventos = context.Eventos.ToList();
                cbEvento.ItemsSource = eventos;
                cbEvento.DisplayMemberPath = "Nome";
                cbEvento.SelectedValuePath = "Id";
            }
        }


    }
}
