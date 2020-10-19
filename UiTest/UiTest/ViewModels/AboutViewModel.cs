using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace UiTest.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private List<string> _lst = new List<string>{
                "Baboon",
                "Capuchin Monkey",
                "Blue Monkey",
                "Squirrel Monkey",
                "Golden Lion Tamarin",
                "Howler Monkey",
                "Japanese Macaque"
            };
        public List<string> MonkeyList
        {
            get => _lst;
            set
            {
                _lst = value;
                OnPropertyChanged();
            }
        }

        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));

        }

        public ICommand OpenWebCommand { get; }
    }
}