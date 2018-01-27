using System.Collections.Generic;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public interface IGuiComponent
    {
        Thickness Margin { get; set; }
        SKPointI Position { get; }
        SKSizeI VisualSize { get; set; }
        float MaxWidth { get; set; }
        float MaxHeight { get; set; }

        HorizAlign HorizAlign { get; set; }
        VertAlign VertAlign { get; set; }

        SKPaint Foreground { get; set; }
        SKPaint Background { get; set; }
        object Content { get; set; }
        object DataContext { get; set; }

        IGuiComponent Parent { get; set; }
        SKMatrix? Transform { get; }
        SKRectI Measure();
        void Render(SKCanvas canvas);
    }
} 