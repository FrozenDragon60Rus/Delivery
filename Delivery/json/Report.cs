using Delivery.sql.Table;
using System.Text.Json;

namespace Delivery.json
{
    internal class Report
    {
        string _deliveryOrder = "report.json";

        public Report(IEnumerable<JoinOrder> data)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(data);
                File.WriteAllText(_deliveryOrder, jsonString);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Во время записи отчёта произошёл сбой: {ex.Message}");
            }
		}
    }
}
