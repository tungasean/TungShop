﻿using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Student> GetById(string id);
    }

    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Student> GetById(string StudentId)
        {
            return this.DbContext.Students.Where(x => x.StudentID == StudentId );
        }
    }
}