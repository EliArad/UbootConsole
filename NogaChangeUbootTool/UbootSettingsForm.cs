using DBConnLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NogaChangeUbootTool
{
    public partial class UbootSettingsForm : Form
    {
      
        public UbootSettingsForm(UbootConsoleConfig config)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            var ports = SerialPort.GetPortNames();
            cmbComPort.DataSource = ports;

            this.KeyPreview = true;
            this.KeyDown += UbootSettingsForm_KeyDown;


            cmbComPort.Text = config.ComPort;
            txtStopString.Text = config.StopString;
            txtBaudRate.Text = config.BaudRate.ToString();
            chkEnable.Checked = config.Enable;
            cmbComPort.Enabled = chkEnable.Checked;
            txtBaudRate.Enabled = chkEnable.Checked;
            txtStopString.Enabled = chkEnable.Checked;
            cmbParity.Enabled = chkEnable.Checked;
            cmbDataBits.Enabled = chkEnable.Checked;
            cmbStopBits.Enabled = chkEnable.Checked;

            cmbStopBits.SelectedIndex = (int)config.stopBits;
            if (config.DataBits == 0)
                config.DataBits = 8;
            cmbDataBits.Text = config.DataBits.ToString();
            cmbParity.SelectedIndex = (int)config.parity;


        }

        private void UbootSettingsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        public UbootConsoleConfig GetConfig()
        {
            UbootConsoleConfig config;
            config = new UbootConsoleConfig();
            config.Enable = chkEnable.Checked;

            if (chkEnable.Checked == true)
            {
                config.ComPort = cmbComPort.Text;
                config.StopString = txtStopString.Text;
                config.BaudRate = int.Parse(txtBaudRate.Text);
                config.DataBits = int.Parse(cmbDataBits.Text);
                config.parity = (Parity)cmbParity.SelectedIndex;
                config.stopBits = (StopBits)cmbStopBits.SelectedIndex;
            }
            
            return config;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (chkEnable.Checked == true)
            {
                if (cmbComPort.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select com port");
                    return;
                }
                if (txtBaudRate.Text == string.Empty)
                {
                    MessageBox.Show("Please set baud rate");
                    return;
                }
                if (cmbStopBits.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select stop bits");
                    return;
                }
                if (cmbParity.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select parity");
                    return;
                }

                if (cmbDataBits.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select data bits");
                    return;
                }
                bool b = int.TryParse(txtBaudRate.Text, out int br);
                if (b == false)
                {
                    MessageBox.Show("Please set valid baud rate");
                    return;
                }
            }
            
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            cmbComPort.Enabled = chkEnable.Checked;
            txtBaudRate.Enabled = chkEnable.Checked;
            txtStopString.Enabled = chkEnable.Checked;
        }
    }
}
