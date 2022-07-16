using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sytafe.Extensions
{
    public static class FormExtensions
    {
        public static DialogResult Error(this Form value, string text)
        {
            return MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Error(this Form value, Exception ex)
        {
            Log.Error("{0}", ex);
            return MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Warning(this Form value, string text)
        {
            return MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
