namespace TungShop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}