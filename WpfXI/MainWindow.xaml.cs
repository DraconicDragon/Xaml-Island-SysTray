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
        private Form trayForm;

        public MainWindow()
        {
            InitializeComponent();

            var trayXamlHost = new Microsoft.Toolkit.Forms.UI.XamlHost.WindowsXamlHost();
            var container = new Windows.UI.Xaml.Controls.ContentControl();

            trayForm = new Form();
            trayForm.TopMost = true;
            trayForm.Size = new System.Drawing.Size(1, 1);
            trayForm.BackColor = System.Drawing.Color.Red;
            trayForm.TransparencyKey = trayForm.BackColor;
            trayForm.Opacity = 0.5;
            trayForm.ShowInTaskbar = false;
            trayForm.FormBorderStyle = FormBorderStyle.None;


            trayForm.Controls.Add(trayXamlHost);
            trayForm.Activate();


            var flyout = new MenuFlyout();
            flyout.Items.Add(new MenuFlyoutSubItem { Text = "First subitem" });
            flyout.Items.Add(new MenuFlyoutSubItem { Text = "Second subitem" });
            flyout.Items.Add(new MenuFlyoutSubItem { Text = "Third subitem" });
            flyout.Items.Add(new MenuFlyoutSubItem { Text = "Fourth subitem" });
            flyout.Items.Add(new MenuFlyoutSubItem { Text = "Fifth subitem" });
            flyout.Items.Add(new MenuFlyoutSeparator());
            flyout.Items.Add(new MenuFlyoutItem { Text = "First item" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "Second Item" });
            flyout.Items.Add(new MenuFlyoutSeparator());
            flyout.Items.Add(new MenuFlyoutItem { Text = "Third looooooong item" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "Fourth item" });
            flyout.Items.Add(new MenuFlyoutItem { Text = "Fifth item with icon", Icon = new SymbolIcon(Symbol.Cancel)});
            flyout.ShouldConstrainToRootBounds = false;
            flyout.Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Top;
            container.Content = flyout;
            trayXamlHost.Child = container;

            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon("C:\\Users\\Drac\\source\\repos\\WinFormsApp2\\WinFormsApp2\\icon.ico");
            notifyIcon.Visible = true;

            notifyIcon.MouseUp += (sender, e) =>
            {
                trayForm.Location = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
                trayForm.Show();
                flyout.ShowAt(container, new Windows.Foundation.Point(0, 0));
                trayForm.Focus();
                trayForm.Activate();
            };

            trayForm.LostFocus += (sender, e) =>
            {
                flyout.Hide();
                trayForm.Hide();
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