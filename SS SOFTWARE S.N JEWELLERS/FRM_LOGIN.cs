using System;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_LOGIN : Form
    {
        public FRM_LOGIN()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Components component = new Components();

        private void FRM_LOGIN_Load(object sender, EventArgs e)
        {

        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            this.Hide();
            FRM_FORGOT_PASSWORD ForgotPassword = new FRM_FORGOT_PASSWORD();
            ForgotPassword.Show();
        }

        private void clearAll()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtUserName.Select(0, 0);
            txtPassword.Select(0, 0);
            txtUserName.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            connection.Login(txtUserName, txtPassword, notifyIcon1, this);
            clearAll();
            component.Backup();
        }

        private void EnterKeyPress(object sender, KeyEventArgs e)
        {
            component.Enter(sender, e);
        }

        private void FRM_LOGIN_KeyDown(object sender, KeyEventArgs e)
        {
            component.ExitApplication(sender, e);
        }
    }
}
