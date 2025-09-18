using SistemaEventosCorporativos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventosCorporativos.Core
{
    public class Evento
    {
        public int Id {  get; set; } 
        public string Nome {  get; set; }
        public DateOnly DataInicio { get; set; }   
        public DateOnly DataFim { get; set; }
        public string Local {  get; set; }
        public string Observacoes { get; set; }
        public int LotacaoMaxima { get; set; }
        public decimal OrcamentoMaximo { get; set; }

        //Relacionamento com TipoEvento (1 evento tem 1 tipo)
        public int TipoEventoId { get; set; }
        public TipoEvento TipoEvento { get; set; }

        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; }

    }
}
