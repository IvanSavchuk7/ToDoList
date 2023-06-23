using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Shared.Models;

namespace ToDoList.Shared.Interfaces
{
    public interface ITaskRepository
    {
        public IEnumerable<TaskModel> GetAllTasks();

        public IEnumerable<TaskModel> GetActiveTasks();

        public void AddTask(TaskModel task);

        public void RemoveTask(int id);

        public void MarkAsDone(int id);
    }
}
