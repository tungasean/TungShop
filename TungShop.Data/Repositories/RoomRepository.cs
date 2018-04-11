using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IRoomRepository : IRepository<Room>
    {
        IEnumerable<Room> GetById(string id);
    }

    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Room> GetById(string id)
        {
            return this.DbContext.Rooms.Where(x => x.RoomID == id);
        }
    }
}