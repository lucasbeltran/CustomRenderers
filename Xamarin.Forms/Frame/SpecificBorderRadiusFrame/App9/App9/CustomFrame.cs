using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform;

//Authors: Moustafa Shaban (https://www.freelancer.com.ar/u/moustafashaban?ref_project_id=15017894)
//         Cristian Giagante (cristiangiagante@hotmail.com)
namespace App9
{
    //Custom frame that inherts from Content View
    public class CustomFrame : ContentView
    {

        //Define the Border Radious bindable properties 
        public readonly static BindableProperty BorderRadiusTopLeftProperty = BindableProperty.Create("BorderRadiusTopLeft", typeof(double), typeof(CustomFrame), 0.0, BindingMode.OneWay, null, null, null, null, null);
        public readonly static BindableProperty BorderRadiusTopRightProperty = BindableProperty.Create("BorderRadiusTopRight", typeof(double), typeof(CustomFrame), 0.0, BindingMode.OneWay, null, null, null, null, null);
        public readonly static BindableProperty BorderRadiusBottomLeftProperty = BindableProperty.Create("BorderRadiusBottomLeft", typeof(double), typeof(CustomFrame), 0.0, BindingMode.OneWay, null, null, null, null, null);
        public readonly static BindableProperty BorderRadiusBottomRightProperty = BindableProperty.Create("BorderRadiusBottomRight", typeof(double), typeof(CustomFrame), 0.0, BindingMode.OneWay, null, null, null, null, null);

        //Define the Border Color bindable property
        public readonly static BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(CustomFrame), Color.Default, BindingMode.OneWay, null, null, null, null, null);

        //Define the Border Width bindable property
        public readonly static BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(CustomFrame), 2, BindingMode.OneWay, null, null, null, null, null);

        //Define the actual properties that are related to previous bindable
        public int BorderWidth
        {
            get { return (int)base.GetValue(CustomFrame.BorderWidthProperty); }
            set { base.SetValue(CustomFrame.BorderWidthProperty, value); }
        }

        public Color BorderColor
        {
            get
            {
                return (Color)base.GetValue(CustomFrame.BorderColorProperty);
            }
            set
            {
                base.SetValue(CustomFrame.BorderColorProperty, value);
            }
        }

        public double BorderRadiusTopLeft
        {
            get
            {
                return (double)base.GetValue(CustomFrame.BorderRadiusTopLeftProperty);
            }
            set
            {
                base.SetValue(CustomFrame.BorderRadiusTopLeftProperty, value);
            }
        }
        public double BorderRadiusTopRight
        {
            get
            {
                return (double)base.GetValue(CustomFrame.BorderRadiusTopRightProperty);
            }
            set
            {
                base.SetValue(CustomFrame.BorderRadiusTopRightProperty, value);
            }
        }
        public double BorderRadiusBottomLeft
        {
            get
            {
                return (double)base.GetValue(CustomFrame.BorderRadiusBottomLeftProperty);
            }
            set
            {
                base.SetValue(CustomFrame.BorderRadiusBottomLeftProperty, value);
            }
        }
        public double BorderRadiusBottomRight
        {
            get
            {
                return (double)base.GetValue(CustomFrame.BorderRadiusBottomRightProperty);
            }
            set
            {
                base.SetValue(CustomFrame.BorderRadiusBottomRightProperty, value);
            }
        }
    }
}
