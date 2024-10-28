using System.ComponentModel.DataAnnotations;

namespace Delivery.sql.Table
{
	public class Region
	{
		[Display(Name = "Номер"),
			Required(ErrorMessage = "Отсутствует Id")]
		public int Id { get; set; }
		[Display(Name = "Регион"),
			Required(ErrorMessage = "Отсутствует название региона"),
			StringLength(30)]
		public string Name { get; set; } = string.Empty;
	}
}
