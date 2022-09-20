using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GSkinLib.GSkinCommon;

namespace GSkinLib
{
    public class SkinButton : ButtonEx
    {
        int m_buttonId = -1;
        public GuiBackground.WITH_AS ControlAs = GuiBackground.WITH_AS.IMAGE;
        bool m_enable = true;
        Bitmap[] bmp = new Bitmap[4];
        bool m_enableButtonExists = false;
        ACTIONS m_state;
        bool m_forePress = false;
        BUTTON_NAME m_btnName;
        private static string m_dir;
        IButton pButton = null;
        ReleaseTimer m_timer;

        
        public SkinButton(BUTTON_NAME name)
        {
            m_btnName = name;
            UseVisualStyleBackColor = false;
            
        }
        public SkinButton()
        {
            UseVisualStyleBackColor = false;
        }
        public static void BaseDir(string b)
        {
            m_dir = b;
        }
        public void NotifyOnTimer(BUTTON_NAME name, int timeInMili)
        {
            m_timer = new ReleaseTimer(pButton, name, timeInMili);
        }

        public void SetControlsAs(GuiBackground.WITH_AS c)
        {
            ControlAs = c;
        }
        public void SetSkin(Bitmap [] bitmaps, BUTTON_NAME buttonName, string name)
        {
            bmp[0] = bitmaps[0];
            bmp[1] = bitmaps[1];
            bmp[2] = bitmaps[2];
            bmp[3] = bitmaps[3];
            if (bmp[3] != null)
            {
                m_enableButtonExists = true;
            }
            else
            {
                m_enableButtonExists = false;

            }
            if (name != string.Empty)
            {
                this.Text = name;
            }
            GuiBackground.CreateControlRegion(this, bmp[0], ControlAs);
            m_state = ACTIONS.LEAVE;
            m_btnName = buttonName;

            this.MouseDown += SkinButton_MouseDown;
            this.MouseEnter += SkinButton_MouseEnter;
            this.MouseLeave += SkinButton_MouseLeave;
            this.MouseUp += SkinButton_MouseUp;

        }

        public void SetSkin(Bitmap[] bitmaps, float wscale = 1, float hscale = 1)
        {
            bmp[0] = bitmaps[0];
            bmp[1] = bitmaps[1];
            bmp[2] = bitmaps[2];
            bmp[3] = bitmaps[3];

            if (wscale < 1 || hscale < 1)
            {
                for (int i = 0; i < bmp.Length; i++)
                {
                    bmp[i] = new Bitmap(bmp[i], new Size((int)(bitmaps[i].Width * wscale), (int)(bmp[i].Height * hscale)));
                }
            }


            if (bmp[3] != null)
            {
                m_enableButtonExists = true;
            }
            else
            {
                m_enableButtonExists = false;

            }             
            GuiBackground.CreateControlRegion(this, bmp[0], ControlAs);
            m_state = ACTIONS.LEAVE;
            m_btnName = BUTTON_NAME.NONAME;

            this.MouseDown += SkinButton_MouseDown;
            this.MouseEnter += SkinButton_MouseEnter;
            this.MouseLeave += SkinButton_MouseLeave;
            this.MouseUp += SkinButton_MouseUp;
        }

        public void SetSkin(Bitmap[] bitmaps, string name)
        {
            bmp[0] = bitmaps[0];
            bmp[1] = bitmaps[1];
            bmp[2] = bitmaps[2];
            bmp[3] = bitmaps[3];
            if (bmp[3] != null)
            {
                m_enableButtonExists = true;
            }
            else
            {
                m_enableButtonExists = false;

            }
            this.Text = name;
            GuiBackground.CreateControlRegion(this, bmp[0], ControlAs);
            m_state = ACTIONS.LEAVE;

            SetCallback(null);

            this.MouseDown += SkinButton_MouseDown;
            this.MouseEnter += SkinButton_MouseEnter;
            this.MouseLeave += SkinButton_MouseLeave;
            this.MouseUp += SkinButton_MouseUp;

        }

