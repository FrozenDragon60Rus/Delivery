using System.ComponentModel.DataAnnotations;

namespace Delivery.sql.Table
{
    public class Order
    {
		[Display(Name = "Номер"),
			Required(ErrorMessage = "Отсутствует Id")]
		public int Id { get; set; }
		[Display(Name = "Вес"),
			Required(ErrorMessage = "Отсутствует запись о весе товара"),
			Range(0f, 5000f)]
		public float Weight { get; set; } = 0;
		[Display(Name = "Номер региона"),
			Required(ErrorMessage = "Отсутствует запись о регионе")]
		public int RegionId { get; set; }
		[Display(Name = "Время доставки заказа")]
		public DateTime? Date { get; set; }
	}
}
