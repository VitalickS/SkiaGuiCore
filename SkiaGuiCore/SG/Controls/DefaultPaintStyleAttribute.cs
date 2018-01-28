using System;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public class DefaultPaintStyleAttribute : Attribute
    {
        public SKPaintStyle Fill { get; }
        public bool Antialias { get; }

        public DefaultPaintStyleAttribute(SKPaintStyle fill, bool antialias = true)
        {
            Fill = fill;
            Antialias = antialias;
        }
    }
}