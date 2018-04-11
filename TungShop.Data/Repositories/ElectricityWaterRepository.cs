using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IElectricityWaterRepository : IRepository<ElectricityWater>
    {
        IEnumerable<ElectricityWater> GetById(string id);
    }

    public class ElectricityWaterRepository : RepositoryBase<ElectricityWater>, IElectricityWaterRepository
    {
        public ElectricityWaterRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<ElectricityWater> GetById(string ElectricityWaterId)
        {
            return this.DbContext.ElectricityWaters.Where(x => x.RoomID == ElectricityWaterId );
        }
    }
}