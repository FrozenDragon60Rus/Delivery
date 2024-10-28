using Delivery.sql.Table;

namespace Delivery.Filter
{
	internal class FirstDeliveryDateFilter(DateTime date) : IFilter
	{
		public DateTime FirstDeliveryDate { set; get; } = date;

		public Type Type { get; } = Type.FirstDeliveryDate;

		public IQueryable<JoinOrder> Run(IQueryable<JoinOrder> data) =>
			data.Where(d => DateTime.Compare(d.Date ?? DateTime.MinValue, FirstDeliveryDate) >= 0);
		public bool IsDefault => DateTime.Compare(DateTime.MinValue, FirstDeliveryDate) == 0;
	}
}
