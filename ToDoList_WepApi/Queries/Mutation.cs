using GraphQL;
using GraphQL.Types;
using ToDoList.Shared.Interfaces;
using ToDoList.Shared.Repositories;
using ToDoList.Shared.Models;
using ToDoList_WepApi.Helpers;
using ToDoList_WepApi.Models;

namespace ToDoList_WepApi.Queries
{
    public class Mutation : ObjectGraphType
    {
        private ITaskRepositoryFactory _taskFactory;

        private ICategoryRepository _categoryRepo;

        private DataStorageHelper _helper;

        public Mutation(ITaskRepositoryFactory taskFactory, ICategoryRepository categoryRepo, DataStorageHelper helper)
        {
            _taskFactory = taskFactory;

            _categoryRepo = categoryRepo;

            _helper = helper;

            string storageType = helper.GetStorageHeaderValue();

            Field<StringGraphType>("CreateTask")
                .Argument<TaskInputType>("Task")
                .Resolve(context =>
                {
                    var taskInput = context.GetArgument<TaskModel>("Task");

                    var newTask = new TaskModel()
                    {
                        Title = taskInput.Title,
                        DueDate = taskInput.DueDate,
                        CategoryId = taskInput.CategoryId,
                        StartDate = DateTime.Now,
                        IsActive = true,
                    };

                    _taskFactory.CreateRepository(storageType).AddTask(newTask);

                    return "Task successfully created.";
                });

            Field<StringGraphType>("RemoveTask")
                .Argument<IntGraphType>("id")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("id");

                    _taskFactory.CreateRepository(storageType).RemoveTask(id);

                    return "Task successfully removed.";
                });

            Field<StringGraphType>("MarkAsDone")
                .Argument<IntGraphType>("id")
                .Resolve(context =>
                {
                    int id = context.GetArgument<int>("id");

                    _taskFactory.CreateRepository(storageType).MarkAsDone(id);

                    return "Task marked as done.";
                });
        }
    }
}
