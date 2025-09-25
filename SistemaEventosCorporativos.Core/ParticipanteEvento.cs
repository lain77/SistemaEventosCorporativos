using SistemaEventosCorporativos.Core;
using SistemaEventosCorporativos.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventosCorporativos.CORE
{
    public class ParticipanteEvento
    {

        public int Id { get; set; }

        public int EventoId { get; set; }

        public int ParticipanteId { get; set; }

        public Evento Evento { get; set; }

        public Participante Participante { get; set; }
    }
}
