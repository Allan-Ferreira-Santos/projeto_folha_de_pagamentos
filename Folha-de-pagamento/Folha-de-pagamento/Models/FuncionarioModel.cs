using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Folha_de_pagamento.Models
{
    public class FuncionarioModel
    {
        [Display(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FuncionarioId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "The Name field is required.")]
        public string Name { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "The CPF field is required.")]
        public string CPF { get; set; }
    }
}
