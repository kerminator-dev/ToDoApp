using Microsoft.AspNetCore.Mvc;
using ToDoList.DBContexts;
using ToDoList.Models;
using ToDoList.Models.ViewModels;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var todoListViewModel = GetAllToDos();
            return View(todoListViewModel);
        }

        public RedirectResult Insert(ToDoItem toDoItem)
        {
            DBManager.Instance.InsertToDo(toDoItem);

            return Redirect("https://localhost:7269/");
        }

        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns></returns>
        internal ToDoViewModel GetAllToDos()
        {
            return DBManager.Instance.GetAllToDos();
        }

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id">Идентификатор ToDo</param>
        /// <returns></returns>
        public JsonResult Delete(int id)
		{
            DBManager.Instance.DeleteToDo(id);
            return Json(new { });
		}

        [HttpGet]
        public JsonResult PopulateForm(int id)
		{
            var todo = DBManager.Instance.GetToDoById(id);

            return Json(todo);
		}

        public RedirectResult Update(ToDoItem toDoItem)
		{
            DBManager.Instance.Update(toDoItem);
            return Redirect("https://localhost:7269/");
        }
    }
}