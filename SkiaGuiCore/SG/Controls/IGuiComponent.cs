using System.Collections.Generic;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public interface IGuiComponent
    {
        SKRectI Margin { get; set; }
        SKPointI Position { get; }
        SKSizeI Size { get; set; }
        float MaxWidth { get; set; }
        float MaxHeight { get; set; }

        HorizAlign HorizAlign { get; set; }
        VertAlign VertAlign { get; set; }

        SKPaint Foreground { get; set; }
        SKPaint Background { get; set; }
        object Content { get; set; }
        object DataContext { get; set; }

        IGuiComponent Parent { get; }
        SKMatrix? Transform { get; }
        SKRectI Measure();
        void Render(SKCanvas canvas);
    }
} 