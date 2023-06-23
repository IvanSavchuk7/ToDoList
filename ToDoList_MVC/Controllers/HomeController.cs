using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList_MVC.Models;
using ToDoList.Shared.Interfaces;
using AutoMapper;
using ToDoList.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ToDoList_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private ICategoryRepository _categoryRepo;

        private IMapper _mapper;

        private ITaskRepositoryFactory _taskFactory;

        private IHttpContextAccessor _contextAccessor;


        public HomeController(ILogger<HomeController> logger, IMapper mapper, ICategoryRepository categoryRepo,
                                ITaskRepositoryFactory taskFactory, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;

            _taskFactory = taskFactory;

            _categoryRepo = categoryRepo;

            _mapper = mapper;

            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {

            ITaskRepository taskRepo = _taskFactory.CreateRepository(GetCookieValue());
            bool isDescending = (bool)(TempData["isDescending"] ?? false);

            var model = new IndexViewModel() { Categories = _categoryRepo.GetAllCategories() };

            model.TaskList = _mapper.Map<List<TaskViewModel>>((isDescending)
                                ? taskRepo.GetActiveTasks().OrderByDescending(t => t.DueDate)
                                : taskRepo.GetActiveTasks().OrderBy(t => t.DueDate));

            TempData.Remove("isDescending");

            return View(model);
        }


        [HttpPost]
        public IActionResult Add(IndexViewModel task)
        {
            TaskModel newTask = _mapper.Map<TaskModel>(task.NewTask);

            _taskFactory.CreateRepository(GetCookieValue()).AddTask(newTask);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {

            _taskFactory.CreateRepository(GetCookieValue()).RemoveTask(id);

            return RedirectToAction("Index");
        }

        public IActionResult MarkAsDone(int id)
        {
            _taskFactory.CreateRepository(GetCookieValue()).MarkAsDone(id);

            return RedirectToAction("Index");
        }

        public IActionResult SortByDueDate(bool isDescending)
        {
            if (isDescending)
                TempData["isDescending"] = !(bool)(TempData["isDescending"] ?? false);

            return RedirectToAction("Index");
        }

        public IActionResult SelectDataStorage(string storageType)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append("StorageType", storageType);

            return RedirectToAction("Index");
        }

        private string GetCookieValue()
        {
            string cookieName = "StorageType";
            string cookieValue = _contextAccessor.HttpContext.Request.Cookies[cookieName];

            if (cookieValue == null)
            {
                cookieValue = "MSSQL";
                _contextAccessor.HttpContext.Response.Cookies.Append(cookieName, cookieValue);

                throw new Exception("Storage type is null.");
            }

            return cookieValue;
        }

    }
}