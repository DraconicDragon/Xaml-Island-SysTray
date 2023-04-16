using MyUwpApp;

namespace test
{
    public static class Programs
    {
        [System.STAThread()]
        public static void Main()
        {

            using (new MyUwpApp.App())
            {
            }

        }
    }
}
