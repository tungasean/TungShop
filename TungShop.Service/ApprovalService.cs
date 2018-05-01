using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IApprovalService
    {
        Approval Add(Approval Approval);

        void Update(Approval Approval);

        Approval Delete(Approval Approval);

        IEnumerable<Approval> GetAll();

        IEnumerable<Approval> GetAll(string keyword);

        Approval GetById(int id);

        void Save();
    }

    public class ApprovalService : IApprovalService
    {
        private IApprovalRepository _ApprovalRepository;
        private IUnitOfWork _unitOfWork;

        public ApprovalService(IApprovalRepository ApprovalRepository, IUnitOfWork unitOfWork)
        {
            this._ApprovalRepository = ApprovalRepository;
            this._unitOfWork = unitOfWork;
        }

        public Approval Add(Approval Approval)
        {
            return _ApprovalRepository.Add(Approval);
        }

        public Approval Delete(Approval Approval)
        {
            return _ApprovalRepository.Delete(Approval);
        }

        public IEnumerable<Approval> GetAll()
        {
            return _ApprovalRepository.GetAll();
        }

        public IEnumerable<Approval> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ApprovalRepository.GetMulti(x => (x.Name.Contains(keyword) || x.StudentId.Contains(keyword)) && x.Status == 1);
            else
                return _ApprovalRepository.GetAll();

        }

//        public IEnumerable<Approval> GetAllByParentId(int parentId)
//        {
//            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
//        }

        public Approval GetById(int id)
        {
            return _ApprovalRepository.GetSingleByCondition(x => x.ApprovalId == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Approval Approval)
        {
            _ApprovalRepository.Update(Approval);
        }
    }
}