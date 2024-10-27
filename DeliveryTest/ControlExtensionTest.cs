using Delivery;
using System.Windows.Forms;

namespace DeliveryTest
{
	public class ControlExtensionTest : IDisposable
	{
		CheckedListBox control;

		public ControlExtensionTest()
		{
			control = new CheckedListBox();

			control.Items.Add("Москва");
			control.Items.Add("Санкт-Петербург");
			control.Items.Add("Псков");

			for(int i = 0; i < control.Items.Count; i++)
				control.SetItemChecked(i, true);
		}

		[Fact]
		public void ResetTest()
		{
			control.Reset();
			
			int expected = 0;
			var actual = control.CheckedItems.Count;

			Assert.Equal(expected, actual);
		}

		[Fact]
		public void ToListTest()
		{
			List<string> expected = ["Москва", "Санкт-Петербург", "Псков"];
			var actual = control.CheckedItems.ToList<string>();

			for (int i = 0; i < actual.Count; i++)
				Assert.True(expected[i] == actual[i]);
		}

		[Fact]
		public void CheckedItemsWithAllCheckedTest()
		{
			List<string> expected = ["Москва", "Санкт-Петербург"];
			ItemCheckEventArgs e = new(2, CheckState.Unchecked, CheckState.Checked);
			var actual = control.CheckedItems<string>(e);

			for (int i = 0; i < actual.Count; i++)
				Assert.True(expected[i] == actual[i]);
		}
		[Fact]
		public void CheckedItemsWithUncheckedTest()
		{
			control.SetItemChecked(2, false);

			List<string> expected = ["Москва", "Санкт-Петербург", "Псков"];
			ItemCheckEventArgs e = new(2, CheckState.Checked, CheckState.Unchecked);
			var actual = control.CheckedItems<string>(e);

			for (int i = 0; i < actual.Count; i++)
				Assert.True(expected[i] == actual[i]);
		}

		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
