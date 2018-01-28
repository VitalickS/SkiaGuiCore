using System;
using System.Collections.Generic;
using System.IO;
using OpenTK;
using SkiaGuiCore.SG.Controls;
using SkiaSharp;

namespace SkiaGuiCore.SG
{
    public class VisualContext : IDisposable
    {
        internal static GRContext GrContext = GRContext.Create(GRBackend.OpenGL, GRGlInterface.CreateNativeGlInterface());
        private SKSurface _backSurface;
        private readonly List<SKRectI> _elementBounds = new List<SKRectI>();
        private readonly List<SKRectI> _dirtyRects = new List<SKRectI>();
        public SKCanvas Canvas => _backSurface.Canvas;
        public IReadOnlyList<SKRectI> DirtyRects => _dirtyRects;
        public IReadOnlyList<SKRectI> ElementBounds => _elementBounds;
        public Window Target { get; }
        public INativeWindow NativeWindow => Target.NativeWindow;
        public IGuiComponent RenderComponent { get; private set; }

        public VisualContext(Window target)
        {
            Target = target;
            InitBackSurf();
        }

        private void InitBackSurf()
        {
            _backSurface?.Dispose();
            _backSurface = SKSurface.Create(GrContext, new GRBackendRenderTargetDesc()
            {
                Width = Target.Size.Width,
                Height = Target.Size.Height,
                SampleCount = 0,
                StencilBits = 0,
                Config = GRPixelConfig.Rgba8888
            });
        }

        public void AddVisual(IGuiComponent component)
        {
            RenderComponent = component;
        }

        public void Reset()
        {
            _dirtyRects.Clear();
            _elementBounds.Clear();
        }

        public void AddVisual(SKRectI bounds, SKRectI dirty)
        {
            _elementBounds.Add(bounds);
            _dirtyRects.Add(dirty);
        }

        public void Dispose()
        {
            Canvas?.Dispose();
            _backSurface?.Dispose();
        }

        public SKImage Snapshot()
        {
            return _backSurface.Snapshot();
        }

        public void SaveToImage(string pngFile)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(pngFile));
            using (var snap = Snapshot())
            using (var data = snap.Encode())
            using (var stream = File.Create(pngFile))
            {
                data.SaveTo(stream);
            }
        }

        public void Render()
        {
            RenderComponent.Render(Canvas);
        }
    }
}
