using System.Diagnostics;
using System.Text;

namespace Delivery
{
    static public class Log
    {
        static private readonly string _deliveryOrder = @"log\log.txt";
		private static string CurrentDateTime => 
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		static public void Create()
        {
            try
            {
                var path = Path.GetDirectoryName(_deliveryOrder);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path!);

                using StreamWriter logWriter = new(
                    File.Open(
                        path: _deliveryOrder,
                        FileMode.OpenOrCreate,
                        FileAccess.ReadWrite,
                        FileShare.Write),
                        Encoding.Unicode);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"При создании файла логов произошла ошибка: {ex}");
            }
		}

        /// <summary>
        /// Запись логов о фильтрации данных
        /// </summary>
        /// <param name="filter">Применённый фильтр</param>
        /// <param name="value">Параметры фильтрации</param>
        /// <param name="count">Количество полученных записей</param>
        static public async Task WriteFilter(string filter, string value, int count) =>
            await Write(@$"{CurrentDateTime}: таблица отфильтрована по: {filter} с параметром ""{value}""; найдено совпадений: {count}.");
		/// <summary>
		/// Запись логов о фильтрации данных
		/// </summary>
		/// <param name="filter">Применённый фильтр</param>
		/// <param name="value">Параметры фильтрации</param>
		/// <param name="count">Количество полученных записей</param>
		static public async Task WriteFilter(string filter, IEnumerable<string> value, int count) =>
			await Write(@$"{CurrentDateTime}: таблица отфильтрована по: {filter} с параметром ""{string.Join(", ", value)}""; найдено совпадений: {count}.");
		/// <summary>
        /// Запись логов о сбросе фильтрации
        /// </summary>
        static public async Task WriteResetFilter() =>
            await Write(@$"{CurrentDateTime}: фильтры сброшены, таблица возвращена в исходное состояние");
		/// <summary>
		/// Запись логов о запуске приложения
		/// </summary>
		static public async Task WriteInitSucces() =>
            await Write($@"{CurrentDateTime}: вход выполнен успешно");
		/// <summary>
		/// Запись логов о поключении к базе данных
		/// </summary>
		static public async Task WriteDBConnection(bool canConnect, string db) {
            if (canConnect) await Write($@"{CurrentDateTime}: cоединение с базой данных {db} успешно устанолено");
            else await Write($@"{CurrentDateTime}: отсутствует подключение к базе данных {db}");
        }
        /// <summary>
        /// Запись логов об ошибке загрузки логов
        /// </summary>
        /// <param name="param">параметр вызвавший ошибку</param>
		static public async Task WriteWrongSettings(string param) =>
			await Write($@"{CurrentDateTime}: Попытка загрузить старые или испорченные настройки с параметром ""{param}""");
		/// <summary>
		/// Запись логов об ошибке загрузки логов
		/// </summary>
		static public async Task WriteWrongSettings(Exception ex) =>
			await Write($@"{CurrentDateTime}: Загрузка настроект произошла с ошибкой: {ex.Message}");

		/// <summary>
		/// Запись логов в файл.
		/// В случае ошибки с интервалом 0.5с. происходит повторная попытка записи.
		/// После 5 попыт, запись логов отменяется
		/// </summary>
		/// <param name="log">Текстовая строка передаваемая для записи в файл</param>
		private static async Task Write(string log)
        {
            int count = 0,
                tryCount = 5;
            do
            {
                try
                {
                    using StreamWriter logWriter = new(
                        File.Open(
                            path: _deliveryOrder, 
                            FileMode.Append, 
                            FileAccess.Write, 
                            FileShare.Write), 
                        Encoding.Unicode);

                    await logWriter.WriteLineAsync(log);
                    return;
                }
                catch (Exception e)
                {
					Debug.WriteLine(e.Message);
					await Task.Delay(500);
                    count++;
                }
            } while (count < tryCount);

            Debug.WriteLine($"После {count} попыток, записать данные в {_deliveryOrder}, так и не удалось");
		}
    }
}
