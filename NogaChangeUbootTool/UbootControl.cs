using CommunicationLayerLib;
using DBConnLib;
using InvokersLib;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NogaChangeUbootTool
{
    public partial class UbootControl : UserControl, IRS232
    {
        ConcurrentQueue<string> m_queue = new ConcurrentQueue<string>();
        int m_index = -1;
        UbootConsole m_db = new UbootConsole();
        RS232 m_comm = new RS232();
        Task m_task;
        bool m_running = false;
        public UbootControl()
        {
            InitializeComponent();
            this.KeyDown += UbootControl_KeyDown;
        }
        string m_fileName;
        public void LoadControl(int index)
        {
            m_index = index;
            m_fileName = $"uboot_config_{index}.json";

        }
        public void Close()
        {
            m_comm.Close();
        }
        void ParserTask()
        {
            m_running = true;
            while (m_running == true)
            {
                if (m_queue.Count > 0)
                {
                    bool b = m_queue.TryDequeue(out string line);
                    INVOKERS.InvokeControlAppendText(txtOutput, line);
                }
                else
                {
                    Thread.Sleep(2);
                }
            }
        }
        public bool Open()
        {
            m_db.Load(m_fileName);

            m_task = new Task(ParserTask);
            m_task.Start();
            m_comm.Connect(m_db.Config.ComPort,
                           m_db.Config.BaudRate,
                           m_db.Config.parity,
                           m_db.Config.stopBits,
                           m_db.Config.DataBits,
                           READ_STYLE.LINE,
                           out string outMessage);
            return true;
        }
        private void UbootControl_KeyDown(object sender, KeyEventArgs e)
        {
            m_db.Load(m_fileName);
            UbootSettingsForm f = new UbootSettingsForm(m_db.Config);
            if (f.ShowDialog() == DialogResult.OK)
            {
                UbootConsoleConfig config = f.GetConfig();
                m_db.Config.Enable = config.Enable;
                if (config.Enable == true)
                {
                    m_db.Config.BaudRate = config.BaudRate;
                    m_db.Config.ComPort = config.ComPort;
                    m_db.Config.StopString = config.StopString;
                    m_db.Config.stopBits = config.stopBits;
                    m_db.Config.parity = config.parity;
                    m_db.Config.DataBits = config.DataBits;
                }
                m_db.Save();
            }
        }

        public void NotifyRead(byte[] data, int size)
        {
            
        }

        public void NotifyRead(string line)
        {
            m_queue.Enqueue(line);
        }
    }
}
