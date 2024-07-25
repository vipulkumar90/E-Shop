namespace Customer.Models
{
	public class OrderHistory
	{
		public string CustomerName { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
    }
}
