using System.Diagnostics;

namespace Delivery
{
	public static class CheckedListBoxExtension
	{
		/// <summary>Убирает галочки со всех CheckBox </summary>
		public static CheckedListBox Reset(this CheckedListBox control) 
		{
			for (int i = 0; i < control.Items.Count; i++)
				control.SetItemChecked(i, false);

			return control;
		}

		/// <summary>Возвращает данные в виде списка List </summary>
		public static List<string> ToList(this CheckedListBox.CheckedItemCollection collection)
		{
			List<string> items = [];
			
			foreach (var item in collection)
				items.Add((string)item);

			return items;
		}

		/// <summary>Возвращает список выбранных параметров </summary>
		public static IEnumerable<string> CheckedItems(this CheckedListBox control, ItemCheckEventArgs e)
		{
			List<string> items = control.CheckedItems.ToList();

			var region = control.Items[e.Index];
			if (e.NewValue == CheckState.Checked)
				items.Add((string)region);
			else
				items.Remove((string)region);

			return items;
		}

		/// <summary>Меняет список  </summary>
		public static void Replace(this CheckedListBox.ObjectCollection items, 
			IEnumerable<string> newItems)
		{
			items.Clear();
			items.AddRange(newItems.ToArray());
		}

		public static void SetItemChecked(this CheckedListBox control, IEnumerable<string> items)
		{
			int index;
			foreach (var item in items)
			{
				index = control.Items.IndexOf(item); Debug.WriteLine(item);
				if (index < 0)
				{
					var task = Task.Factory.StartNew(async () =>
							await Log.WriteWrongSettings(item)
					);
					continue;
				}
				control.SetItemChecked(index, true);
			}
		}
	}
}
