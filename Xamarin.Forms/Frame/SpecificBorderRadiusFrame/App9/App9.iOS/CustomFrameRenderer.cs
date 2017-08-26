using CoreAnimation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//Authors: Moustafa Shaban (https://www.freelancer.com.ar/u/moustafashaban?ref_project_id=15017894)
//         Cristian Giagante (cristiangiagante@hotmail.com)
[assembly: ExportRenderer(typeof(App9.CustomFrame), typeof(App9.iOS.CustomFrameRenderer))]
namespace App9.iOS
{
    //IOS Custom Frame Renderer
    public class CustomFrameRenderer : VisualElementRenderer<CustomFrame>
    {

        private CustomFrame _control;

        protected override void OnElementChanged(ElementChangedEventArgs<CustomFrame> e)
        {
            //calling the base OnElementChanged method
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                //setting custom frame control
                _control = e.NewElement as CustomFrame;
                //set up the main Layer with border width default value 1
                this.SetupLayer(_control.BorderWidth, 1);
            }
        }

        //same as android and UWP, will called when any bindable property will be changed
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomFrame.BorderColorProperty.PropertyName ||
                e.PropertyName == CustomFrame.BorderWidthProperty.PropertyName ||
                e.PropertyName == CustomFrame.BorderRadiusTopLeftProperty.PropertyName ||
                e.PropertyName == CustomFrame.BorderRadiusTopRightProperty.PropertyName ||
                e.PropertyName == CustomFrame.BorderRadiusBottomRightProperty.PropertyName ||
                e.PropertyName == CustomFrame.BorderRadiusBottomLeftProperty.PropertyName)
            {
                this.SetupLayer(_control?.BorderWidth ?? 1, 1);
            }
        }
        private void SetupLayer(int borderWidth, nfloat borderRadius)
        {
            //the below code was exists in this area, but i found that the Bounds property will not be initialized yet 
            //untill LayoutSubviews method is calling so I moved the code to there

            //so no need to this method you can remove it
        }


        public override void LayoutSubviews()
        {
            //calling base LayoutSubviews method
            base.LayoutSubviews();

            //taking corders radious values
            float topLeftRadius = (float)base.Element.BorderRadiusTopLeft;
            float topRightRadius = (float)base.Element.BorderRadiusTopRight;
            float bottomRightRadius = (float)base.Element.BorderRadiusBottomRight;
            float bottomLeftRadius = (float)base.Element.BorderRadiusBottomLeft;

            //get MinX in the Bounds 
            var minx = Bounds.X;
            //get MinY in the Bounds 
            var miny = Bounds.Y;
            //get MaxX in the Bounds 
            var maxx = Bounds.X + Bounds.Width;
            //get MaxY in the Bounds 
            var maxy = Bounds.Y + Bounds.Height;

            //creating a path that draws the shaope that we want depending on the radious that we have
            UIBezierPath maskPath = new UIBezierPath();
            maskPath.MoveTo(new CoreGraphics.CGPoint(minx + topLeftRadius, miny));
            maskPath.AddLineTo(new CoreGraphics.CGPoint(maxx - topRightRadius, miny));
            maskPath.AddArc(new CoreGraphics.CGPoint(maxx - topRightRadius, miny + topRightRadius), topRightRadius, (float)(3 * Math.PI / 2), 0, true);
            maskPath.AddLineTo(new CoreGraphics.CGPoint(maxx, maxy - bottomRightRadius));
            maskPath.AddArc(new CoreGraphics.CGPoint(maxx - bottomRightRadius, maxy - bottomRightRadius), bottomRightRadius, 0, (float)(Math.PI / 2), true);
            maskPath.AddLineTo(new CoreGraphics.CGPoint(minx + bottomLeftRadius, maxy));
            maskPath.AddArc(new CoreGraphics.CGPoint(minx + bottomLeftRadius, maxy - bottomLeftRadius), bottomLeftRadius, (float)(Math.PI / 2), (float)Math.PI, true);
            maskPath.AddLineTo(new CoreGraphics.CGPoint(minx, miny + topLeftRadius));
            maskPath.AddArc(new CoreGraphics.CGPoint(minx + topLeftRadius, miny + topLeftRadius), topLeftRadius, (float)Math.PI, (float)(3 * Math.PI / 2), true);
            maskPath.ClosePath();

            //create a layer that will be the masked layer of the frame
            CAShapeLayer maskLayer = new CAShapeLayer();
            //setting its frame the same as the control frame
            maskLayer.Frame = Bounds;
            //the path of this frame is the path that we alreadt drawn
            maskLayer.Path = maskPath.CGPath;
            //clipping the current layer mask to the created layer
            this.Layer.Mask = maskLayer;

            //creating the same previous path
            UIBezierPath maskPath1 = new UIBezierPath();
            maskPath1.MoveTo(new CoreGraphics.CGPoint(minx + topLeftRadius, miny));
            maskPath1.AddLineTo(new CoreGraphics.CGPoint(maxx - topRightRadius, miny));
            maskPath1.AddArc(new CoreGraphics.CGPoint(maxx - topRightRadius, miny + topRightRadius), topRightRadius, (float)(3 * Math.PI / 2), 0, true);
            maskPath1.AddLineTo(new CoreGraphics.CGPoint(maxx, maxy - bottomRightRadius));
            maskPath1.AddArc(new CoreGraphics.CGPoint(maxx - bottomRightRadius, maxy - bottomRightRadius), bottomRightRadius, 0, (float)(Math.PI / 2), true);
            maskPath1.AddLineTo(new CoreGraphics.CGPoint(minx + bottomLeftRadius, maxy));
            maskPath1.AddArc(new CoreGraphics.CGPoint(minx + bottomLeftRadius, maxy - bottomLeftRadius), bottomLeftRadius, (float)(Math.PI / 2), (float)Math.PI, true);
            maskPath1.AddLineTo(new CoreGraphics.CGPoint(minx, miny + topLeftRadius));
            maskPath1.AddArc(new CoreGraphics.CGPoint(minx + topLeftRadius, miny + topLeftRadius), topLeftRadius, (float)Math.PI, (float)(3 * Math.PI / 2), true);
            maskPath1.ClosePath();

            //add this new path to new layer
            CAShapeLayer maskLayer1 = new CAShapeLayer();
            maskLayer1.Frame = Bounds;
            maskLayer1.Path = maskPath1.CGPath;
            //setting its line width to the borderWidth property
            maskLayer1.LineWidth = base.Element.BorderWidth;
            //clear its fill color to avoid outer spaces in the shape, try to make it red and see the ghosts too xD
            maskLayer1.FillColor = UIColor.Clear.CGColor;
            //setting the stroke color (border color) to the BorderColor Property
            maskLayer1.StrokeColor = base.Element.BorderColor.ToCGColor();
            //add this new lauer to the main frame layer
            this.Layer.AddSublayer(maskLayer1);

            //Mask the Frame layer to Bounds so you can focusing only on the actual space inside the border
            Layer.MasksToBounds = true;
            _control.IsClippedToBounds = true;
            //setting the Layer backgound to the Background property
            Layer.BackgroundColor = base.Element.BackgroundColor.ToCGColor(); ;
        }
    }
}