        public void SetColor(Color color)
        {
            this.ForeColor = color;
        }
        
        public void UpdateText(string name)
        {
            this.Text = name;
        }
        public void SetForeColor(Color color)
        {
            this.ForeColor = color;
        }
        public void SetSkin(string btnName, BUTTON_NAME buttonName, string name = "")
        {
            m_btnName = buttonName;
            if (File.Exists(m_dir + btnName + "_normal.png"))
            {
                bmp[0] = new Bitmap(m_dir + btnName + "_normal.png");
            }
            else
            {
                throw (new SystemException("File: " + m_dir + btnName + "_normal.png" + " does not exists"));
            }

            if (File.Exists(m_dir + btnName + "_enter.png"))
            {
                bmp[1] = new Bitmap(m_dir + btnName + "_enter.png");
            }
            else
            {                
               throw (new SystemException("File: " + m_dir + btnName + "_enter.png" + " does not exists"));             
            }

            if (File.Exists(m_dir + btnName + "_press.png"))
            {
                bmp[2] = new Bitmap(m_dir + btnName + "_press.png");
            }
            else
            {
                throw (new SystemException("File: " + m_dir + btnName + "_press.png" + " does not exists"));
            }

            if (File.Exists(m_dir + btnName + "_disable.png"))
            {
                bmp[3] = new Bitmap(m_dir + btnName + "_disable.png");
                m_enableButtonExists = true;
            }
            else
            {
                m_enableButtonExists = false;
            }


            GuiBackground.CreateControlRegion(this, bmp[0], ControlAs);
            m_state = ACTIONS.LEAVE;

            this.MouseDown += SkinButton_MouseDown;
            this.MouseEnter += SkinButton_MouseEnter;
            this.MouseLeave += SkinButton_MouseLeave;
            this.MouseUp += SkinButton_MouseUp;


        }
        public void SetButtonId(int buttonId)
        {
            m_buttonId = buttonId;
        }
        public void SetSkin(Bitmap[] bitmaps, 
                            BUTTON_NAME buttonName, 
                            IButton p, 
                            string name = "")
        {
            SetSkin(bitmaps, buttonName, name);
            SetCallback(p);
        }
        public void SetCallback(IButton p)
        {
            pButton = p;
             
        }
  
        public void SetCallback(IButton p, BUTTON_NAME btnName)
        {
            pButton = p;
            m_btnName = btnName;
            
        }

        private void SkinButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_enable == false) return;

            
            GuiBackground.CreateControlRegion(this, bmp[UP], ControlAs);
             
             

            m_state = ACTIONS.UP;

