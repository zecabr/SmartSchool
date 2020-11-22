using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]

    public class AlunoController : ControllerBase {
        List<Aluno> listAlunos = new List<Aluno> () {
            new Aluno () {
            Id = 1,
            Nome = "Zeca",
            Sobrenome = "Oliveira",
            Telefone = "47-99272-8397"
            },
            new Aluno () {
            Id = 2,
            Nome = "Alice",
            Sobrenome = "Oliveira",
            Telefone = "47-99272-8397"
            },
            new Aluno () {
            Id = 3,
            Nome = "Adriana",
            Sobrenome = "Oliveira",
            Telefone = "47-99272-8397"
            }
        };

        public AlunoController () { }

        [HttpGet]
        public IActionResult Get () {
            return Ok (listAlunos);
        }

        [HttpGet ("byId/{id}")]
        public IActionResult GetById (int id) {
            var result = listAlunos.FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();

            return Ok (result);
        }

        [HttpGet ("byName")]
        public IActionResult GetByName (string name) {
            var result = listAlunos.FirstOrDefault (a => a.Nome.Contains (name));
            if (result == null) return BadRequest ();

            return Ok (result);
        }

    }
}