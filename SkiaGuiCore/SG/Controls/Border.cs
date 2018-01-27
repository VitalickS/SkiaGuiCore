using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public class Border : GuiComponent
    {
        public Border()
        {
            HorizAlign = HorizAlign.Stretch;
            VertAlign = VertAlign.Stretch;
        }

        public SKPaint BorderBrush { get; set; }
        public Thickness Thickness { get; set; } = new Thickness(1);
        public CornerRadius CornerRadius { get; set; }

        public override void Render(SKCanvas canvas)
        {
            var bounds = Measure();
            var boundsAligned = BoundsStrokeAligned(bounds);
            var boundsInner = BoundsMinusThickness(bounds);
            if (CornerRadius.IsEmpty && Thickness.IsSymmetric)
            {
                // Simple rect
                canvas.DrawRect(boundsInner, Background);
                if (!Thickness.IsEmpty)
                    canvas.DrawRect(boundsAligned, Foreground);
            }
            else if (CornerRadius.IsSymetric && Thickness.IsSymmetric)
            {
                // Simple round rect
                canvas.DrawRoundRect(boundsInner, CornerRadius.TopLeft, CornerRadius.TopLeft, Background);
                if (!Thickness.IsEmpty)
                    canvas.DrawRoundRect(boundsAligned, CornerRadius.TopLeft, CornerRadius.TopLeft, Foreground);
            }
            else
            {
                BorderBrush.IsStroke = false;
                // Custom rect
                var outerPath = new SKPath();
                if (CornerRadius.TopLeft > 0)
                {
                    outerPath.AddArc(new SKRect(0, 0, CornerRadius.TopLeft * 2, CornerRadius.TopLeft * 2), 180, 90);
                }
                outerPath.RLineTo(VisualSize.Width - CornerRadius.TopLeft - CornerRadius.TopRight, 0);
                if (CornerRadius.TopRight > 0)
                {
                    outerPath.RArcTo(CornerRadius.TopRight, CornerRadius.TopRight, 90, SKPathArcSize.Small, SKPathDirection.Clockwise, CornerRadius.TopRight, CornerRadius.TopRight);
                }
                outerPath.RLineTo(0, VisualSize.Height - CornerRadius.TopRight - CornerRadius.BottomRight);
                if (CornerRadius.BottomRight > 0)
                {
                    outerPath.RArcTo(-CornerRadius.BottomRight, CornerRadius.BottomRight, 90, SKPathArcSize.Small, SKPathDirection.Clockwise, -CornerRadius.BottomRight, CornerRadius.BottomRight);
                }
                outerPath.RLineTo(-VisualSize.Width + CornerRadius.BottomLeft + CornerRadius.BottomRight, 0);
                if (CornerRadius.BottomLeft > 0)
                {
                    outerPath.RArcTo(-CornerRadius.BottomLeft, -CornerRadius.BottomLeft, 90, SKPathArcSize.Small, SKPathDirection.Clockwise, -CornerRadius.BottomLeft, -CornerRadius.BottomLeft);
                }
                outerPath.Close();


                var innerPath = new SKPath();
                if (CornerRadius.TopLeft > 0)
                {
                    if (CornerRadius.TopLeft - Thickness.Left > 0 && CornerRadius.TopLeft - Thickness.Top > 0)
                    {
                        var arc = new SKRect(Thickness.Left, Thickness.Top,
                            Thickness.Left + (CornerRadius.TopLeft - Thickness.Left) * 2,
                            Thickness.Top + (CornerRadius.TopLeft - Thickness.Top) * 2);
                        innerPath.AddArc(arc, 180, 90);
                    }
                    else
                        innerPath.RMoveTo(Thickness.Left, Thickness.Top);
                }
                else
                    innerPath.RMoveTo(Thickness.Left, Thickness.Top);

                // innerPath.LineTo(VisualSize.Width - Math.Max(CornerRadius.TopRight, Thickness.Right), innerPath.LastPoint.Y);

                innerPath.RLineTo(VisualSize.Width - innerPath.LastPoint.X - Math.Max(CornerRadius.TopRight, Thickness.Right), 0);

                if (CornerRadius.TopRight > 0)
                {
                    if (CornerRadius.TopRight - Thickness.Right > 0 && CornerRadius.TopRight - Thickness.Top > 0)
                    {
                        innerPath.ArcTo(CornerRadius.TopRight - Thickness.Right, CornerRadius.TopRight - Thickness.Top, 90, SKPathArcSize.Small,
                            SKPathDirection.Clockwise, VisualSize.Width - Thickness.Right, CornerRadius.TopRight);
                    }
                }
                innerPath.LineTo(innerPath.LastPoint.X, VisualSize.Height - Math.Max(CornerRadius.BottomRight, Thickness.Bottom));

                //if (CornerRadius.BottomRight > 0)
                //{
                //    innerPath.RArcTo(-CornerRadius.BottomRight, CornerRadius.BottomRight, 90, SKPathArcSize.Small, SKPathDirection.Clockwise, -CornerRadius.BottomRight, CornerRadius.BottomRight);
                //}
                //innerPath.RLineTo(-VisualSize.Width + CornerRadius.BottomLeft + CornerRadius.BottomRight, 0);
                //if (CornerRadius.BottomLeft > 0)
                //{
                //    innerPath.RArcTo(-CornerRadius.BottomLeft, -CornerRadius.BottomLeft, 90, SKPathArcSize.Small, SKPathDirection.Clockwise, -CornerRadius.BottomLeft, -CornerRadius.BottomLeft);
                //}
                innerPath.Close();

                var borderPath = outerPath.Op(innerPath, SKPathOp.Difference);
                canvas.DrawPath(innerPath, Background);
                canvas.DrawPath(borderPath, BorderBrush);
            }
        }

        private SKRect BoundsMinusThickness(SKRectI bounds)
        {
            return new SKRect(bounds.Left + Thickness.Left, bounds.Top + Thickness.Top, bounds.Right - Thickness.Right, bounds.Bottom - Thickness.Bottom);
        }

        private static SKRect BoundsStrokeAligned(SKRectI bounds)
        {
            return new SKRect(bounds.Left + .5f, bounds.Top + .5f, bounds.Right - .5f, bounds.Bottom - .5f);
        }
    }
}
