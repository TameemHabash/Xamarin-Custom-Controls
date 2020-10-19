using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UiTestApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HitPicker : ContentView
    {
        int _placeholderFontSize = 18;
        int _titleFontSize = 14;
        int _topMargin = -32;

        public event EventHandler Completed;


        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(HitPicker), Color.FromHex("374955"), BindingMode.TwoWay, null);
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(HitPicker), string.Empty, BindingMode.TwoWay, null);
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create("TitleColor", typeof(Color), typeof(HitPicker), Color.FromHex("2196F3"), BindingMode.TwoWay, null);
        public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create("BackgroundColor", typeof(Color), typeof(HitPicker), Color.FromHex("F6F6F6"), BindingMode.TwoWay, null);
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(List<string>), typeof(HitPicker), default(List<string>), BindingMode.TwoWay, null);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(object), typeof(HitPicker), default(object), BindingMode.TwoWay, null, OnSelectedItemChanged);

        //public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create("SelectedItem", typeof(string), typeof(string), null, BindingMode.TwoWay, null, HandleBindingPropertyChangedDelegate);
        //public static readonly BindableProperty SelectListProperty = BindableProperty.Create("SelectList", typeof(List<string>), typeof(List<string>), null, BindingMode.TwoWay, null);
        //public static BindableProperty ItemsSourceProperty = BindableProperty.Create<HitPicker, IEnumerable>(o => o.ItemsSource, default(IEnumerable), bindingPropertyChanged: OnItemsSourceChanged);
        //public static BindableProperty SelectedItemProperty =   BindableProperty.Create<HitPicker, object>(o => o.SelectedItem, default(object), bindingPropertyChanged: OnSelectedItemChanged);
        //public static readonly BindableProperty MyPickerFieldProperty = BindableProperty.Create("MyPickerField", typeof(IList), typeof(IList), default(IList), BindingMode.TwoWay, null);

        //public string SelectedItem
        //{
        //    get => (string)GetValue(SelectedItemProperty);
        //    set => SetValue(SelectedItemProperty, value);
        //}

        //public List<string> SelectList
        //{
        //    get => (List<string>)GetValue(SelectListProperty);
        //    set => SetValue(SelectListProperty, value);
        //}
        //public IList MyPickerField
        //{
        //    get => (IList)GetValue(MyPickerFieldProperty);
        //    set => SetValue(MyPickerFieldProperty, value);
        //}
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
            get => (Color)GetValue(TitleColorProperty);
            set => SetValue(TitleColorProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public List<string> ItemsSource
        {
            get => (List<string>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public HitPicker()
        {
            try
            {
                InitializeComponent();
                LabelTitle.TranslationX = 10;
                LabelTitle.FontSize = _placeholderFontSize;
                //BindingContext = this;
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
                PickerField.Focus();
            }
        }

        async void Handle_Focused(object sender, FocusEventArgs e)
        {
            if (SelectedItem == null)
            {
                await TransitionToTitle(true);
            }
        }

        async void Handle_Unfocused(object sender, FocusEventArgs e)
        {
            if (SelectedItem == null)
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
                    PickerField.IsEnabled = IsEnabled;
                }
            }
            catch (Exception e)
            {
                int a = 0;
            }
        }
      
        //private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        //{
        //    var picker = (bindable as HitPicker).PickerField;
        //    picker.Items.Clear();
        //    var newList = newvalue as List<string>;
        //    if (newvalue != null)
        //    {
        //        foreach (var item in newList)
        //        {
        //            picker.Items.Add(item.ToString());
        //        }
        //    }
        //}

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (PickerField.SelectedIndex < 0 || PickerField.SelectedIndex > PickerField.Items.Count - 1)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = PickerField.Items[PickerField.SelectedIndex];
            }
        }
        private static void OnSelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var picker = (bindable as HitPicker).PickerField;
            if (newvalue != null)
            {
                picker.SelectedIndex = picker.Items.IndexOf(newvalue.ToString());
            }
        }

        //private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    HitPicker bindablePicker = (HitPicker)bindable;
        //    bindablePicker.ItemsSource = (IList)newValue;
        //    loadItemsAndSetSelected(bindable);
        //}
        //static void loadItemsAndSetSelected(BindableObject bindable)
        //{
        //    HitPicker bindablePicker = (HitPicker)bindable;
        //    if (bindablePicker.ItemsSource as IEnumerable != null)
        //    {
        //        PropertyInfo propertyInfo = null;
        //        int count = 0;
        //        foreach (object obj in (IEnumerable)bindablePicker.ItemsSource)
        //        {
        //            string value = string.Empty;
        //            if (bindablePicker.DisplayProperty != null)
        //            {
        //                if (propertyInfo == null)
        //                {
        //                    propertyInfo = obj.GetType().GetRuntimeProperty(bindablePicker.DisplayProperty);
        //                    if (propertyInfo == null)
        //                        throw new Exception(String.Concat(bindablePicker.DisplayProperty, " is not a property of ", obj.GetType().FullName));
        //                }
        //                value = propertyInfo.GetValue(obj).ToString();
        //            }
        //            else
        //            {
        //                value = obj.ToString();
        //            }
        //            bindablePicker.Items.Add(value);
        //            if (bindablePicker.SelectedItem != null)
        //            {
        //                if (bindablePicker.SelectedItem == obj)
        //                {
        //                    bindablePicker.SelectedIndex = count;
        //                }
        //            }
        //            count++;
        //        }
        //    }
        //}


    }
}