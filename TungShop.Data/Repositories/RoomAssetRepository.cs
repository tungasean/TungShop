using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IRoomAssetRepository : IRepository<RoomAsset>
    {
        IEnumerable<RoomAsset> GetById(int id);
    }

    public class RoomAssetRepository : RepositoryBase<RoomAsset>, IRoomAssetRepository
    {
        public RoomAssetRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<RoomAsset> GetById(int RoomAssetId)
        {
            return this.DbContext.RoomAssets.Where(x => x.RoomID == RoomAssetId);
        }
    }
}