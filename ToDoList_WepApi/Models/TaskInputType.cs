using GraphQL.Types;
using ToDoList.Shared.Models;

namespace ToDoList_WepApi.Models
{
    public class TaskInputType : InputObjectGraphType<TaskModel>
    {
        public TaskInputType()
        {
            Field(x => x.Title);
            Field(x => x.DueDate);
            Field(x => x.CategoryId);
        }
    }
}
