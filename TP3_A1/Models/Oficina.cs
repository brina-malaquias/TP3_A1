using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3_A1.Models
{
    public class Oficina
    {
        public int OficinaId { get; set; }
        public string Nome { get; set; }
        public string Responsavel { get; set; }
        public string Telefone { get; set; }

        // Relacionamento com Serviço
        public ICollection<Servico> Servicos { get; set; }
    }

}