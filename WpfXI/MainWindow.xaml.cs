using System;
using System.Windows.Forms;
using System.Drawing;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace WpfXI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Closing += (sender, e) =>
            {
                e.Cancel = true; // Cancel the closing of the window
                this.Hide(); // Hide the window instead
            };
        }

        public string WPFMessage
        {
            get
            {
                return "Binding from WPF to UWP XAML";
            }
        }

        private void WindowsXamlHost_ChildChanged(object sender, EventArgs e)
        {
            // Hook up x:Bind source.
            global::Microsoft.Toolkit.Wpf.UI.XamlHost.WindowsXamlHost windowsXamlHost =
                sender as global::Microsoft.Toolkit.Wpf.UI.XamlHost.WindowsXamlHost;
            global::UWPClassLibrary.MyUserControl userControl =
                windowsXamlHost.GetUwpInternalObject() as global::UWPClassLibrary.MyUserControl;

            if (userControl != null)
            {
                userControl.XamlIslandMessage = this.WPFMessage;
            }
        }
    }
}