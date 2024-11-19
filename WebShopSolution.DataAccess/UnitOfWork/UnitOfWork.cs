using WebShop;
using WebShop.Repositories;
using WebShopSolution.DataAccess.Notifications;
using WebShopSolution.DataAccess.Repositories;

namespace WebShopSolution.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        // Hämta produkter från repository
        public IProductRepository Products { get; private set; }
        private readonly WebShopSolutionDbContext _context;
        

        public IProductRepository ProductRepository
        {
	        get
	        {
		        if (Products == null)
		        {
			        Products = new ProductRepository(_context);
		        }

                return Products;
	        }
        }
		private readonly ProductSubject _productSubject;

        // Konstruktor används för tillfället av Observer pattern
        public UnitOfWork(WebShopSolutionDbContext context, ProductSubject productSubject)
        {
            Products = null;

            _context = context;

            // Om inget ProductSubject injiceras, skapa ett nytt
            _productSubject = productSubject ?? new ProductSubject();

            // Registrera standardobservatörer
            _productSubject.Attach(new EmailNotification());
        }

        public async Task SaveChangesAsync()
        {
	        await _context.SaveChangesAsync();
        }

		public void Dispose()
        {
            _context.Dispose();
        }

       

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }
    }
}
