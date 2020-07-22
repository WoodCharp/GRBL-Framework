using System.IO.Ports;

namespace GRBL
{
    public class PortData
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; } = 115200;
        public int ReadTimeout { get; set; } = 500;
        public int ReadBufferSize { get; set; } = 2048;
        public int WriteTimeout { get; set; } = 3000;

        public PortData() { }

        public PortData(string portName, int baudRate)
        {
            PortName = portName;
            BaudRate = baudRate;
        }

        /// <summary>
        /// Set port data to SerialPort
        /// </summary>
        /// <param name="sp">Serial Port to set</param>
        public void PortDataToSerialPort(SerialPort sp)
        {
            sp.PortName = PortName;
            sp.BaudRate = BaudRate;
            sp.ReadTimeout = ReadTimeout;
            sp.ReadBufferSize = ReadBufferSize;
            sp.WriteTimeout = WriteTimeout;
        }

        /// <summary>
        /// Get available ports
        /// </summary>
        /// <returns>COMx, where 'x' is number</returns>
        public static string[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
