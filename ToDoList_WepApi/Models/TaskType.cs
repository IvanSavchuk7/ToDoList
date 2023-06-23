using GraphQL.Types;
using ToDoList.Shared.Models;

namespace ToDoList_WepApi.Models
{
    public class TaskType : ObjectGraphType<TaskModel>
    {
        public TaskType()
        {
            Field(x => x.Id);
            Field(x => x.Title);
            Field(x => x.StartDate);
            Field(x => x.DueDate);
            Field(x => x.IsActive);
            Field(x => x.CategoryId);
        }
    }
}
