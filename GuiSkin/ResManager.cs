using GSkinLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSkinLib
{
    public class ResManager
    {
        static ResourceManager m_resManager;
        static bool m_skinApp = true;
        public static bool S
        {
            get
            {
                return m_skinApp;
            }
        }
        public static ResourceManager R
        {
            get
            {
                return m_resManager;
            }
        }
        public static bool Load(string FileName)
        {

            m_resManager = new ResourceManager(FileName);
            if (m_resManager.Load() == false)
            {
                m_resManager = null;
                return false;
            }
            return true;
        }
    }
}
