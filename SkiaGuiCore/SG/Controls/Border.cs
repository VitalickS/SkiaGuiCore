using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public class Border : GuiComponent
    {
        public Thickness Thickness { get; set; }

        public override void Render(SKCanvas canvas)
        {
            canvas.DrawRect(new SKRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height), Background);
            canvas.DrawRect(new SKRect(Position.X, Position.Y, Position.X + Size.Width, Position.Y + Size.Height), Foreground);
        }
    }

    public struct Thickness
    {
        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
    }

    public struct CornerRadius
    {
        public float TopLeft { get; set; }
        public float TopRight { get; set; }
        public float BottomLeft { get; set; }
        public float BottomRight { get; set; }
    }
}
