using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IElectricityWaterHistoryService
    {
        ElectricityWaterHistory Add(ElectricityWaterHistory ElectricityWaterHistory);

        void Update(ElectricityWaterHistory ElectricityWaterHistory);

        ElectricityWaterHistory Delete(ElectricityWaterHistory ElectricityWaterHistory);

        IEnumerable<ElectricityWaterHistory> GetAll();

        IEnumerable<ElectricityWaterHistory> GetAll(string keyword);

        IEnumerable<ElectricityWaterHistory> GetById(string id);

        void Save();
    }

    public class ElectricityWaterHistoryService : IElectricityWaterHistoryService
    {
        private IElectricityWaterHistoryRepository _ElectricityWaterHistoryRepository;
        private IUnitOfWork _unitOfWork;

        public ElectricityWaterHistoryService(IElectricityWaterHistoryRepository ElectricityWaterHistoryRepository, IUnitOfWork unitOfWork)
        {
            this._ElectricityWaterHistoryRepository = ElectricityWaterHistoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ElectricityWaterHistory Add(ElectricityWaterHistory ElectricityWaterHistory)
        {
            return _ElectricityWaterHistoryRepository.Add(ElectricityWaterHistory);
        }

        public ElectricityWaterHistory Delete(ElectricityWaterHistory ElectricityWaterHistory)
        {
            return _ElectricityWaterHistoryRepository.Delete(ElectricityWaterHistory);
        }

        public IEnumerable<ElectricityWaterHistory> GetAll()
        {
            return _ElectricityWaterHistoryRepository.GetAll();
        }

        public IEnumerable<ElectricityWaterHistory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ElectricityWaterHistoryRepository.GetMulti(x => x.RoomID.Contains(keyword));
            else
                return _ElectricityWaterHistoryRepository.GetAll();

        }

//        public IEnumerable<ElectricityWater> GetAllByParentId(int parentId)
//        {
//            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
//        }

        public IEnumerable<ElectricityWaterHistory> GetById(string id)
        {
            return _ElectricityWaterHistoryRepository.GetMulti(x => x.RoomID == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ElectricityWaterHistory ElectricityWaterHistory)
        {
            _ElectricityWaterHistoryRepository.Update(ElectricityWaterHistory);
        }
    }
}