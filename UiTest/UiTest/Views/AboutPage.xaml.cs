using System;
using System.ComponentModel;
using UiTest.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
       
        public AboutPage()
        {
            InitializeComponent();
            AboutViewModel vm = new AboutViewModel();
            BindingContext = vm;

        }
    }
}