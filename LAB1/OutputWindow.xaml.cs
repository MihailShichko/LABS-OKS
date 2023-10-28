using LAB1.Builders;
using LAB1.Packages;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LAB1
{
    /// <summary>
    /// Interaction logic for OutputWindow.xaml
    /// </summary>
    public partial class OutputWindow : Window
    {
        public SerialPort OutputPort { get; set; }

        public PackageBuilder<BasicPackage> _packageBuilder = new PackageBuilder<BasicPackage>();
        public OutputWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OutputPort.DataReceived += RecieveHandler;
        }

        private void RecieveHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string message = string.Empty;
            message = OutputPort.ReadExisting();
            Dispatcher.Invoke(() => Message.Text = message);
            var packages = _packageBuilder.UnPackMessage(message);
            var stringBuilder = new StringBuilder();
            foreach (var package in packages)
            {
                stringBuilder.Append("Data:" + package.Data);
            }

            Dispatcher.Invoke(() => MessageDestaffed.Text = stringBuilder.ToString());
        }
    }
}
