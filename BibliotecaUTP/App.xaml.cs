﻿using Xamarin.Forms;

namespace BibliotecaUTP {
    public partial class App : Application {
        public static MasterDetailPage MasterDetailPage { get; set; }

        public App() {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}
