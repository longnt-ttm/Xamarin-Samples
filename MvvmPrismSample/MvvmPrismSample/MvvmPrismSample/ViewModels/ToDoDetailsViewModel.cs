using MvvmPrismSample.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MvvmPrismSample.ViewModels
{
    public class ToDoDetailsViewModel : BindableBase, INavigatedAware
    {
        protected INavigationService navigationService;

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                SetProperty(ref title, value);
            }
        }

        private ToDoItem theItem;
        public ToDoItem TheItem
        {
            get { return theItem; }
            set
            {
                SetProperty(ref theItem, value);
            }
        }

        public ToDoDetailsViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            Title = string.Format("ToDo Details");
            TheItem = new ToDoItem
            {
                Name = "Buy apple 2kg",
                Note = "Shopping at Big C Thang Long",
                Date = new DateTime(2017,10,25),
                Time = new TimeSpan(18,30,00),
                Done = false
            };
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("Id"))
            {
                //int theId = int.Parse((string)parameters["Id"]);
                //TheItem = ToDoManager.GetTodoitem(theId);
                TheItem = ToDoManager.GetTodoitem((int)parameters["Id"]);
                Title = string.Format("Name: {0}", TheItem.Name);
            }
        }
    }
}
