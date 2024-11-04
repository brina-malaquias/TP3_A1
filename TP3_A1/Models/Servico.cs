using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP3_A1.Models
{
    public class Servico
    {
        public int ServicoId { get; set; }
        [CustomValidation(typeof(Servico), nameof(ValidarData))]
        public DateTime Data { get; set; }
        public StatusServico Status { get; set; }
        public string Observacoes { get; set; }

        // Relacionamento com Veículo
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        // Relacionamento muitos para muitos com Categoria de Serviço
        public virtual ICollection<ServicoCategoria> ServicoCategorias { get; set; }


        // Relacionamento com Oficina
        public int OficinaId { get; set; }
        public Oficina Oficina { get; set; }

        // Método de validação da data
        public static ValidationResult ValidarData(DateTime data, ValidationContext context)
        {
            if (data > DateTime.Now)
            {
                return new ValidationResult("A data do serviço não pode ser futura.");
            }
            return ValidationResult.Success;
        }
    }

}