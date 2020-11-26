using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using SmartSchool.API.V1.Dtos;

namespace SmartSchool.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]

    public class AlunoController : ControllerBase {

        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public AlunoController (IRepository repo, IMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }

       /// <summary>
        /// Método responsável para retornar todos os meus alunos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            var alunos = await _repo.GetAllAlunosAsync(pageParams, true);

            var alunosResult = _mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosResult);
        }

        [HttpGet ("{id}")]
        public IActionResult GetById (int id) {
            var result = _repo.GetAlunoById (id);
            if (result == null) return BadRequest ();

            var alunoDto = _mapper.Map<AlunoDto> (result);

            return Ok (alunoDto);
        }

        [HttpPost]
        public IActionResult Post (AlunoRegistrarDto model) {

            var aluno = _mapper.Map<Aluno> (model);

            _repo.Add (aluno);
            if (_repo.SaveChanges ()) {
                return Created ($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto> (aluno));
            }
            return BadRequest ();
        }

        [HttpPut]
        public IActionResult Put (int id, AlunoRegistrarDto model) {
            var aluno = _repo.GetAlunoById (id);
            if (aluno == null) return BadRequest ();

            _mapper.Map (model, aluno);

            _repo.Update (aluno);
            if (_repo.SaveChanges ()) {
                return Created ($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto> (aluno));
            }
            return BadRequest ();
        }

        [HttpPatch ("{id}")]
        public IActionResult Patch (int id, AlunoRegistrarDto model) {
            var aluno = _repo.GetAlunoById (id);
            if (aluno == null) return BadRequest ();

            _mapper.Map (model, aluno);

            _repo.Update (aluno);
            if (_repo.SaveChanges ()) {
                return Created ($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto> (aluno));
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