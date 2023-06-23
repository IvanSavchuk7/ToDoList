using ToDoList.Shared.Models;

namespace ToDoList_MVC.Models
{
    public class IndexViewModel
    {
        public CreateTaskViewModel NewTask { get; set; } = null!;

        public IEnumerable<CategoryModel> Categories { get; set; } = null!;

        public IEnumerable<TaskViewModel> TaskList { get; set; } = null!;
    }
}
