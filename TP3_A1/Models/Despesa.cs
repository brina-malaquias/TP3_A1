using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP3_A1.Models
{
    public class Despesa
    {
        public int DespesaId { get; set; }
        [Required(ErrorMessage = "O valor da despesa é obrigatório.")]
        [CustomValidation(typeof(Despesa), nameof(ValidarValor))]
        public decimal Valor { get; set; }
        [CustomValidation(typeof(Despesa), nameof(ValidarData))]
        public DateTime Data { get; set; }
        public string Observacoes { get; set; }

        // Relacionamento com Veículo
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        // Relacionamento com Tipo de Despesa
        public int TipoDespesaId { get; set; }
        public TipoDespesa TipoDespesa { get; set; }

        // Método de validação personalizada para o valor
        public static ValidationResult ValidarValor(decimal valor, ValidationContext context)
        {
            if (valor <= 0)
            {
                return new ValidationResult("O valor da despesa deve ser maior que zero.");
            }
            return ValidationResult.Success;
        }

        // Método de validação da data
        public static ValidationResult ValidarData(DateTime data, ValidationContext context)
        {
            if (data > DateTime.Now)
            {
                return new ValidationResult("A data da despesa não pode ser futura.");
            }
            return ValidationResult.Success;
        }
    }

}