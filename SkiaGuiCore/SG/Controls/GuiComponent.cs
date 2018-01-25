using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public class GuiComponent : IGuiComponent
    {
        public virtual SKRectI Margin { get; set; }
        public SKPointI Position { get; private set; }
        public SKSizeI Size { get; set; }
        public float MaxWidth { get; set; } = float.PositiveInfinity;
        public float MaxHeight { get; set; } = float.PositiveInfinity;
        public HorizAlign HorizAlign { get; set; }
        public VertAlign VertAlign { get; set; }
        public SKPaint Foreground { get; set; }
        public SKPaint Background { get; set; }
        public object Content { get; set; }
        public object DataContext { get; set; }
        public IGuiComponent Parent { get; }
        public SKMatrix? Transform { get; }

        public virtual SKRectI Measure()
        {
            var bounds = new SKRectI();
            if (Size.Width > MaxWidth) Size = new SKSizeI((int)MaxWidth, Size.Height);
            if (Size.Height > MaxHeight) Size = new SKSizeI(Size.Width, (int)MaxHeight);
            Size = new SKSizeI((int)Math.Min(Size.Width, MaxWidth), (int)Math.Min(Size.Height, MaxHeight));

            switch (HorizAlign)
            {
                case HorizAlign.Left:
                    bounds.Left = Margin.Left;
                    bounds.Right = bounds.Left = Size.Width;
                    break;
                case HorizAlign.Center:
                    bounds.Left = (Parent.Size.Width - Size.Width) / 2 + Margin.Left;
                    bounds.Right = bounds.Left = Size.Width;
                    break;
                case HorizAlign.Right:
                    bounds.Left = Parent.Size.Width - Size.Width - Margin.Right;
                    bounds.Right = bounds.Left + Size.Width;
                    break;
                case HorizAlign.Stretch:
                    bounds.Left = Margin.Left;
                    bounds.Right = Margin.Right;
                    break;
            }

            switch (VertAlign)
            {
                case VertAlign.Top:
                    bounds.Top = Margin.Top;
                    bounds.Bottom = bounds.Top + Size.Height;
                    break;
                case VertAlign.Center:
                    bounds.Top = (Parent.Size.Height - Size.Height) / 2 + Margin.Top;
                    bounds.Bottom = bounds.Top + Size.Height;
                    break;
                case VertAlign.Bottom:
                    bounds.Top = Parent.Size.Height - Size.Height - Margin.Bottom;
                    bounds.Bottom = bounds.Top + Size.Height;
                    break;
                case VertAlign.Stretch:
                    bounds.Top = Margin.Top;
                    bounds.Bottom = Margin.Bottom;
                    break;
            }

            Position = bounds.Location;
            return bounds;
        }

        public virtual void Render(SKCanvas canvas)
        {
            if (Transform.HasValue) canvas.SetMatrix(Transform.Value);
        }
    }
}
