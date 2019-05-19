using System.Linq;
using System.Windows;
using System.Windows.Controls;
using API.Client.Models.Todo;

namespace API
{
    public class TodoListDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && item is TodoInfo)
            {
                var todoInfo = (TodoInfo) item;
                var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.Name == "TodoListWindow");

                return
                    window.FindResource("MyTaskTemplate") as DataTemplate;
            }

            return null;
        }
    }
}