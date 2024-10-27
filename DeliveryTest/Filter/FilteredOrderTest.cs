using Delivery.Filter;
using Delivery.sql;
using Moq;
using System.Reflection;
using Xunit.Abstractions;

namespace DeliveryTest.Filter
{
	public class FilteredOrderTest : IDisposable
	{
		private readonly FilteredOrder service;

		private ITestOutputHelper Output { get; }

		public FilteredOrderTest(ITestOutputHelper output)
		{
			Output = output;

			//Order setup
			var MockDBOrder = Setup.DbSet(Setup.GetOrderData);

			//Region setup
			var MockDBRegion = Setup.DbSet(Setup.GetRegionData);

			//Context setup
			var mockContext = new Mock<OrderContext>();
			mockContext.Setup(c => c.Order).Returns(MockDBOrder);
			mockContext.Setup(c => c.Region).Returns(MockDBRegion);

			service = new(mockContext.Object);
		}

		[Fact]
		public void GetTest()
		{
			var actual = service.Get().ToList();
			var expected = Setup.GetJoinData.ToList();

			for (int i = 0; i < actual.Count; i++)
				Assert.Equal(actual[i].ToString(), expected[i].ToString());
		}


		[Theory,
			InlineData(new string[] { "Москва" }, 1),
			InlineData(new string[] { "Санкт-Петербург" }, 2),
			InlineData(new string[] { "Псков" }, 3),
			InlineData(new string[] { "Псков", "Москва" }, 4),
			InlineData(new string[] { "Псков", "Москва", "Санкт-Петербург" }, 6)]
		public void FilterByRegionTest(string[] region, int count)
		{
			var filter = new RegionFilter(region);
			service.AddFilter(filter);

			var actual = service.Get().ToList();

			foreach (var item in actual)
				Assert.Contains(item.Region, region);

			Assert.Equal(count, actual.Count);
		}

		[Theory,
			InlineData("2020-01-01", 6),
			InlineData("2024-01-01", 4),
			InlineData("2024-06-20", 1)]
		public void FilterByDateFromTest(DateTime date, int count)
		{
			var filter = new DateFromFilter(date);
			service.AddFilter(filter);

			var actual = service.Get().ToList();

			foreach (var item in actual)
				Assert.True(item.Date > date);

			Assert.Equal(expected: count, actual.Count);
		}

		[Theory,
			InlineData("2025-01-01", 6),
			InlineData("2024-04-01", 4),
			InlineData("2023-05-01", 1)]
		public void FilterByDateBeforeTest(DateTime date, int count)
		{
			var filter = new DateToFilter(date);
			service.AddFilter(filter);

			var actual = service.Get().ToList();

			foreach (var item in actual)
				Assert.True(item.Date < date);

			Assert.Equal(expected: count, actual.Count);
		}

		[Theory,
			InlineData("2020-01-01", "2025-01-01", 6),
			InlineData("2023-12-01", "2024-03-01", 2),
			InlineData("2024-03-01", "2024-03-28", 1)]
		public void FilterInDateRangeTest(DateTime dateFrom, DateTime dateTo, int count)
		{
			var filterFrom = new DateFromFilter(dateFrom);
			service.AddFilter(filterFrom);

			var filterTo = new DateToFilter(dateTo);
			service.AddFilter(filterTo);

			var actual = service.Get().ToList();

			foreach (var item in actual)
				Assert.True(item.Date > dateFrom && item.Date < dateTo);

			Assert.Equal(expected: count, actual.Count);
		}

		[Fact]
		public void AddFilterTest()
		{
			service.RemoveAllFilter();

			service.AddFilter(new RegionFilter(["Москва"]));

			Output.WriteLine("Filter count: " + service.FilterCount.ToString());
			Assert.True(service.FilterCount == 1);
		}
		[Fact]
		public void TryAddSameFilterTest()
		{
			service.AddFilter(new RegionFilter(["Москва"]));
			service.AddFilter(new RegionFilter(["Москва"]));
			int expected = 1;

			Assert.Equal(expected, service.FilterCount);
		}
		[Fact]
		public void TryReplaseFilterTest()
		{
			service.AddFilter(new RegionFilter(["Москва"]));
			service.AddFilter(new RegionFilter(["Псков"]));
			string expected = "Псков";

			var type = service.GetType();
			var flag = BindingFlags.Instance | BindingFlags.NonPublic;
			var field = type.GetProperty("Filter", flag);
			var filter = (List<IFilter>)field.GetValue(service);
			var regionFilter = filter.First() as RegionFilter;

			Assert.Equal(expected, regionFilter.RegionList.First());
		}
		[Fact]
		public void CountFiltersAfterReplaseTest()
		{
			service.AddFilter(new RegionFilter(["Москва"]));
			service.AddFilter(new RegionFilter(["Псков"]));
			int expected = 1;

			Assert.Equal(expected, service.FilterCount);
		}

		void IDisposable.Dispose()
		{
			service.RemoveAllFilter();

			GC.SuppressFinalize(this);
		}
	}
}
