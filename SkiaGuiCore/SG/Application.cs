namespace SkiaGuiCore.SG
{
    public class Application
    {
        private Window _mainWindow;

        public Window MainWindow
        {
            get { return _mainWindow = _mainWindow ?? new Window(300, 300); }
            set => _mainWindow = value;
        }

        public void Run()
        {
            MainWindow.Run();
        }

        public void Exit()
        {
            MainWindow?.Exit();
        }
    }
}
