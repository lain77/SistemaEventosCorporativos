using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaEventosCorporativos.Core
{
    public class Fornecedor
    {
        public int Id { get; set; }
        public string NomeServico { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
    }
}
