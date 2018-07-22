using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CursosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CursosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Curso> Index()
        {
            var curso = _context.Curso.ToList();
            return curso;
        }

        [HttpGet]
        public Curso ObterCurso([FromBody]int id)
        {
            var curso = _context.Curso.SingleOrDefault(m => m.Id == id);
            return curso;
        }

        [HttpPost]
        public bool Create([FromBody]Curso curso)
        {
            try
            {
                _context.Add(curso);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Edit([FromBody]Curso curso)
        {
            try
            {
                _context.Update(curso);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpDelete("{id}")]
        public bool Delete([FromBody]int id)
        {
            try
            {
                var curso = _context.Curso.SingleOrDefault(m => m.Id == id);
                _context.Curso.Remove(curso);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
