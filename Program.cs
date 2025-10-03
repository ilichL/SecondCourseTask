using System.Diagnostics;

namespace SecondCourseTask
{
    class Program
    {
        public static string ProcessData(string dataName)
        {
            Thread.Sleep(3000);
            return $"Обработка '{dataName}' завершена за 3 секунды (синхронно)";
        }

        public static async Task<string> ProcessDataAsync(string dataName)
        {
            await Task.Delay(3000);
            return $"Обработка {dataName} завершена за 3 секунды";
        }

        static async Task Main()
        {
            Console.WriteLine("Синхронные методы");
            var timeMethodSync = Stopwatch.StartNew();

            Console.WriteLine(ProcessData("Файл 1"));
            Console.WriteLine(ProcessData("Файл 2"));
            Console.WriteLine(ProcessData("Файл 3"));

            timeMethodSync.Stop();
            Console.WriteLine($"Время выполнения синхронных методов: {timeMethodSync.Elapsed.TotalSeconds:F1} сек\n");

            Console.WriteLine("Асинхронные методы");
            var timeMethodAsync = Stopwatch.StartNew();

            var tasks = new[]
            {
                ProcessDataAsync("Файл 1"),
                ProcessDataAsync("Файл 2"),
                ProcessDataAsync("Файл 3")
            }.ToList();

            while (tasks.Any())
            {
                var finished = await Task.WhenAny(tasks);
                tasks.Remove(finished);
                Console.WriteLine(await finished);
            }

            timeMethodAsync.Stop();
            Console.WriteLine($"Время выполнения асинхронных методов: {timeMethodAsync.Elapsed.TotalSeconds:F1} сек");

        }
    }
}
