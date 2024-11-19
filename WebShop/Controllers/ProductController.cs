using Microsoft.AspNetCore.Mvc;
using WebShopSolution.DataAccess.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Endpoint för att hämta alla produkter
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            // Behöver använda repository via Unit of Work för att hämta produkter

            var products = _unitOfWork.ProductRepository.GetAllAsync();

            return Ok(products);
        }

        // Endpoint för att lägga till en ny produkt
        [HttpPost]
        public ActionResult AddProduct([FromBody]Product product)
        {
	        if (product == null)
	        {
		        return BadRequest("Product is null");
	        }

	        // Lägger till produkten via repository

			try
			{
		        _unitOfWork.ProductRepository.AddAsync(product);

		        // Sparar förändringar

				_unitOfWork.SaveChangesAsync();

		        return Ok("Product added successfully");
	        }
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
	        
			// Notifierar observatörer om att en ny produkt har lagts till

			
        }
    }
}
