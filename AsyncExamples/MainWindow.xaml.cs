using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncExamples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Delay_Click(object sender, RoutedEventArgs e)
        {
            SyncOverAsync(4000);
        }

        private void SyncOverAsync(int milliseconds)
        {
            // not going all the way with async
            DelayAsync(milliseconds).Wait();
        }

        private async Task DelayAsync(int milliseconds)
        {
            //await Task.Delay(milliseconds);
            await Task.Delay(milliseconds).ConfigureAwait(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var count = int.Parse(Count.Text) + 1;
            Count.Text = count.ToString();
        }
    }
}
