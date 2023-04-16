using MyUwpApp;

namespace WpfXI
{
    public static class Programs
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {

            using (new MyUwpApp.App())
            {
                WpfXI.App app = new WpfXI.App();
                new Tray(app);
                app.InitializeComponent();
                app.Run();
            }

        }
    }
}
