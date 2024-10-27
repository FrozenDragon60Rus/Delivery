using Delivery.sql.Table;

namespace Delivery.Filter
{
	internal class DateFromFilter(DateTime date) : IFilter
	{
		public DateTime DateFrom { set; get; } = date;

		public Type Type { get; } = Type.DateFrom;

		public IQueryable<JoinOrder> Run(IQueryable<JoinOrder> data) =>
			data.Where(d => DateTime.Compare(d.Date, DateFrom) >= 0);
	}
}
