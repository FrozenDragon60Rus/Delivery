namespace Delivery.sql.Table
{
    public class Order
    {
        public int Id { get; set; }
        public float Weight { get; set; }
        public int RegionId { get; set; }
        public DateTime Date { get; set; }
	}
}
