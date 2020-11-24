using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using SmartSchool.API.V1.Dtos;

namespace SmartSchool.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProfessorController : ControllerBase {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public ProfessorController (IRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get () {
            var result = _repo.GetallProfessores (true);

            return Ok (_mapper.Map<IEnumerable<ProfessorDto>> (result));
        }

        [HttpGet ("{id}")]
        public IActionResult GetById (int id) {
            var result = _repo.GetProfessorById (id);
            if (result == null) return BadRequest ();

            var professorDto = _mapper.Map<ProfessorDto> (result);

            return Ok (professorDto);
        }

        [HttpPost]
        public IActionResult Post (ProfessorRegistrarDto model) {

            var professor = _mapper.Map<Professor> (model);
            _repo.Add (professor);

            if (_repo.SaveChanges ()) {
                return Created ($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto> (professor));
            }
            return BadRequest ();
        }

        [HttpPut]
        public IActionResult Put (int id, ProfessorRegistrarDto model) {
            var professor = _repo.GetProfessorById (id);
            if (professor == null) return BadRequest ();

            _mapper.Map (model, professor);
            _repo.Update (professor);
            if (_repo.SaveChanges ()) {
                return Created ($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto> (professor));
            }
            return BadRequest ();
        }

        [HttpPatch ("{id}")]
        public IActionResult Patch (int id, ProfessorRegistrarDto model) {
            var professor = _repo.GetProfessorById (id);
            if (professor == null) return BadRequest ();

            _mapper.Map (model, professor);
            _repo.Update (professor);
            if (_repo.SaveChanges ()) {
                return Created ($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto> (professor));
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