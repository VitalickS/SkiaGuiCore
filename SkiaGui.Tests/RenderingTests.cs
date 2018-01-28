using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SkiaGuiCore.SG;
using SkiaGuiCore.SG.Controls;
using SkiaSharp;
using Xunit;

namespace SkiaGui.Tests
{
    public class RenderingTests : IDisposable
    {
        private readonly Application _app;

        public RenderingTests()
        {
            _app = new Application();
        }

        [Fact]
        public void ShowWindow()
        {
            Task.Run(() => _app.Run()).Wait(3000);
        }

        [Fact]
        public void BorderAndLayout()
        {
            const string file = "..\\..\\..\\AssertionImages\\border.png";
            var bord = new Border
            {
                Foreground = new SKPaint { Color = SKColors.White, IsAntialias = true },
                BorderBrush = new SKPaint { Color = SKColors.Gray.WithAlpha(255), IsStroke = true, IsAntialias = true },
                Background = new SKPaint
                {
                    Color = SKColors.White,
                    IsStroke = false,
                    IsAntialias = true,
                    Shader = SKShader.CreateLinearGradient(new SKPoint(0, 250), new SKPoint(0, 280),
                        new[] { new SKColor(70, 70, 70), SKColors.Black },
                        new[] { 0f, 1f }, SKShaderTileMode.Clamp)
                },
                CornerRadius = new CornerRadius(10),
                Thickness = new Thickness(1, 5),
                HorizAlign = HorizAlign.Left,
                VertAlign = VertAlign.Top,
                Width = 100,
                Height = 50,
                Margin = new Thickness(20),
                Content = "Hello World"
            };
            var visualContext = new VisualContext(_app.MainWindow);
            visualContext.AddVisual(bord);
            visualContext.Render();
            visualContext.SaveToImage(file);
            Process.Start("mspaint.exe", Path.Combine(Environment.CurrentDirectory, file));
        }

        public void Dispose()
        {
            _app.Exit();
        }
    }
}
