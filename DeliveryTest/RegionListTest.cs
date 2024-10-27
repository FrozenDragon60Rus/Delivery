using Delivery;
using Delivery.sql;
using Moq;
using Xunit.Abstractions;


namespace DeliveryTest
{
	public class RegionListTest : IDisposable
	{
		private readonly RegionList service;

		private ITestOutputHelper Output { get; }

		public RegionListTest(ITestOutputHelper output)
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
		public async Task GetTest()
		{
			List<string> expected = ["Москва", "Санкт-Петербург", "Псков"];

			var get = service.Get();
			var actual = (List<string>)get;

			Assert.Equal(expected, actual);
		}

		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
