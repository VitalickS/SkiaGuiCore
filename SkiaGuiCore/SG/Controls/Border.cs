using SkiaSharp;
using System;

namespace SkiaGuiCore.SG.Controls
{
    public class Border : GuiContentControl
    {
        public Border()
        {
            HorizAlign = HorizAlign.Stretch;
            VertAlign = VertAlign.Stretch;
        }

        public SKPaint BorderBrush { get; set; }
        public Thickness Thickness { get; set; } = new Thickness(1);
        public CornerRadius CornerRadius { get; set; }
        public CornerRadius FixedCornerRadius
        {
            get {
                var minHalfSize = Math.Min(Width / 2, Height / 2);
                return new CornerRadius(
                    Math.Min(minHalfSize, CornerRadius.TopLeft),
                    Math.Min(minHalfSize, CornerRadius.TopRight),
                    Math.Min(minHalfSize, CornerRadius.BottomRight),
                    Math.Min(minHalfSize, CornerRadius.BottomLeft));
            }
        }

        public override SKRect Measure(SKSize availableSize)
        {
            return Bounds = base.Measure(availableSize);
        }

        public override void Render(SKCanvas canvas)
        {
            var boundsAligned = BoundsStrokeAligned(Bounds);
            var boundsInner = BoundsMinusThickness(Bounds);
            var radiuses = FixedCornerRadius;
            if (radiuses.IsEmpty && Thickness.IsSymmetric)
            {
                // Simple rect
                canvas.DrawRect(Bounds, Background);
                if (!Thickness.IsEmpty)
                {
                    var sw = BorderBrush.StrokeWidth;
                    BorderBrush.StrokeWidth = Thickness.Top;
                    canvas.DrawRect(BorderBrush.StrokeWidth % 2 > 0 ? boundsAligned : Bounds, BorderBrush);
                    BorderBrush.StrokeWidth = sw;
                }
            }
            else if (radiuses.IsSymetric && Thickness.IsSymmetric)
            {
                // Simple round rect
                canvas.DrawRoundRect(Bounds, radiuses.TopLeft, radiuses.TopLeft, Background);
                if (!Thickness.IsEmpty)
                {
                    var sw = BorderBrush.StrokeWidth;
                    BorderBrush.StrokeWidth = Thickness.Top;
                    canvas.DrawRoundRect(BorderBrush.StrokeWidth % 2 > 0 ? boundsAligned : Bounds, radiuses.TopLeft, radiuses.TopLeft, BorderBrush);
                    BorderBrush.StrokeWidth = sw;
                }
            }
            else
            {
                BorderBrush.IsStroke = false;
                // Custom rect
                var outerPath = new SKPath();
                if (!radiuses.IsSymetric)
                {
                    if (radiuses.TopLeft > 0)
                    {
                        outerPath.AddArc(new SKRect(Bounds.Left, Bounds.Top, Bounds.Left + radiuses.TopLeft * 2, Bounds.Top + radiuses.TopLeft * 2), 180, 90);
                    }

                    outerPath.RLineTo(Width - radiuses.TopLeft - radiuses.TopRight, 0);
                    if (radiuses.TopRight > 0)
                    {
                        outerPath.RArcTo(radiuses.TopRight, radiuses.TopRight, 90, SKPathArcSize.Small,
                            SKPathDirection.Clockwise, radiuses.TopRight, radiuses.TopRight);
                    }

                    outerPath.RLineTo(0, Height - radiuses.TopRight - radiuses.BottomRight);
                    if (radiuses.BottomRight > 0)
                    {
                        outerPath.RArcTo(-radiuses.BottomRight, radiuses.BottomRight, 90, SKPathArcSize.Small,
                            SKPathDirection.Clockwise, -radiuses.BottomRight, radiuses.BottomRight);
                    }

                    outerPath.RLineTo(-Width + radiuses.BottomLeft + radiuses.BottomRight, 0);
                    if (radiuses.BottomLeft > 0)
                    {
                        outerPath.RArcTo(-radiuses.BottomLeft, -radiuses.BottomLeft, 90, SKPathArcSize.Small,
                            SKPathDirection.Clockwise, -radiuses.BottomLeft, -radiuses.BottomLeft);
                    }

                    outerPath.Close();
                }
                else
                {
                    if (!radiuses.IsEmpty)
                        outerPath.AddRoundedRect(Bounds, radiuses.TopLeft, radiuses.TopLeft);
                    else
                    {
                        outerPath.AddRect(Bounds);
                    }
                }

                var innerPath = new SKPath();

                var rx = radiuses.TopLeft - Thickness.Left;
                var ry = radiuses.TopLeft - Thickness.Top;
                if (rx > 0 && ry > 0)
                {
                    // Use arc
                    innerPath.MoveTo(boundsInner.Left, Bounds.Top + radiuses.TopLeft);
                    innerPath.RArcTo(rx, ry, 0, SKPathArcSize.Small, SKPathDirection.Clockwise, rx, -ry);
                }
                else
                {
                    // Use point
                    innerPath.MoveTo(boundsInner.Location);
                }

                rx = radiuses.TopRight - Thickness.Right;
                ry = radiuses.TopRight - Thickness.Top;
                if (rx > 0 && ry > 0)
                {
                    innerPath.LineTo(Bounds.Right - radiuses.TopRight, boundsInner.Top);
                    innerPath.RArcTo(rx, ry, 0, SKPathArcSize.Small, SKPathDirection.Clockwise, rx, ry);
                }
                else
                {
                    innerPath.LineTo(boundsInner.Right, boundsInner.Top);
                }

                rx = radiuses.BottomRight - Thickness.Right;
                ry = radiuses.BottomRight - Thickness.Bottom;
                if (rx > 0 && ry > 0)
                {
                    innerPath.LineTo(boundsInner.Right, Bounds.Bottom - radiuses.BottomRight);
                    innerPath.RArcTo(rx, ry, 0, SKPathArcSize.Small, SKPathDirection.Clockwise, -rx, ry);
                }
                else
                {
                    innerPath.LineTo(boundsInner.Right, boundsInner.Bottom);
                }


                rx = radiuses.BottomLeft - Thickness.Left;
                ry = radiuses.BottomLeft - Thickness.Bottom;
                if (rx > 0 && ry > 0)
                {
                    innerPath.LineTo(Bounds.Left + radiuses.BottomLeft, boundsInner.Bottom);
                    innerPath.RArcTo(rx, ry, 0, SKPathArcSize.Small, SKPathDirection.Clockwise, -rx, -ry);
                }
                else
                {
                    innerPath.LineTo(boundsInner.Left, boundsInner.Bottom);
                }

                innerPath.Close();

                var borderPath = outerPath.Op(innerPath, SKPathOp.Difference);
                canvas.DrawPath(borderPath, BorderBrush);
                canvas.DrawPath(innerPath, Background);

                base.Render(canvas);
            }
        }

        private SKRect BoundsMinusThickness(SKRect bounds)
        {
            return new SKRect(bounds.Left + Thickness.Left, bounds.Top + Thickness.Top, bounds.Right - Thickness.Right, bounds.Bottom - Thickness.Bottom);
        }

        private static SKRect BoundsStrokeAligned(SKRect bounds)
        {
            return new SKRect(bounds.Left + .5f, bounds.Top + .5f, bounds.Right - .5f, bounds.Bottom - .5f);
        }
    }
}
