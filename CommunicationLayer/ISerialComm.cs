using System;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;


namespace CommunicationLayerLib
{

    public enum READ_STYLE
    {
        BYTES,
        LINE,
        NONE
    }
    public interface IRS232
    {
        void NotifyRead(byte[] data, int size);
        void NotifyRead(string line);
    }

    public abstract class ISerialComm
    {

        protected Thread m_thread = null;
        protected SerialPort m_serialPort = new SerialPort();
        protected IRS232 pIRS232;

        // The server and client are in the same place.
        public ISerialComm(IRS232 p)
        {
            pIRS232 = p;
        }

        public abstract bool Connect(string ComPort,
                                    int baudrate,
                                    bool receiveThread,
                                    out string error);

        public abstract bool Connect(string ComPort,
                                    int baudrate,
                                    Parity parity,
                                    StopBits stopBits,
                                    int DataBits,
                                    READ_STYLE readStyle,
                                    out string error);


        protected bool m_running = true;

        public abstract void SetDTR(bool enable);
        public abstract void SetRTS(bool enable);
        public abstract void SetReadTimeOut(int timeOut);
        public abstract bool IsOpen();
        public abstract void SendEnter();

        public byte[] StructToByteArray<T>(T structVal) where T : struct
        {
            int size = Marshal.SizeOf(structVal);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structVal, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
        public abstract void Send<T>(T t) where T : struct;

        public abstract void Send(string msg);
        public abstract bool Receive(ref byte[] buf, int size, out int readSize);
        public abstract bool Receive(ref byte[] buf, int size, int index, out int readSize);
        public abstract void Send(byte[] data);
        public abstract void Send(byte[] data, int size);
        public abstract void Send(byte[] data, int index, int size);
        public abstract void Close();
         

    }
}
