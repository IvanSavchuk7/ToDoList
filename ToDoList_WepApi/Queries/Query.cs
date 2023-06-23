using GraphQL;
using GraphQL.Types;
using ToDoList.Shared.Interfaces;
using ToDoList.Shared.Repositories;
using ToDoList_WepApi.Helpers;
using ToDoList_WepApi.Models;

namespace ToDoList_WepApi.Queries
{
    public class Query : ObjectGraphType
    {
        private ITaskRepositoryFactory _taskFactory;

        private ICategoryRepository _categoryRepo;

        private DataStorageHelper _helper;

        public Query(ITaskRepositoryFactory taskFactory, ICategoryRepository categoryRepo, DataStorageHelper helper)
        {
            _taskFactory = taskFactory;

            _categoryRepo = categoryRepo;

            _helper = helper;

            Field<ListGraphType<TaskType>>("ActiveTasks")
                .Resolve(context => _taskFactory.CreateRepository(helper.GetStorageHeaderValue()).GetActiveTasks());

            Field<ListGraphType<CategoryType>>("Categories")
                .Resolve(context => _categoryRepo.GetAllCategories());

            Field<TaskType>("Task")
                .Argument<IntGraphType>("id")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("id");

                    return _taskFactory.CreateRepository(helper.GetStorageHeaderValue()).GetAllTasks().Where(t => t.Id == id).FirstOrDefault();
                });

            Field<CategoryType>("Category")
                .Argument<IntGraphType>("id")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("id");

                    return _categoryRepo.GetAllCategories().Where(c => c.Id == id).FirstOrDefault();
                });
        }


    }
}
