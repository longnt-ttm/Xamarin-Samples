using Prism.Mvvm;
using System.Collections.ObjectModel;
using MvvmPrismSample.Models;
using Prism.Navigation;
using Prism.Commands;
using System;
using Xamarin.Forms;
using Prism.Services;
using Prism.Events;

namespace MvvmPrismSample.ViewModels
{
    public class ToDoListViewModel : BindableBase, INavigatedAware
    {
        protected INavigationService navigationService;

        protected IPageDialogService dialogService;

        protected IEventAggregator eventAggregator;

        private const int DefaultTimer = 5;
        private int timer;

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

        private string timerText;
        public string TimerText
        {
            get { return timerText; }
            set
            {
                SetProperty(ref timerText, value);
            }
        }

        public DelegateCommand ColorJumpingCommand { get; private set; }
        public DelegateCommand ShowDialogCommand { get; private set; }
        public DelegateCommand CountDownCommand { get; private set; }

        public DelegateCommand RefreshCommand { get; private set; }

        public DelegateCommand<ToDoItem> ItemSelectedCommand { get; private set; }

        public ToDoListViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator eventAggregator)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.eventAggregator = eventAggregator;

            BgColor = Color.Default;
            timer = DefaultTimer;
            TimerText = string.Format("Count Down({0})", timer);

            ColorJumpingCommand = new DelegateCommand(JumpColor);
            ShowDialogCommand = new DelegateCommand(ShowDialog);
            CountDownCommand = new DelegateCommand(StartCountDown);

            RefreshCommand = new DelegateCommand(LoadItems);
            ItemSelectedCommand = new DelegateCommand<ToDoItem>(OnItemSelected);
        }

        private async void OnItemSelected(ToDoItem selectedItem)
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("Id", selectedItem.Id);
            //await navigationService.NavigateAsync("ToDoDetails?Id="+selectedItem.Id);
            await navigationService.NavigateAsync("ToDoDetails", param);
        }

        private void LoadItems()
        {
            ToDoList = ToDoManager.GetTodolist();
        }

        private void JumpColor()
        {
            Random rnd = new Random();
            int r = rnd.Next(0, 255);
            int g = rnd.Next(0, 255);
            int b = rnd.Next(0, 255);
            BgColor = Color.FromRgb(r,g,b);
        }

        private async void ShowDialog()
        {
            await dialogService.DisplayAlertAsync("Sample", "What color do you see?", "Oke");
        }

        private void StartCountDown()
        {
            eventAggregator.GetEvent<CountDownEvent>().Subscribe(ReachCountDown);
            Device.StartTimer(TimeSpan.FromSeconds(1), () => 
            {
                timer--;
                TimerText = string.Format("Count Down({0})", timer);

                if (timer == 0)
                {
                    eventAggregator.GetEvent<CountDownEvent>().Publish("All your data is wiped!");
                    return false;
                }
                else
                {
                    return true;
                }
            });
        }

        private async void ReachCountDown(string message)
        {
            await dialogService.DisplayAlertAsync("Time Out", message, "Oke");
            
            //unscribe itself once called
            eventAggregator.GetEvent<CountDownEvent>().Unsubscribe(ReachCountDown);
            //reset timer state
            timer = DefaultTimer;
            TimerText = string.Format("Count Down({0})", timer);

            //foreach (var item in ToDoList)
            //{
            //    NavigationParameters param = new NavigationParameters();
            //    param.Add("Id", item.Id);
            //    await navigationService.NavigateAsync("ToDoDetails", param);
            //    //ToDoManager.DeleteTodoitem(item.Id);
            //    await navigationService.GoBackAsync();
            //}
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //throw new System.NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            RefreshCommand.Execute();
        }
    }
}
