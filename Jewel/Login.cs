using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jewel
{
    public partial class loginForm : Form
    {
        private api.UserAPI rqUser;
        private mainForm mf;
        
        public loginForm()
        {
            InitializeComponent();
            rqUser = new api.UserAPI();
            mf = new mainForm();
           // lf = new loginForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            txtREmail.Text = "";
            txtRUserName.Text = "";
            txtRPassword.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text;
            var password = txtPassword.Text;
            if (!(checkString(username) && checkString(password)))
            {
                MessageBox.Show("Login Fail. Please try again!");
                return;
            }
            var user = new api.User { email = "", username = username, password = password };
            var resp = rqUser.Login(user);
            if (resp)
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(rqUser.getResponseLogin());
                Properties.Settings.Default.accessToken = dict["access"];
                Properties.Settings.Default.refreshToken = dict["refresh"];
                Properties.Settings.Default.userId = rqUser.getUserID(dict["access"]);
                MessageBox.Show("Login Success");
                this.Close();
                mf.Show();
                
            }
            else MessageBox.Show("Login Fail");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private bool checkString(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) return false;
            if (!(new Regex("^[a-zA-Z0-9]+$").IsMatch(s))) return false;
            return true;
        }
        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            
            var email = txtREmail.Text;
            if(string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Created Fail. Please try again !");
                return;
            }
            var username = txtRUserName.Text;
            var password = txtRPassword.Text;
            if(!(IsValid(email) && checkString(username) && checkString(password))){
                MessageBox.Show("Created Fail. Please try again !");
                return;
            }
            var user1 = new api.User { email = email, username = username, password = password };
            var api = new api.UserAPI();
            var resp = api.Create(user1);
            if (resp)
            {
                MessageBox.Show("Created Success");
                button1.PerformClick();
            }else MessageBox.Show("Created Fail. Please try again !");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtREmail.Text = "";
            txtRUserName.Text = "";
            txtRPassword.Text = "";
            panel2.Visible = false;
            panel1.Visible = true;
        }
    }
}
