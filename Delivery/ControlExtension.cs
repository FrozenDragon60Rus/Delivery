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
		public static List<T> ToList<T>(this CheckedListBox.CheckedItemCollection collection)
		{
			List<T> items = [];
			
			foreach (var item in collection)
				items.Add((T)item);

			return items;
		}

		/// <summary>Возвращает список выбранных параметров </summary>
		public static List<T> CheckedItems<T>(this CheckedListBox control, ItemCheckEventArgs e)
		{
			List<T> items = control.CheckedItems.ToList<T>();

			var region = (T)control.Items[e.Index];
			if (e.NewValue == CheckState.Checked)
				items.Add(region);
			else
				items.Remove(region);

			return items;
		}
	}
}
