using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.CORE;
using SistemaEventosCorporativos.DATA;
using System;
using System.Linq;
using System.Security.Cryptography;
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
                    int eventoId = (int)cbEvento.SelectedValue;

                    var evento = context.Eventos.FirstOrDefault(ev => ev.Id == eventoId);
                    if (evento == null)
                    {
                        MessageBox.Show("Evento não encontrado.");
                        return;
                    }

                    int participantesCadastrados = context.ParticipanteEvento
                        .Count(pe => pe.EventoId == eventoId);

                    if (participantesCadastrados >= evento.LotacaoMaxima)
                    {
                        MessageBox.Show("Lotação máxima atingida. Não é possível cadastrar mais participantes.");
                        return;
                    }

                    string cpfDigitado = (txtCpf.Text ?? "")
                        .Replace(".", "")
                        .Replace("-", "")
                        .Replace(" ", "");

                    var participante = context.Participantes
                        .FirstOrDefault(p => p.CPF.Replace(".", "").Replace("-", "").Replace(" ", "") == cpfDigitado);

                    if (participante == null)
                    {
                        participante = new Participante
                        {
                            Nome = txtNome.Text,
                            CPF = cpfDigitado,
                            Telefone = txtTelefone.Text,
                            Tipo = txtTipo.Text
                        };
                        context.Participantes.Add(participante);
                        context.SaveChanges();
                    }

                    bool conflitoData = context.ParticipanteEvento
                        .Where(pe => pe.ParticipanteId == participante.Id && pe.EventoId != eventoId)
                        .Any(pe => pe.Evento.DataInicio <= evento.DataFim &&
                                   pe.Evento.DataFim >= evento.DataInicio);

                    if (conflitoData)
                    {
                        MessageBox.Show("Este participante já está inscrito em outro evento que ocorre nas mesmas datas.");
                        return;
                    }

                    var participanteEvento = new ParticipanteEvento
                    {
                        ParticipanteId = participante.Id,
                        EventoId = eventoId
                    };

                    context.ParticipanteEvento.Add(participanteEvento);
                    context.SaveChanges();

                    MessageBox.Show("Participante cadastrado no evento com sucesso!");
                    OnVoltar?.Invoke();
                }
            }
            catch (Exception ex)
            {
                string mensagemErro = ex.Message;
                if (ex.InnerException != null)
                    mensagemErro += "\nInnerException: " + ex.InnerException.Message;

                MessageBox.Show("Erro ao cadastrar participante: " + mensagemErro);
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
