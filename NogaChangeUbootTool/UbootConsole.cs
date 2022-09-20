using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnLib
{
    public class UbootConsoleConfig
    { 
        public string ComPort;
        public int BaudRate;
        public string StopString;
        public bool Enable;
        public int DataBits;
        public StopBits stopBits;
        public Parity parity;
    }

    public class UbootConsole
    {

        UbootConsoleConfig m_config;
        string m_fileName;

        public UbootConsole()
        {
        }
        public UbootConsoleConfig Config
        {
            get
            {
                return m_config;
            }
        }

        public void Default()
        {
             
        }
         
        public string Save()
        {
            try
            {
               
                
                string json = JsonConvert.SerializeObject(m_config);
                string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);                    
                File.WriteAllText(m_fileName, jsonFormatted);
               
                return "ok";
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }
        
        public string Load(string fileName)
        {
            try
            {
                m_fileName = fileName;
                if (File.Exists(fileName) == false)
                {
                    m_config = new UbootConsoleConfig();
                    Save();
                    return "file not found";
                }
                string text = File.ReadAllText(m_fileName);
                m_config = JsonConvert.DeserializeObject<UbootConsoleConfig>(text);
                if (m_config == null)
                {
                    m_config = new UbootConsoleConfig();
                    Save();
                }
                 

                return "ok";
            }
            catch (Exception err)
            {
                m_config = new UbootConsoleConfig();
                return err.Message;
            }
        }
       
    }

}
