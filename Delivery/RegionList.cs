using Delivery.sql;
using Microsoft.EntityFrameworkCore;

namespace Delivery
{
	public class RegionList(OrderContext context)
	{
		private readonly OrderContext context = context;

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Возвращает список регионов </returns>
		public object? Get()
		{
			var region = context.Region;
			region.Load();
			var regions = region.Select(r => r.Name).ToList();

			return regions;
		}
	}
}
