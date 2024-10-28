using Delivery.Filter;
using Delivery.sql;
using System.Diagnostics;

namespace Delivery
{
	public partial class DeliveryForm : Form
	{
		readonly OrderFilterController order = new(new OrderContext());

		public DeliveryForm()
		{
			InitializeComponent();
			Init();
		}

		/// <summary>
		/// Инициализация компонентоа формы
		/// </summary>
		private async void Init()
		{
			Log.Create();

			DataGridFormat();

			await RegionInit();

			View();
		}

		/// <summary>Передаёт список регионов в CheckListBox</summary>
		private async Task RegionInit()
		{
			((ListBox)CLBRegion).DataSource = new RegionList(new OrderContext()).Get();

			await Log.WriteInitSucces();
		}

		/// <summary>Вывод даты в формате yyyy-MM-dd hh:mm:ss</summary>
		private void DataGridFormat()
		{
			for (int i = 0; i < DGDelivery.ColumnCount; i++)
				if (DGDelivery.Columns[i].HeaderText == "Date")
					DGDelivery.Columns[i].DefaultCellStyle.Format = "yyyy-MM-dd hh:mm:ss";
		}

		/// <summary>Вывод информации в DataGridView</summary>
		private void View() =>
			DGDelivery.DataSource = order.Get().ToList();

		private async void DTPFrom_ValueChanged(object sender, EventArgs e)
		{
			if (DTPFrom.Value > DTPTo.Value)
				DTPFrom.Value = DTPTo.Value;

			//Добавление фильтрации по времени
			var filter = new DateFromFilter(DTPFrom.Value);
			order.AddFilter(filter);

			View();

			await Log.WriteFilter(
				filter.Type.ToString(),
				DTPFrom.Value.ToString("yyyy-MM-dd hh:mm:ss")!,
				DGDelivery.RowCount);
		}

		private async void DTPTo_ValueChanged(object sender, EventArgs e)
		{
			if (DTPTo.Value < DTPFrom.Value)
				DTPTo.Value = DTPFrom.Value;

			//Добавление фильтрации по времени
			var filter = new DateToFilter(DTPTo.Value);
			order.AddFilter(filter);

			View();

			await Log.WriteFilter(
				filter.Type.ToString(),
				DTPFrom.Value.ToString("yyyy-MM-dd hh:mm:ss")!,
				DGDelivery.RowCount);
		}

		private async void NOrderCount_ValueChanged(object sender, EventArgs e)
		{
			//Добавление фильтрации по количеству заказов
			var filter = new OrderCountFilter((int)NOrderCount.Value);
			order.AddFilter(filter);

			View();

			await Log.WriteFilter(
				filter.Type.ToString(),
				NOrderCount.Value.ToString()!,
				DGDelivery.RowCount);
		}

		private async void ResetButton_Click(object sender, EventArgs e)
		{
			//Сброс к исходному состоянию
			DTPFrom.Value = DateTime.Now;
			DTPTo.Value = DateTime.Now;
			CLBRegion = CLBRegion.Reset();
			order.RemoveAllFilter();

			View();

			await Log.WriteResetFilter();
		}

		private async void CLBRegion_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			List<string> items = CLBRegion.CheckedItems<string>(e);

			UpdateRegionFilter(items);

			View();

			await Log.WriteFilter(
				Filter.Type.Region.ToString(),
				items,
				DGDelivery.RowCount);
		}

		/// <summary>
		/// Обление фильтрации по регионами.
		/// Если фильтра нет, он будет добавлен. Если фильтр есть, он будет удалён.
		/// </summary>
		/// <param name="items">Парамерты фильра</param>
		private void UpdateRegionFilter(IEnumerable<string> items)
		{
			if (items.Any())
			{
				var filter = new RegionFilter(items);
				order.AddFilter(filter);
			}
			else
				order.RemoveFilter(Filter.Type.Region);
		}
	}
}
