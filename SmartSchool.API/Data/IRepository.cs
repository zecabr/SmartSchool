using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;

namespace SmartSchool.API.Data {
    public interface IRepository {
        void Add<T> (T entity) where T : class;
        void Update<T> (T entity) where T : class;
        void Delete<T> (T entity) where T : class;
        bool SaveChanges ();

         Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false);
        Aluno[] GetallAlunos(bool incluirProfessor = false);
        Aluno[] GetallAlunosByDisciplinaId(int id, bool incluirProfessor = false);
        Aluno GetAlunoById(int id, bool incluirProfessor = false);


        Professor[] GetallProfessores(bool incluirAlunos = false);
        Professor[] GetallProfessorsByDisciplinaId(int id, bool incluirAlunos = false);
        Professor GetProfessorById(int id, bool incluirAlunos = false);
    }
}