using AplicacaoWebComAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AplicacaoWebComAPI.Controllers
{
    public class CursosController : Controller
    {

        public CursosController()
        {
        }

        public IActionResult Index()
        {
            IList<Curso> cursos;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:63634/api/cursos/index").Result;
                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;
                    cursos = JsonConvert.DeserializeObject<IList<Curso>>(JsonString);
                }
                else
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }

            return View(cursos);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Curso curso = new Curso();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:63634/api/cursos/ObterCurso").Result;
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:63634/api/cursos/ObterCurso"),
                    Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
                };

                response = client.SendAsync(httpRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;
                    curso = JsonConvert.DeserializeObject<Curso>(JsonString);
                }
                else
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }

            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,NomeDoCurso,Descricao")] Curso curso)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        var content = new StringContent(JsonConvert.SerializeObject(curso), Encoding.UTF8, "application/json");
                        response = client.PostAsync("http://localhost:63634/api/cursos/create", content).Result;
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                    }
                }
            }
            return View(curso);
        }

        // GET: Alunos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Curso curso = new Curso();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:63634/api/cursos/ObterCurso").Result;
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:63634/api/cursos/ObterCurso"),
                    Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
                };

                response = client.SendAsync(httpRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;
                    curso = JsonConvert.DeserializeObject<Curso>(JsonString);
                }
                else
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }
            if (curso == null)
            {
                return NotFound();
            }
            return View(curso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,NomeDoCurso,Descricao")] Curso curso)
        {
            if (id != curso.Id)
            {
                return NotFound();
            }

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                using (var client = new HttpClient())
                {
                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri("http://localhost:63634/api/cursos/Edit"),
                        Content = new StringContent(JsonConvert.SerializeObject(curso), Encoding.UTF8, "application/json")
                    };

                    response = client.SendAsync(httpRequest).Result;

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                        return View(curso);
                }
            }
            catch (Exception)
            {
                throw new Exception("Falha na comunicação da API: " + response.StatusCode);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Curso curso = new Curso();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:63634/api/cursos/ObterCurso").Result;
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:63634/api/cursos/ObterCurso"),
                    Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
                };

                response = client.SendAsync(httpRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;
                    curso = JsonConvert.DeserializeObject<Curso>(JsonString);
                }
                else
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }
            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri("http://localhost:63634/api/cursos/delete/" + id),
                        Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
                    };
                    response = client.SendAsync(httpRequest).Result;
                }
                catch (Exception)
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}