using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaGuiCore.SG.Controls
{
    public struct VisibilityStruct
    {
        public float Opacity { get; set; }
        public bool Visible { get; set; }
        public bool Hidden { get; set; }
        public bool Relative { get; set; }

        public bool HasVisual => Opacity > 0 && Visible && !Hidden && !Relative;

        public VisibilityStruct(float opacity = 1, bool visible = true, bool hidden = false, bool relative = false)
        {
            Opacity = opacity;
            Visible = visible;
            Hidden = hidden;
            Relative = relative;
        }
    }
}
