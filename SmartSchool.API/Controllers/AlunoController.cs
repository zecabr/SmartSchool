using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]

    public class AlunoController : ControllerBase {

        private readonly IRepository _repo;
        public AlunoController (IRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get () {
            var result = _repo.GetallAlunos (true);
            return Ok (result);
        }

        [HttpGet ("{id}")]
        public IActionResult GetById (int id) {
            var result = _repo.GetAlunoById (id);
            if (result == null) return BadRequest ();

            return Ok (result);
        }

        [HttpPost]
        public IActionResult Post (Aluno aluno) {
            _repo.Add (aluno);
            if (_repo.SaveChanges ()) {
                return Ok (aluno);
            }
            return BadRequest ();
        }

        [HttpPut]
        public IActionResult Put (int id, Aluno aluno) {
            var result = _repo.GetAlunoById (id);
            if (result == null) return BadRequest ();

            _repo.Update (aluno);
            if (_repo.SaveChanges ()) {
                return Ok (aluno);
            }
            return BadRequest ();
        }

        [HttpPatch ("{id}")]
        public IActionResult Patch (int id, Aluno aluno) {
            var result = _repo.GetAlunoById (id);
            if (result == null) return BadRequest ();

            _repo.Update (aluno);
            if (_repo.SaveChanges ()) {
                return Ok (aluno);
            }
            return BadRequest ();
        }

        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {

            var result = _repo.GetAlunoById (id);
            if (result == null) return BadRequest ();

            _repo.Delete (result);
            if (_repo.SaveChanges ()) {
                return Ok ("Deletado");
            }
            return BadRequest ();

        }
    }
}