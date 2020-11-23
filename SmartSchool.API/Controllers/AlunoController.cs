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

        private readonly SmartContext _context;
        public AlunoController (SmartContext context) {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get () {
            return Ok (_context.Alunos);
        }

        [HttpGet ("byId/{id}")]
        public IActionResult GetById (int id) {
            var result = _context.Alunos.AsNoTracking().FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();

            return Ok (result);
        }

        [HttpGet ("byName")]
        public IActionResult GetByName (string name) {
            var result = _context.Alunos.AsNoTracking().FirstOrDefault (a => a.Nome.Contains (name));
            if (result == null) return BadRequest ();

            return Ok (result);
        }

        [HttpPost]
        public IActionResult Post (Aluno aluno) {
            _context.Add (aluno);
            _context.SaveChanges ();
            return Ok (aluno);
        }

        [HttpPut]
        public IActionResult Put (int id, Aluno aluno) {
            var result = _context.Alunos.AsNoTracking().FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();
            _context.Update (aluno);
            _context.SaveChanges ();
            return Ok (aluno);
        }

        [HttpPatch ("{id}")]
        public IActionResult Patch (int id, Aluno aluno) {
            var result = _context.Alunos.AsNoTracking().FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();
            _context.Update (aluno);
            _context.SaveChanges ();
            return Ok (aluno);
        }

        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {

            var result = _context.Alunos.AsNoTracking().FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();
            _context.Remove (result);
            _context.SaveChanges ();
            return Ok (result);
        }

    }
}