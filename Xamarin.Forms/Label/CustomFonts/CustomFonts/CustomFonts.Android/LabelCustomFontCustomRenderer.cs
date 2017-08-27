using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using CustomFonts;
using CustomFonts.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LabelCustomFont), typeof(LabelCustomFontCustomRenderer))]
namespace CustomFonts.Droid
{
    public class LabelCustomFontCustomRenderer : LabelRenderer
    {
        private LabelCustomFont _control;
        private string _fontPath;
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            _control = e.NewElement as LabelCustomFont;
            _fontPath= "Fonts/" + _control.FontFamily;
            _control.FontFamily = _fontPath;
            base.OnElementChanged(e);
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Font")
            {
                return;
            };

            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "FontFamily" && Control!=null)
            {
                var fontFamily = ((LabelCustomFont)sender).FontFamily;
                if (!string.IsNullOrEmpty(fontFamily))
                {
                    if (!fontFamily.Contains(".ttf")) fontFamily += ".ttf";
                    
                    var typeface = Typeface.CreateFromAsset(Resources.Assets, "Fonts/" + _fontPath[0]);

                    var label = ((TextView)Control);
                    label.Typeface = typeface;
                }
            }

        }
    }
}