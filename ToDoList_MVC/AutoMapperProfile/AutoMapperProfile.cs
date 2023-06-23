using AutoMapper;
using Dapper;
using System.Data.SqlClient;
using ToDoList.Shared.Models;
using ToDoList_MVC.Models;

namespace ToDoList_MVC.AutoMapperProfile
{
    public class AutoMapperProfile
    {
        public class AutoMapperProfle : Profile
        {
            public AutoMapperProfle()
            {
                CreateMap<TaskModel, TaskViewModel>()
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => GetCategoryName(src.CategoryId)));

                CreateMap<CreateTaskViewModel, TaskModel>()
                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.Now));

            }

            private string GetCategoryName(int categoryId)
            {
                using (SqlConnection connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;DataBase=ToDoList;Trusted_Connection = true;"))
                {
                    string query = "SELECT Name FROM Categories WHERE Id = @CategoryId";

                    return connection.QueryFirstOrDefault<string>(query, new { CategoryId = categoryId });
                }
            }
        }
    }
}
