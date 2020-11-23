using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProfessorController : ControllerBase {

        private readonly SmartContext _context;
        public ProfessorController (SmartContext context) {
            _context = context;

        }

        [HttpGet]
        public IActionResult Get () {
            return Ok (_context.Professors);
        }

        [HttpGet ("byId/{id}")]
        public IActionResult GetById (int id) {
            var result = _context.Professors.AsNoTracking ().FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();

            return Ok (result);
        }

        [HttpGet ("byName")]
        public IActionResult GetByName (string name) {
            var result = _context.Professors.AsNoTracking ().FirstOrDefault (a => a.Nome.Contains (name));
            if (result == null) return BadRequest ();

            return Ok (result);
        }

        [HttpPost]
        public IActionResult Post (Professor Professor) {
            _context.Add (Professor);
            _context.SaveChanges ();
            return Ok (Professor);
        }

        [HttpPut]
        public IActionResult Put (int id, Professor Professor) {
            var result = _context.Professors.AsNoTracking ().FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();
            _context.Update (Professor);
            _context.SaveChanges ();
            return Ok (Professor);
        }

        [HttpPatch ("{id}")]
        public IActionResult Patch (int id, Professor Professor) {
            var result = _context.Professors.AsNoTracking ().FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();
            _context.Update (Professor);
            _context.SaveChanges ();
            return Ok (Professor);
        }

        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {

            var result = _context.Professors.AsNoTracking ().FirstOrDefault (a => a.Id == id);
            if (result == null) return BadRequest ();
            _context.Remove (result);
            _context.SaveChanges ();
            return Ok (result);
        }
    }
}