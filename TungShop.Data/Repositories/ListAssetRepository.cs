using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IListAssetRepository : IRepository<ListAsset>
    {
        IEnumerable<ListAsset> GetById(int id);
    }

    public class ListAssetRepository : RepositoryBase<ListAsset>, IListAssetRepository
    {
        public ListAssetRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<ListAsset> GetById(int ListAssetId)
        {
            return this.DbContext.ListAssets.Where(x => x.AssetsID == ListAssetId );
        }
    }
}