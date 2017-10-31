using MvvmPrismSample.Views;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MvvmPrismSample
{
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnInitialized()
        {

            NavigationService.NavigateAsync("NavigationPage/ToDoList");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<ToDoList>();
            Container.RegisterTypeForNavigation<ToDoDetails>();
        }
    }
}
