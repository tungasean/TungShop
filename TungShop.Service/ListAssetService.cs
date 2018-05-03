using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IListAssetService
    {
        ListAsset Add(ListAsset ListAsset);

        void Update(ListAsset ListAsset);

        ListAsset Delete(ListAsset ListAsset);

        IEnumerable<ListAsset> GetAll();

        IEnumerable<ListAsset> GetAll(string keyword);

        ListAsset GetById(int id);

        void Save();
    }

    public class ListAssetService : IListAssetService
    {
        private IListAssetRepository _ListAssetRepository;
        private IUnitOfWork _unitOfWork;

        public ListAssetService(IListAssetRepository ListAssetRepository, IUnitOfWork unitOfWork)
        {
            this._ListAssetRepository = ListAssetRepository;
            this._unitOfWork = unitOfWork;
        }

        public ListAsset Add(ListAsset ListAsset)
        {
            return _ListAssetRepository.Add(ListAsset);
        }

        public ListAsset Delete(ListAsset ListAsset)
        {
            return _ListAssetRepository.Delete(ListAsset);
        }

        public IEnumerable<ListAsset> GetAll()
        {
            return _ListAssetRepository.GetAll();
        }

        public IEnumerable<ListAsset> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _ListAssetRepository.GetMulti(x => x.AssetName.Contains(keyword));
            else
                return _ListAssetRepository.GetAll();

        }

//        public IEnumerable<ListAsset> GetAllByParentId(int parentId)
//        {
//            return _ProductCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
//        }

        public ListAsset GetById(int id)
        {
            return _ListAssetRepository.GetSingleByCondition(x => x.AssetsID == id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ListAsset ListAsset)
        {
            _ListAssetRepository.Update(ListAsset);
        }
    }
}