using LAB1.PortAlgorithms;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAB1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        SerialPort InputPort;
        SerialPort OutputPort;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new InputWindow();
            window.Owner = this;
            window.InputPort = InputPort;
            window.Show();
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new OutputWindow();
            window.Owner = this;
            window.OutputPort = OutputPort;
            window.Show();
        }

        private void ConfigurationButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new ConfigurationWindow();
            window.OutputPort = OutputPort;
            window.InputPort = InputPort;
            window.Owner = this;
            window.Show();
        }

        private void StateButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new StateWindow();
            window.OutputPort = OutputPort;
            window.InputPort = InputPort;
            window.Owner = this;
            window.Show();
        }

        private bool IsEven(int num)
        {
            return num % 2 == 0;  
        }

        private SerialPort InitInputPort()
        {
            var inputPort = new SerialPort();
            string[] ports = SerialPort.GetPortNames();

            inputPort.PortName = ports.First(port => !IsEven(Convert.ToInt32(port[^1])) && port != inputPort.PortName);

            try
            {
                inputPort.Open();
            }
            catch (Exception ex)
            {
                hype.Text = "Привет ты второй!!!";
                inputPort.PortName = ports.First(port => !IsEven(Convert.ToInt32(port[^1])) && port != inputPort.PortName);
                try
                {
                    inputPort.Open();
                } 
                catch(Exception)
                {
                    var ErrorWindow = new ErrorWindow("There is no available ports");
                    ErrorWindow.ShowDialog();             
                }
            }
            finally
            {

                hype.Text += "Твой порт на отправке: " + inputPort.PortName + "\n";
            }

            inputPort = DefaultPortSettings(inputPort);
            return inputPort;
        }

        
        private SerialPort InitPort(Func<string[], SerialPort, string> algprithm)
        {
            var Port = new SerialPort();
            string[] ports = SerialPort.GetPortNames();

            Port.PortName = algprithm(ports, Port);

            try
            {
                Port.Open();
            }
            catch (Exception ex)
            {
                hype.Text = "Привет ты второй!!!";
                Port.PortName = algprithm(ports, Port);
                Port.Open();
            }
            finally
            {

                hype.Text += "Твой порт на отправке: " + Port.PortName + "\n";
            }

            Port = DefaultPortSettings(Port);
            return Port;
        }


        private SerialPort DefaultPortSettings(SerialPort port)
        {
            port.BaudRate = 9600;
            port.DataBits = 8;
            port.Parity = Parity.None;
            port.StopBits = StopBits.One;
            return port;
        }

        private SerialPort InitOutputPort()
        {
            var outputPort = new SerialPort();
            var ports = SerialPort.GetPortNames();
            try
            {
                outputPort.PortName = ports.Last(port => IsEven(Convert.ToInt32(port[^1])));
                outputPort.Open();
            }
            catch(Exception ex)
            {
                outputPort.PortName = ports.Last(port => IsEven(Convert.ToInt32(port[^1])) && port != outputPort.PortName);
                outputPort.Open();
            }
            finally
            {
                hype.Text += "Твой порт на прослушке: " + outputPort.PortName + "\n";
            }
            
            outputPort = DefaultPortSettings(outputPort);

            return outputPort;

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            InputPort = InitInputPort();
            OutputPort = InitOutputPort();
            //var alg = new PortChoosingAlgorithm();
            //InputPort = InitPort(alg.InputPortChoosing);
            //OutputPort = InitPort(alg.OutputPortChoosing);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InputPort.Close();
            OutputPort.Close(); 
        }

    }
}
