using OpenTK;
using OpenTK.Graphics;
using SkiaGuiCore.SG.Controls;
using SkiaSharp;

namespace SkiaGuiCore.SG
{
    public class Window : IGuiComponent
    {
        public readonly GameWindow NativeWindow;
        private readonly VisualContext _context;
        public float Height { get; set; }
        public float Width { get; set; }


        public Window() : this(300, 300)
        {
            Initialize();
        }

        public Window(int width, int height)
        {
            Width = width;
            Height = height;
            NativeWindow = new GameWindow(width, height);
            _context = new VisualContext(this);
            Initialize();
        }


        public Window(int width, int height, GraphicsMode mode, string title = "", GameWindowFlags options = GameWindowFlags.Default)
        {
            Width = width;
            Height = height;
            NativeWindow = new GameWindow(width, height, mode, title, options);
            _context = new VisualContext(this);
            Initialize();
        }

        private void Initialize()
        {
            NativeWindow.RenderFrame += NativeWindowRenderFrame;
        }

        private void NativeWindowRenderFrame(object sender, FrameEventArgs e)
        {
            _context.Reset();
            _context.Render();
        }

        public Thickness Margin { get; set; }
        public SKPointI Position { get; set; }

        public SKSizeI Size
        {
            get => new SKSizeI(NativeWindow.ClientSize.Width, NativeWindow.ClientSize.Height);
            set => NativeWindow.ClientSize = new Size(value.Width, value.Height);
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

        public SKRect Measure(SKSize availableSize)
        {
            return new SKRect(NativeWindow.X, NativeWindow.Y, NativeWindow.X + Width, NativeWindow.Y + Height);
        }

        public void Render(SKCanvas canvas)
        {
        }
    }
}
