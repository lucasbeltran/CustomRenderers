using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Color = Xamarin.Forms.Color;

//Authors: Moustafa Shaban (https://www.freelancer.com.ar/u/moustafashaban?ref_project_id=15017894)
//         Cristian Giagante (cristiangiagante@hotmail.com)
[assembly: ExportRenderer(typeof(App9.CustomFrame), typeof(App9.UWP.CustomFrameRenderer))]
namespace App9.UWP
{
    //Custom Frame for UWP
    public class CustomFrameRenderer : ViewRenderer<CustomFrame, Border>
    {
        private CustomFrame _control;
        public CustomFrameRenderer()
        {
            //dont know exactly why is this for :D but you can remove it also
            base.AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomFrame> e)
        {
            //calling the base OnElementChanged method
            base.OnElementChanged(e);

            //Creatin a new border to the Frame control (the border will be at the top of the frame)
            base.SetNativeControl(new Border());
            //calling PackChild
            this.PackChild();
            //setting _control as a Custom Frame (that will be created in shared code)
            _control = e.NewElement as CustomFrame;
            //updating the border when any property (of our created properties) will be changed
            this.UpdateBorder(_control.BorderWidth, _control.BorderRadiusTopLeft, _control.BorderRadiusTopRight, _control.BorderRadiusBottomLeft, _control.BorderRadiusBottomRight);
        }

        //this overriden method will be called when any bindable property will be changed
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //if the changed property is Content
            if (e.PropertyName == "Content")
            {
                this.PackChild();
                return;
            }
            //if the changed property is one of the properies that we created then update the border
            if (e.PropertyName == CustomFrame.BorderColorProperty.PropertyName || e.PropertyName == CustomFrame.BorderWidthProperty.PropertyName || e.PropertyName == CustomFrame.BorderRadiusTopLeftProperty.PropertyName || e.PropertyName == CustomFrame.BorderRadiusTopRightProperty.PropertyName || e.PropertyName == CustomFrame.BorderRadiusBottomRightProperty.PropertyName || e.PropertyName == CustomFrame.BorderRadiusBottomLeftProperty.PropertyName|| e.PropertyName == CustomFrame.BorderRadiusBottomLeftProperty.PropertyName)
            {
                this.UpdateBorder(_control.BorderWidth, _control.BorderRadiusTopLeft, _control.BorderRadiusTopRight, _control.BorderRadiusBottomLeft, _control.BorderRadiusBottomRight);
            }
        }

        private void PackChild()
        {
            if (base.Element.Content == null)
                return;
            //put the Frame Content inside the Border
            Platform.SetRenderer(base.Element.Content, Platform.CreateRenderer(base.Element.Content));
            UIElement containerElement = Platform.GetRenderer(base.Element.Content).ContainerElement;
            //the control here represents the border
            base.Control.Child = containerElement;
        }

        //this method updates the border when any bindable property will change
        private void UpdateBorder(int borderWidth, double borderRadiusTopLeft, double borderRadiusTopRight, double borderRadiusBottomLeft, double borderRadiusBottomRight)
        {
            //setting the corner radious
            base.Control.CornerRadius = new CornerRadius(borderRadiusTopLeft, borderRadiusTopRight, borderRadiusBottomLeft, borderRadiusBottomRight);

            //if there is no border color then no need to update
            if (base.Element.BorderColor == Color.Default)
            {
                Xamarin.Forms.Color borderColor = new Xamarin.Forms.Color(0, 0, 0, 0);
                base.Control.BorderBrush = ToBrush(borderColor);
                return;
            }
            //setting the border background
            base.Control.Background = ToBrush(Element.BackgroundColor);
            //making the custom frame background Transparent so it can not make any problems in view (you can remove this line and you will see ghosts :D
            _control.BackgroundColor = Color.Transparent;
            //setting the border Color
            base.Control.BorderBrush = ToBrush(Element.BorderColor);
            //setting the border Width
            base.Control.BorderThickness = new Windows.UI.Xaml.Thickness(borderWidth);
        }

        //Convert Xamarin Color To Brush
        public static Brush ToBrush(Xamarin.Forms.Color color)
        {
            return new SolidColorBrush(ToMediaColor(color));
        }
        //Convert Xamarin Color To UWP Color
        public static Windows.UI.Color ToMediaColor(Xamarin.Forms.Color color)
        {
            return Windows.UI.Color.FromArgb(((byte)(color.A * 255)), (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));
        }
    }
}
