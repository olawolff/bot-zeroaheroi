using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacaoWeb.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Curso Curso { get; set; }
    }
}
