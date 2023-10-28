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
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        public SerialPort InputPort { get; set; }

        private PackageBuilder<BasicPackage> _packageBuilder = new PackageBuilder<BasicPackage>();
        public InputWindow()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                var packages = _packageBuilder.PackMessage(Message.Text, InputPort.PortName);
                var stringBuilder = new StringBuilder();
                foreach(var package in packages)
                {
                    stringBuilder.Append(BasicPackage.Flag);
                    stringBuilder.Append(package.ToString());
                }

                stringBuilder.Remove(0, 1);
                
                InputPort.WriteLine(stringBuilder.ToString());
                Note.Text = "Sent!";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Note.Text = InputPort.PortName;
        }
    }
}
