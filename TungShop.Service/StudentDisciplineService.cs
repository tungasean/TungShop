using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IStudentDisciplineService
    {
        StudentDiscipline Add(StudentDiscipline StudentDiscipline);

        void Update(StudentDiscipline StudentDiscipline);

        StudentDiscipline Delete(StudentDiscipline StudentDiscipline);

        IEnumerable<StudentDiscipline> GetAll();

        //IEnumerable<StudentDiscipline> GetAll(string keyword);

        IEnumerable<StudentDiscipline> GetById(string studentId);

        StudentDiscipline GetById(int id);
        

        void Save();
    }

    public class StudentDisciplineService : IStudentDisciplineService
    {
        private IStudentDisciplineRepository _StudentDisciplineRepository;
        private IUnitOfWork _unitOfWork;

        public StudentDisciplineService(IStudentDisciplineRepository StudentDisciplineRepository, IUnitOfWork unitOfWork)
        {
            this._StudentDisciplineRepository = StudentDisciplineRepository;
            this._unitOfWork = unitOfWork;
        }

        public StudentDiscipline Add(StudentDiscipline StudentDiscipline)
        {
            return _StudentDisciplineRepository.Add(StudentDiscipline);
        }

        public StudentDiscipline Delete(StudentDiscipline StudentDiscipline)
        {
            return _StudentDisciplineRepository.Delete(StudentDiscipline);
        }

        public IEnumerable<StudentDiscipline> GetAll()
        {
            return _StudentDisciplineRepository.GetAll();
        }

//        public IEnumerable<StudentDiscipline> GetAll(string keyword)
//        {
//            if (!string.IsNullOrEmpty(keyword))
//                return _StudentDisciplineRepository.GetMulti(x => x.Name.Contains(keyword) || x.StudentDisciplineID.Contains(keyword));
//            else
//                return _StudentDisciplineRepository.GetAll();
//
//        }

//        public IEnumerable<StudentDiscipline> GetAllByParentId(int parentId)
//        {
//            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
//        }

        public IEnumerable<StudentDiscipline> GetById(string studentId)
        {
            return _StudentDisciplineRepository.GetMulti(x => x.StudentID == studentId);
        }

        public StudentDiscipline GetById(int id)
        {
            return _StudentDisciplineRepository.GetSingleById(id);
        }
        

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(StudentDiscipline StudentDiscipline)
        {
            _StudentDisciplineRepository.Update(StudentDiscipline);
        }
    }
}