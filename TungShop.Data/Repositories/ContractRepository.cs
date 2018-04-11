using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IContractRepository : IRepository<Contract>
    {
        IEnumerable<Contract> GetSingleByCondition(string studentID, string roomId);
    }

    public class ContractRepository : RepositoryBase<Contract>, IContractRepository
    {
        public ContractRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Contract> GetSingleByCondition(string studentID, string roomId)
        {
            return this.DbContext.Contracts.Where(x => x.StudentID == studentID && x.RoomID == roomId);
        }
    }
}