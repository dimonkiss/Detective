using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient; // Додайте цю директиву для використання SqlConnection
using DetectiveAgencyProject.Models;
using System.Collections.Generic; // Для використання List<T>

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

        public IActionResult Index()
        {
            var detectives = GetDetectivesWithSpecialization("Fraud Investigation");
            var totalOrders = GetTotalOrdersInLastMonth();


            ViewData["Detectives"] = detectives;
            ViewData["TotalOrders"] = totalOrders;

            return View();
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

        private List<string> GetDetectivesWithSpecialization(string specialization)
        {
            var result = new List<string>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT Name FROM Detectives WHERE Specialization = @specialization", connection))
                {
                    command.Parameters.AddWithValue("@specialization", specialization);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(reader["Name"].ToString());
                        }
                    }
                }
            }
            return result;
        }

        private int GetTotalOrdersInLastMonth()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT COUNT(*) FROM Orders WHERE Deadline >= DATEADD(MONTH, -1, GETDATE())", connection))
                {
                    return (int)command.ExecuteScalar();
                }
            }
        }


    }
}
