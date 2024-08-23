using System;
using System.Data;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_SUPPLIER_MASTER : Form
    {

        Components comp = new Components();
        Connection con = new Connection();
        string query;
        string validate;
        int i = 0;

        public FRM_SUPPLIER_MASTER()
        {
            InitializeComponent();
        }

        private void AutoNumber()
        {
            string query = "Select f_supplier_master from Master_Prefix_db";
            con.GetId(query, txtSupplierId);

            string str = "Select f_supplier_id from Supplier_db Order By ID Desc";
            con.AutoNumber(str, txtSupplierId);
        }

        private void FRM_SUPPLIER_MASTER_Load(object sender, EventArgs e)
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
            Control[] boxes = { txtSupplierName, txtGstin, txtMobileNo };
            comp.Clear(boxes);
            grpCustomer.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwDetails.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtSupplierName, txtMobileNo };

            if (comp.validateControls(textBoxes))
            {
                if (btnSave.Text == "F2 Save" && grpCustomer.Text == "Create")
                {
                    validate = "Select * from Supplier_db where f_supplier_name ='" + txtSupplierName.Text + "'";
                    query = "Insert into Supplier_db (f_supplier_id,f_supplier_name,f_gstin,f_mobile_no) values ('" + txtSupplierId.Text + "','" + txtSupplierName.Text + "','" + txtGstin.Text + "','" + txtMobileNo.Text + "')";
                    con.SaveData(query, validate);
                    ClearAll();
                    AutoNumber();
                }
                else if (btnSave.Text == "F2 Update" && grpCustomer.Text == "Update")
                {
                    validate = "Select * from Supplier_db where f_supplier_name ='" + txtSupplierName.Text + "'";
                    query = "Update Supplier_db set f_supplier_id ='" + txtSupplierId.Text + "',f_supplier_name='" + txtSupplierName.Text + "' ,f_gstin ='" + txtGstin.Text + "',f_mobile_no='" + txtMobileNo.Text + "' where ID=" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                    con.EditData(query, validate);
                    query = "Update Supplier_db set f_supplier_id ='" + txtSupplierId.Text + "',f_supplier_name='" + txtSupplierName.Text + "' ,f_gstin ='" + txtGstin.Text + "',f_mobile_no='" + txtMobileNo.Text + "' where f_supplier_id=" + dgwDetails.SelectedRows[i].Cells[1].Value.ToString() + "";
                    con.SaveOrEditItems(query);
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
            query = "Select ID,f_supplier_id,f_supplier_name,f_gstin,f_mobile_no from Supplier_db Order By ID Desc";
            string[] headerText = { "ID", "Supplier ID", "Supplier Name", "Gstin", "Mobile No" };
            con.GetData(query, dgwDetails, headerText);
            dgwDetails.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grpCustomer.Text == "Update" || grpCustomer.Text == "View" || dgwDetails.Visible == true)
            {
                query = "Delete From Supplier_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
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
            Control[] txtBoxes = { txtSupplierId, txtSupplierName, txtGstin, txtMobileNo };
            if (comp.getDataDgwToTextBox(dgwDetails, txtBoxes))
            {
                if (grpCustomer.Text == "Edit")
                {
                    btnSave.Text = "F2 Update";
                    grpCustomer.Text = "Update";
                    txtSupplierName.Focus();
                    dgwDetails.Hide();
                }
                else if (grpCustomer.Text == "View")
                {
                    txtSupplierName.Focus();
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
                Control[] txtBoxes = { txtSupplierId, txtSupplierName, txtGstin, txtMobileNo };
                if (comp.getDataDgwToTextBox(dgwDetails, txtBoxes))
                {
                    if (grpCustomer.Text == "Edit")
                    {
                        btnSave.Text = "F2 Update";
                        grpCustomer.Text = "Update";
                        dgwDetails.Hide();
                        txtSupplierName.Focus();
                    }
                    else if (grpCustomer.Text == "View")
                    {
                        dgwDetails.Hide();
                        txtSupplierName.Focus();
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

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgwDetails.Visible && txtSupplierName.Text != "")
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

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grpCustomer.Text == "Edit" || grpCustomer.Text == "View" && dgwDetails.Visible == true)
                {
                    (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_supplier_name LIKE '%{0}%'", txtSupplierName.Text);
                }
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FRM_SUPPLIER_MASTER_KeyDown(object sender, KeyEventArgs e)
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
