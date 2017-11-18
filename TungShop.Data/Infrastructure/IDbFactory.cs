using System;

namespace TungShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        TungShopDbContext Init();
    }
}