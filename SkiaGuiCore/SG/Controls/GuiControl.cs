using System.Xml.Linq;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public class GuiControl : GuiComponent
    {
        // Pos
        public Thickness Margin { get; set; }
        public HorizAlign HorizAlign { get; set; }
        public VertAlign VertAlign { get; set; }
        // Size
        public float MaxWidth { get; set; }
        public float MaxHeight { get; set; }

        // Brushes
        [DefaultPaintStyle(SKPaintStyle.Fill)]
        public SKPaint Foreground { get; set; }
        [DefaultPaintStyle(SKPaintStyle.Fill)]
        public SKPaint Background { get; set; }

        // Transformation
        public SKMatrix? Transform { get; set; }

        // Style
        public object StyleKey { get; set; }
        public XElement Template { get; set; }

        public override SKRect Measure(SKSize availableSize)
        {
            var bounds = new SKRect();
            if (Width > MaxWidth) Width = MaxWidth;
            if (Height > MaxHeight) Height = MaxHeight;

            switch (HorizAlign)
            {
                case HorizAlign.Left:
                    bounds.Left = (int)Margin.Left;
                    bounds.Right = bounds.Left + Width;
                    break;
                case HorizAlign.Center:
                    bounds.Left = (availableSize.Width - Width) / 2 + (int)Margin.Left;
                    bounds.Right = bounds.Left + Width;
                    break;
                case HorizAlign.Right:
                    bounds.Left = availableSize.Width - Width - (int)Margin.Right;
                    bounds.Right = bounds.Left + Width;
                    break;
                case HorizAlign.Stretch:
                    bounds.Left = (int)Margin.Left;
                    bounds.Right = availableSize.Width - (int)Margin.Right;
                    break;
            }

            switch (VertAlign)
            {
                case VertAlign.Top:
                    bounds.Top = (int)Margin.Top;
                    bounds.Bottom = bounds.Top + Height;
                    break;
                case VertAlign.Center:
                    bounds.Top = (availableSize.Height - Height) / 2 + (int)Margin.Top;
                    bounds.Bottom = bounds.Top + Height;
                    break;
                case VertAlign.Bottom:
                    bounds.Top = availableSize.Height - Height - (int)Margin.Bottom;
                    bounds.Bottom = bounds.Top + Height;
                    break;
                case VertAlign.Stretch:
                    bounds.Top = (int)Margin.Top;
                    bounds.Bottom = availableSize.Height - (int)Margin.Bottom;
                    break;
            }
            return bounds;
        }

        public override void Render(SKCanvas canvas)
        {
            var bounds = Measure(this.GetSize());
            canvas.DrawRect(bounds, Background);
        }
    }
}
