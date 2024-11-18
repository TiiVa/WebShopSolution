using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShop.Repositories
{
    // Gränssnitt för produktrepositoryt enligt Repository Pattern
    public interface IProductRepository : IRepository<Product, int>
    {
       
    }
}
