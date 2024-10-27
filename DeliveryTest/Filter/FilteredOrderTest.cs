using Delivery.Filter;
using Delivery.sql;
using Delivery.sql.Table;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Globalization;
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
			var MockDBOrder = MockSetup(GetOrderData);

			//Region setup
			var MockDBRegion = MockSetup(GetRegionData);

			//Context setup
			var mockContext = new Mock<OrderContext>();
			mockContext.Setup(c => c.Order).Returns(MockDBOrder);
			mockContext.Setup(c => c.Region).Returns(MockDBRegion);

			service = new(mockContext.Object);
		}

		private DbSet<T> MockSetup<T>(IQueryable<T> data) where T : class
		{
			var MockDb = new Mock<DbSet<T>>();

			MockDb.As<IQueryable<T>>()
				.Setup(o => o.Provider)
				.Returns(data.Provider);
			MockDb.As<IQueryable<T>>()
				.Setup(o => o.Expression)
				.Returns(data.Expression);
			MockDb.As<IQueryable<T>>()
				.Setup(o => o.ElementType)
				.Returns(data.ElementType);
			MockDb.As<IQueryable<T>>()
				.Setup(o => o.GetEnumerator())
				.Returns(data.GetEnumerator());

			return MockDb.Object;
		}

		private IQueryable<Order> GetOrderData =>
			new List<Order> {
				new()
				{
					Id = 1,
					Weight = 1.1f,
					RegionId = 1,
					Date = DateTime.ParseExact(
						"2024-05-08 14:40:52",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 2,
					Weight = 2f,
					RegionId = 2,
					Date = DateTime.ParseExact(
						"2023-12-09 09:45:00",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 3,
					Weight = 5f,
					RegionId = 3,
					Date = DateTime.ParseExact(
						"2024-03-10 23:59:59",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 4,
					Weight = 0.1f,
					RegionId = 3,
					Date = DateTime.ParseExact(
						"2024-02-13 01:49:59",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 5,
					Weight = 1.3f,
					RegionId = 3,
					Date = DateTime.ParseExact(
						"2023-04-02 23:59:59",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 6,
					Weight = 2.4f,
					RegionId = 2,
					Date = DateTime.ParseExact(
						"2024-06-21 23:59:59",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				}
			}.AsQueryable();

		private IQueryable<Region> GetRegionData =>
			new List<Region> {
				new()
				{
					Id = 1,
					Name = "Москва"
				},
				new()
				{
					Id = 2,
					Name = "Санкт-Петербург"
				},
				new()
				{
					Id = 3,
					Name = "Псков"
				}
			}.AsQueryable();

		private IQueryable<JoinOrder> GetJoinData =>
			new List<JoinOrder> {
				new()
				{
					Id = 1,
					Weight = 1.1f,
					Region = "Москва",
					Date = DateTime.ParseExact(
						"2024-05-08 14:40:52",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 2,
					Weight = 2f,
					Region = "Санкт-Петербург",
					Date = DateTime.ParseExact(
						"2023-12-09 09:45:00",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 3,
					Weight = 5f,
					Region = "Псков",
					Date = DateTime.ParseExact(
						"2024-03-10 23:59:59",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 4,
					Weight = 0.1f,
					Region = "Псков",
					Date = DateTime.ParseExact(
						"2024-02-13 01:49:59",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 5,
					Weight = 1.3f,
					Region = "Псков",
					Date = DateTime.ParseExact(
						"2023-04-02 23:59:59",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				},
				new()
				{
					Id = 6,
					Weight = 2.4f,
					Region = "Санкт-Петербург",
					Date = DateTime.ParseExact(
						"2024-06-21 23:59:59",
						"yyyy-MM-dd HH:mm:ss",
						CultureInfo.InvariantCulture)
				}
			}.AsQueryable();

		[Fact]
		public void GetTest()
		{
			var actual = service.Get().ToList();
			var expected = GetJoinData.ToList();

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
