
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppLayoutAspCore.Models
{
    public class EnderecoEntrega
    {
        //PK
        public int Id { get; set; }
        
        [Display(Name = "CEP", Description = "CEP.")]
        [MaxLength(10, ErrorMessage = "A senha deve ter entre 6 e 10 caracteres")]
        [Required(ErrorMessage = "O CEP é obrigatório.")]
        public string CEP { get; set; }

        [Display(Name = "Estado", Description = "Estado.")]
        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public string Estado { get; set; }

        [Display(Name = "Cidade", Description = "Cidade.")]
        [Required(ErrorMessage = "A Cidade é obrigatório.")]
        public string Cidade { get; set; }

        [Display(Name = "Bairro", Description = "Bairro.")]
        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        public string Bairro { get; set; }

        [Display(Name = "Endereco", Description = "Endereco.")]
        [Required(ErrorMessage = "O Endereco é obrigatório.")]
        public string Endereco { get; set; }

        [Display(Name = "Complemento", Description = "Complemento.")]
        [Required(ErrorMessage = "O Complemento é obrigatório.")]
        public string Complemento { get; set; }

        [Display(Name = "Número", Description = "Complemento.")]
        public string Numero { get; set; }

        public Cliente RefCliente { get; set; }

    }
}
