using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IStudentService
    {
        Student Add(Student Student);

        void Update(Student Student);

        Student Delete(Student Student);

        IEnumerable<Student> GetAll();

        IEnumerable<Student> GetAll(string keyword);

        Student GetById(string id);

        void Save();
    }

    public class StudentService : IStudentService
    {
        private IStudentRepository _StudentRepository;
        private IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository StudentRepository, IUnitOfWork unitOfWork)
        {
            this._StudentRepository = StudentRepository;
            this._unitOfWork = unitOfWork;
        }

        public Student Add(Student Student)
        {
            return _StudentRepository.Add(Student);
        }

        public Student Delete(Student Student)
        {
            return _StudentRepository.Delete(Student);
        }

        public IEnumerable<Student> GetAll()
        {
            return _StudentRepository.GetAll();
        }

        public IEnumerable<Student> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _StudentRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _StudentRepository.GetAll();

        }

//        public IEnumerable<Student> GetAllByParentId(int parentId)
//        {
//            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
//        }

        public Student GetById(string id)
        {
            return _StudentRepository.GetSingleByCondition(x => x.StudentID == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Student Student)
        {
            _StudentRepository.Update(Student);
        }
    }
}