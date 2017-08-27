using CustomFonts;
using CustomFonts.UWP;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(LabelCustomFont), typeof(LabelCustomFontCustomRenderer))]
namespace CustomFonts.UWP
{
    class LabelCustomFontCustomRenderer : LabelRenderer
    {
        private LabelCustomFont _control;

        protected async override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            _control = e.NewElement as LabelCustomFont;
            if (_control.FontFamily != null)
                _control.FontFamily = $"/Assets/Fonts/{_control.FontFamily}";
        }
    }
}
