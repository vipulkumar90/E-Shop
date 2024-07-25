using Microsoft.AspNetCore.Mvc;
using Order.DataContext;
using Order.Models;

namespace Order.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly OrderDbContext _context;
		public OrderController(OrderDbContext context)
		{
			_context = context;
		}
		[HttpGet]
		public ActionResult<IEnumerable<OrderEntity>> Get()
		{
			return Ok(_context.Orders.ToList());
		}

		[HttpGet("{id}")]
		public ActionResult<OrderEntity> Get(int id)
		{
			return Ok(_context.Orders.FirstOrDefault(o => o.Id == id)!);
		}

		[HttpPost]
		public ActionResult Post(OrderEntity orderEntity)
		{
			_context.Orders.Add(orderEntity);
			_context.SaveChanges();
			return Created();
		}


		[HttpDelete("{id}")]
		public ActionResult Delete(int id)
		{
			var orderEntity = _context.Orders.Find(id);
			if (orderEntity == null)
			{
				return NotFound();
			}
			_context.Orders.Remove(orderEntity);
			_context.SaveChanges();
			return NoContent();
		}
		[HttpGet("customer/{id}")]
		public ActionResult<IEnumerable<OrderEntity>> GetbyCustomerId(int id)
		{
			var ordersByCustomerId = _context.Orders.Where(o => o.CustomerId == id).ToList();
			return Ok(ordersByCustomerId);
		}
	}
}
