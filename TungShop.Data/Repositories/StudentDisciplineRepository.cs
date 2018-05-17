using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IStudentDisciplineRepository : IRepository<StudentDiscipline>
    {
        IEnumerable<StudentDiscipline> GetById(string id);
    }

    public class StudentDisciplineRepository : RepositoryBase<StudentDiscipline>, IStudentDisciplineRepository
    {
        public StudentDisciplineRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<StudentDiscipline> GetById(string StudentId)
        {
            return this.DbContext.StudentDisciplines.Where(x => x.StudentID == StudentId);
        }
    }
}