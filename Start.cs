namespace PaintfromScratch
{
    internal static class Start
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainWindow());
        }
    }
}