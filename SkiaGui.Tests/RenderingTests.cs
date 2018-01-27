using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
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
            const string file = "AssertionImages/border.png";
            var bord = new Border
            {
                Foreground = new SKPaint { Color = SKColors.Black, IsAntialias = true },
                BorderBrush = new SKPaint { Color = SKColors.Gray.WithAlpha(255), IsStroke = true, IsAntialias = true },
                Background = new SKPaint { Color = SKColors.White, IsStroke = false, IsAntialias = true },
                CornerRadius = new CornerRadius(35, 25, 25, 35),
                Thickness = new Thickness(5, 10, 5, 2),
                VisualSize = new SKSizeI(100, 40),
                Margin = new Thickness(5, 10, 15, 3),
                Content = "Hello World"
            };
            var visualContext = new VisualContext(_app.MainWindow);
            visualContext.AddVisual(bord);
            visualContext.Render();
            visualContext.SaveToImage(file);
            Process.Start(@"C:\Program Files\paint.net\PaintDotNet.exe", Path.Combine(Environment.CurrentDirectory, file));
        }

        public void Dispose()
        {
            _app.Exit();
        }
    }
}
