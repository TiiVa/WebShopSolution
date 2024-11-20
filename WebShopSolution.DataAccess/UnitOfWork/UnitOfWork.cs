using WebShop;
using WebShop.Repositories;
using WebShopSolution.DataAccess.Notifications;
using WebShopSolution.DataAccess.Repositories;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopSolution.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        // Hämta produkter från repository
        public IProductRepository Products { get; private set; }
        public IUserRepository _userRepository { get; private set; }
        public IOrderRepository _orderRepository { get; private set; }

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

        public IUserRepository UserRepository
        {
	        get
	        {
		        if (_userRepository is null)
		        {
                    _userRepository = new UserRepository(_context);
				}

				return _userRepository;
			}
        }

        public IOrderRepository OrderRepository
        {
	        get
	        {
		        if (_orderRepository is null)
		        {
			        _orderRepository = new OrderRepository(_context);
		        }

		        return _orderRepository;
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
