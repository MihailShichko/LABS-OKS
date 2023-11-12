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
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public SerialPort InputPort { get; set; }
        public SerialPort OutputPort { get; set; }

        private bool Collision = false;

        private string message = string.Empty;

        private PackageBuilder<BasicPackage> _packageBuilder = new PackageBuilder<BasicPackage>();
        private HammingCodeService _hammingCodeService = new HammingCodeService(); 
        public InputWindow()
        {
            InitializeComponent();
        }

        private bool AskIfCollisions()
        {
            InputPort.WriteLine("SIG:Collision");
            Random rnd = new Random();
            return rnd.Next(1,4) == 1;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                while (true)
                {
                        var packages = _packageBuilder.PackMessage(Message.Text, InputPort.PortName);
                        var stringBuilder = new StringBuilder();
                        foreach (var package in packages)
                        {
                            stringBuilder.Append(BasicPackage.Flag);
                            stringBuilder.Append(package.ToString());
                        }

                        stringBuilder.Remove(0, 1); // tupost
                        this.message = stringBuilder.ToString();
                        InputPort.WriteLine(message);
                        Note.Text = "Sent!";
                    if (!Collision)//tut menial
                    {
                        break;
                    }

                    Thread.Sleep(new Random().Next(500, 1000));
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Note.Text = InputPort.PortName;
            OutputPort.DataReceived += OutputPort_DataReceived;
        }

        private void OutputPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string dataSent = OutputPort.ReadExisting();
            if (String.Compare(this.message, dataSent) != 0)
            {
                this.Collision = true;
                InputPort.Write("1");
                Dispatcher.Invoke(() => Note.Text = "COLLISION");
            }
            else
            {
                this.Collision = false;
                InputPort.Write("0");
            }
        }
    }
}
