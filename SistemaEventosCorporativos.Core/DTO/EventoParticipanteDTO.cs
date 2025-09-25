using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventosCorporativos.CORE.DTO
{
    public class EventoParticipanteDTO
    {
        public string NomeEvento {  get; set; }

        public DateOnly DataInicio { get; set; }

        public DateOnly DataFim { get; set; }
    }
}
