using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToDoList.Shared.Interfaces;
using ToDoList.Shared.Models;

namespace ToDoList.Shared.Repositories
{
    public class XMLTaskRepository : ITaskRepository
    {
        private XDocument? _doc;

        private string? _filePath;

        public XMLTaskRepository(IConfiguration cfg)
        {
            _filePath = cfg.GetSection("XMLStoragePath").Value;

            _doc = InitializeRepository(_filePath);
        }

        public void AddTask(TaskModel newTask)
        {
            XElement xmlTask = new XElement("Task",
                new XElement("Id", SetTaskId()),
                new XElement("Title", newTask.Title),
                new XElement("StartDate", newTask.StartDate),
                new XElement("DueDate", newTask.DueDate),
                new XElement("IsActive", newTask.IsActive),
                new XElement("CategoryId", newTask.CategoryId)
            );

            _doc.Element("Tasks").Add(xmlTask);

            _doc.Save(_filePath);
        }


        public IEnumerable<TaskModel> GetAllTasks()
        {
            var xmlAllTasks = _doc.Element("Tasks").Elements("Task");

            IEnumerable<TaskModel> xmlTasksList = xmlAllTasks.Select(
                task => new TaskModel
                {
                    Id = (int)task.Element("Id")!,
                    Title = (string)task.Element("Title")!,
                    StartDate = (DateTime)task.Element("StartDate")!,
                    DueDate = (DateTime)task.Element("DueDate")!,
                    IsActive = (bool)task.Element("IsActive")!,
                    CategoryId = (int)task.Element("CategoryId")!
                });

            return xmlTasksList;
        }

        public IEnumerable<TaskModel> GetActiveTasks()
        {
            var xmlActiveTasks = _doc.Element("Tasks")
                                .Elements("Task")
                                .Where(task => bool.Parse(task.Element("IsActive").Value) == true);

            List<TaskModel> xmlTasksList = new List<TaskModel>();

            if (xmlActiveTasks != null)
            {
                foreach (XElement task in xmlActiveTasks)
                {
                    xmlTasksList.Add(new TaskModel
                    {
                        Id = (int)task.Element("Id")!,
                        Title = (string)task.Element("Title")!,
                        StartDate = (DateTime)task.Element("StartDate")!,
                        DueDate = (DateTime)task.Element("DueDate")!,
                        IsActive = (bool)task.Element("IsActive")!,
                        CategoryId = (int)task.Element("CategoryId")!
                    });
                }
            }

            return xmlTasksList;
        }

        public void MarkAsDone(int id)
        {
            XElement? taskDone = _doc.Element("Tasks")
                                    .Elements("Task")
                                    .FirstOrDefault(task => task.Element("Id").Value == id.ToString());

            if (taskDone != null)
            {
                taskDone.Element("IsActive").Value = false.ToString();

                _doc.Save(_filePath);
            }

        }

        public void RemoveTask(int id)
        {
            XElement? taskToRemove = _doc.Element("Tasks")
                                    .Elements("Task")
                                    .FirstOrDefault(task => task.Element("Id").Value == id.ToString());

            if (taskToRemove != null)
            {
                taskToRemove.Remove();
                _doc.Save(_filePath);
            }
        }

        private int SetTaskId()
        {
            int id;

            if (_doc.Element("Tasks").Elements("Task").Any())
            {
                id = _doc.Element("Tasks").Elements("Task").Max(t => (int)t.Element("Id")) + 1;
            }
            else
            {
                id = 1;
            }

            return id;
        }

        private XDocument InitializeRepository(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            if (!File.Exists(filePath))
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    new XDocument(new XElement("Tasks")).Save(fs);
                }
            }

            return XDocument.Load(filePath);
        }
    }
}
