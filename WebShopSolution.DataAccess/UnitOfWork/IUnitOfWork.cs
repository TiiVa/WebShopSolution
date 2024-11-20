using WebShop;
using WebShop.Repositories;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopSolution.DataAccess.UnitOfWork
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
         // Repository för produkter
         IProductRepository ProductRepository { get; }
         IUserRepository UserRepository { get; }
         IOrderRepository OrderRepository { get; }


		Task SaveChangesAsync();
         
         // Sparar förändringar (om du använder en databas)
        void NotifyProductAdded(Product product); // Notifierar observatörer om ny produkt
    }
}

