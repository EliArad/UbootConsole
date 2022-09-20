using System;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;


namespace CommunicationLayerLib
{
   

    public class RS232 : ISerialComm
    {

         
        // The server and client are in the same place.
        public RS232(IRS232 p = null) : base(p)
        {
            pIRS232 = p;
        }

        public override bool Connect(string ComPort,
                                    int baudrate,
                                    Parity parity,
                                    StopBits stopBits,
                                    int DataBits,
                                    READ_STYLE readStyle,
                                    out string error)


        {
            try
            {

                if (m_serialPort.IsOpen == true)
                {
                    error = "already open";
                    return true;
                }

                m_serialPort.PortName = ComPort;
                m_serialPort.BaudRate = baudrate;
                m_serialPort.Parity = parity;
                m_serialPort.DataBits = DataBits;
                m_serialPort.StopBits = stopBits;
                m_serialPort.Handshake = Handshake.None;

                // Set the read/write timeouts
                m_serialPort.ReadTimeout = -1;
                m_serialPort.WriteTimeout = 500;
                m_serialPort.Open();
                if (m_serialPort.IsOpen == false)
                {
                    error = "failed to open serial port " + ComPort;
                    return false;
                }
                if (readStyle == READ_STYLE.BYTES)
                {
                    m_thread = new Thread(ReceiverThread);
                    m_thread.Start();
                }
                if (readStyle == READ_STYLE.LINE)
                {
                    m_thread = new Thread(ReceiverThread2);
                    m_thread.Start();
                    
                }
                error = string.Empty;
                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }
         
        public override bool Connect(string ComPort,
                                    int baudrate,
                                    bool receiveThread,
                                    out string error)


        {
            try
            {

                if (m_serialPort.IsOpen == true)
                {
                    error = "already open";
                    return true;
                }

                m_serialPort.PortName = ComPort;
                m_serialPort.BaudRate = baudrate;
                m_serialPort.Parity =  Parity.None;
                m_serialPort.DataBits = 8;
                m_serialPort.StopBits = StopBits.One;
                m_serialPort.Handshake = Handshake.None;

                // Set the read/write timeouts
                m_serialPort.ReadTimeout = -1;
                m_serialPort.WriteTimeout = 500;
                m_serialPort.Open();
                if (m_serialPort.IsOpen == false)
                {
                    error = "failed to open serial port " + ComPort;
                    return false;
                }
                if (receiveThread == true)
                {
                    m_thread = new Thread(ReceiverThread);
                    m_thread.Start();
                }
                
                error = string.Empty;
                return true;
            }
            catch (Exception err)
            {
                error = err.Message;
                return false;
            }
        }

        void ReceiverThread()
        {
            byte[] buffer = new byte[1000];
            while (m_running == true)
            {
                if (Receive(ref buffer, 1000, out int readSize) == true)
                {
                    pIRS232.NotifyRead(buffer, readSize);
                }
            }
        }

        void ReceiverThread2()
        {
   
            while (m_running == true)
            {
                string line = m_serialPort.ReadLine();                
                pIRS232.NotifyRead(line);                
            }
        }

        public override void SetDTR(bool enable)
        {
            m_serialPort.DtrEnable = enable;
        }
        public override void SetRTS(bool enable)
        {
            m_serialPort.RtsEnable = enable;
        }

        public override void SetReadTimeOut(int timeOut)
        {
            m_serialPort.ReadTimeout = timeOut;
        }

        public override bool IsOpen()
        {
            if (m_serialPort != null)
                return m_serialPort.IsOpen;
            return false;
        }

        
        public override void Send<T>(T t)
        {
            lock (this)
            {
                byte[] data = StructToByteArray<T>(t);
                m_serialPort.Write(data, 0, data.Length);
            }
        }
        public override void Send(string msg)
        {
            lock (this)
            {
                m_serialPort.Write(msg);
            }
        }

        public override bool Receive(ref byte[] buf, int size, out int readSize)
        {
            readSize = 0;
            try
            {
                readSize = m_serialPort.Read(buf, 0, size);
                if (readSize > 0)
                    return true;
                return false;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public override bool Receive(ref byte[] buf, int index, int size, out int readSize)
        {
            readSize = 0;
            try
            {
                readSize = m_serialPort.Read(buf, index, size);
                if (readSize > 0)
                    return true;
                return false;
            }
            catch (Exception err)
            {
                return false;
            }
        }
        public override void SendEnter()
        {
            m_serialPort.Write(new byte[] { 13, 10 }, 0, 2);
            //m_serialPort.Write("\r\n")
        }
        public override void Send(byte[] data)
        {
            lock (this)
            {
                m_serialPort.Write(data, 0, data.Length);
            }
        }

        public override void Send(byte[] data, int size)
        {
            lock (this)
            {
                m_serialPort.Write(data, 0, size);
            }
        }

        public override void Send(byte[] data, int index, int size)
        {
            lock (this)
            {
                m_serialPort.Write(data, index, size);
            }
        }

        public override void Close()
        {
            lock (this)
            {
                m_running = false;
                if (m_serialPort != null && m_serialPort.IsOpen)
                {
                    Thread.Sleep(200);
                    m_serialPort.Close();
                    m_serialPort = null;
                }
            }
        }

    }
}
