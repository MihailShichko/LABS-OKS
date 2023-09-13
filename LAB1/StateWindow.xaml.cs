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
    /// Interaction logic for StateWindow.xaml
    /// </summary>
    public partial class StateWindow : Window
    {
        public SerialPort InputPort { get; set; }
        public SerialPort OutputPort { get; set; }
        public StateWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InputInfo.Text = InputPort.GetInfo();
            OutputInfo.Text = OutputPort.GetInfo();
        }
    }

    public static class SerialPortExtensions
    {
        public static string GetInfo(this SerialPort port)
        {
            var builder = new StringBuilder();
            builder.AppendLine("Port name: " + port.PortName);
            builder.AppendLine("Parity: " + port.Parity);
            builder.AppendLine("BaudRate: " + port.BaudRate);
            builder.AppendLine("DataBits: " + port.DataBits);
            builder.AppendLine("StopBits: " + port.StopBits);
            return builder.ToString();
        }
    }
}


