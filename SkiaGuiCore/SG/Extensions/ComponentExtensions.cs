using System;
using System.Collections.Generic;
using System.Text;
using SkiaGuiCore.SG.Controls;
using SkiaSharp;

namespace SkiaGuiCore.SG
{
    public static class ComponentExtensions
    {
        public static SKSize GetSize(this IGuiComponent component)
        {
            return new SKSize(component.Width, component.Height);
        }

        public static SKSize GetParentSize(this GuiComponent component)
        {
            return component.Parent?.GetSize() ?? SKSize.Empty;
        }
    }
}
