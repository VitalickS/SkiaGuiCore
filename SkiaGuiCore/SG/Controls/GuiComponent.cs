using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

namespace SkiaGuiCore.SG.Controls
{
    public abstract class GuiComponent : IGuiComponent
    {
        public IGuiComponent Parent { get; set; }
        public float Width { get; set; } = float.NaN;
        public float Height { get; set; } = float.NaN;
        public SKRect Bounds { get; protected set; }
        public object DataContext { get; set; }

        /// <summary>
        /// Measure element and return Bounds
        /// </summary>
        /// <param name="availableSize">Available size to insert into</param>
        /// <returns>Calculated bounds</returns>
        public abstract SKRect Measure(SKSize availableSize);
        public abstract void Render(SKCanvas canvas);
    }
}
