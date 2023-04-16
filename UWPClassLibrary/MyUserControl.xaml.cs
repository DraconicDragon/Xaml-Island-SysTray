using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ServiceModel.Channels;
using System.Reflection.Metadata;
using Windows.Devices.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UWPClassLibrary
{
    public sealed partial class MyUserControl : UserControl
    {
        public string XamlIslandMessage { get; set; }
        public MyUserControl()
        {
            this.InitializeComponent();
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            flyout.ShowAt(MyButton, new FlyoutShowOptions { ShowMode = FlyoutShowMode.Standard });
        }

        public void openFlyout(UIElement a, double b, double c)
        {
            //flyout.ShowAt(a, new Point(b, c));
            flyout.ShowAt(null, new FlyoutShowOptions
            {
                ShowMode = FlyoutShowMode.Standard,
                Position = new Point((int)b, (int)c)
            });
        }
    }
}
