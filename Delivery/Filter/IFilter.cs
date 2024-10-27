using Delivery.sql.Table;

namespace Delivery.Filter
{
    internal interface IFilter
    {
        public Type Type { get; }

		/// <summary>Выполняет фильтрацию данных</summary>
		/// <param name="data">данные переданные для фильтрации</param>
		public IQueryable<JoinOrder> Run(IQueryable<JoinOrder> data);
	}
}
