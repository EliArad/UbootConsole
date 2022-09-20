using System;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
 

namespace CommunicationLayerLib
{
    
    
    public class RSNull: RS232
    {
                 
        // The server and client are in the same place.
        public RSNull(IRS232 p) :base(p)
        {
            
        }

        public override bool Connect(string ComPort, 
                            int baudrate,
                            bool receiveThread,
                            out string error)

                                      
                                     
        {
            error = "it null rs";
            return true;
        }


        public override bool Connect(string ComPort,
                                  int baudrate,
                                  Parity parity,
                                  StopBits stopBits,
                                  int DataBits,
                                  READ_STYLE readStyle,
                                  out string error)
        {

            error = "it null rs";
            return true;
        }

        public override void SendEnter()
        {

        }
        public override void SetDTR(bool enable)
        {
                        
        }
        public override void SetRTS(bool enable)
        {
           
        }

        public override void SetReadTimeOut(int timeOut)
        {
             
        }

        public override bool IsOpen()
        {
            return true;
        }

        
        public override void Send<T>(T t) 
        {
            lock (this)
            {
                 
            }
        }
        public override void Send(string msg)
        {
            
        }

        public override  bool Receive(ref byte [] buf, int size, out int readSize)
        {
            readSize = 10;
            return true;
        }

        public override void Send(byte[] data)
        {
            
        }

        public override void Send(byte[] data, int size)
        {
            lock (this)
            {
                
            }
        }

        public override void Send(byte[] data, int index, int size)
        {
            lock (this)
            {
                
            }
        }

        public override void Close()
        {
            lock (this)
            {
                 
            }
        }
         
    }
}
