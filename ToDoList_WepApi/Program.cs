using ToDoList.Shared.Repositories;
using ToDoList_WepApi.Models;
using ToDoList_WepApi.Queries;
using GraphQL;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Ui.Altair;
using ToDoList_WepApi.Schemas;
using ToDoList_WepApi.Helpers;
using ToDoList.Shared.Interfaces;

namespace ToDoList_WepApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

            builder.Services.AddSingleton<ITaskRepositoryFactory, TaskRepositoryFactory>();

            builder.Services.AddSingleton<ITaskRepository, XMLTaskRepository>();

            builder.Services.AddSingleton<ITaskRepository, DbTaskRepository>();

            builder.Services.AddSingleton<DataStorageHelper>();

            builder.Services.AddSingleton<TaskType>();

            builder.Services.AddSingleton<CategoryType>();

            builder.Services.AddSingleton<TaskInputType>();

            builder.Services.AddSingleton<Query>();

            builder.Services.AddSingleton<Mutation>();

            builder.Services.AddGraphQL(builder => builder
                .AddSystemTextJson()
                .AddSchema<ToDoListSchema>());

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseGraphQLAltair();

            app.UseGraphQL();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}