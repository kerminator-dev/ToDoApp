namespace ToDoList.Models.ViewModels
{
    public class ToDoViewModel
    {
        public List<ToDoItem> ToDoItems { get; set; }
        public ToDoItem ToDoItem { get; set; }

        public ToDoViewModel(List<ToDoItem> toDoItems)
        {
            this.ToDoItems = toDoItems;
        }
    }
}
