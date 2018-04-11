using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IElectricityWaterService
    {
        ElectricityWater Add(ElectricityWater ElectricityWater);

        void Update(ElectricityWater ElectricityWater);

        ElectricityWater Delete(ElectricityWater ElectricityWater);

        IEnumerable<ElectricityWater> GetAll();

        IEnumerable<ElectricityWater> GetAll(string keyword);

        ElectricityWater GetById(string id);

        void Save();
    }

    public class ElectricityWaterService : IElectricityWaterService
    {
        private IElectricityWaterRepository _ElectricityWaterRepository;
        private IUnitOfWork _unitOfWork;

        public ElectricityWaterService(IElectricityWaterRepository ElectricityWaterRepository, IUnitOfWork unitOfWork)
        {
            this._ElectricityWaterRepository = ElectricityWaterRepository;
            this._unitOfWork = unitOfWork;
        }

        public ElectricityWater Add(ElectricityWater ElectricityWater)
        {
            return _ElectricityWaterRepository.Add(ElectricityWater);
        }

        public ElectricityWater Delete(ElectricityWater ElectricityWater)
        {
            return _ElectricityWaterRepository.Delete(ElectricityWater);
        }

        public IEnumerable<ElectricityWater> GetAll()
        {
            return _ElectricityWaterRepository.GetAll();
        }

        public IEnumerable<ElectricityWater> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ElectricityWaterRepository.GetMulti(x => x.RoomID.Contains(keyword));
            else
                return _ElectricityWaterRepository.GetAll();

        }

//        public IEnumerable<ElectricityWater> GetAllByParentId(int parentId)
//        {
//            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
//        }

        public ElectricityWater GetById(string id)
        {
            return _ElectricityWaterRepository.GetSingleByCondition(x => x.RoomID == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ElectricityWater ElectricityWater)
        {
            _ElectricityWaterRepository.Update(ElectricityWater);
        }
    }
}