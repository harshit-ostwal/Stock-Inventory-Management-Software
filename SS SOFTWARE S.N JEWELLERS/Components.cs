using Bunifu.UI.WinForms.BunifuButton;
using System;
using System.IO;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    class Components
    {

        Connection con = new Connection();

        public void Minimize(Form Form)
        {
            Form.WindowState = FormWindowState.Minimized;
        }

        public void Backup()
        {
            string ThisDB = Application.StartupPath + "\\DATABASE\\Main_db.accdb";
            string SP = Application.StartupPath + "\\BACKUP\\";
            string Destitnation = SP + "\\Main_db " + DateTime.Now.ToString(" dd-MM-yyyy hh-mm-ss") + ".bak";
            File.Copy(ThisDB, Destitnation);
        }

        public void ExitApplication(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Do You Wanna Quit Application?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        public void ExitApplication()
        {
            if (MessageBox.Show("Do You Wanna Quit Application?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        public string GetBarcode(TextBox txtProductName, ComboBox cmbCategoryName, ComboBox cmbSizeNo)
        {
            try
            {
                if (txtProductName == null || cmbCategoryName == null || cmbSizeNo == null)
                {
                    return "";
                }

                string productName = txtProductName.Text?.Trim();
                string categoryName = cmbCategoryName.Text?.ToString().Trim();
                string sizeNo = cmbSizeNo.Text?.ToString().Trim();
                string barcode = "";

                if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(categoryName) || string.IsNullOrEmpty(sizeNo))
                {
                    return "";
                }

                string[] productWords = productName.Split(' ');
                string productCode = "";
                if (productWords.Length > 1)
                {
                    foreach (string word in productWords)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            productCode += word[0].ToString().ToUpper();
                        }
                    }
                }
                else
                {
                    productCode = productName.Substring(0, Math.Min(2, productName.Length)).ToUpper();
                }

                if (productCode.Length > 3)
                {
                    productCode = productCode.Substring(0, 3);
                }

                string[] categoryWords = categoryName.Split(' ');
                string categoryCode = "";
                if (categoryWords.Length > 1)
                {
                    foreach (string word in categoryWords)
                    {
                        if (!string.IsNullOrEmpty(word))
                        {
                            categoryCode += word[0].ToString().ToUpper();
                        }
                    }
                }
                else
                {
                    categoryCode = categoryName.Substring(0, Math.Min(2, categoryName.Length)).ToUpper();
                }

                if (categoryCode.Length > 3)
                {
                    categoryCode = categoryCode.Substring(0, 3);
                }

                barcode = $"{productCode}-{categoryCode}-{sizeNo.ToUpper()}";

                string query = "Select f_barcode from Product_Items_db Where f_barcode = '" + barcode + "'";
                string foundOrNot = con.FetchData(query);

                Random random = new Random();

                while (!string.IsNullOrEmpty(foundOrNot))
                {
                    string newProductCode = productCode.Substring(0, 2) + ((char)random.Next('A', 'Z' + 1)).ToString();

                    string newCategoryCode = categoryCode.Substring(0, 2) + ((char)random.Next('A', 'Z' + 1)).ToString();

                    barcode = $"{newProductCode}-{newCategoryCode}-{sizeNo.ToUpper()}";

                    query = "Select f_barcode from Product_Items_db Where f_barcode = '" + barcode + "'";

                    foundOrNot = con.FetchData(query);
                }

                return barcode;
            }
            catch
            {
                return "";
            }

        }

        public void Close(Form Form)
        {
            Form.Close();
        }

        public void Close(Form Form, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Form.Close();
            }
        }

        public void ShortcutKey(Keys key, BunifuButton btn, KeyEventArgs e)
        {
            if (e.KeyCode == key)
            {
                btn.PerformClick();
            }
        }

        public void Clear(Control[] controls)
        {
            foreach (Control control in controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
                if (control is ComboBox comboBox)
                {
                    comboBox.SelectedItem = null;
                }
                controls[0].Focus();
            }
        }

        public bool getDataDgwToTextBox(DataGridView dgwDetails, Control[] controls)
        {
            if (dgwDetails.RowCount > 0)
            {
                if (MessageBox.Show("Do You Wanna View/Edit Data?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < controls.Length; i++)
                    {
                        controls[i].Text = dgwDetails.SelectedRows[0].Cells[i + 1].Value.ToString();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool getItemsDgwToTextBox(DataGridView dgwDetails, Control[] controls)
        {
            if (dgwDetails.RowCount > 0)
            {
                if (MessageBox.Show("Do You Wanna View/Edit Data?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < controls.Length; i++)
                    {
                        controls[i].Text = dgwDetails.SelectedRows[0].Cells[i].Value.ToString();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool getDgwToTextBox(DataGridView dgwDetails, Control[] controls)
        {
            if (dgwDetails.RowCount > 0)
            {
                for (int i = 0; i < controls.Length; i++)
                {
                    controls[i].Text = dgwDetails.SelectedRows[0].Cells[i].Value.ToString();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool validateControls(Control[] controls)
        {
            foreach (Control control in controls)
            {
                if (string.IsNullOrWhiteSpace(control.Text))
                {
                    MessageBox.Show("Kindly Fill All The Required Boxes?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus();
                    return false;
                }
            }
            return true;
        }

        public void Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
