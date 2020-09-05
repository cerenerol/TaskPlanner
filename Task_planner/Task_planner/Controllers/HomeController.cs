using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Task_planner.Models;

namespace Task_planner.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index()
        {
            List<Tasks> taskList = new List<Tasks>();

            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                string sql = "Select * From Tasks";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Tasks task = new Tasks();
                        task.TaskId = Convert.ToInt32(dataReader["Id"]);
                        task.TaskName = Convert.ToString(dataReader["Name"]);
                        task.Detail = Convert.ToString(dataReader["Detsil"]);
                        task.CreatedAt = Convert.ToDateTime(dataReader["Created"]);

                        taskList.Add(task);
                    }
                }

                connection.Close();
            }
            return View(taskList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tasks task)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Tasks (Name, Detail) Values ('{task.TaskName}','{task.Detail}',')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
                return View();
        }

        public IActionResult Update(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];

            Tasks task = new Tasks();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Tasks Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        task.TaskId = Convert.ToInt32(dataReader["Id"]);
                        task.TaskName = Convert.ToString(dataReader["Name"]);
                        task.Detail = Convert.ToString(dataReader["Detsil"]);
                        task.CreatedAt = Convert.ToDateTime(dataReader["Created"]);
                    }
                }

                connection.Close();
            }
            return View(task);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update_Post(Tasks task)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Task SET Name='{task.TaskName}', Detail='{task.Detail}',', Where Id='{task.TaskId}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Task Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Operation got error:" + ex.Message;
                    }
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
    }
}
