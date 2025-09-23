using SistemaEventosCorporativos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventosCorporativos.CORE
{
    public class FornecedorEvento
    {

        public int Id { get; set; }

        public int EventoId { get; set; }

        public int FornecedorId { get; set; }

        public Evento Evento { get; set; }

        public Fornecedor Fornecedor { get; set; }

    }
}
