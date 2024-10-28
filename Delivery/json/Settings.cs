using Delivery.sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Delivery.json
{
	internal class Settings
	{
		private string _settings = "settings.json";

		private IEnumerable<string> region = [];
		private DateTime firstDeliveryDate;
		private DateTime lastDeliveryDate;
		private int orderCount;

		public IEnumerable<string> Region 
		{ 
			get => region;
			set
			{
				region = value;
				Save();
			} 
		}
		public DateTime LastDeliveryDate
		{
			get => lastDeliveryDate;
			set
			{
				lastDeliveryDate = value;
				Save();
			}
		}
		public DateTime FirstDeliveryDate
		{
			get => firstDeliveryDate;
			set
			{
				firstDeliveryDate = value;
				Save();
			}
		}
		public int OrderCount
		{
			get => orderCount;
			set
			{
				orderCount = value;
				Save();
			}
		}

		public Settings()
		{
			var DateTimeList = GetDate();

			firstDeliveryDate = DateTimeList.Min() ?? DateTime.MinValue;
			lastDeliveryDate = DateTimeList.Max() ?? DateTime.MaxValue;
		}

		/// <summary>
		/// Записывает настройки в файл
		/// </summary>
		private async void Save()
		{
			int count = 0,
				tryCount = 5;

			do
			{
				try
				{
					string jsonString = JsonSerializer.Serialize(this);
					File.WriteAllText(_settings, jsonString);
					return;
				}
				catch (Exception e)
				{
					Debug.WriteLine(e.Message);
					await Task.Delay(500);
					count++;
				}
			} while (count < tryCount);
		}

		/// <summary>
		/// Создаёт файл с настройками
		/// </summary>
		public void Create()
		{
			try
			{
				if (File.Exists(_settings))
					return;

				using (var file = File.Create(_settings));
			}
			catch(Exception ex)
			{
				MessageBox.Show($"При создании файла найстроек произошла ошибка: {ex.Message}");
			}
		}

		/// <summary> Выгрузка настроек из файла </summary>
		/// <returns>
		/// Получает настройки фильтрации, 
		/// при отсутствии файла влзвращается null
		/// </returns>
		public Settings Load()
		{
			Settings? settings = null;

			try
			{
				string file = File.ReadAllText(_settings);
				settings = JsonSerializer.Deserialize<Settings>(file);
			}
			catch(Exception ex) 
			{
				Task.Factory.StartNew(async () =>
					await Log.WriteWrongSettings(ex));
			}

			if (settings == null)
				return this;
			return settings;
		}

		/// <summary>
		/// Возвращает настройки к дефолтным
		/// </summary>
		public void Reset()
		{
			var DateTimeList = GetDate();

			region = [];
			firstDeliveryDate = DateTimeList.Min() ?? DateTime.MinValue;
			lastDeliveryDate = DateTimeList.Max() ?? DateTime.MaxValue;
			orderCount = 0;

			Save();
		}

		/// <summary>
		/// Получает даты доставок
		/// </summary>
		private IQueryable<DateTime?>? GetDate()
		{
			OrderContext context = new();
			context.Order.Load();

			var DateTimeList = context.Order.Select(x => x.Date);

			return DateTimeList;
		}
	}
}
