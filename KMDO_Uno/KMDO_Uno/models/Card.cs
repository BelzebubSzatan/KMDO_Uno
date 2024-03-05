using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KMDO_Uno.models
{
    public class Card
    {
        public string Value { get; set; }
        public System.Drawing.Color Color { get; set; }
        public SpecialActions Action { get; set; }
        public Button Render()
        {
            return new Button() { 
                WidthRequest=80,
                HeightRequest=150,
                VerticalOptions=LayoutOptions.FillAndExpand,
                BackgroundColor=Color,
                TextColor=System.Drawing.Color.White,
                Text=Value,
                Margin=new Thickness(5),
            };
        }
    }
}
