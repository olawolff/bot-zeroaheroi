using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplicacaoWeb.Data;
using AplicacaoWeb.Models;

namespace AplicacaoWeb.Controllers
{
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            //Como eu coloquei a pasta Cursos (da view) dentro de outro diretório (Gestão) eu preciso
            //informar para a minha controller aonde está o novo arquivo, por isso passo a URL completa
            //dele.

            //Vale lembrar que se você não colocar a url completa dele, ele irá procurar por um Index dentro
            //da pasta /Views/Cursos/ [ele pega o nome da controller], e na pasta /Views/Shared
            return View("~/Views/Gestao/Cursos/Index.cshtml", await _context.Curso.ToListAsync());
        }

        // GET: Cursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.SingleOrDefaultAsync(m => m.Id == id);

            if (curso == null)
            {
                return NotFound();
            }

            //Aqui eu não só mudei o diretório como também coloquei um novo nome para o arquivo.
            return View("~/Views/Gestao/Cursos/Detalhes.cshtml", curso);
        }
        
        public IActionResult Create()
        {
            //Somente as views Detalhes e Index estão dentro do novo diretório
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeDoCurso,Descricao")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curso);
        }

        // GET: Cursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.SingleOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeDoCurso,Descricao")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoExists(curso.Id))
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
            return View(curso);
        }

        // GET: Cursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curso = await _context.Curso.SingleOrDefaultAsync(m => m.Id == id);
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curso = await _context.Curso.SingleOrDefaultAsync(m => m.Id == id);
            _context.Curso.Remove(curso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoExists(int id)
        {
            return _context.Curso.Any(e => e.Id == id);
        }
    }
}
