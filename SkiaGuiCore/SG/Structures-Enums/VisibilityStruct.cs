namespace SkiaGuiCore.SG
{
    public struct VisibilityStruct
    {
        public float Opacity { get; set; }
        public bool Visible { get; set; }
        public bool Hidden { get; set; }
        public bool NoLayout { get; set; }

        public bool HasVisual => Opacity > 0 && Visible && !Hidden;

        public VisibilityStruct(float opacity = 1, bool visible = true, bool hidden = false, bool noLayout = false)
        {
            Opacity = opacity;
            Visible = visible;
            Hidden = hidden;
            NoLayout = noLayout;
        }
    }
}
