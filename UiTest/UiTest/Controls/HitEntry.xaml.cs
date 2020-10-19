using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Size = System.Drawing.Size;

namespace UiTestApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HitEntry : ContentView
    {
        int _placeholderFontSize = 18;
        int _titleFontSize = 14;
        int _topMargin = -32;

        public event EventHandler Completed;

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null, HandleBindingPropertyChangedDelegate);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(Color), Color.FromHex("374955"), BindingMode.TwoWay, null);
                
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(string), string.Empty, BindingMode.TwoWay, null);
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create("TitleColor", typeof(Color), typeof(Color), Color.FromHex("2196F3"), BindingMode.TwoWay, null);
        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(HitEntry), ReturnType.Default);
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create("IsPassword", typeof(bool), typeof(HitEntry), default(bool));
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create("Keyboard", typeof(Keyboard), typeof(HitEntry), Keyboard.Default, coerceValue: (o, v) => (Keyboard)v ?? Keyboard.Default);
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create("BackgroundColor", typeof(Color), typeof(Color), Color.FromHex("F6F6F6"), BindingMode.TwoWay, null);


        static async void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as HitEntry;
            if (!control.EntryField.IsFocused)
            {
                if (!string.IsNullOrEmpty((string)newValue))
                {
                    await control.TransitionToTitle(false);
                }
                else
                {
                    await control.TransitionToPlaceholder(false);
                }
            }
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color TextColor  
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public Color TitleColor
        {
            get => (Color) GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }  
        
        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color) GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public HitEntry()
        {
            try
            {
            InitializeComponent();
            LabelTitle.TranslationX = 10;
            LabelTitle.FontSize = _placeholderFontSize;
            }
            catch (Exception e)
            {
                int s;
            }
        }

        public new void Focus()
        {
            if (IsEnabled)
            {
                EntryField.Focus();
            }
        }

        async void Handle_Focused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                await TransitionToTitle(true);
            }
        }

        async void Handle_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                await TransitionToPlaceholder(true);
            }
        }

        async Task TransitionToTitle(bool animated)
        {
            if (animated)
            {
                var t1 = LabelTitle.TranslateTo(0, _topMargin, 100);
                var t2 = SizeTo(_titleFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                LabelTitle.TranslationX = 0;
                LabelTitle.TranslationY = -30;
                LabelTitle.FontSize = 14;
            }
        }

        async Task TransitionToPlaceholder(bool animated)
        {
            if (animated)
            {
                var t1 = LabelTitle.TranslateTo(10, 0, 100);
                var t2 = SizeTo(_placeholderFontSize);
                await Task.WhenAll(t1, t2);
            }
            else
            {
                LabelTitle.TranslationX = 10;
                LabelTitle.TranslationY = 0;
                LabelTitle.FontSize = _placeholderFontSize;
            }
        }

        void Handle_Tapped(object sender, EventArgs e)
        {
            Focus();
        }

        Task SizeTo(int fontSize)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            // setup information for animation
            Action<double> callback = input => { LabelTitle.FontSize = input; };
            double startingHeight = LabelTitle.FontSize;
            double endingHeight = fontSize;
            uint rate = 5;
            uint length = 100;
            Easing easing = Easing.Linear;

            // now start animation with all the setup information
            LabelTitle.Animate("invis", callback, startingHeight, endingHeight, rate, length, easing, (v, c) => taskCompletionSource.SetResult(c));

            return taskCompletionSource.Task;
        }

        void Handle_Completed(object sender, EventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                base.OnPropertyChanged(propertyName);

                if (propertyName == nameof(IsEnabled))
                {
                    EntryField.IsEnabled = IsEnabled;
                }
            }
            catch (Exception e)
            {
                int a = 0;
            }
        }

    }
}