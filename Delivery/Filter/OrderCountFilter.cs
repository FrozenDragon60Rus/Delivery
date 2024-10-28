using Delivery.sql.Table;

namespace Delivery.Filter
{
	internal class OrderCountFilter(int count) : IFilter
	{
		public int Count { set; get; } = count;

		public Type Type { get; } = Type.OrderCount;

		public IQueryable<JoinOrder> Run(IQueryable<JoinOrder> data)
		{
			List<string> region = [..data.Select(d => d.Region).Distinct()],
				filtered = [];
			int count;

			foreach(var r in region)
			{
				count = data.Where(d => d.Region == r).Count();
				if (count == Count) 
					filtered.Add(r);
			}

			return data.Where(d => filtered.Contains(d.Region));
		}
		public bool IsDefault =>
			Count == 0;
	}
}
