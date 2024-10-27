using Delivery.Filter;
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
		public async Task<object?> Get()
		{
			await Log.WriteDBConnection(context.Database.CanConnect(), "Delivery");

			var region = context.Region;
			region.Load();
			var regions = region.Select(r => r.Name).ToList();

			return regions;
		}
	}
}
