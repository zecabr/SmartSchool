using System.Linq;
using Microsoft.EntityFrameworkCore;
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