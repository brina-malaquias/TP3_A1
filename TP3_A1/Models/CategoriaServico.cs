using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3_A1.Models
{
    public class CategoriaServico
    {
        public int CategoriaServicoId { get; set; } // Chave primária
        public string Nome { get; set; } // Nome da categoria

        public virtual ICollection<ServicoCategoria> ServicoCategorias { get; set; }
    }

}