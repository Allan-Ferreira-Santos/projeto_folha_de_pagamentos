using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Folha_de_pagamento.Models
{
    public class FolhaModel
    {
        [Display(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FolhaId { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "The Value field is required.")]
        public int Valor { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "The quantity field is required.")]
        public int Quantidade { get; set; }

        [Display(Name = "Mês")]
        [Required(ErrorMessage = "The month field is required.")]
        public int Mes { get; set; }

        [Display(Name = "Ano")]
        [Required(ErrorMessage = "The Year field is required.")]
        public int Ano { get; set; }

        [ForeignKey("Funcionario")]
        [Required(ErrorMessage = "The Employee field is required.")]
        public int FuncionarioId { get; set; }

        public double CalcularSalarioBruto()
        {
            return Valor * Quantidade;
        }

        public double CalcularImpostoIrrf()
        {
            double salarioBruto = CalcularSalarioBruto();

            if (salarioBruto <= 1903.98)
            {
                return 0.0;
            }
            else if (salarioBruto <= 2826.65)
            {  
                return (salarioBruto - 1903.98) * 0.075 + 142.80;
            }
            else if (salarioBruto <= 3751.05)
            {
                return (salarioBruto - 2826.65) * 0.15 + 354.80;
            }
            else if (salarioBruto <= 4664.68)
            { 
                return (salarioBruto - 3751.05) * 0.225 + 636.13;
            }
            else
            {
                return (salarioBruto - 4664.68) * 0.275 + 869.36;
            }
        }

        public double CalcularImpostoInss()
        {
            double salarioBruto = CalcularSalarioBruto();

            if (salarioBruto <= 1693.72)
            {
                return salarioBruto * 0.08;
            }
            else if (salarioBruto <= 2822.90)
            {
                return salarioBruto * 0.09;
            }
            else if (salarioBruto <= 5645.80)
            {
                return salarioBruto * 0.11;
            }
            else
            {
                return 621.03;
            }
        }

        public double CalcularImpostoFgts()
        {
            double salarioBruto = CalcularSalarioBruto();
            return salarioBruto * 0.08;
        }

        public double CalcularSalarioLiquido()
        {
            double salarioBruto = CalcularSalarioBruto();
            double impostoIrrf = CalcularImpostoIrrf();
            double impostoInss = CalcularImpostoInss();
            return salarioBruto - impostoIrrf - impostoInss;
        }
    }


}
