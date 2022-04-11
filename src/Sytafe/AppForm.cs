using System.Diagnostics;

namespace Sytafe
{
    public partial class AppForm : Form
    {
        private int _minuteLimit = 120;
        private DateTime _startUpTime = DateTime.Now;

        public AppForm()
        {
            InitializeComponent();
        }

        private void Ok()
        {
            if (passwordTextBox.Text == "024518936")
            {
                passwordTextBox.Clear();
                WindowState = FormWindowState.Minimized;

                _startUpTime = DateTime.Now;
            }
        }

        private void AppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = passwordTextBox.Text != "024518936";
        }

        private void AppForm_SizeChanged(object sender, EventArgs e)
        {
            var x = Width / 2;
            var y = Height / 2;

            var halfPasswordHeight = passwordTextBox.Height / 2;
            var passwordLocationY = y - (halfPasswordHeight);

            okButton.Location = new Point(x - (okButton.Width / 2), passwordLocationY + okButton.Height + halfPasswordHeight + 10);
            passwordTextBox.Location = new Point(x - (passwordTextBox.Width / 2), passwordLocationY);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Ok();
        }

        private void shutdownButton_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown -f -t 2");
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Ok();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Now - _startUpTime).TotalMinutes >= _minuteLimit)
            {
                WindowState = FormWindowState.Maximized;
            }
        }
    }
}