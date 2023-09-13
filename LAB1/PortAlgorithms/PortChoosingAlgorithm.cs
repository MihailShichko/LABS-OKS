using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LAB1.PortAlgorithms
{
    public class PortChoosingAlgorithm
    {
        private static bool IsEven(int num)
        {
            return num % 2 == 0;
        }

        public Func<string[], SerialPort, string> OutputPortChoosing = (ports, Port) => ports.Last(port => IsEven(Convert.ToInt32(port[^1])) && port != Port.PortName);

        public Func<string[], SerialPort, string> InputPortChoosing = (ports, Port) => ports.First(port => !IsEven(Convert.ToInt32(port[^1])) && port != Port.PortName);
    }
}
