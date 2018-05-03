using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IRoomAssetService
    {
        RoomAsset Add(RoomAsset RoomAsset);

        void Update(RoomAsset RoomAsset);

        RoomAsset Delete(RoomAsset RoomAsset);

        IEnumerable<RoomAsset> GetAll();

        //IEnumerable<RoomAsset> GetAll(int keyword);

        RoomAsset GetById(int id);

        void Save();
    }

    public class RoomAssetService : IRoomAssetService
    {
        private IRoomAssetRepository _RoomAssetRepository;
        private IUnitOfWork _unitOfWork;

        public RoomAssetService(IRoomAssetRepository RoomAssetRepository, IUnitOfWork unitOfWork)
        {
            this._RoomAssetRepository = RoomAssetRepository;
            this._unitOfWork = unitOfWork;
        }

        public RoomAsset Add(RoomAsset RoomAsset)
        {
            return _RoomAssetRepository.Add(RoomAsset);
        }

        public RoomAsset Delete(RoomAsset RoomAsset)
        {
            return _RoomAssetRepository.Delete(RoomAsset);
        }

        public IEnumerable<RoomAsset> GetAll()
        {
            return _RoomAssetRepository.GetAll();
        }
//
//        public IEnumerable<RoomAsset> GetAll(int keyword)
//        {
//            if (keyword != 0)
//                return _RoomAssetRepository.GetMulti(x => x.AssetsID = keyword);
//            else
//                return _RoomAssetRepository.GetAll();
//
//        }

//        public IEnumerable<RoomAsset> GetAllByParentId(int parentId)
//        {
//            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
//        }

        public RoomAsset GetById(int id)
        {
            return _RoomAssetRepository.GetSingleByCondition(x => x.RoomID == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(RoomAsset RoomAsset)
        {
            _RoomAssetRepository.Update(RoomAsset);
        }
    }
}