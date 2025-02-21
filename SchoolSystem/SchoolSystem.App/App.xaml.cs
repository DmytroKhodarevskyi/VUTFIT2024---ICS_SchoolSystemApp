﻿using SchoolSystem.App.Shells;

namespace SchoolSystem.App
{
   public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }
    }
}
