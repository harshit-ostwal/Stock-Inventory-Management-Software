using System;
using System.Data;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_COMPANY_MASTER : Form
    {

        Components comp = new Components();
        Connection con = new Connection();
        string query;
        string validate;
        int i = 0;

        public FRM_COMPANY_MASTER()
        {
            InitializeComponent();
        }

        private void AutoNumber()
        {
            string query = "Select f_company_master from Master_Prefix_db";
            con.GetId(query, txtCompanyId);

            string str = "Select f_company_id from Company_db Order By ID Desc";
            con.AutoNumber(str, txtCompanyId);
        }

        private void FRM_COMPANY_MASTER_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            comp.Close(this);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            Control[] boxes = { txtCompanyName, txtGstin, txtAddress, txtArea, txtPincode, txtEmailId, txtMobileNo };
            comp.Clear(boxes);
            grpCustomer.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwDetails.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtCompanyName, txtMobileNo };

            if (comp.validateControls(textBoxes))
            {
                if (btnSave.Text == "F2 Save" && grpCustomer.Text == "Create")
                {
                    validate = "Select * from Company_db where f_company_name ='" + txtCompanyName.Text + "'";
                    query = "Insert into Company_db (f_company_id,f_company_name,f_gstin,f_address,f_area,f_pincode,f_email_id,f_mobile_no) values ('" + txtCompanyId.Text + "','" + txtCompanyName.Text + "','" + txtGstin.Text + "','" + txtAddress.Text + "','" + txtArea.Text + "','" + txtPincode.Text + "','" + txtEmailId.Text + "','" + txtMobileNo.Text + "')";
                    con.SaveData(query, validate);
                    ClearAll();
                    AutoNumber();
                }
                else if (btnSave.Text == "F2 Update" && grpCustomer.Text == "Update")
                {
                    validate = "Select * from Company_db where f_company_name ='" + txtCompanyName.Text + "'";
                    query = "Update Company_db set f_company_id ='" + txtCompanyId.Text + "',f_company_name='" + txtCompanyName.Text + "' ,f_gstin ='" + txtGstin.Text + "',f_address='" + txtAddress.Text + "',f_area='" + txtArea.Text + "',f_pincode ='" + txtPincode.Text + "',f_email_id='" + txtEmailId.Text + "',f_mobile_no='" + txtMobileNo.Text + "' where ID=" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                    con.EditData(query, validate);
                    ClearAll();
                    AutoNumber();
                    btnSave.Text = "F2 Save";
                    grpCustomer.Text = "Create";
                }
                else if (grpCustomer.Text == "Create")
                {
                    btnSave.Text = "F2 Save";
                }
                else if (grpCustomer.Text == "Edit")
                {
                    MessageBox.Show("Please Select A Data❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearAll();
                }
                else if (grpCustomer.Text == "View")
                {
                    MessageBox.Show("You Are In The View Mode❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearAll();
                }
                else
                {
                    btnSave.Text = "F2 Save";
                    ClearAll();
                }
            }
        }

        private void displayData()
        {
            query = "Select ID,f_company_id,f_company_name,f_gstin,f_address,f_area,f_pincode,f_email_id,f_mobile_no from Company_db Order By ID Desc";
            string[] headerText = { "ID", "Company ID", "Company Name", "Gstin", "Address", "Area", "Pincode", "Email ID", "Mobile No" };
            con.GetData(query, dgwDetails, headerText);
            dgwDetails.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grpCustomer.Text == "Update" || grpCustomer.Text == "View" || dgwDetails.Visible == true)
            {
                query = "Delete From Company_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                con.DeleteData(query, dgwDetails);
            }
            ClearAll();
            displayData();
            AutoNumber();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            grpCustomer.Text = "View";
            if (grpCustomer.Text == "View")
            {
                ClearAll();
                grpCustomer.Text = "View";
                displayData();
                AutoNumber();
            }
            else
            {
                ClearAll();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            grpCustomer.Text = "Edit";
            if (grpCustomer.Text == "Edit")
            {
                ClearAll();
                grpCustomer.Text = "Edit";
                displayData();
            }
            else
            {
                ClearAll();
            }
        }

        private void dgwDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control[] txtBoxes = { txtCompanyId, txtCompanyName, txtGstin, txtAddress, txtArea, txtPincode, txtEmailId, txtMobileNo };
            if (comp.getDataDgwToTextBox(dgwDetails, txtBoxes))
            {
                if (grpCustomer.Text == "Edit")
                {
                    btnSave.Text = "F2 Update";
                    grpCustomer.Text = "Update";
                    txtCompanyName.Focus();
                    dgwDetails.Hide();
                }
                else if (grpCustomer.Text == "View")
                {
                    txtCompanyName.Focus();
                    dgwDetails.Hide();
                }
            }
            else
            {
                ClearAll();
            }
        }

        private void dgwDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control[] txtBoxes = { txtCompanyId, txtCompanyName, txtGstin, txtAddress, txtArea, txtPincode, txtEmailId, txtMobileNo };
                if (comp.getDataDgwToTextBox(dgwDetails, txtBoxes))
                {
                    if (grpCustomer.Text == "Edit")
                    {
                        btnSave.Text = "F2 Update";
                        grpCustomer.Text = "Update";
                        dgwDetails.Hide();
                        txtCompanyName.Focus();
                    }
                    else if (grpCustomer.Text == "View")
                    {
                        dgwDetails.Hide();
                        txtCompanyName.Focus();
                    }
                    else
                    {
                        ClearAll();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ClearAll();
            }
        }

        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgwDetails.Visible && txtCompanyName.Text != "")
                {
                    dgwDetails.Focus();
                }
                else if (!dgwDetails.Visible)
                {
                    dgwDetails.Hide();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    dgwDetails.Hide();
                    SendKeys.Send("{TAB}");
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grpCustomer.Text == "Edit" || grpCustomer.Text == "View" && dgwDetails.Visible == true)
                {
                    (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_company_name LIKE '%{0}%'", txtCompanyName.Text);
                }
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FRM_COMPANY_MASTER_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
            comp.ShortcutKey(Keys.F1, btnNew, e);
            comp.ShortcutKey(Keys.F2, btnSave, e);
            comp.ShortcutKey(Keys.F3, btnEdit, e);
            comp.ShortcutKey(Keys.F4, btnDelete, e);
            comp.ShortcutKey(Keys.F5, btnView, e);
        }

        private void Enter_Key_Press(object sender, KeyEventArgs e)
        {
            comp.Enter(sender, e);
        }
    }
}
