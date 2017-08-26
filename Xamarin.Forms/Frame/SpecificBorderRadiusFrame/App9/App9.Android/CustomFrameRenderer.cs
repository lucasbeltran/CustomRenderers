using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Xamarin.Forms.Platform.Android;

//Authors: Moustafa Shaban (https://www.freelancer.com.ar/u/moustafashaban?ref_project_id=15017894)
//         Cristian Giagante (cristiangiagante@hotmail.com)
[assembly: Xamarin.Forms.ExportRenderer(typeof(App9.CustomFrame), typeof(App9.Droid.CustomFrameRenderer))]
namespace App9.Droid
{
    //Creating the custom frame renderer
    public class CustomFrameRenderer : VisualElementRenderer<CustomFrame>
    {
        //Defining Gradient Drawable
        private GradientDrawable drawable;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomFrame> e)
        {
            //taking the main custom frame (that exists in shared app)
            CustomFrame customFram = e.NewElement as CustomFrame;
            // Create a drawable for the button's normal state
            drawable = new Android.Graphics.Drawables.GradientDrawable();
            //taking the BackgroundColor property value that exists in ContentView and give it to the drawable object
            drawable.SetColor(customFram.BackgroundColor.ToAndroid());
            //taking the BorderWidth property value that we add it in  CustomFrame and give it to the drawable object
            drawable.SetStroke(customFram.BorderWidth, customFram.BorderColor.ToAndroid());
            //taking the BorderRadious properties values that we add it in  CustomFrame and send it to SetCornerRaii method
            drawable.SetCornerRadii(new float[] { (float)customFram.BorderRadiusTopLeft, (float)customFram.BorderRadiusTopLeft,
                (float)customFram.BorderRadiusTopRight, (float)customFram.BorderRadiusTopRight,
                (float)customFram.BorderRadiusBottomLeft, (float)customFram.BorderRadiusBottomLeft,
                (float)customFram.BorderRadiusBottomRight, (float)customFram.BorderRadiusBottomRight });
            //put the drawable object in the content view Background
            SetBackgroundDrawable(drawable);

            //call the base OnElementChanged method 
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }
    }
}