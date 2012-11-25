using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ShipsDB
{
    public partial class ConnectForm : Form
    {
        String user;

        public String User
        {
            get { return user; }
            set { user = value; }
        }
        String password;

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
        Form1 caller;


        public ConnectForm(Form1 caller)
        {
            this.caller = caller;
            user = "";
            password = "";
            InitializeComponent();
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                caller.User = userBox.Text;
                passwordBox.Focus();
            }
        }

        private void passwordBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r")
            {
                caller.User = userBox.Text;
                caller.Password = passwordBox.Text;
                caller.Enabled = true;
                caller.Focus();
                this.Close();
            }
        }
    }
}
