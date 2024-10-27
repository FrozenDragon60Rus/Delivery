using Delivery.sql.Table;

namespace Delivery.Filter
{
    internal class RegionFilter(IEnumerable<string> region) : IFilter
    {
        public IEnumerable<string> RegionList { set; get; } = region;
        public Type Type { get; } = Type.Region;

        public IQueryable<JoinOrder> Run(IQueryable<JoinOrder> data) =>
            data.Where(d => RegionList.Contains(d.Region));
    }
}
