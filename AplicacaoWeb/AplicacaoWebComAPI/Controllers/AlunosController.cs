using AplicacaoWebComAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoWebComAPI.Controllers
{
    public class AlunosController : Controller
    {
        public AlunosController()
        {
        }

        public IActionResult Index()
        {
            IList<Aluno> alunos;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:63634/api/alunos/index").Result;
                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;
                    alunos = JsonConvert.DeserializeObject<IList<Aluno>>(JsonString);
                }
                else
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }

            return View(alunos);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aluno aluno = new Aluno();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:63634/api/alunos/ObterAluno").Result;
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:63634/api/alunos/ObterAluno"),
                    Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
                };

                response = client.SendAsync(httpRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;
                    aluno = JsonConvert.DeserializeObject<Aluno>(JsonString);
                }
                else
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Nome,Email")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    try
                    {
                        var content = new StringContent(JsonConvert.SerializeObject(aluno), Encoding.UTF8, "application/json");
                        response = client.PostAsync("http://localhost:63634/api/alunos/create", content).Result;
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                    }
                }
            }
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aluno aluno = new Aluno();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:63634/api/alunos/ObterAluno").Result;
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:63634/api/alunos/ObterAluno"),
                    Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
                };

                response = client.SendAsync(httpRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;
                    aluno = JsonConvert.DeserializeObject<Aluno>(JsonString);
                }
                else
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Email")] Aluno aluno)
        {
            if (id != aluno.Id)
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
                        RequestUri = new Uri("http://localhost:63634/api/alunos/Edit"),
                        Content = new StringContent(JsonConvert.SerializeObject(aluno), Encoding.UTF8, "application/json")
                    };

                    response = client.SendAsync(httpRequest).Result;

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    else
                        return View(aluno);
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

            Aluno aluno = new Aluno();
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:63634/api/alunos/ObterAluno").Result;
                var httpRequest = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:63634/api/alunos/ObterAluno"),
                    Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json")
                };

                response = client.SendAsync(httpRequest).Result;

                if (response.IsSuccessStatusCode)
                {
                    var JsonString = response.Content.ReadAsStringAsync().Result;
                    aluno = JsonConvert.DeserializeObject<Aluno>(JsonString);
                }
                else
                {
                    throw new Exception("Falha na comunicação da API: " + response.StatusCode);
                }
            }
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
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    var httpRequest = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri("http://localhost:63634/api/alunos/delete/" + id),
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
