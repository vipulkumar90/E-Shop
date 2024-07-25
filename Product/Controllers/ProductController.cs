using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.DataContext;
using Product.Models;

namespace Product.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ProductDbContext _context;
		public ProductController(ProductDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public ActionResult<IEnumerable<ProductEntity>> Get()
		{
			return Ok(_context.Products.ToList());
		}
		[HttpGet("{id}")]
		public ActionResult<ProductEntity> Get(int id)
		{
			return Ok(_context.Products.FirstOrDefault(p => p.Id == id));
		}
	}
}
