using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IElectricityWaterHistoryRepository : IRepository<ElectricityWaterHistory>
    {
        IEnumerable<ElectricityWaterHistory> GetById(string id);
    }

    public class ElectricityWaterHistoryRepository : RepositoryBase<ElectricityWaterHistory>, IElectricityWaterHistoryRepository
    {
        public ElectricityWaterHistoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<ElectricityWaterHistory> GetById(string ElectricityWaterHistoryId)
        {
            return this.DbContext.ElectricityWaterHistorys.Where(x => x.RoomID == ElectricityWaterHistoryId);
        }
    }
}