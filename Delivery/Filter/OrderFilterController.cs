using Delivery.sql;
using Delivery.sql.Table;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DeliveryTest")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Delivery.Filter
{
    internal class OrderFilterController(OrderContext orderContext)
    {
        private readonly OrderContext context = orderContext;
        private List<IFilter> Filter { get; } = [];

		public int FilterCount => Filter.Count();

		/// <summary>Добавляет фильтр или заменяет его, если фильтр уже был в списке </summary>
		/// <param name="filter">добавляемый фильтр </param>
		public void AddFilter(IFilter filter)
        {
            var item = Filter.Find(x => x.Type == filter.Type);

            if (item == null)
                Filter.Add(filter);
            else
            {
				var i = Filter.IndexOf(item);
				Filter[i] = filter;
            }
        }

		/// <summary>Удаляет фильтр </summary>
		/// <param name="filter">удаляемый фильтр </param>
		public void RemoveFilter(IFilter filter)
        {
			var f = Filter.Where(x => x.Type == filter.Type).Single();
			Filter.Remove(f);
        }
		/// <summary>Удаляет фильтр </summary>
		/// <param name="type">тип удаляемого фильтра </param>
		public void RemoveFilter(Type type)
        {
            var filter = Filter.Where(x => x.Type == type);

            if (filter.Any())
				Filter.Remove(filter.Single()); ;  
        }
        
        /// <summary>
        /// Обновляет или удаляет фильтр
        /// </summary>
        /// <param name="filter">Обновляемый фильтр</param>
        public void Update(IFilter filter)
        {
			if (filter.IsDefault)
				RemoveFilter(filter.Type);
			else
				AddFilter(filter);
		}

		/// <summary>Получает данные из БД и фильтрует их (при наличии фильтров)</summary>
        /// <returns>Возвращает отфильтрованные данные</returns>
		public IQueryable<JoinOrder> Get()
        {
            var data = Load();

            foreach (var f in Filter)
                data = f.Run(data);

            return data;
        }

		/// <summary>Удаляет все фильтры </summary>
		public void RemoveAllFilter() =>
            Filter.Clear();

		/// <summary>Получает данные о заказах </summary>
		private IQueryable<JoinOrder> Load()
        {
			context.Order.Load();
			context.Region.Load();

            var orders = context.Order.Join(context.Region,
                o => o.RegionId,
                r => r.Id,
                (o, r) => new JoinOrder
                {
                    Id = o.Id,
                    Weight = o.Weight,
                    Region = r.Name,
                    Date = o.Date
                });
            return orders;
        }
	}
}
