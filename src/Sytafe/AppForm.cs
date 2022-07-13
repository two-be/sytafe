using Microsoft.AspNetCore.SignalR.Client;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Sytafe.Extensions;
using Sytafe.Forms;
using Sytafe.Library.Models;
using Sytafe.Models;
using Sytafe.Services;

namespace Sytafe
{
    public partial class AppForm : Form
    {
        [DllImport("user32")]
        public static extern void LockWorkStation();

        private HubConnection _connection;
        private int _minuteLeft = 2;
        private AppService _service = new AppService(string.Empty);
        private AppSettings _settings = new AppSettings();
        private UserInfo _user = new UserInfo();

        public AppForm(AppSettings settings)
        {
            InitializeComponent();

            _service = new AppService(settings.ServerAddress);
            _settings = settings;

            if (SignIn())
            {
                if (_user.IsAdministrator)
                {
                    return;
                }
                _service.Token = _user.Token;
                Connect();

                SetUsed();
            }
        }

        private void Connect()
        {
            _connection = new HubConnectionBuilder().WithUrl($"{_settings.ServerAddress}/Hub").Build();
            _connection.StartAsync().GetAwaiter().GetResult();
            _connection.Closed += _connection_Closed;
        }

        private void SetUsed()
        {
            try
            {
                var used = _service.GetUsedTodayUsing(_user.Id);
                if (used is null)
                {
                    used = _service.CreateUsedToday(_user.Id);
                }
                _connection.InvokeAsync("used", used.Id).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        private bool SignIn()
        {
            var signInForm = new SignInForm(_service);
            var rs = signInForm.ShowDialog();
            if (rs == DialogResult.OK)
            {
                _user = signInForm.User;
                _minuteLeft = _user.MinuteLeft;
                return true;
            }
            else
            {
                this.Warning("Less than 2 minutes left.");
                return false;
            }
        }

        private void AppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!_user.IsAdministrator)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                this.Error(ex);
            }
        }

        private async Task _connection_Closed(Exception? arg)
        {
            var now = DateTime.Now;
            var notNow = now.AddMinutes(1);
            while (now <= notNow)
            {
                if (_connection.State == HubConnectionState.Disconnected)
                {
                    Connect();
                }
                else
                {
                    SetUsed();
                    return;
                }
                await Task.Delay(2000);
            }
        }

        private void secondTimer_Tick(object sender, EventArgs e)
        {
            if (_user.IsAdministrator)
            {
                secondTimer.Enabled = false;
                return;
            }
            var process = Process.GetProcessesByName("Taskmgr").FirstOrDefault();
            if (process is not null)
            {
                process.Kill();
            }
        }

        private void signInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _minuteLeft = 2;
            SignIn();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_user.IsAdministrator)
            {
                timer.Enabled = false;
                return;
            }
            _minuteLeft--;
            if (_minuteLeft == 15)
            {
                this.Warning("Less than 15 minutes left.");
            }
            if (_minuteLeft <= 0)
            {
                if (timer.Interval <= 10000)
                {
                    Process.Start(@"C:\Windows\System32\shutdown.exe -f -s -t 2");
                }
                else
                {
                    timer.Interval = timer.Interval - 10000;
                    LockWorkStation();
                }
            }
        }
    }
}