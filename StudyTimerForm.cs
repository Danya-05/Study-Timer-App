using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudyTimerProject
{
    public partial class StudyTimerForm : Form
    {
        public StudyTimerForm()
        {
            InitializeComponent();
        }

        private int Reminder = 0;
        private int Min = 0;
        private int Sec = 0;

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Reminder == 0)
            {
                Reminder = Convert.ToInt32(numericUpDown1.Value) * 60;
            }
            timer1.Enabled = true;
            numericUpDown1.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblTimer.Text = numericUpDown1.Value.ToString("00") + " : 00";
            timer1.Enabled = false;
            numericUpDown1.Enabled = true;
            Reminder = 0;
            Min = 0;
            Sec = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Reminder > 0)
            {
                Reminder--;
                Min = Reminder / 60;
                Sec = Reminder % 60;
                lblTimer.Text = Min.ToString("00") + " : " + Sec.ToString("00");
            }
            else
            {
                timer1.Enabled = false;
                notifyIcon1.Icon = SystemIcons.Warning;
                notifyIcon1.ShowBalloonTip(30000);
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            lblTimer.Text = numericUpDown1.Value.ToString("00") + " : 00";
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStart.PerformClick(); // to make enter click do start button
                e.SuppressKeyPress = true; // to close beep audio after click enter
            }
        }

        private void StudyTimerForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Select(0, numericUpDown1.Text.Length); // to highlight the num 0 
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void numericUpDown1_Validating(object sender, CancelEventArgs e)
        {
            if (numericUpDown1.Value <= 0) 
            {
                e.Cancel = true;
                //numericUpDown1.Focus(); لما حطيتها صار البلوك بعد ما اكبس ستارت
                errorProvider1.SetError(numericUpDown1, "Value must be greater than 0!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(numericUpDown1, "");
            }
        }

        
    }
}
