using System;
using System.Diagnostics;

namespace ExampleLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Examples.Ex01.App();
            app.Run();
        }
    }
}
