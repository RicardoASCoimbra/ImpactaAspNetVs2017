using System.ComponentModel.DataAnnotations;

namespace AspNetVS2017.Capitulo03.Mvc.Portfolio.Models
{
    public class ContatoViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        //[RegularExpression()]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Mensagem { get; set; }
    }
}