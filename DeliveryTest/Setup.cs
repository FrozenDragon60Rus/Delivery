using Delivery.sql;
using Delivery.sql.Table;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Globalization;

namespace DeliveryTest
{
	static public class Setup
	{
		public static DbSet<T> DbSet<T>(IQueryable<T> data) where T : class
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

		public static IQueryable<Order> GetOrderData =>
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

		public static IQueryable<Region> GetRegionData =>
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

		public static IQueryable<JoinOrder> GetJoinData =>
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
	}
}
