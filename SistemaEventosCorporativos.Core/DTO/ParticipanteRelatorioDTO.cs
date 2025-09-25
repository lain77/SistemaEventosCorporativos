using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventosCorporativos.CORE.DTO
{
    public class ParticipanteRelatorioDTO
    {
        public string NomeParticipante { get; set; }
        public int TotalEventos { get; set; }
        public decimal ValorTotal { get; set; }

    }
}
