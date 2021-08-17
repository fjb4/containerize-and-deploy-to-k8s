using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using dotnet_demo.Models;

namespace dotnet_demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation($"Index page visited at {DateTime.UtcNow.ToLongTimeString()}");
            ViewBag.SqlTableNames = GetSqlServerTableNames();
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

        private List<string> GetSqlServerTableNames()
        {
            var tableNames = new List<string>();
        
            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = "mssql-deployment",
                    UserID = "sa",
                    Password = "pass@word1",
                    InitialCatalog = "master",
                    ConnectTimeout = 2
                };
        
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
        
                    using (var command = new SqlCommand("SELECT TABLE_NAME FROM information_schema.tables", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableNames.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                tableNames.Add(e.Message);
            }
        
            return tableNames;
        }
    }
}
