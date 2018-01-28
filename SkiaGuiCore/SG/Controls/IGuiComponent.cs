using System.Collections.Generic;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public interface IGuiComponent
    {
        float Width { get; set; }
        float Height { get; set; }
        object DataContext { get; set; }
        SKRect Measure(SKSize availableSize);
        void Render(SKCanvas canvas);
    }
}