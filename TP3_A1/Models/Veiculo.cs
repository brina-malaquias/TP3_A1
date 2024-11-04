using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TP3_A1.Models
{
    public class Veiculo
    {
        public int VeiculoId { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quilometragem deve ser um valor positivo.")]
        public int Quilometragem { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$", ErrorMessage = "Formato da placa inválido.")]
        public string Placa { get; set; }
        public string Observacoes { get; set; }

        // Relacionamento com Usuário
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Relacionamento com Serviço e Despesa
        public ICollection<Servico> Servicos { get; set; }
        public ICollection<Despesa> Despesas { get; set; }

        public bool IsUniquePlate(string plate, ApplicationDbContext db)
        {
            return !db.Veiculoes.Any(v => v.Placa == plate && v.UserId == this.UserId);
        }
        public bool IsKilometragemIncreasing(int newQuilometragem, int oldQuilometragem)
        {
            return newQuilometragem >= oldQuilometragem; // Verifica se a nova quilometragem é maior ou igual à antiga
        }

    }

}