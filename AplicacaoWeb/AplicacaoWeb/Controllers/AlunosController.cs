using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplicacaoWeb.Data;
using AplicacaoWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace AplicacaoWeb.Controllers
{
    //Usando o Authorize somente usuários logados podem acessar essa página. 
    //Você também pode utilizar ele somente para determinadas Actions nas controllers.
    [Authorize]
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            //Aqui ele busca o ApplicationDbContext com todos os dados e métodos referentes a conexão com o
            //banco e adiciona na variável _context
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            //Ele esta retornando a View com o nome Index (nome do método), e passando como parâmetro
            //o todos os alunos cadastrados na base de dados.
            return View(await _context.Aluno.ToListAsync());
        }

        // GET: Alunos/Details/5
        //Aqui ele irá chamar os detalhes do aluno que possuir o ID passado (que virá pela a URL)
        public async Task<IActionResult> Details(int? id)
        {
            //se não aparecer nenhum id, ele simplesmente irá colocar um erro 404 (not found)
            if (id == null)
            {
                return NotFound();
            }

            //aqui ele irá buscar pelo aluno com esse id, essa é uma Query do LINQ e você consegue fazer
            //bastente coisa aqui, como colocar um .Where() .StartsWith() .EndWith()
            var aluno = await _context.Aluno.SingleOrDefaultAsync(m => m.Id == id);

            //se ele não encontrou nenhum aluno com esse id, então ele irá retornar nulo
            if (aluno == null)
            {
                return NotFound();
            }

            //caso contrário, ele irá retornar a view Details com o parametro aluno.
            return View(aluno);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.SingleOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.SingleOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.SingleOrDefaultAsync(m => m.Id == id);
            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.Id == id);
        }
    }
}
