using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProfessorController : ControllerBase {
        private readonly IRepository _repo;
        public ProfessorController (IRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get () {
            var result = _repo.GetallProfessores (true);

            return Ok (result);
        }

        [HttpGet ("{id}")]
        public IActionResult GetById (int id) {
            var result = _repo.GetProfessorById (id);
            if (result == null) return BadRequest ();

            return Ok (result);
        }

        [HttpPost]
        public IActionResult Post (Professor professor) {
            _repo.Add (professor);

            if (_repo.SaveChanges ()) {
                return Ok (professor);
            }
            return BadRequest ();
        }

        [HttpPut]
        public IActionResult Put (int id, Professor professor) {
            var result = _repo.GetProfessorById (id);

            if (result == null) return BadRequest ();

            _repo.Update (professor);
            if (_repo.SaveChanges ()) {
                return Ok (professor);
            }
            return BadRequest ();
        }

        [HttpPatch ("{id}")]
        public IActionResult Patch (int id, Professor professor) {
            var result = _repo.GetProfessorById (id);
            if (result == null) return BadRequest ();

            _repo.Update (professor);
            if (_repo.SaveChanges ()) {
                return Ok (professor);
            }
            return BadRequest ();
        }

        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {

            var result = _repo.GetProfessorById (id);
            if (result == null) return BadRequest ();

            _repo.Delete (result);
            if (_repo.SaveChanges ()) {
                return Ok ("Deletado");
            }
            return BadRequest ();
        }
    }
}