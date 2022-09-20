using InvokersLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GSkinLib
{
    public class SkinPictureBox : PictureBox 
    { 
        Bitmap m_bitmap;
 
        BUTTON_NAME m_buttonName;
        
        private Label label;
        private Label label2;
        public Size m_LabelSizePixel = new Size(40, 20);
        int m_padleft = 0;
        public void AddLabel(Color color, int x , int y, string font, int fontSize, string text, int padLeft = 0)
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
            label.Text = text.PadLeft(padLeft);
            m_padleft = padLeft;
            this.Controls.Add(label);
            label.MouseDown += Label_MouseDown;
            label.MouseEnter += Label_MouseEnter;
            label.MouseLeave += Label_MouseLeave;
            label.MouseUp += Label_MouseUp;
        }

        public void AddLabel2(Color color, int x, int y, string font, int fontSize, string text, int padLeft = 0)
        {
            label2 = new Label();
            label2.AutoSize = true;
            Font fnt = new Font("Arial", fontSize);
            label2.Font = fnt;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.ForeColor = color;
            label2.Location = new System.Drawing.Point(x, y);
            label2.Name = "label1";
            label2.Size = m_LabelSizePixel;// new System.Drawing.Size(35, 13);
            label2.TabIndex = 10;
            label2.Text = text.PadLeft(padLeft);
            m_padleft = padLeft;
            this.Controls.Add(label2);
            label2.MouseDown += Label2_MouseDown;
            label2.MouseEnter += Label2_MouseEnter;
            label2.MouseLeave += Label2_MouseLeave;
            label2.MouseUp += Label2_MouseUp;
        }

        private void Label_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_ib != null)
            {
                label.ForeColor = Color.Cyan;
            }
        }

        private void Label2_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_ib != null)
            {
                label2.ForeColor = Color.Cyan;
            }
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            if (m_ib != null)
            {
                label.ForeColor = Color.White;
            }
        }

        private void Label2_MouseLeave(object sender, EventArgs e)
        {
            if (m_ib != null)
            {
                label2.ForeColor = Color.White;
            }
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            if (m_ib != null)
            {
                label.ForeColor = Color.Cyan;
            }
            
        }

        private void Label2_MouseEnter(object sender, EventArgs e)
        {
            if (m_ib != null)
            {
                label2.ForeColor = Color.Cyan;
            }

        }

        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_ib != null)
            {
                label.ForeColor = Color.Orange;
                m_ib.NotifyDown(m_buttonName);                
            }
        }

        private void Label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_ib != null)
            {
                label2.ForeColor = Color.Orange;
                m_ib.NotifyDown(m_buttonName);
            }
        }

        public void LableSize(Size size)
        {
            label.Size = size;
        }

        IButton m_ib = null;
        public void SetCallback(IButton ib)
        {
            m_ib = ib;
            this.MouseDown += SkinPictureBox_MouseDown;
            this.DoubleClick += SkinPictureBox_DoubleClick;
        }

        private void SkinPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (m_ib != null)
                m_ib.NotifyDoubleClick(m_buttonName);
        }

        private void SkinPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_ib != null)
                m_ib.NotifyDown(m_buttonName);
        }
         

        public void SetTextForeColor(Color color)
        {
            INVOKERS.InvokeControlForeColor(label, color);
        }

        public void SetTextBackColor(Color color)
        {
            INVOKERS.InvokeControlBackColor(label, color);
        }

        public void UpdateText(string text)
        {
            INVOKERS.InvokeControlText(label, text.PadLeft(m_padleft));
        }

        public void UpdateText(string text, Color color)
        {
            INVOKERS.InvokeControlText(label, text.PadLeft(m_padleft));
            INVOKERS.InvokeControlForeColor(label, color);
        }

        public void UpdateText2(string text)
        {
            INVOKERS.InvokeControlText(label2, text.PadLeft(m_padleft));
        }

        public void SetButtonName(BUTTON_NAME name)
        {
            m_buttonName = name;
        }
        public void SetSkin(Bitmap b)
        {
            INVOKERS.InvokeControlBackColor(this, Color.Transparent);
             
            this.Width = b.Width;
            this.Height = b.Height;
            this.Image = b;
        }

        public void SetWidth(int width)
        {
            this.Width = width;
        }
    }
}
