﻿using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Sentry;
using Sentry.Xamarin.Sample.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContribSentry.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Disco_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Disco());
        }

        private void Popup_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new AboutPopupPage());
        }
        private void Xaml_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new XamlPageWithIssue());
        }
    }
}
