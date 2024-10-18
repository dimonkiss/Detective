using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using DetectiveAgencyProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace DetectiveAgencyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString = "Server=d1mon_k1ss;Database=DetectiveAgencyDB;Integrated Security=True;Encrypt=False;Trusted_Connection=True"; // Змініть на свій рядок підключення

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int coreCount)
        {
            // Заповнюємо таблицю даними (100,000 записів)
            // AddRecordsToDetectivesTable(100000);

            // Виконуємо запити та вимірюємо час виконання
            var sequentialTimeFor20 = MeasureSequentialExecution(20);
            var parallelTimeFor20 = MeasureParallelExecution(20);
            var multiCoreTimeFor20 = MeasureMultiCoreExecution(20, coreCount);
            var multiThreadedTimeFor20 = MeasureMultiThreadedExecution(20, coreCount);
            var tplTimeFor20 = MeasureTPLExecution(20, coreCount);
            var sequentialTimeFor100000 = MeasureSequentialExecution(100000);
            var parallelTimeFor100000 = MeasureParallelExecution(100000);
            var multiCoreTimeFor100000 = MeasureMultiCoreExecution(100000, coreCount);
            var multiThreadedTimeFor100000 = MeasureMultiThreadedExecution(100000, coreCount);
            var tplTimeFor100000 = MeasureTPLExecution(100000, coreCount);

            // Створюємо дані для таблиці
            var timings = new List<(string Method, long TimeFor20, long TimeFor100000)>
            {
                ("Послідовне", sequentialTimeFor20, sequentialTimeFor100000),
                ("Паралельне", parallelTimeFor20, parallelTimeFor100000),
                ("Мультиядрове", multiCoreTimeFor20, multiCoreTimeFor100000),
                ("Багатопоточність", multiThreadedTimeFor20, multiThreadedTimeFor100000),
                ("TPL", tplTimeFor20, tplTimeFor100000)
            };

            ViewData["Timings"] = timings;

            return View();
        }

        // Метод для додавання 100 000 записів до таблиці Detectives
        private void AddRecordsToDetectivesTable(int numberOfRecords)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                for (int i = 0; i < numberOfRecords; i++)
                {
                    var command = new SqlCommand("INSERT INTO Detectives (Name, Specialization, Experience, AgencyId) VALUES (@Name, @Specialization, @Experience, @AgencyId)", connection);
                    command.Parameters.AddWithValue("@Name", "Detective " + i);
                    command.Parameters.AddWithValue("@Specialization", "Fraud Investigation");
                    command.Parameters.AddWithValue("@Experience", "5 years");  // Додаємо дані для Experience
                    command.Parameters.AddWithValue("@AgencyId", 1);  // Вказуємо значення для AgencyId

                    command.ExecuteNonQuery();
                }
            }
        }

        // Метод для послідовного виконання запиту
        private long MeasureSequentialExecution(int numberOfRecords)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT TOP {numberOfRecords} * FROM Detectives", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Обробляємо дані
                }
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        // Метод для паралельного виконання запиту
        private long MeasureParallelExecution(int numberOfRecords)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT TOP {numberOfRecords} * FROM Detectives", connection);
                var reader = command.ExecuteReader();

                var data = new List<object>();
                while (reader.Read())
                {
                    data.Add(new
                    {
                        DetectiveId = reader["DetectiveId"],
                        Name = reader["Name"]
                    });
                }

                data.AsParallel().ForAll(item =>
                {
                    var detectiveId = item.GetType().GetProperty("DetectiveId").GetValue(item, null);
                });
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        // Метод для мультиядрового виконання
        private long MeasureMultiCoreExecution(int numberOfRecords, int coreCount)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT TOP {numberOfRecords} * FROM Detectives", connection);
                var reader = command.ExecuteReader();

                var data = new List<object>();
                while (reader.Read())
                {
                    data.Add(new
                    {
                        DetectiveId = reader["DetectiveId"],
                        Name = reader["Name"]
                    });
                }

                // Встановлюємо кількість потоків
                ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = coreCount };

                Parallel.ForEach(data, parallelOptions, item =>
                {
                    var detectiveId = item.GetType().GetProperty("DetectiveId").GetValue(item, null);
                });
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        // Метод для багатопоточної обробки
        private long MeasureMultiThreadedExecution(int numberOfRecords, int coreCount)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT TOP {numberOfRecords} * FROM Detectives", connection);
                var reader = command.ExecuteReader();

                var data = new List<object>();
                while (reader.Read())
                {
                    data.Add(new
                    {
                        DetectiveId = reader["DetectiveId"],
                        Name = reader["Name"]
                    });
                }

                // Встановлюємо кількість потоків
                var tasks = new List<Task>();
                foreach (var item in data)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        var detectiveId = item.GetType().GetProperty("DetectiveId").GetValue(item, null);
                    }));
                }

                Task.WaitAll(tasks.ToArray());
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        // Метод для TPL
        private long MeasureTPLExecution(int numberOfRecords, int coreCount)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT TOP {numberOfRecords} * FROM Detectives", connection);
                var reader = command.ExecuteReader();

                var data = new List<object>();
                while (reader.Read())
                {
                    data.Add(new
                    {
                        DetectiveId = reader["DetectiveId"],
                        Name = reader["Name"]
                    });
                }

                // Встановлюємо кількість потоків
                var results = data.AsParallel()
                    .WithDegreeOfParallelism(coreCount)
                    .Select(item =>
                    {
                        return item.GetType().GetProperty("DetectiveId").GetValue(item, null);
                    }).ToArray();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
