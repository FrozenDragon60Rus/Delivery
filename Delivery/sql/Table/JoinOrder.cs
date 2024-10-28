using System.ComponentModel;

namespace Delivery.sql.Table
{
	public class JoinOrder
	{
		[DisplayName("Номер")]
		public int Id { get; set; }
		[DisplayName("Вес")]
		public float Weight { get; set; }
		[DisplayName("Регион")]
		public string Region { get; set; } = string.Empty;
		[DisplayName("Дата доставки")]
		public DateTime? Date { get; set; }

		public override string ToString() =>
			$"Number {Id}; Weight {Weight}; Region {Region}; Date {Date.ToString() ?? "not delivered"}";
	}
}
