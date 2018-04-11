using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IContractService
    {
        Contract Add(Contract Contract);

        void Update(Contract Contract);

        Contract Delete(Contract Contract);

        IEnumerable<Contract> GetAll();

//        IEnumerable<Contract> GetAll(string keyword);

        Contract GetSingleByCondition(string studentId);

        void Save();
    }

    public class ContractService : IContractService
    {
        private IContractRepository _ContractRepository;
        private IUnitOfWork _unitOfWork;

        public ContractService(IContractRepository ContractRepository, IUnitOfWork unitOfWork)
        {
            this._ContractRepository = ContractRepository;
            this._unitOfWork = unitOfWork;
        }

        public Contract Add(Contract Contract)
        {
            return _ContractRepository.Add(Contract);
        }

        public Contract Delete(Contract Contract)
        {
            return _ContractRepository.Delete(Contract);
        }

        public IEnumerable<Contract> GetAll()
        {
            return _ContractRepository.GetAll();
        }

//        public IEnumerable<Contract> GetAll(string keyword)
//        {
//            if (!string.IsNullOrEmpty(keyword))
//                return _ContractRepository.GetMulti(x => x.ContractName.Contains(keyword));
//            else
//                return _ContractRepository.GetAll();
//
//        }

        public Contract GetSingleByCondition(string studentId)
        {
            return _ContractRepository.GetSingleByCondition(x =>  x.StudentID == studentId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Contract Contract)
        {
            _ContractRepository.Update(Contract);
        }
    }
}