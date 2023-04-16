using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Windows.UI.Xaml.Controls;

namespace WpfXI
{
    internal class Tray
    {
        private App mainApp;
        private Form trayForm;
        public Tray(WpfXI.App app)
        {
            mainApp = app;
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

            var fourthItem = new MenuFlyoutItem { Text = "Fourth item" };
            fourthItem.Click += (sender, e) => ShowWindow();
            flyout.Items.Add(fourthItem);

            var fifthItem = new MenuFlyoutItem { Text = "Fifth item with icon", Icon = new SymbolIcon(Symbol.Cancel) };
            fifthItem.Click += (sender, e) =>
            {
                trayForm.Close();
                mainApp.Shutdown();
            };
            flyout.Items.Add(fifthItem);


            flyout.ShouldConstrainToRootBounds = false;
            flyout.Placement = Windows.UI.Xaml.Controls.Primitives.FlyoutPlacementMode.Top;
            container.Content = flyout;
            trayXamlHost.Child = container;

            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = new Icon(@"C:\Users\Drac\Downloads\icon.ico");
            notifyIcon.Visible = true;

            notifyIcon.MouseUp += (sender, e) => // needs debugging
            {
                // Flyout shows around top left corner of wpf window on first use
                //trayForm.Location = new System.Drawing.Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
                //trayForm.Show();
                //flyout.ShowAt(container, new Windows.Foundation.Point(0, 0));
                //trayForm.Focus();
                //trayForm.Activate();

                // Flyout shows at wrong position at first use, but next to systray
                trayForm.Location = new System.Drawing.Point(0, 0);
                trayForm.Show();
                flyout.ShowAt(container, new Windows.Foundation.Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y));
                trayForm.Focus();
                trayForm.Activate();
            };

            trayForm.LostFocus += (sender, e) =>
            {
                flyout.Hide();
                trayForm.Hide();
            };
        }
        private void ShowWindow()
        {
            mainApp.MainWindow.Show();
        }
    }
}
