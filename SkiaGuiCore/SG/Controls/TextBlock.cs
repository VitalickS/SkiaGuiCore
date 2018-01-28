using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public class TextBlock : GuiControl
    {
        public string Text { get; set; }


        public override SKRect Measure(SKSize availableSize)
        {
            var bounds = base.Measure(availableSize);
            if (!string.IsNullOrWhiteSpace(Text))
            {
                var width = Foreground.MeasureText(Text, ref bounds);
                return new SKRectI(0, 0, (int)width, (int)Foreground.FontMetrics.Top);
            }
            return SKRect.Empty;
        }

        public override void Render(SKCanvas canvas)
        {
            var bounds = new SKRect(Bounds.Left, Bounds.Top, Bounds.Width, Bounds.Height);
            var textWidth = Foreground.MeasureText(Text, ref bounds);
            canvas.DrawText(Text, Bounds.Left + (Bounds.Width - textWidth) / 2, Bounds.Top + (Bounds.Height + bounds.Height) / 2, Foreground);
        }
    }
}
