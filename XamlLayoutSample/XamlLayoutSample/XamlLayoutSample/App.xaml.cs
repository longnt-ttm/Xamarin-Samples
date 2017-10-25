using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamlLayoutSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new XamlLayoutSample.MainPage();
            MainPage = new TabbedPage
            {
                Children =
                {
                    new Page1
                    {
                        Title = "Page 1"
                    },
                    new Page2
                    {
                        Title = "Page 2"
                    },
                    new Page3
                    {
                        Title = "Page 3"
                    },
                    new Page4
                    {
                        Title = "Page 4"
                    },
                    new Page5
                    {
                        Title = "Page 5"
                    },
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
