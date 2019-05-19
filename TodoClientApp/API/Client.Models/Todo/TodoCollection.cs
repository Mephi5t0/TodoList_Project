using System.Collections.ObjectModel;

namespace API.Client.Models.Todo
{
    public class TodoCollection : ObservableCollection<TodoInfo>
    {
        public TodoCollection()
        {
        }
    }
}