            if (m_timer == null)
            {
                if (pButton != null)
                {
                    if (e.Button == MouseButtons.Left)
                        pButton.NotifyUp(m_btnName, m_buttonId);
                }
            }             
        }

        private void SkinButton_MouseLeave(object sender, EventArgs e)
        {
            if (m_enable == false) return;
            if (m_forePress == true) return;

            if (this.InvokeRequired == false)
            {
                GuiBackground.CreateControlRegion(this, bmp[LEAVE], ControlAs);
            }
            else
            {
                BeginInvoke((Action)delegate
                {
                    GuiBackground.CreateControlRegion(this, bmp[LEAVE], ControlAs);
                });
            }
             
            m_state = ACTIONS.LEAVE;
        }

        private void SkinButton_MouseEnter(object sender, EventArgs e)
        {
            if (m_enable == false) return;
            if (m_forePress == true) return;

            if (this.InvokeRequired == false)
            {
                GuiBackground.CreateControlRegion(this, bmp[ENTER], ControlAs);
            }
            else
            {
                BeginInvoke((Action)delegate
                {
                    GuiBackground.CreateControlRegion(this, bmp[ENTER], ControlAs);
                });
            }
             
            m_state = ACTIONS.ENTER;
        }

        public void SetPressed()
        {
            if (m_enable == false) return;
            if (m_forePress == true) return;

            if (this.InvokeRequired == false)
            {
                GuiBackground.CreateControlRegion(this, bmp[DOWN], ControlAs);
            }
            else
            {
                BeginInvoke((Action)delegate
                {
                    GuiBackground.CreateControlRegion(this, bmp[DOWN], ControlAs);
                });
            }

            m_state = ACTIONS.DOWN;
        }

        public void SetNormal()
        {
            if (m_enable == false) return;
            if (m_forePress == true) return;

            if (this.InvokeRequired == false)
            {
                GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.LEAVE], ControlAs);
            }
            else
            {
                BeginInvoke((Action)delegate
                {
                    GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.LEAVE], ControlAs);
                });
            }

            m_state = ACTIONS.UP;
        }

        private void SkinButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_enable == false) return;
            if (m_forePress == true) return;

            if (this.InvokeRequired == false)
            {
                GuiBackground.CreateControlRegion(this, bmp[DOWN], ControlAs);
            }
            else
            {
                BeginInvoke((Action)delegate
                {
                    GuiBackground.CreateControlRegion(this, bmp[DOWN], ControlAs);
                });
            }
            
            m_state = ACTIONS.DOWN;

            if (m_timer != null)
            {
                m_timer.ResetTimer();
            }
            
            if (pButton != null)
            {
                if (e.Button == MouseButtons.Left)
                    pButton.NotifyDown(m_btnName, m_buttonId);
            }            
        }

        public bool IsEnable()
        {
            return m_enable;
        } 
        public void Enable(bool b)
        {
            if (m_enableButtonExists == false) return;
            m_enable = b;
            if (b == false)
            {
                GuiBackground.CreateControlRegion(this, bmp[DISABLE], ControlAs);
                m_state = ACTIONS.DISABLE;
            }
            else
            {
                if (m_state == ACTIONS.LEAVE)
                {
                   
                    GuiBackground.CreateControlRegion(this, bmp[LEAVE], ControlAs);
                    
                }
                else
                if (m_state == ACTIONS.ENTER)
                {                    
                    GuiBackground.CreateControlRegion(this, bmp[ENTER], ControlAs);                     
                }
                else
                {
                    GuiBackground.CreateControlRegion(this, bmp[LEAVE], ControlAs);                    
                }
            }
        }

        public void PressState()
        {
            GuiBackground.CreateControlRegion(this, bmp[DOWN], ControlAs);
            m_state = ACTIONS.DOWN;
        }

        public void EnterState()
        {
           
            GuiBackground.CreateControlRegion(this, bmp[ENTER], ControlAs);
            
            m_state = ACTIONS.ENTER;
        }
        public void NormalState()
        {
            
            GuiBackground.CreateControlRegion(this, bmp[LEAVE], ControlAs);
            
            m_state = ACTIONS.LEAVE;
        }

        public void UpdateSate()
        {
            switch (m_state)
            {
                case ACTIONS.LEAVE:
                    GuiBackground.CreateControlRegion(this, bmp[LEAVE], ControlAs);
                break;
                case ACTIONS.ENTER:
                    GuiBackground.CreateControlRegion(this, bmp[ENTER], ControlAs);
                break;
                case ACTIONS.DOWN:
                    GuiBackground.CreateControlRegion(this, bmp[DOWN], ControlAs);
                break;
                case ACTIONS.DISABLE:
                    GuiBackground.CreateControlRegion(this, bmp[DISABLE], ControlAs);
                break;

            }
        }

        public void ForcePress(bool value)
        {
            m_forePress = value;
            if (value == true) PressState();
            else NormalState();
            
        }
        public void SimulateEnter()
        {
            GuiBackground.CreateControlRegion(this, bmp[ENTER], ControlAs);

        }
    }
}
