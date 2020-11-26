using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data {
    public class Repository : IRepository {
        private readonly SmartContext _context;

        public Repository (SmartContext context) {
            _context = context;
        }
        public void Add<T> (T entity) where T : class {
            _context.Add (entity);
        }
        public void Update<T> (T entity) where T : class {
            _context.Update (entity);
        }
        public void Delete<T> (T entity) where T : class {
            _context.Remove (entity);
        }
        public bool SaveChanges () {
            return (_context.SaveChanges () > 0);
        }

        public Aluno[] GetallAlunos (bool incluirProfessor = false) {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor) {
                query = query.Include (a => a.AlunosDisciplinas)
                    .ThenInclude (ad => ad.Disciplina)
                    .ThenInclude (d => d.Professor);
            }
            query = query.AsNoTracking ().OrderBy (a => a.Id);
            return query.ToArray ();
        }
        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(a => a.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(d => d.Professor);
            }

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Nome))
                query = query.Where(aluno => aluno.Nome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()) ||
                                             aluno.Sobrenome
                                                  .ToUpper()
                                                  .Contains(pageParams.Nome.ToUpper()));

            if (pageParams.Matricula > 0)
                query = query.Where(aluno => aluno.Matricula == pageParams.Matricula);
            
            if (pageParams.Ativo != null)
                query = query.Where(aluno => aluno.Ativo == (pageParams.Ativo != 0));

            // return await query.ToListAsync();
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Aluno[] GetallAlunosByDisciplinaId (int disciplinaId, bool incluirProfessor = false) {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor) {
                query = query.Include (a => a.AlunosDisciplinas)
                    .ThenInclude (ad => ad.Disciplina)
                    .ThenInclude (d => d.Professor);
            }
            query = query.AsNoTracking ()
                .OrderBy (a => a.Id)
                .Where (a => a.AlunosDisciplinas.Any (ad => ad.DisciplinaId == disciplinaId));
            return query.ToArray ();
        }

        public Aluno GetAlunoById (int id, bool incluirProfessor = false) {
            IQueryable<Aluno> query = _context.Alunos;
            if (incluirProfessor) {
                query = query.Include (a => a.AlunosDisciplinas)
                    .ThenInclude (ad => ad.Disciplina)
                    .ThenInclude (d => d.Professor);
            }
            query = query.AsNoTracking ()
                .OrderBy (a => a.Id)
                .Where (a => a.Id == id);
            return query.FirstOrDefault ();
        }

        public Professor[] GetallProfessores (bool incluirAlunos = false) {
            IQueryable<Professor> query = _context.Professores;
            if (incluirAlunos) {
                query = query.Include (a => a.Disciplinas)
                    .ThenInclude (ad => ad.AlunosDisciplinas)
                    .ThenInclude (d => d.Aluno);
            }
            query = query.AsNoTracking ().OrderBy (a => a.Id);
            return query.ToArray ();
        }

        public Professor[] GetallProfessorsByDisciplinaId (int disciplinaId, bool incluirAlunos = false) {
            IQueryable<Professor> query = _context.Professores;
            if (incluirAlunos) {
                query = query.Include (a => a.Disciplinas)
                    .ThenInclude (ad => ad.AlunosDisciplinas)
                    .ThenInclude (d => d.Aluno);
            }
            query = query.AsNoTracking ()
                .OrderBy (a => a.Id)
                .Where (a => a.Disciplinas.Any (ad => ad.AlunosDisciplinas
                    .Any (ad => ad.DisciplinaId == disciplinaId)));
            return query.ToArray ();
        }

        public Professor GetProfessorById (int id, bool incluirAlunos = false) {
            IQueryable<Professor> query = _context.Professores;
            if (incluirAlunos) {
                query = query.Include (a => a.Disciplinas)
                    .ThenInclude (ad => ad.AlunosDisciplinas)
                    .ThenInclude (d => d.Aluno);
            }
            query = query.AsNoTracking ()
                .OrderBy (a => a.Id)
                .Where (a => a.Id == id);
            return query.FirstOrDefault ();
        }
    }
}