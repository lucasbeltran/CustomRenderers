using CustomFonts;
using CustomFonts.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(LabelCustomFont), typeof(LabelCustomFontCustomRenderer))]
namespace CustomFonts.iOS
{
    class LabelCustomFontCustomRenderer : LabelRenderer
    {
        private LabelCustomFont _control;
        private string[] _fontPath;
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            _control = e.NewElement as LabelCustomFont;
            string[] _fontPath = _control.FontFamily.Split(new char[] { '#' });
            _control.FontFamily = _fontPath[1];
            base.OnElementChanged(e);

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName == "Font" || Control==null)
            {
                return;
            };

            base.OnElementPropertyChanged(sender, e);

        }
    }
}
