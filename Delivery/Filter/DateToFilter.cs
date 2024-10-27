using Delivery.sql.Table;

namespace Delivery.Filter
{
	internal class DateToFilter(DateTime date) : IFilter
	{
		public DateTime DateBefore { set; get; } = date;

		public Type Type { get; } = Type.DateTo;

		public IQueryable<JoinOrder> Run(IQueryable<JoinOrder> data) =>
			data.Where(d => DateTime.Compare(d.Date, DateBefore) <= 0);
	}
}
