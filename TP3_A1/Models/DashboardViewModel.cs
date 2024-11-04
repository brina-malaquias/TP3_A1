using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP3_A1.Models
{
    public class DashboardViewModel
    {
        public List<Veiculo> Veiculos { get; set; }
        public List<Servico> Servicos { get; set; }
        public List<Despesa> Despesas { get; set; }
    }
}