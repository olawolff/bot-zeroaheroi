using Microsoft.AspNetCore.Identity;

namespace AplicacaoWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        //Você pode adicionar novas variáveis aqui para o usuário
        public string DataNascimento { get; set; }
    }
}
