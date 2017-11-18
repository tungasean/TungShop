namespace TungShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private TungShopDbContext dbContext;

        public TungShopDbContext Init()
        {
            return dbContext ?? (dbContext = new TungShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}