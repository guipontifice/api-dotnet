using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDB.Models
{
    public class Personagem
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é um campo obrigatório")]
        [MaxLength(50, ErrorMessage = "Nome precisa ter no máximo 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Tipo é um campo obrigatório")]
        [MaxLength(50, ErrorMessage = "Tipo precisa ter no máximo 50 caracteres")] 
        public string Tipo { get; set; }


    }
}