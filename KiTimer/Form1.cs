using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace KiTimer
{
    public partial class Form1 : Form
    {
        private Timer _timer;

        public Form1()
        {
            InitializeComponent();

            Text = $@"My Tmer v{Application.ProductVersion}";

            LblMessage.Text = "";
            lblStart.Text = "";

            _timer = new Timer();
            _timer.Elapsed += _timer_Elapsed;

            BtnExit.Click += (sender, args) => Close();
            BtnStart.Click += BtnStart_Click;
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (BtnStart.Text.Contains("Start"))
            {
                var secs = (numericUpDown1.Value * 1000) * 60;
                _timer.Interval = Convert.ToDouble(secs);
                _timer.Enabled = true;
                BtnStart.Text = @"Stop";
                lblStart.Text = $@"started: {DateTime.Now.ToShortTimeString()}";
                LblMessage.Text = @"...";
                WindowState = FormWindowState.Minimized;
                return;
            }

            // btn is now a stop button so...
            _timer.Enabled = false;
            BtnStart.Text = @"Start";
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                LblMessage.ForeColor = Color.Brown;
                LblMessage.Text = $@"{TxtDescription.Text}: TIME IS UP!";
                BtnStart.Text = @"Start";
                SendNotification();
            }));
        }

        private void SendNotification()
        {
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = $@"Time ALERT!!!";
            notifyIcon1.BalloonTipText = $@"{TxtDescription.Text}!";
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(15000);
            WindowState = FormWindowState.Normal;
        }
    }
}
