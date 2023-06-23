using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Interfaces;
using ToDoList.Shared.Models;

namespace ToDoList.Shared.Repositories
{
    public class DbTaskRepository : ITaskRepository
    {
        private readonly string? _connectionString;

        public DbTaskRepository(IConfiguration cfg)
        {
            _connectionString = cfg.GetConnectionString("Default");
        }

        public void AddTask(TaskModel task)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Tasks1 (Title, StartDate, DueDate, CategoryId, IsActive) VALUES (@Title, @StartDate, @DueDate, @CategoryId, @IsActive)";

                connection.Execute(query, task);
            }
        }

        public void MarkAsDone(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tasks1 SET IsActive = 0 WHERE Id = @Id";

                connection.Execute(query, new { Id = id });
            }
        }


        /*---------- UNDONE MAKE PAGE FOR ALL TASKS (ARCHIEVE) ----------*/
        public IEnumerable<TaskModel> GetAllTasks()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks1";

                var res = connection.Query<TaskModel>(query);


                return res.ToList();
            }
        }

        public IEnumerable<TaskModel> GetActiveTasks()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tasks1 WHERE IsActive = 1";

                var res = connection.Query<TaskModel>(query);


                return res.ToList();
            }
        }

        public void RemoveTask(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Tasks1 WHERE id = @Id";

                connection.Execute(query, new { Id = id });
            }
        }
    }
}
