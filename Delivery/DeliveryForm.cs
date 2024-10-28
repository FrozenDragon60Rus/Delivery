using Delivery.Filter;
using Delivery.json;
using Delivery.sql;

namespace Delivery
{
	public partial class DeliveryForm : Form
	{
		private readonly OrderFilterController order = new(new OrderContext());
		private Settings? settings = new();

		public DeliveryForm()
		{
			InitializeComponent();
			Init();
		}

		/// <summary>
		/// Инициализация компонентоа формы
		/// </summary>
		private void Init()
		{
			FileInit();

			DataGridFormat();

			RegionInit();

			LoadSetting();

			View();
		}

		/// <summary>Передаёт список регионов в CheckListBox</summary>
		private void RegionInit()
		{
			try
			{
				((ListBox)CLBRegion).DataSource = new RegionList(new OrderContext()).Get();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка подключения к базе данных: {ex.Message}");
				Task.Factory.StartNew(
					async () => 
						await Log.WriteDBConnection(false, "Delivery"));
			}

			Task.Factory.StartNew(
				async () =>
					await Log.WriteInitSucces());
			
		}

		/// <summary>Вывод даты в формате yyyy-MM-dd hh:mm:ss</summary>
		private void DataGridFormat()
		{
			for (int i = 0; i < DGDelivery.ColumnCount; i++)
				if (DGDelivery.Columns[i].HeaderText == "Date")
					DGDelivery.Columns[i].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
		}

		/// <summary>Вывод информации в DataGridView</summary>
		private void View() =>
			DGDelivery.DataSource = order.Get().ToList();

		private void DTPFrom_ValueChanged(object sender, EventArgs e)
		{
			if (DTPFrom.Value > DTPTo.Value)
				DTPFrom.Value = DTPTo.Value;

			//Добавление фильтрации по времени
			var filter = new FirstDeliveryDateFilter(DTPFrom.Value);
			order.Update(filter);

			View();

			settings.FirstDeliveryDate = DTPFrom.Value;

			Task.Factory.StartNew(
				async () =>
					await Log.WriteFilter(
						filter.Type.ToString(),
						DTPFrom.Value.ToString("yyyy-MM-dd HH:mm:ss")!,
						DGDelivery.RowCount));
		}

		private void DTPTo_ValueChanged(object sender, EventArgs e)
		{
			if (DTPTo.Value < DTPFrom.Value)
				DTPTo.Value = DTPFrom.Value;

			//Добавление фильтрации по времени
			var filter = new LastDeliveryDateFilter(DTPTo.Value);
			order.Update(filter);

			View();

			settings.LastDeliveryDate = DTPTo.Value;

			Task.Factory.StartNew(
				async () =>
					await Log.WriteFilter(
						filter.Type.ToString(),
						DTPFrom.Value.ToString("yyyy-MM-dd HH:mm:ss")!,
						DGDelivery.RowCount));
			
		}

		private void NOrderCount_ValueChanged(object sender, EventArgs e)
		{
			//Добавление фильтрации по количеству заказов
			int count = (int)NOrderCount.Value;

			var filter = new OrderCountFilter(count);
			order.Update(filter);

			View();

			settings.OrderCount = count;

			Task.Factory.StartNew(
				async () =>
					await Log.WriteFilter(
						Filter.Type.OrderCount.ToString(),
						NOrderCount.Value.ToString()!,
						DGDelivery.RowCount));
		}

		private void CLBRegion_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			var items = CLBRegion.CheckedItems(e);

			var filter = new RegionFilter(items);
			order.Update(filter);

			View();

			settings!.Region = items;

			Task.Factory.StartNew(
				async () =>
					await Log.WriteFilter(
						filter.Type.ToString(),
						items,
						DGDelivery.RowCount));
		}

		private void ResetButton_Click(object sender, EventArgs e)
		{
			//Сброс к исходному состоянию
			DTPFrom.Value = DateTime.Now;
			DTPTo.Value = DateTime.Now;
			CLBRegion = CLBRegion.Reset();
			NOrderCount.Value = 0;
			order.RemoveAllFilter();
			settings!.Reset();

			View();

			Task.Factory.StartNew(
				async () =>
					await Log.WriteResetFilter());
		}

		/// <summary>
		/// Создание файла логов, и настроект
		/// </summary>
		private void FileInit()
		{
			Log.Create();
			settings!.Create();
		}

		/// <summary>
		/// Загрузка параметров фильтрования
		/// </summary>
		public void LoadSetting()
		{
			settings = settings!.Load();

			if (settings == null)
				return;

			DTPFrom.Value = settings.FirstDeliveryDate;
			DTPTo.Value = settings.LastDeliveryDate;
			CLBRegion.SetItemChecked(settings.Region.ToList());
			NOrderCount.Value = settings.OrderCount;
		}

		private void ReportButton_Click(object sender, EventArgs e)
		{
			new Report(order.Get());

			MessageBox.Show("Данные выгружены");
		}
	}
}
