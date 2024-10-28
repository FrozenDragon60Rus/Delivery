using Delivery;
using System.Diagnostics.CodeAnalysis;
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
			var actual = control.CheckedItems.ToList();

			for (int i = 0; i < actual.Count; i++)
				Assert.True(expected[i] == actual[i]);
		}

		[Fact]
		public void CheckedItemsWithAllCheckedTest()
		{
			List<string> expected = ["Москва", "Санкт-Петербург"];
			ItemCheckEventArgs e = new(2, CheckState.Unchecked, CheckState.Checked);
			var actual = control.CheckedItems(e);

			for (int i = 0; i < actual.Count(); i++)
				Assert.True(expected[i] == actual.ElementAt(i));
		}
		[Fact]
		public void CheckedItemsWithUncheckedTest()
		{
			control.SetItemChecked(2, false);

			List<string> expected = ["Москва", "Санкт-Петербург", "Псков"];
			ItemCheckEventArgs e = new(2, CheckState.Checked, CheckState.Unchecked);
			var actual = control.CheckedItems(e);

			for (int i = 0; i < actual.Count(); i++)
				Assert.True(expected[i] == actual.ElementAt(i));
		}
		[Fact]
		public void ReplaceItemTest()
		{
			IEnumerable<string> expected = ["Псков"];
			control.Items.Replace(expected);

			Assert.Equal(expected.First(), control.Items[0]);
		}
		[Fact]
		public void ReplaceItemsCountTest()
		{
			IEnumerable<string> region = ["Псков"];
			control.Items.Replace(region);

			int expected = 1;

			int actual = control.Items.Count;

			Assert.Equal(expected, actual);
		}

		[Theory,
			InlineData(new string[] { "Псков" }, new bool[] { false, false, true }),
			InlineData(new string[] { "Псков", "Москва" }, new bool[] { true, false, true })]
		public void SetItemCheckedTest(string[] region, bool[] expected)
		{
			for(int i = 0; i < control.Items.Count; i++)
				control.SetItemChecked(i, false);

			control.SetItemChecked(region);

			bool[] actual =
				[
					control.GetItemChecked(0),
					control.GetItemChecked(1),
					control.GetItemChecked(2)
				];

			for(int i = 0; i < actual.Length; i++)
				Assert.Equal(expected[i], actual[i]);
		}

		void IDisposable.Dispose()
		{
			GC.SuppressFinalize(this);
		}
	}
}
