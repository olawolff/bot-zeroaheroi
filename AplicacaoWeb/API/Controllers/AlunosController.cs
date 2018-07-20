using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Aluno> Index()
        {
            var aluno = _context.Aluno.ToList();
            return aluno;
        }

        [HttpGet]
        public Aluno ObterCnae([FromBody]int id)
        {
            var aluno = _context.Aluno.SingleOrDefault(m => m.Id == id);
            return aluno;
        }

        [HttpPost]
        public bool Create([FromBody]Aluno aluno)
        {
            try
            {
                _context.Add(aluno);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Edit([FromBody]Aluno aluno)
        {
            try
            {
                _context.Update(aluno);
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
                var aluno = _context.Aluno.SingleOrDefault(m => m.Id == id);
                _context.Aluno.Remove(aluno);
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
