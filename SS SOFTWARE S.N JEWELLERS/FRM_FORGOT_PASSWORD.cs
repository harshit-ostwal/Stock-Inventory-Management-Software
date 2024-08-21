using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_FORGOT_PASSWORD : Form
    {
        public FRM_FORGOT_PASSWORD()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Components component = new Components();
        string randomCode;
        int seconds = 60;

        private void FRM_FORGOT_PASSWORD_Load(object sender, EventArgs e)
        {

        }

        private void clearOTP()
        {
            txtOTP1.ResetText();
            txtOTP2.ResetText();
            txtOTP3.ResetText();
            txtOTP4.ResetText();
            txtOTP5.ResetText();
            txtOTP6.ResetText();
            txtOTP1.Select(0, 0);
            txtOTP2.Select(0, 0);
            txtOTP3.Select(0, 0);
            txtOTP4.Select(0, 0);
            txtOTP5.Select(0, 0);
            txtOTP6.Select(0, 0);
            txtOTP1.Focus();
        }

        private void clearAll()
        {
            clearForgotPassword();
            clearOTP();
            clearVerfied();
        }

        private void clearForgotPassword()
        {
            txtEmail.ResetText();
            txtEmail.Select(0, 0);
            txtEmail.Focus();
        }

        private void clearVerfied()
        {
            lblEmail.ResetText();
        }

        private bool IsEmailValid(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool checkEmail()
        {
            string query = "Select f_email_id FROM Company_db Where f_email_id = @emailid";
            string email = txtEmail.Text.Trim();

            if (!IsEmailValid(email))
            {
                MessageBox.Show("Invalid Email Format?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            using (OleDbConnection con = new OleDbConnection(connection.Main))
            {
                try
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@emailid", txtEmail.Text);

                        using (OleDbDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                MessageBox.Show("Email Verification Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                            else
                            {
                                MessageBox.Show("Incorrect Email ID Verfication. Please Check Properly?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                clearForgotPassword();
                                return false;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured While Email ID Verfication?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clearForgotPassword();
                    return false;

                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> OTP()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("No Internet Connection?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearForgotPassword();
                return false;
            }
            try
            {

                using (SmtpClient Client = new SmtpClient("smtp.gmail.com"))
                {
                    Client.Port = 587;
                    Client.EnableSsl = true;
                    Client.Credentials = new NetworkCredential("official.sssoftware@gmail.com", "zhxovvaxvngyacyr");

                    using (MailMessage Message = new MailMessage())
                    {
                        MailAddress FromEmail = new MailAddress("official.sssoftware@gmail.com", "SS SOFTWARE");
                        MailAddress ToEmail = new MailAddress(txtEmail.Text);

                        if (!IsValidEmail(txtEmail.Text))
                        {
                            MessageBox.Show("Invalid Email ID?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            clearForgotPassword();
                            return false;
                        }

                        Message.From = FromEmail;
                        Random rand = new Random();
                        randomCode = (rand.Next(100000, 999999)).ToString();
                        Message.To.Add(ToEmail);
                        Message.Subject = "Your One-Time Password (OTP) For SS SOFTWARE On Forgot Password";
                        Message.Body = GenerateOTPEmailBody(randomCode, lbluser.Text);
                        Message.IsBodyHtml = true;
                        Message.Priority = MailPriority.High;
                        MessageBox.Show("OTP We Will Be Sent To Your Given Mail ID. Kindly Please Check?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await Client.SendMailAsync(Message);
                    }
                }
                MessageBox.Show("OTP Sent Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                resendTimer.Start();
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occurred While Sending OTP?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearForgotPassword();
                return false;
            }
        }

        private async void sendDetails()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("No Internet Connection?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearForgotPassword();
            }
            try
            {

                using (SmtpClient Client = new SmtpClient("smtp.gmail.com"))
                {
                    Client.Port = 587;
                    Client.EnableSsl = true;
                    Client.Credentials = new NetworkCredential("official.sssoftware@gmail.com", "zhxovvaxvngyacyr");

                    using (MailMessage Message = new MailMessage())
                    {
                        MailAddress FromEmail = new MailAddress("official.sssoftware@gmail.com", "SS SOFTWARE");
                        MailAddress ToEmail = new MailAddress(txtEmail.Text);

                        if (!IsValidEmail(txtEmail.Text))
                        {
                            MessageBox.Show("Invalid Email ID?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            clearForgotPassword();
                        }

                        Message.From = FromEmail;
                        Message.To.Add(ToEmail);
                        Message.Subject = "Your Login Credentials For SS SOFTWARE On Forgot Password";
                        Message.Body = GeneratePasswordEmailBody(lblUserName.Text, lblPassword.Text, lbluser.Text);
                        Message.IsBodyHtml = true;
                        Message.Priority = MailPriority.High;
                        MessageBox.Show("Login Credentials We Will Be Sent To Your Given Mail ID. Kindly Please Check?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await Client.SendMailAsync(Message);
                    }
                }
                MessageBox.Show("Login Credentials Sent Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FRM_LOGIN Login = new FRM_LOGIN();
                clearAll();
                this.Hide();
                Login.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occurred While Sending Login Credentials?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clearForgotPassword();
            }
        }

        private string GenerateOTPEmailBody(string otp, string userName)
        {
            return @"
<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <link href="" https://fonts.googleapis.com/css2?family=Onest&display=swap"" rel="" stylesheet"">
    <style>
        * {
            font-family: 'Onest', sans-serif;
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            background-color: #171717;
            color: #e3e3e3;
        }

        .main {
            max-width: 600px;
            background-color: #171717;
            color: #e3e3e3;
            padding: 60px 20px;
        }

        .title {
            padding-bottom: 60px;
            font-weight: bold;
            font-size: 36px;
            text-align: center;
        }

        .user {
            font-weight: bold;
            text-decoration: underline;
            text-decoration-style: wavy;
            text-decoration-color: #f59e0b;
            text-decoration-thickness: 2px;
            text-underline-offset: 8px;
        }

        .desc {
            padding: 20px 0px;
        }

        .otp {
            padding: 60px 0px;
            font-weight: bold;
            font-size: 36px;
            text-align: center;
            letter-spacing :16px;
        }

        .container-1 {
            padding-bottom: 60px;
        }

        .container-1 .title1 {
            font-weight: bold;
            text-decoration: underline;
            text-decoration-style: wavy;
            text-decoration-color: #0ea5e9;
            text-decoration-thickness: 2px;
            text-underline-offset: 8px;
        }

        .container-1 h1 {
            font-weight: bold;
        }

        .container-1 .title2 {
            font-weight: bold;
            text-decoration: underline;
            text-decoration-style: wavy;
            text-decoration-color: #0ea5e9;
            text-decoration-thickness: 2px;
            text-underline-offset: 8px;
        }

        .container-1 li {
            padding-top: 16px;
            text-indent: 16px;
            list-style-type: disc;
            list-style-position: inside;
        }

        .container-2 .me {
            padding-top: 14px;
            font-weight: bold;
            text-decoration: underline;
            text-decoration-style: wavy;
            text-decoration-color: #f43f5e;
            text-decoration-thickness: 2px;
            text-underline-offset: 8px;
        }

        .container-2 .title3 {
            font-weight: normal;
        }

        .footer{
            text-align: center;
            font-weight: bold;
            padding-top: 40px;
        }
    </style>
</head>

<body>
    <div class="" main"">
        <h1 class="" title"">Forgot Password OTP</h1>
        <h3>Hi <span class="" user"">" + userName + @"</span>,</h3>
        <p class=""desc"">Here Is Your One-Time Password (OTP) For Forgot Password.</p>
        <h1 class=""otp"">" + otp + @"</h1>
        <div class="" container-1"">
            <h3 class="" title1"">Please Note :-</h3>
            <li>Your OTP Is Valid For The Next 10 Minutes. Make Sure To Use It Before It Expires!</li>
        </div>
        <div class="" container-1"">
            <h3 class="" title2"">Stay Connected :-</h3>
            <li>Got Questions, Suggestions, Or Just Want To Chat? We're Here For You. Don't Hesitate To Reach Out; We're
                Just A Click Away.</li>
            <li>Thank You For Choosing SS SOFTWARE. We Appreciate Your Trust In Our Services.</li>
        </div>
        <div class="" container-2"">
            <h4 class="" title3"">Best Regards,</h4>
            <h3 class="" me"">Harshit Jain</h3>
        </div>
        <h4 class=""footer"">&copy; " + DateTime.Now.Year + @" SS SOFTWARE. All rights reserved.</h4>
    </div>
</body>

</html>";
        }

        private string GeneratePasswordEmailBody(string userName, string password, string user)
        {
            return @"
<!DOCTYPE html>
<html lang=""en"">

<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <link href="" https://fonts.googleapis.com/css2?family=Onest&display=swap"" rel="" stylesheet"">
    <style>
        * {
            font-family: 'Onest', sans-serif;
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            background-color: #171717;
            color: #e3e3e3;
        }

        .main {
            max-width: 600px;
            background-color: #171717;
            color: #e3e3e3;
            padding: 60px 20px;
        }

        .title {
            padding-bottom: 60px;
            font-weight: bold;
            font-size: 36px;
            text-align: center;
        }

        .user {
            font-weight: bold;
            text-decoration: underline;
            text-decoration-style: wavy;
            text-decoration-color: #f59e0b;
            text-decoration-thickness: 2px;
            text-underline-offset: 8px;
        }

        .desc {
            padding: 20px 0px;
        }

        .otp {
            padding: 60px 0px;
            font-weight: bold;
            font-size: 18px;
            text-align: center;
        }

        .container-1 {
            padding-bottom: 60px;
        }

        .container-1 .title1 {
            font-weight: bold;
            text-decoration: underline;
            text-decoration-style: wavy;
            text-decoration-color: #0ea5e9;
            text-decoration-thickness: 2px;
            text-underline-offset: 8px;
        }

        .container-1 h1 {
            font-weight: bold;
        }

        .container-1 .title2 {
            font-weight: bold;
            text-decoration: underline;
            text-decoration-style: wavy;
            text-decoration-color: #0ea5e9;
            text-decoration-thickness: 2px;
            text-underline-offset: 8px;
        }

        .container-1 li {
            padding-top: 16px;
            text-indent: 16px;
            list-style-type: disc;
            list-style-position: inside;
        }

        .container-2 .me {
            padding-top: 14px;
            font-weight: bold;
            text-decoration: underline;
            text-decoration-style: wavy;
            text-decoration-color: #f43f5e;
            text-decoration-thickness: 2px;
            text-underline-offset: 8px;
        }

        .container-2 .title3 {
            font-weight: normal;
        }

        .footer{
            text-align: center;
            font-weight: bold;
            padding-top: 40px;
        }
    </style>
</head>

<body>
    <div class="" main"">
        <h1 class="" title"">Forgot Password OTP</h1>
        <h3>Hi <span class="" user"">" + user + @"</span>,</h3>
        <p class=""desc"">Here Is Your Login Credentials For SS SOFTWARE.</p>
        <h1 class=""otp"">User Name : " + userName + @"</h1>
        <h1 class=""otp"">Password : " + password + @"</h1>
        <div class="" container-1"">
            <h3 class="" title1"">Please Note :-</h3>
            <li>Please Login Using These Details In SS SOFTWARE. Make Sure Don't Share To Anyone!</li>
        </div>
        <div class="" container-1"">
            <h3 class="" title2"">Stay Connected :-</h3>
            <li>Got Questions, Suggestions, Or Just Want To Chat? We're Here For You. Don't Hesitate To Reach Out; We're
                Just A Click Away.</li>
            <li>Thank You For Choosing SS SOFTWARE. We Appreciate Your Trust In Our Services.</li>
        </div>
        <div class="" container-2"">
            <h4 class="" title3"">Best Regards,</h4>
            <h3 class="" me"">Harshit Jain</h3>
        </div>
        <h4 class=""footer"">&copy; " + DateTime.Now.Year + @" SS SOFTWARE. All rights reserved.</h4>
    </div>
</body>

</html>";
        }


        private async void btnSendOTP_Click(object sender, EventArgs e)
        {
            if (checkEmail() && await OTP())
            {
                pnlOtpVerify.Visible = true;
                pnlVerified.Visible = false;
                pnlForgotPassword.Visible = false;
                clearOTP();
            }
            else
            {
                pnlOtpVerify.Visible = false;
                pnlVerified.Visible = false;
                pnlForgotPassword.Visible = true;
                clearForgotPassword();
                resendTimer.Stop();
            }
        }

        private bool verifyOTP()
        {
            string enteredOTP = txtOTP1.Text + txtOTP2.Text + txtOTP3.Text + txtOTP4.Text + txtOTP5.Text + txtOTP6.Text;
            if (randomCode == enteredOTP)
            {
                MessageBox.Show("OTP Verification Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Incorrect OTP? Please Try Again?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            if (verifyOTP())
            {
                sendDetails();
                clearOTP();
                pnlVerified.Visible = true;
                pnlOtpVerify.Visible = false;
                pnlForgotPassword.Visible = false;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            lblEmail.Text = txtEmail.Text;
        }

        private void EnterKeyPress(object sender, KeyEventArgs e)
        {
            component.Enter(sender, e);
        }

        private void FRM_FORGOT_PASSWORD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pnlForgotPassword.Visible = true;
                pnlOtpVerify.Visible = false;
                pnlVerified.Visible = false;
                if (e.KeyCode == Keys.Escape)
                {
                    FRM_LOGIN Login = new FRM_LOGIN();
                    clearAll();
                    this.Hide();
                    Login.Show();
                }
            }
        }

        private void resendTimer_Tick(object sender, EventArgs e)
        {
            seconds--;
            lblTime.Text = seconds.ToString();

            if (seconds <= 0)
            {
                lblResendOTP.Visible = true;
                resendTimer.Stop();
            }
        }

        private async void lblResendOTP_Click(object sender, EventArgs e)
        {
            seconds = 60;
            lblTime.Text = seconds.ToString();
            resendTimer.Start();
            lblResendOTP.Visible = false;
            if (await OTP())
            {
                clearOTP();
            }
        }

        private void txtOTP1_TextChanged(object sender, EventArgs e)
        {
            txtOTP2.Select(0, 0);
            txtOTP2.Focus();
        }

        private void txtOTP6_TextChanged(object sender, EventArgs e)
        {
            btnVerifyOTP.Focus();
        }

        private void txtOTP3_TextChanged(object sender, EventArgs e)
        {
            txtOTP4.Select(0, 0);
            txtOTP4.Focus();
        }

        private void txtOTP4_TextChanged(object sender, EventArgs e)
        {
            txtOTP5.Select(0, 0);
            txtOTP5.Focus();
        }

        private void txtOTP5_TextChanged(object sender, EventArgs e)
        {
            txtOTP6.Select(0, 0);
            txtOTP6.Focus();
        }

        private void txtOTP2_TextChanged(object sender, EventArgs e)
        {
            txtOTP3.Select(0, 0);
            txtOTP3.Focus();
        }

        private void txtOTP1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtOTP1.ResetText();
                txtOTP1.Select(0, 0);
                txtOTP1.Focus();
            }
        }

        private void txtOTP2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtOTP1.ResetText();
                txtOTP1.Select(0, 0);
                txtOTP1.Focus();
            }
        }

        private void txtOTP3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtOTP2.ResetText();
                txtOTP2.Select(0, 0);
                txtOTP2.Focus();
            }
        }

        private void txtOTP4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtOTP3.ResetText();
                txtOTP3.Select(0, 0);
                txtOTP3.Focus();
            }
        }

        private void txtOTP5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtOTP4.ResetText();
                txtOTP4.Select(0, 0);
                txtOTP4.Focus();
            }
        }

        private void txtOTP6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtOTP5.ResetText();
                txtOTP5.Select(0, 0);
                txtOTP5.Focus();
            }
        }

        private void btnVerifyOTP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                txtOTP6.ResetText();
                txtOTP6.Select(0, 0);
                txtOTP6.Focus();
            }
        }
    }
}
