using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_SETTINGS : Form
    {

        Connection con = new Connection();
        Components comp = new Components();
        string query;

        public FRM_SETTINGS()
        {
            InitializeComponent();
        }

        private void FRM_SETTINGS_Load(object sender, EventArgs e)
        {
            ViewMasterData();
            ViewTransactionData();
            ViewUserMaster();
        }

        private void ViewMasterData()
        {
            query = "Select f_company_master,f_customer_master,f_supplier_master,f_godown_master,f_product_category_master,f_product_size_master,f_product_master From Master_Prefix_db";
            Control[] MasterBoxes = { txtCompanyPrefix, txtCustomerPrefix, txtSupplierPrefix, txtGodownPrefix, txtProductCategoryPrefix, txtProductSizePrefix, txtProductPrefix };
            con.FetchAdminData(MasterBoxes, query);
        }

        private void ViewTransactionData()
        {
            query = "Select f_purchase,f_sales,f_stock_transfer From Transaction_Prefix_db";
            Control[] TransactionBoxes = { txtPurchasePrefix, txtSalesPrefix, txtStockTransferPrefix };
            con.FetchAdminData(TransactionBoxes, query);
        }

        private void ViewUserMaster()
        {
            query = "Select f_user_name,f_password From Login_db";
            Control[] UserMaster = { txtUsername, txtPassword };
            con.FetchAdminData(UserMaster, query);
        }

        private void btnRestoreDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog RestoreDB = new OpenFileDialog())
                {
                    RestoreDB.Filter = "BAK File|*.bak";
                    RestoreDB.Multiselect = true;
                    string MainDB = Application.StartupPath + "/DATABASE/Restore_Main_db.accdb";
                    string SettingsDB = Application.StartupPath + "/DATABASE/Restore_Settings_db.accdb";
                    if (MessageBox.Show("Do You Want To Restore Your Database? 📅", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (RestoreDB.ShowDialog() == DialogResult.OK)
                        {
                            foreach (string PickedFile in RestoreDB.FileNames)
                            {
                                if (File.Exists(PickedFile))
                                {
                                    File.Copy(PickedFile, SettingsDB, true);
                                    File.Copy(PickedFile, MainDB, true);
                                }
                                else
                                {
                                    MessageBox.Show("No Database File Selected?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            MessageBox.Show("Database Restored Successfull! ✅", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Restoring Database?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackupDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do You Want To Backup Your Database? 📅", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string Destination;
                    string MainDB = Application.StartupPath + "/DATABASE/Main_db.accdb";
                    string SettingsDB = Application.StartupPath + "/DATABASE/Settings_db.accdb";
                    if (SaveDB.ShowDialog() == DialogResult.OK)
                    {
                        string Path = SaveDB.SelectedPath;
                        Destination = Path + "/Main_db " + DateTime.Now.ToString(" dd-MM-yyyy hh-mm-ss") + ".bak";
                        File.Copy(MainDB, Destination);
                        Destination = Path + "/Settings_db " + DateTime.Now.ToString(" dd-MM-yyyy hh-mm-ss") + ".bak";
                        File.Copy(SettingsDB, Destination);
                        MessageBox.Show("Database Backup Successfull! ✅", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Backup Database?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMasterView_Click(object sender, EventArgs e)
        {
            txtCompanyPrefix.Focus();
        }

        private void btnTransactionView_Click(object sender, EventArgs e)
        {
            txtPurchasePrefix.Focus();
        }

        private void Enter_Only(object sender, KeyEventArgs e)
        {
            comp.Enter(sender, e);
        }

        private void btnUserView_Click(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnUserUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do You Wanna Update Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string query = "Update Login_db set f_user_name ='" + txtUsername.Text + "',f_password='" + txtPassword.Text + "'";
                    con.AdminSaveOrEdit(query);
                    MessageBox.Show("Data Updation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ViewUserMaster();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Data Updation?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMasterUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do You Wanna Update Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string query = "Update Master_Prefix_db set f_company_master ='" + txtCompanyPrefix.Text + "',f_customer_master='" + txtCustomerPrefix.Text + "',f_supplier_master ='" + txtSupplierPrefix.Text + "',f_godwon_master ='" + txtGodownPrefix.Text + "',f_product_category_master ='" + txtProductCategoryPrefix.Text + "',f_product_size_master ='" + txtProductSizePrefix.Text + "',f_product_master ='" + txtProductPrefix.Text + "'";
                    con.AdminSaveOrEdit(query);
                    MessageBox.Show("Data Updation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ViewMasterData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Data Updation?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTransactionUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Do You Wanna Update Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string query = "Update Transaction_Prefix_db set f_purchase ='" + txtPurchasePrefix.Text + "',f_sales ='" + txtSalesPrefix.Text + "',f_stock_transfer ='" + txtStockTransferPrefix.Text + "'";
                    con.AdminSaveOrEdit(query);
                    MessageBox.Show("Data Updation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ViewTransactionData();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Data Updation?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            comp.Close(this);
        }

        private void FRM_SETTINGS_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void ValidateTextBoxLength(TextBox textBox)
        {
            if (textBox.Text.Length > 3)
            {
                MessageBox.Show("Prefix Must Exactly Have Only 3 Characters?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtStockTransferPrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtStockTransferPrefix);
        }

        private void txtSalesPrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtSalesPrefix);
        }

        private void txtPurchasePrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtPurchasePrefix);
        }

        private void txtProductPrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtProductPrefix);
        }

        private void txtCustomerPrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtCustomerPrefix);
        }

        private void txtSupplierPrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtSupplierPrefix);
        }

        private void txtGodownPrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtGodownPrefix);
        }

        private void txtProductCategoryPrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtProductCategoryPrefix);
        }

        private void txtProductSizePrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtProductSizePrefix);
        }

        private void txtCompanyPrefix_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtCompanyPrefix);
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtPassword);
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBoxLength(txtUsername);
        }

    }
}
