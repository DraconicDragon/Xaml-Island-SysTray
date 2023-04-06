using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Wpf.UI.XamlHost;
using System.Runtime.CompilerServices;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Hosting;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI;
using static System.Windows.Forms.DataFormats;
using System.ComponentModel;

namespace WpfXI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private NotifyIcon _notifyIcon;
        private WindowsXamlHost wxh;
        private UWPClassLibrary.MyUserControl uc;
        private Form form2;
        private MenuFlyout flyout;
        private Windows.UI.Xaml.Controls.ContentControl container;
        private Windows.UI.Xaml.Controls.Button uwpB;

        public MainWindow()
        {
            InitializeComponent();

            var xamlHost = new Microsoft.Toolkit.Forms.UI.XamlHost.WindowsXamlHost();
            var container = new Windows.UI.Xaml.Controls.ContentControl();

            form2 = new Form();
            form2.TopMost = true;
            form2.Size = new System.Drawing.Size(1, 1);
            form2.BackColor = System.Drawing.Color.Red;
            form2.TransparencyKey = form2.BackColor;
            form2.Opacity = 0.5;
            form2.ShowInTaskbar = false;
            form2.FormBorderStyle = FormBorderStyle.None;
            //form2.Bounds = Screen.PrimaryScreen.Bounds;


            form2.Controls.Add(xamlHost);
            form2.Activate();


            var flyout = new MenuFlyout();
            flyout.Items.Add(new MenuFlyoutItem { Text = "aa" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "11111111111" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "11111111111" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "11111111111" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "11111111111" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "11111111111" });
            flyout.Items.Add(new MenuFlyoutSeparator());
            flyout.Items.Add(new MenuFlyoutItem { Text = "1111111111aaaaaaaaaa1" });
            flyout.Items.Add(new MenuFlyoutSeparator());
            flyout.Items.Add(new MenuFlyoutItem { Text = "11111111111" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "11111111111" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "11111111111" });
            flyout.ShouldConstrainToRootBounds = false;
            flyout.Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Top;
            container.Content = flyout;
            xamlHost.Child = container;

            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("C:\\Users\\Drac\\source\\repos\\WinFormsApp2\\WinFormsApp2\\icon.ico");
            notifyIcon.Visible = true;

            notifyIcon.MouseUp += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    form2.Location = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
                    form2.Show();
                    flyout.ShowAt(container, new Windows.Foundation.Point(0, 0));
                    container.Focus(FocusState.Pointer);
                    form2.Focus();
                    form2.Activate();
                }
                if (e.Button == MouseButtons.Left)
                {
                    flyout.Hide();
                    form2.Hide();
                }
            };
            form2.LostFocus += (sender, e) =>
            {
                flyout.Hide();
                form2.Hide();
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
            wxh = windowsXamlHost;
            global::UWPClassLibrary.MyUserControl userControl =
                windowsXamlHost.GetUwpInternalObject() as global::UWPClassLibrary.MyUserControl;
            uc = userControl;

            if (userControl != null)
            {
                userControl.XamlIslandMessage = this.WPFMessage;
            }
            var xamlHost = new WindowsXamlHost();
            Windows.UI.Xaml.Controls.Button uwpButton = new Windows.UI.Xaml.Controls.Button();
            uwpButton.Content = "Hello, UWP!";
            var uwpsp = new Windows.UI.Xaml.Controls.StackPanel();
            uwpsp.Background = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(122, 122, 122, 122));
            uwpsp.Children.Add(uwpButton);
            xamlHost.Child = uwpsp;
            sp.Children.Add(xamlHost);
        }
    }
}
