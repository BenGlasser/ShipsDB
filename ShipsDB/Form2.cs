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
        String url {get; set;} 

        Form1 caller;


        public ConnectForm(Form1 caller)
        {
            this.caller = caller;
            user = "";
            password = "";
            InitializeComponent();

            caller.host = urlBox.Text = "131.252.208.122";
            userBox.Focus();
        }

        private void ConnectForm_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r" || e.KeyChar.ToString() == "\t")
            {
                caller.User = userBox.Text;
                passwordBox.Focus();
            }
        }

        private void passwordBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r" || e.KeyChar.ToString() == "\t")
            {
                caller.User = userBox.Text;
                caller.Password = passwordBox.Text;
                caller.Enabled = true;
                caller.Focus();
                this.Close();
            }
        }

        private void urlBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == "\r" || e.KeyChar.ToString() == "\t")
            {
                caller.host = urlBox.Text;
                userBox.Focus();
            }
        }
    }
}
