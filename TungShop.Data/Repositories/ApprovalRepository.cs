using System.Collections.Generic;
using System.Linq;
using TungShop.Data.Infrastructure;
using TungShop.Model.Models;

namespace TungShop.Data.Repositories
{
    public interface IApprovalRepository : IRepository<Approval>
    {
        IEnumerable<Approval> GetById(int id);
    }

    public class ApprovalRepository : RepositoryBase<Approval>, IApprovalRepository
    {
        public ApprovalRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Approval> GetById(int ApprovalId)
        {
            return this.DbContext.Approvals.Where(x => x.ApprovalId == ApprovalId );
        }
    }
}