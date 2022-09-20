using InvokersLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSkinLib
{
    public class SkinCheckBox : Button
    {
        object m_lock = new object();
       
        private Bitmap[] bmp = new Bitmap[4];
        bool m_isEnter = false;
        bool m_checked = false;
        bool m_checkedState = false;
        bool m_enable = true;
        bool m_disableButtonExists = false;
        public static string BaseDir;
        IButton pButton = null;
        BUTTON_NAME m_btnName = BUTTON_NAME.NONAME;
        Color NormalColor;
        Color EnterColor;
        Color PressColor;
        public SkinCheckBox()
        {
            UseVisualStyleBackColor = false;
        }

        public SkinCheckBox(BUTTON_NAME btnName)
        {
            m_btnName = btnName;

            NormalColor = Color.FromArgb(40, 40, 40);
            EnterColor = Color.FromArgb(200, 200, 20);
            PressColor = Color.Red;
            UseVisualStyleBackColor = false;
        }

        public GuiBackground.WITH_AS ControlAs = GuiBackground.WITH_AS.IMAGE;
        public void SetSkin(Bitmap [] bitmaps, BUTTON_NAME buttonName = BUTTON_NAME.NONAME, string name = "")
        {
            bmp[0] = bitmaps[0];
            bmp[1] = bitmaps[1];
            bmp[2] = bitmaps[2];
            bmp[3] = bitmaps[3];
            if (bmp[3] != null)
            {
                m_disableButtonExists = true;
            }
            else
            {
                m_disableButtonExists = false;
            }
            this.Text = name;
            m_btnName = buttonName;

            this.MouseEnter += SkinCheckBox_MouseEnter;
            this.MouseLeave += SkinCheckBox_MouseLeave;
            this.MouseDown += SkinCheckBoxButton_MouseDown;
            this.MouseUp += SkinCheckBoxButton_MouseUp;

            GuiBackground.CreateControlRegion(this, bmp[0], ControlAs);
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
                m_disableButtonExists = true;
            }
            else
            {
                m_disableButtonExists = false;
            }
            m_btnName =  BUTTON_NAME.NONAME;

            this.MouseEnter += SkinCheckBox_MouseEnter;
            this.MouseLeave += SkinCheckBox_MouseLeave;
            this.MouseDown += SkinCheckBoxButton_MouseDown;
            this.MouseUp += SkinCheckBoxButton_MouseUp;

            GuiBackground.CreateControlRegion(this, bmp[0], ControlAs);
        }

        public void SetSkin(Bitmap[] bitmaps, string name)
        {
            bmp[0] = bitmaps[0];
            bmp[1] = bitmaps[1];
            bmp[2] = bitmaps[2];
            bmp[3] = bitmaps[3];
            if (bmp[3] != null)
            {
                m_disableButtonExists = true;
            }
            else
            {
                m_disableButtonExists = false;
            }
            this.Text = name;
            GuiBackground.CreateControlRegion(this, bmp[0], ControlAs);
            SetCallback(null);
        }

        public void SetColor(Color color)
        {
            this.ForeColor = color;
        }
        public void SetSkin(Bitmap[] bitmaps, BUTTON_NAME buttonName, IButton p, string name)
        {
            SetSkin(bitmaps, buttonName, name);
            SetCallback(p);
        }

        private void SkinCheckBoxButton_MouseDown(object sender, MouseEventArgs e)
        {   
            if (m_enable == false)
                return;

            if (pButton != null)
            {
                if (e.Button == MouseButtons.Left)
                    pButton.NotifyDown(m_btnName);
            }

        }

        private void SkinCheckBoxButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_enable == false)
                return;

            if (pButton != null)
            {
                if (e.Button == MouseButtons.Left)
                    pButton.NotifyUp(m_btnName);
            }

        }
        public void UpdateText(string name)
        {
            InvokersLib.INVOKERS.InvokeControlText(this, name);
            //this.Text = name;
        }
        
        public void SetCallback(IButton p)
        {
            pButton = p;            
        }

        public void SetCallback()
        {            
            this.MouseEnter += SkinCheckBox_MouseEnter;
            this.MouseLeave += SkinCheckBox_MouseLeave;
            this.MouseDown += SkinCheckBoxButton_MouseDown;
            this.MouseUp += SkinCheckBoxButton_MouseUp;
        }

        public void SetSkin(string buttonName)
        {
            if (File.Exists(BaseDir + buttonName + "_normal.png") == true)
                bmp[0] = new Bitmap(BaseDir + buttonName + "_normal.png");
            else
                throw (new SystemException("File: " + BaseDir + buttonName + "_normal.png" + " does not exists"));

            if (File.Exists(BaseDir + buttonName + "_enter.png") == true)
                bmp[1] = new Bitmap(BaseDir + buttonName + "_enter.png");
            else
                throw (new SystemException("File: " + BaseDir + buttonName + "_enter.png" + " does not exists"));

            if (File.Exists(BaseDir + buttonName + "_press.png") == true)
                bmp[2] = new Bitmap(BaseDir + buttonName + "_press.png");
            else
                throw (new SystemException("File: " + BaseDir + buttonName + "_press.png" + " does not exists"));

            if (File.Exists(BaseDir + buttonName + "_disable.png") == true)
            {
                bmp[3] = new Bitmap(BaseDir + buttonName + "_disable.png");
                m_disableButtonExists = true;
            }
            else
            {
                m_disableButtonExists = false;
            }
                
            GuiBackground.CreateControlRegion(this, bmp[0], ControlAs);
        }


        public void UpdateSkin(int state, string dir, string buttonName)
        {
            if (state == 1)
            {
                bmp[2] = new Bitmap(dir +  buttonName + ".png");
            }
        }
        private void SkinCheckBox_MouseLeave(object sender, EventArgs e)
        {
            onEnter = false;
  
        }

        public void Toggle(Action<bool> cb)
        {
            if (m_enable == false) return;

            m_checked = !m_checked;            
            Checked(m_checked);
            cb(m_checked);
        }

        public void Toggle()
        {
            if (m_enable == false) return;

            m_checked = !m_checked;
            Checked(m_checked);
        }

        private void SkinCheckBox_MouseEnter(object sender, EventArgs e)
        {
            onEnter = true;
        }

        public bool onEnter
        {
            set
            {
                if (m_enable == false) return;
                m_isEnter = value;   
                if (m_isEnter == true && m_checked == false)
                {
                    if (bmp[GSkinCommon.ENTER] != null)
                        GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.ENTER], ControlAs);
                    else
                        this.BackColor = EnterColor;
                }
                if (m_isEnter == false && m_checked == false)
                {
                    if (bmp[GSkinCommon.LEAVE] != null)
                        GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.LEAVE], ControlAs);
                    else
                        this.BackColor = NormalColor;
                }
            }
        }
        public void Checked(bool c, Action<bool> cb)
        {
            m_checkedState = c;
            if (m_enable == false) return;
            m_checked = c;
            if (m_checked == true)
            {
                GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.DOWN], ControlAs);
            }
            else
            {
                if (m_isEnter == false)
                {
                    if (bmp[GSkinCommon.UP] != null)
                    {
                        GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.UP], ControlAs);
                    }
                    else
                    {
                        this.BackColor = EnterColor;
                    }
                }
                else
                {
                    if (bmp[GSkinCommon.ENTER] != null)
                    {
                        GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.ENTER], ControlAs);
                    }
                    else
                    {
                        this.BackColor = EnterColor;
                    }
                }
            }
            if (cb != null)
                cb(m_checked);
             
        }

        public bool GetChecked()
        {
            return m_checked;
        }

        public void Checked(bool c, Color fontChecked, Color fontNormal)
        {
            //this.ForeColor = c == true ? fontChecked : fontNormal;
            INVOKERS.InvokeControlForeColor(this, c == true ? fontChecked : fontNormal);
            Checked(c);
        }


        public void Checked(bool c)
        {
            m_checkedState = c;
            if (m_enable == false) return;
            m_checked = c;
            if (c)
            {
                if (bmp[GSkinCommon.DOWN] != null)
                {
                    if (this.InvokeRequired == false)
                    {
                        GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.DOWN], ControlAs);
                    }
                    else
                    {
                        this.BeginInvoke((MethodInvoker)delegate ()
                        {
                            GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.DOWN], ControlAs);
                        });
                    }
                }
                else
                {
                    this.BackColor = PressColor;
                }
            }
            else
            {
                if (m_isEnter == false)
                {
                    if (bmp[GSkinCommon.LEAVE] != null)
                    {
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
                    }
                    else
                    {
                        this.BackColor = NormalColor;
                    }
                }
                else
                {
                    if (bmp[GSkinCommon.ENTER] != null)
                    {
                        if (this.InvokeRequired == false)
                        {
                            GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.ENTER], ControlAs);
                        }
                        else
                        {
                            BeginInvoke((Action)delegate
                            {
                                GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.ENTER], ControlAs);
                            });
                        }
                    }
                    else
                    {
                        this.BackColor = EnterColor;
                    }
                }
            }           
        }
        /*
        public void Toggle()
        {

            if (m_enable == false) return;
            m_checked = !m_checked;
            if (m_checked)
            {
                GuiBackground.CreateControlRegion(this, bmp[Common.DOWN], GuiBackground.WITH_AS.IMAGE);
            }
            else
            {
                if (m_isEnter == false)
                    GuiBackground.CreateControlRegion(this, bmp[Common.LEAVE], GuiBackground.WITH_AS.IMAGE);
                else
                    GuiBackground.CreateControlRegion(this, bmp[Common.ENTER], GuiBackground.WITH_AS.IMAGE);
            }
        }
        */
        /*
        public void Toggle(Action<bool> cb)
        {

            if (m_enable == false) return;
            m_checked = !m_checked;
            if (m_checked)
            {
                GuiBackground.CreateControlRegion(this, bmp[Common.DOWN], GuiBackground.WITH_AS.IMAGE);
            }
            else
            {
                if (m_isEnter == false)
                    GuiBackground.CreateControlRegion(this, bmp[Common.LEAVE], GuiBackground.WITH_AS.IMAGE);
                else
                    GuiBackground.CreateControlRegion(this, bmp[Common.ENTER], GuiBackground.WITH_AS.IMAGE);
            }
            cb(m_checked);
        }
        */
        public void Normal()
        {
            if (m_enable == true)
            {
                if (bmp[GSkinCommon.LEAVE] != null)
                {
                    
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
                }
                else
                {
                    this.BackColor = NormalColor;
                }
            }
        }

        public bool IsEnable()
        {
            return m_enable;
        }
        public void Enable(bool b)
        {
            if (m_disableButtonExists == false)
                return;
            m_enable = b;
            if (b == false)
            {
                GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.DISABLE], ControlAs);
            }
            else
            {
                if (m_checked == false && m_checkedState == true)
                    m_checked = m_checkedState;

                if (m_isEnter == false && m_checked == false)
                    GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.LEAVE], ControlAs);
                if (m_isEnter == true && m_checked == false)
                    GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.ENTER], ControlAs);
                if (m_checked == true)
                    GuiBackground.CreateControlRegion(this, bmp[GSkinCommon.DOWN], ControlAs);
            }           
        }
        private System.Windows.Forms.Label label;
        public Size m_LabelSizePixel = new Size(40, 20);
        public void AddLabel(Color color, int x, int y, string font, int fontSize, string text)
        {
            label = new Label();
            label.AutoSize = true;
            Font fnt = new Font("Arial", fontSize);
            label.Font = fnt;
            label.BackColor = System.Drawing.Color.Transparent;
            label.ForeColor = color;
            label.Location = new System.Drawing.Point(x, y);
            label.Name = "label1";
            label.Size = m_LabelSizePixel;// new System.Drawing.Size(35, 13);
            label.TabIndex = 10;
            label.Text = text;
            this.Controls.Add(label);
            label.BringToFront();
            //label.MouseDown += Label_MouseDown;
            //label.MouseEnter += Label_MouseEnter;
            //label.MouseLeave += Label_MouseLeave;
            //label.MouseUp += Label_MouseUp;
        }
    }
}
