namespace Delivery.sql.Table
{
	public class JoinOrder
	{
		public int Id { get; set; }	
		public float Weight { get; set; }
		public string Region { get; set; } = string.Empty;
		public DateTime Date { get; set; }

		public override string ToString() =>
			$"Number {Id}; Weight {Weight}; Region {Region}; Date {Date}";
	}
}
