using SistemaEventosCorporativos.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventosCorporativos.Core
{



    public class Participante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Tipo { get; set; }
        public ICollection<ParticipanteEvento> Eventos { get; set; } = new List<ParticipanteEvento>();

    }
}
