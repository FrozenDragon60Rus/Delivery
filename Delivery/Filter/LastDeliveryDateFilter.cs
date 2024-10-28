using Delivery.sql.Table;

namespace Delivery.Filter
{
	internal class LastDeliveryDateFilter(DateTime date) : IFilter
	{
		public DateTime LastDeliveryDate { set; get; } = date;

		public Type Type { get; } = Type.LastDeliveryDate;

		public IQueryable<JoinOrder> Run(IQueryable<JoinOrder> data) =>
			data.Where(d => DateTime.Compare(d.Date ?? DateTime.MaxValue, LastDeliveryDate) <= 0);
		public bool IsDefault =>
			DateTime.Compare(DateTime.MaxValue, LastDeliveryDate) == 0;
	}
}
