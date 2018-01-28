using System.Reflection.Metadata;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public class RenderResult
    {
        public SKRect Bounds { get; set; }
        public SKSurface Surface { get; set; }
    }
}