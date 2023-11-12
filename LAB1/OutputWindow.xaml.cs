using LAB1.Builders;
using LAB1.Packages;
using LAB1.Services;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
        
        public SerialPort InputPort { get; set; }

        public PackageBuilder<BasicPackage> _packageBuilder = new PackageBuilder<BasicPackage>();

        private HammingCodeService _hammingCodeService = new HammingCodeService();
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
            message = message.Replace("0", "1");
            InputPort.WriteLine(message);
            OutputPort.DiscardInBuffer();
            OutputPort.DiscardOutBuffer();

            Thread.Sleep(10);
            bool Collision = OutputPort.ReadExisting() == "1" ? true : false;
            if (Collision) return;
            
            Dispatcher.Invoke(() => Message.Text = message);
            var packages = _packageBuilder.UnPackMessage(message.Trim('\n'));
            var stringBuilder = new StringBuilder();
            foreach (var package in packages)
            {
                stringBuilder.Append(package.GetData());
            }

            Dispatcher.Invoke(() => MessageDestaffed.Text = stringBuilder.ToString());
        }
    }
}
