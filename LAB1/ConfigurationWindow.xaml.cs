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
    /// Interaction logic for ConfigurationWindow.xaml
    /// </summary>
    public partial class ConfigurationWindow : Window
    {
        public SerialPort InputPort { get; set; }
        public SerialPort OutputPort { get; set; }

        public ConfigurationWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InputParity.SelectedValue = InputPort.Parity;
            OutputParity.SelectedValue = OutputPort.Parity;
        }


        private void InputParity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InputPort.Parity = (Parity)InputParity.SelectedValue;
        }
    

        private void OutputParity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OutputPort.Parity = (Parity)OutputParity.SelectedValue;

        }
    }
}
