using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacaoWeb.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Curso")]
        [MinLength(5, ErrorMessage = "Este campo deve ter no mínimo 5 caracteres.")]
        public string NomeDoCurso { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
