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
            //AppDomain.CurrentDomain.UnhandledException += (_, args) => { MessageBox.Show($"Exception was unhandled: {args.ExceptionObject.ToString()}"); };
        }

        private void Delay_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = "waiting";
            Delay(4000);
            Label1.Content = "finished";
        }

        private void Delay(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        private async Task DelayAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);
            //await Task.Delay(milliseconds).ConfigureAwait(false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var count = int.Parse(Count.Text) + 1;
            Count.Text = count.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            throw new InvalidOperationException();
        }
    }
}
