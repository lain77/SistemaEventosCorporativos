using SistemaEventosCorporativos.CORE;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
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

        [Column("Cnpj")]
        public string CNPJ { get; set; }

        public string Tipo { get; set; }
        public ICollection<FornecedorEvento> Eventos { get; set; } = new List<FornecedorEvento>();

    }
}
