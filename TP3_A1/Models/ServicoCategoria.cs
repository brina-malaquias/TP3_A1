using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3_A1.Models
{ 
    public class ServicoCategoria
    {
        public int ServicoCategoriaId { get; set; } // Chave primária
        public int ServicoId { get; set; } // Chave estrangeira para Servico
        public int CategoriaServicoId { get; set; } // Chave estrangeira para CategoriaServico

        public virtual Servico Servico { get; set; }
        public virtual CategoriaServico CategoriaServico { get; set; }
    }


}