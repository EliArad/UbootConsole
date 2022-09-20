using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace GSkinLib
{
    public class ResourceManager
    {
        string m_fileName;
        Dictionary<string, Bitmap> m_dicRes = new Dictionary<string, Bitmap>();
        public ResourceManager(string fileName)
        {
            m_fileName = fileName;

        }
        public bool Load()
        {
            if (File.Exists(m_fileName) == false)
                return false;
            ResourceReader res = new ResourceReader(m_fileName);
            IDictionaryEnumerator dict = res.GetEnumerator();
            while (dict.MoveNext())
                m_dicRes.Add((string)dict.Key, (Bitmap)dict.Value);

            res.Close();
            return true;
        }
        public Bitmap GetBitmap(string name)
        {
            return m_dicRes[name];
        }
        Bitmap[] b = new Bitmap[4];
        public Bitmap [] GetBitmaps(string name)
        {
            b[0] = m_dicRes[name + "_normal"];
            b[1] = m_dicRes[name + "_enter"];
            b[2] = m_dicRes[name + "_press"];
            if (m_dicRes.ContainsKey(name + "_disable") == true)
                b[3] = m_dicRes[name + "_disable"];
            else
                b[3] = null;

            
            return b;
        }
    }
}
