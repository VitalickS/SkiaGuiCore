using System.Collections.Generic;

namespace SkiaGuiCore.SG.Controls
{
    public class GuiItemsControl : GuiControl
    {
        // Items
        public IEnumerable<object> ItemsSource { get; set; }
    }
}