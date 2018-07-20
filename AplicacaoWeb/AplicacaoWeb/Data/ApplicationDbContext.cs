using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AplicacaoWeb.Models;

namespace AplicacaoWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Após adicionar novas classes, ou alterar alguma propriedade, você precisa adicionar um Migration
        //Para isso use o Add-Migration [Nome da migration] no console de gerenciador de pacotes

        //Uma vez que migration tenha sido criada, você pode utilizar o Update-Database para atualizar
        //a sua base de dados
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Aluno> Aluno { get; set; }

        public DbSet<Curso> Curso { get; set; }
    }
}
