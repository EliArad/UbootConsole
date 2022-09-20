using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NogaChangeUbootTool
{
    public partial class Form1 : Form
    {
        UbootControl[] m_ubootControls = new UbootControl[4];
            
        public Form1()
        {
            InitializeComponent();
            ubootControl1.LoadControl(1);
            ubootControl2.LoadControl(2);
            ubootControl3.LoadControl(3);
            ubootControl4.LoadControl(4);

            m_ubootControls[0] = ubootControl1;
            m_ubootControls[1] = ubootControl2;
            m_ubootControls[2] = ubootControl3;
            m_ubootControls[3] = ubootControl4;

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseAll();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.F8)
            {
                for (int i = 0; i < m_ubootControls.Length; i++)
                {
                    if (m_ubootControls[i].Open() == false)
                    {
                        CloseAll();
                        MessageBox.Show("Failed to open Uboot " + i + 1);
                        return;
                    }
                }
            }
        }
        void CloseAll()
        {
            for (int i = 0; i < m_ubootControls.Length; i++)
            {
                m_ubootControls[i].Close();
            }
        }
    }
}
