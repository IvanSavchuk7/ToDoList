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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string? _connectionString;

        public CategoryRepository(IConfiguration cfg)
        {
            _connectionString = cfg.GetConnectionString("Default");
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Categories";

                return connection.Query<CategoryModel>(query).ToList();
            }
        }
    }
}
