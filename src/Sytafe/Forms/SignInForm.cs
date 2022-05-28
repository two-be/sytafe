using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sytafe.Extensions;
using Sytafe.Library.Models;
using Sytafe.Models;
using Sytafe.Services;

namespace Sytafe.Forms
{
    public partial class SignInForm : Form
    {
        public UserInfo User { get; set; }

        private readonly AppService _service;

        public SignInForm(AppService service)
        {
            InitializeComponent();

            _service = service;
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            try
            {
                User = _service.SignIn(usernameTextBox.Text, passwordTextBox.Text);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.Error(ex.Message);
            }
        }
    }
}
