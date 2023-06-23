namespace ToDoList_MVC.Models
{
    public class CreateTaskViewModel
    {
        public string? Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public int? CategoryId { get; set; }

    }
}
