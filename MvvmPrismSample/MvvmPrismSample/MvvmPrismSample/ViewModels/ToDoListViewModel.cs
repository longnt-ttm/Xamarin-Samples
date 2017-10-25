using Prism.Mvvm;
using System.Collections.ObjectModel;
using MvvmPrismSample.Models;
using Prism.Navigation;
using Prism.Commands;
using System;
using Xamarin.Forms;

namespace MvvmPrismSample
{
    public class ToDoListViewModel : BindableBase, INavigatedAware
    {
        private ObservableCollection<ToDoItem> _toDoList;
        public ObservableCollection<ToDoItem> ToDoList
        {
            get { return _toDoList; }
            set
            {
                SetProperty(ref _toDoList, value);
            }
        }

        private Color _bgColor;
        public Color BgColor
        {
            get { return _bgColor; }
            set
            {
                SetProperty(ref _bgColor, value);
            }
        }

        public DelegateCommand GreetingCommand { get; private set; }

        public ToDoListViewModel()
        {
            ToDoList = ToDoManager.GetTodolist();
            BgColor = Color.Default;
            GreetingCommand = new DelegateCommand(ShowGreeting);
        }

        private void ShowGreeting()
        {
            Random rnd = new Random();
            int r = rnd.Next(0, 255);
            int g = rnd.Next(0, 255);
            int b = rnd.Next(0, 255);
            BgColor = Color.FromRgb(r,g,b);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new System.NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            //throw new System.NotImplementedException();
        }
    }
}
