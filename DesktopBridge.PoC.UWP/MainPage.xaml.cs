using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DesktopBridge.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(App.Connection!=null)
            {
                messages.Items.Add("Sending: "+messageToSend.Text);
                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", messageToSend.Text);
                
                if (App.Connection != null)
                {
                    AppServiceResponse response = await App.Connection.SendMessageAsync(valueSet);
                    foreach(var item in response.Message)
                    {
                        messages.Items.Add($"Received {item.Key}: {item.Value}");
                    }
                }
            }
            else
            {
                MessageDialog dialog = new MessageDialog("The background Win32 AppService doesn't seem to run. Please see the other error messages.");
                await dialog.ShowAsync();
            }
        }

       
    }
}
