using System;
using System.Collections.Generic;
using System.Linq;
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
                Width = Target.ClientSize.Width,
                Height = Target.ClientSize.Height,
                SampleCount = 0,
                StencilBits = 0,
                Config = GRPixelConfig.Rgba8888
            });
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
    }
}
