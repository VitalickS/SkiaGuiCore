using System.Runtime.CompilerServices;
using OpenTK;
using OpenTK.Graphics;
using SkiaGuiCore.SG.Controls;
using SkiaSharp;

namespace SkiaGuiCore.SG
{
    public class Window : GameWindow, IGuiComponent
    {
        private VisualContext _context;
        public Window() : this(300, 300)
        {
            Initialize();
        }

        public Window(int width, int height) : base(width, height)
        {
            Initialize();
        }

        public Window(int width, int height, GraphicsMode mode, string title = "", GameWindowFlags options = GameWindowFlags.Default) : base(width, height, mode, title, options)
        {
            Initialize();
        }

        private void Initialize()
        {
            _context = new VisualContext(this);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            _context.Reset();

        }

        public Thickness Margin { get; set; }
        public SKPointI Position => Margin.Location;

        public SKSizeI VisualSize
        {
            get { return new SKSizeI(ClientSize.Width, ClientSize.Height); }
            set { ClientSize = new Size(value.Width, value.Height); }
        }

        public float MaxWidth { get; set; }
        public float MaxHeight { get; set; }
        public HorizAlign HorizAlign { get; set; }
        public VertAlign VertAlign { get; set; }
        public SKPaint Foreground { get; set; }
        public SKPaint Background { get; set; }
        public object Content { get; set; }
        public object DataContext { get; set; }
        public IGuiComponent Parent { get; set; }
        public SKMatrix? Transform { get; }

        public SKRectI Measure()
        {
            return new SKRectI(X, Y, X + Width, Y + Height);
        }

        public void Render(SKCanvas canvas)
        {

        }
    }
}
