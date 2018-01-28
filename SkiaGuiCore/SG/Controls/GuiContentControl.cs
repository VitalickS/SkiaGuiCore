namespace SkiaGuiCore.SG.Controls
{
    public class GuiContentControl : GuiControl
    {
        // Layout
        public Thickness Padding { get; set; }
        public HorizAlign HContentAlign { get; set; }
        public VertAlign VContentAlign { get; set; }

        // Content
        public object Content { get; set; }
    }
}