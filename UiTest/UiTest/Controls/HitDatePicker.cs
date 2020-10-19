using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UiTestApp.Controls
{
    public class HitDatePicker:DatePicker
    {
        public static readonly BindableProperty HitTitleProperty =
            BindableProperty.Create("HitTitle",typeof(string),typeof(HitDatePicker),"");

        public string HitTitle
        {
            get { return (string) GetValue(HitTitleProperty);}
            set { SetValue(HitTitleProperty,value); }
        }

    }
}
