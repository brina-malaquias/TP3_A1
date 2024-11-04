using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3_A1.Models
{
    public class TipoDespesa
    {
        public int TipoDespesaId { get; set; }
        public string Nome { get; set; }

        // Relacionamento com Despesa
        public ICollection<Despesa> Despesas { get; set; }
    }

}