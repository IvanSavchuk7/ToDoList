namespace ToDoList_MVC.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public string? Category { get; set; }

    }
}
