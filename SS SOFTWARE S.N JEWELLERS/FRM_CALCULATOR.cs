using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{

    public partial class FRM_CALCULATOR : Form
    {

        //operation 
        Double Result = 0;


        string operation = string.Empty;

        // string is used for operation like +,-,*,/
        string first_num, sec_num;

        //bool value is 0 and 1 Eg: True and False
        bool Enter_Value = false;

        public FRM_CALCULATOR()
        {
            InitializeComponent();
        }

        private void FRM_CALCULATOR_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.Width = MinimumSize.Width;
            if (rtbDisplayHistory.Text == "")
            {
                lblHistoryDisplay.Text = "There's no history yet";
                lblHistoryDisplay.Visible = true;
            }
            else
            {
                lblHistoryDisplay.Visible = false;
            }
            if (rtbDisplayMemory.Text == "")
            {
                lblmemorydisplay.Text = "There's no Memory yet";
                rtbDisplayMemory.Visible = true;
            }
            else
            {
                lblmemorydisplay.Visible = false;
            }
            rtbDisplayHistory.Visible = true;
            lblHistoryDisplay.Visible = true;
            rtbDisplayMemory.Visible = false;
            lblmemorydisplay.Visible = false;
            pnlmemory.Visible = false;
            pnlhistory.Visible = true;
        }

        private void btnminimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnNum_Click(object sender, EventArgs e)
        {
            if (txtdisplay1.Text == "0" || Enter_Value) txtdisplay1.Text = string.Empty;

            Enter_Value = false;
            Button button = (Button)sender;
            if (button.Text == ".")
            {
                if (!txtdisplay1.Text.Contains("."))
                {
                    txtdisplay1.Text = txtdisplay1.Text + button.Text;
                }
            }
            else
            {
                txtdisplay1.Text = txtdisplay1.Text + button.Text;
            }
            if (rtbDisplayHistory.Text == "")
            {
                lblHistoryDisplay.Text = "There's no history yet";
                lblHistoryDisplay.Visible = true;
            }
            else
            {
                lblHistoryDisplay.Visible = false;
            }
            if (rtbDisplayMemory.Text == "")
            {
                lblmemorydisplay.Text = "There's no Memory yet";
                rtbDisplayMemory.Visible = true;
            }
            else
            {
                lblmemorydisplay.Visible = false;
            }
        }

        private void Btn_Math_Operation_Click(object sender, EventArgs e)
        {
            if (Result != 0) btnequals.PerformClick();
            else Result = Double.Parse(txtdisplay1.Text);

            Button button = (Button)sender;
            operation = button.Text;
            Enter_Value = true;
            if (txtdisplay1.Text != "0")
            {
                txtdisplay2.Text = first_num = $"{Result} {operation} ";
                txtdisplay1.Text = string.Empty;
            }
            if (rtbDisplayHistory.Text == "")
            {
                lblHistoryDisplay.Text = "There's no history yet";
                lblHistoryDisplay.Visible = true;
            }
            else
            {
                lblHistoryDisplay.Visible = false;
            }
            if (rtbDisplayMemory.Text == "")
            {
                lblmemorydisplay.Text = "There's no Memory yet";
                rtbDisplayMemory.Visible = true;
            }
            else
            {
                lblmemorydisplay.Visible = false;
            }
        }

        private void btnequals_Click(object sender, EventArgs e)
        {
            sec_num = txtdisplay1.Text;
            txtdisplay2.Text = $"{txtdisplay2.Text} {txtdisplay1.Text} =";
            if (txtdisplay1.Text != string.Empty)
            {
                if (txtdisplay1.Text == "0") txtdisplay2.Text = string.Empty;
                switch (operation)
                {
                    case "+":
                        txtdisplay1.Text = (Result + double.Parse(txtdisplay1.Text)).ToString();
                        rtbDisplayHistory.AppendText($"{first_num} {sec_num} = {txtdisplay1.Text}\n");
                        break;
                    case "-":
                        txtdisplay1.Text = (Result - double.Parse(txtdisplay1.Text)).ToString();
                        rtbDisplayHistory.AppendText($"{first_num} {sec_num} = {txtdisplay1.Text}\n");
                        break;
                    case "x":
                        txtdisplay1.Text = (Result * double.Parse(txtdisplay1.Text)).ToString();
                        rtbDisplayHistory.AppendText($"{first_num} {sec_num} = {txtdisplay1.Text}\n");
                        break;
                    case "÷":
                        txtdisplay1.Text = (Result / double.Parse(txtdisplay1.Text)).ToString();
                        rtbDisplayHistory.AppendText($"{first_num} {sec_num} = {txtdisplay1.Text}\n");
                        break;
                    default:
                        txtdisplay2.Text = $"{txtdisplay1.Text} =";
                        break;
                }
                Result = double.Parse(txtdisplay1.Text);
                operation = string.Empty;
                if (rtbDisplayHistory.Text == "")
                {
                    lblHistoryDisplay.Text = "There's no history yet";
                    lblHistoryDisplay.Visible = true;
                }
                else
                {
                    lblHistoryDisplay.Visible = false;
                }
                if (rtbDisplayMemory.Text == "")
                {
                    lblmemorydisplay.Text = "There's no Memory yet";
                    rtbDisplayMemory.Visible = true;
                }
                else
                {
                    lblmemorydisplay.Visible = false;
                }
            }
        }

        private void btnhistory_Click(object sender, EventArgs e)
        {
            if (this.Width == MaximumSize.Width)
            {
                this.Width = MinimumSize.Width;
                this.CenterToScreen();
                if (rtbDisplayHistory.Text == "")
                {
                    lblHistoryDisplay.Text = "There's no history yet";
                    lblHistoryDisplay.Visible = true;
                }
                else
                {
                    lblHistoryDisplay.Visible = false;
                }
                if (rtbDisplayMemory.Text == "")
                {
                    lblmemorydisplay.Text = "There's no Memory yet";
                    rtbDisplayMemory.Visible = true;
                }
                else
                {
                    lblmemorydisplay.Visible = false;
                }
            }
            else
            {
                this.Width = MaximumSize.Width;
                this.CenterToScreen();
                if (rtbDisplayHistory.Text == "")
                {
                    lblHistoryDisplay.Text = "There's no history yet";
                    lblHistoryDisplay.Visible = true;
                }
                else
                {
                    lblHistoryDisplay.Visible = false;
                }
                if (rtbDisplayMemory.Text == "")
                {
                    lblmemorydisplay.Text = "There's no Memory yet";
                    rtbDisplayMemory.Visible = true;
                }
                else
                {
                    lblmemorydisplay.Visible = false;
                }
            }
        }

        private void btnclearhistory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Wanna Clear Records???", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                rtbDisplayHistory.Clear();
                if (rtbDisplayHistory.Text == "")
                {
                    lblHistoryDisplay.Text = "There's no history yet";
                    lblHistoryDisplay.Visible = true;
                }
                else
                {
                    lblHistoryDisplay.Visible = false;
                }
                if (rtbDisplayMemory.Text == "")
                {
                    lblmemorydisplay.Text = "There's no Memory yet";
                    rtbDisplayMemory.Visible = true;
                }
                else
                {
                    lblmemorydisplay.Visible = false;
                }
            }
        }

        private void lblhistory_Click(object sender, EventArgs e)
        {
            rtbDisplayHistory.Visible = true;
            lblHistoryDisplay.Visible = true;
            rtbDisplayMemory.Visible = false;
            lblmemorydisplay.Visible = false;
            pnlmemory.Visible = false;
            pnlhistory.Visible = true;
            if (rtbDisplayHistory.Text == "")
            {
                lblHistoryDisplay.Text = "There's no history yet";
                lblHistoryDisplay.Visible = true;
            }
            else
            {
                lblHistoryDisplay.Visible = false;
            }
            if (rtbDisplayMemory.Text == "")
            {
                lblmemorydisplay.Text = "There's no Memory yet";
                rtbDisplayMemory.Visible = true;
            }
            else
            {
                lblmemorydisplay.Visible = false;
            }
        }

        private void lblmemory_Click(object sender, EventArgs e)
        {
            rtbDisplayHistory.Visible = false;
            lblHistoryDisplay.Visible = false;
            rtbDisplayMemory.Visible = true;
            lblmemorydisplay.Visible = true;
            pnlmemory.Visible = true;
            pnlhistory.Visible = false;
            if (rtbDisplayHistory.Text == "")
            {
                lblHistoryDisplay.Text = "There's no history yet";
                lblHistoryDisplay.Visible = true;
            }
            else
            {
                lblHistoryDisplay.Visible = false;
            }
            if (rtbDisplayMemory.Text == "")
            {
                lblmemorydisplay.Text = "There's no Memory yet";
                rtbDisplayMemory.Visible = true;
            }
            else
            {
                lblmemorydisplay.Visible = false;
            }
        }

        private void btnbackspace_Click(object sender, EventArgs e)
        {
            if (txtdisplay1.Text.Length > 0)
                txtdisplay1.Text = txtdisplay1.Text.Remove(txtdisplay1.Text.Length - 1, 1);
            if (txtdisplay1.Text == string.Empty) txtdisplay1.Text = "0";
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            txtdisplay1.Text = "0";
            txtdisplay2.Text = string.Empty;
            Result = 0;
        }

        private void btnce_Click(object sender, EventArgs e)
        {
            txtdisplay1.Text = "0";
        }

        private void Btn_Operation_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            operation = button.Text;
            switch (operation)
            {
                case "√x":
                    txtdisplay2.Text = $"√{txtdisplay1.Text}";
                    txtdisplay1.Text = Convert.ToString(Math.Sqrt(Double.Parse(txtdisplay1.Text)));
                    break;
                case "^2":
                    txtdisplay2.Text = $"{txtdisplay1.Text}^2";
                    txtdisplay1.Text = Convert.ToString(Convert.ToDouble(txtdisplay1.Text) * Convert.ToDouble(txtdisplay1.Text));
                    break;
                case "1/x":
                    txtdisplay2.Text = $"1/{txtdisplay1.Text}";
                    txtdisplay1.Text = Convert.ToString(1.0 / Convert.ToDouble(txtdisplay1.Text));
                    break;
                case "%":
                    txtdisplay2.Text = $"{txtdisplay1.Text}%";
                    txtdisplay1.Text = Convert.ToString(Convert.ToDouble(txtdisplay1.Text) / Convert.ToDouble(100));
                    break;
                case "±":
                    txtdisplay1.Text = Convert.ToString(-1 * Convert.ToDouble(txtdisplay1.Text));
                    break;
            }
            rtbDisplayHistory.AppendText($"{txtdisplay2.Text} = {txtdisplay1.Text}\n");
        }

        private void FRM_CALCULATOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (txtdisplay1.Text == "")
                {
                    if (MessageBox.Show("Do You Wanna Close Calculator?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    txtdisplay1.Text = "";
                }
            }
            if (e.KeyCode == Keys.NumPad0)
            {
                btn0.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad1)
            {
                btn1.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad2)
            {
                btn2.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad3)
            {
                btn3.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad4)
            {
                btn4.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad5)
            {
                btn5.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad6)
            {
                btn6.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad7)
            {
                btn7.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad8)
            {
                btn8.PerformClick();
            }
            if (e.KeyCode == Keys.NumPad9)
            {
                btn9.PerformClick();
            }
            if (e.KeyCode == Keys.Add)
            {
                btnadd.PerformClick();
            }
            if (e.KeyCode == Keys.Subtract)
            {
                btnminus.PerformClick();
            }
            if (e.KeyCode == Keys.Multiply)
            {
                btnmultiply.PerformClick();
            }
            if (e.KeyCode == Keys.Divide)
            {
                btndivide.PerformClick();
            }
            if (e.KeyCode == Keys.Enter)
            {
                btnequals.PerformClick();
            }
            if (e.KeyCode == Keys.Back)
            {
                btnbackspace.PerformClick();
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Wanna Close Calculator?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
