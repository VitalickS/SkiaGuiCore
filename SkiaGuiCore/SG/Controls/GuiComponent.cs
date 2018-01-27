using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public class GuiComponent : IGuiComponent
    {
        public virtual Thickness Margin { get; set; }
        public SKPointI Position { get; private set; }
        public SKSizeI VisualSize { get; set; }
        public float MaxWidth { get; set; } = float.PositiveInfinity;
        public float MaxHeight { get; set; } = float.PositiveInfinity;
        public HorizAlign HorizAlign { get; set; }
        public VertAlign VertAlign { get; set; }
        public SKPaint Foreground { get; set; }
        public SKPaint Background { get; set; }
        public object Content { get; set; }
        public object DataContext { get; set; }
        public IGuiComponent Parent { get; set; }
        public SKMatrix? Transform { get; }

        public virtual SKRectI Measure()
        {
            var bounds = new SKRectI();
            if (VisualSize.Width > MaxWidth) VisualSize = new SKSizeI((int)MaxWidth, VisualSize.Height);
            if (VisualSize.Height > MaxHeight) VisualSize = new SKSizeI(VisualSize.Width, (int)MaxHeight);
            VisualSize = new SKSizeI((int)Math.Min(VisualSize.Width, MaxWidth), (int)Math.Min(VisualSize.Height, MaxHeight));

            switch (HorizAlign)
            {
                case HorizAlign.Left:
                    bounds.Left = (int)Margin.Left;
                    bounds.Right = bounds.Left = VisualSize.Width;
                    break;
                case HorizAlign.Center:
                    bounds.Left = (Parent.VisualSize.Width - VisualSize.Width) / 2 + (int)Margin.Left;
                    bounds.Right = bounds.Left = VisualSize.Width;
                    break;
                case HorizAlign.Right:
                    bounds.Left = Parent.VisualSize.Width - VisualSize.Width - (int)Margin.Right;
                    bounds.Right = bounds.Left + VisualSize.Width;
                    break;
                case HorizAlign.Stretch:
                    bounds.Left = (int)Margin.Left;
                    bounds.Right = Parent.VisualSize.Width - (int)Margin.Right;
                    break;
            }

            switch (VertAlign)
            {
                case VertAlign.Top:
                    bounds.Top = (int)Margin.Top;
                    bounds.Bottom = bounds.Top + VisualSize.Height;
                    break;
                case VertAlign.Center:
                    bounds.Top = (Parent.VisualSize.Height - VisualSize.Height) / 2 + (int)Margin.Top;
                    bounds.Bottom = bounds.Top + VisualSize.Height;
                    break;
                case VertAlign.Bottom:
                    bounds.Top = Parent.VisualSize.Height - VisualSize.Height - (int)Margin.Bottom;
                    bounds.Bottom = bounds.Top + VisualSize.Height;
                    break;
                case VertAlign.Stretch:
                    bounds.Top = (int)Margin.Top;
                    bounds.Bottom = Parent.VisualSize.Height - (int)Margin.Bottom;
                    break;
            }

            Position = bounds.Location;
            VisualSize = bounds.Size;
            return bounds;
        }

        public virtual void Render(SKCanvas canvas)
        {
            if (Transform.HasValue) canvas.SetMatrix(Transform.Value);
        }
    }
}
