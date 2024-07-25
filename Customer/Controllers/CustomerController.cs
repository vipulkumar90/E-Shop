using Customer.DataContext;
using Customer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace Customer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly CustomerDbContext _context;
		private readonly HttpClient _client;
		private readonly IConfiguration _configuration;
		public CustomerController(CustomerDbContext context, HttpClient client, 
			IConfiguration configuration)
		{
			_context = context;
			_client = client;
			_configuration = configuration;
		}
		[HttpGet]
		public ActionResult<IEnumerable<CustomerEntity>> Get()
		{
			return Ok(_context.Customers.ToList());
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderHistory>> GetAsync(int id)
		{
			var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
			var orderHistory = new OrderHistory
			{
				CustomerName = customer.Name,
				OrderItems = new List<OrderItem>()
			};
			var response = await _client.GetAsync(string.Concat(_configuration.GetValue<string>("OrderServiceUrlGetByCustomerId"), id));
			var responseString = await response.Content.ReadAsStringAsync();
			var ordersByCustomer = JsonConvert.DeserializeObject<IEnumerable<OrderEntity>>(responseString);
			foreach (var order in ordersByCustomer)
			{
				var productResponse = await _client.GetAsync(string.Concat(_configuration.GetValue<string>("ProductServiceUrlGetById"), order.ProductId));
				var productResponseString = await productResponse.Content.ReadAsStringAsync();
				var productByCustomer = JsonConvert.DeserializeObject<ProductEntity>(productResponseString);
				orderHistory.OrderItems.Add(new OrderItem
				{
					ProductName = productByCustomer.Name,
					ProductPrice = productByCustomer.Price,
				});
			}
			return Ok(orderHistory);
		}
	}
}
