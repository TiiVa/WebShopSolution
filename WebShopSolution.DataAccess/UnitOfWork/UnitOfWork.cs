using WebShop;
using WebShop.Repositories;
using WebShopSolution.DataAccess.Notifications;
using WebShopSolution.DataAccess.Repositories;
using WebShopSolution.DataAccess.RepositoryInterfaces;

namespace WebShopSolution.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        // Hämta produkter från repository
        public IProductRepository Products { get; private set; }
        public IUserRepository _userRepository { get; private set; }
        public IOrderRepository _orderRepository { get; private set; }
        private readonly ProductSubject _productSubject;
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

        // Konstruktor används för tillfället av Observer pattern
        public UnitOfWork(WebShopSolutionDbContext context, IProductRepository productRepository, IUserRepository userRepository, IOrderRepository orderRepository, ProductSubject productSubject)
        {
            Products = productRepository;
			_userRepository = userRepository;
			_orderRepository = orderRepository;
			_context = context;

            // Om inget ProductSubject injiceras, skapa ett nytt
            _productSubject = productSubject ?? new ProductSubject();

            // Registrera standardobservatörer
            _productSubject.Attach(new EmailNotification());
			_productSubject.Attach(new SmsNotification());
		}

        public async Task CommitAsync()
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
