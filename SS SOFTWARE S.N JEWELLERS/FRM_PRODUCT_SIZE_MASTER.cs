using System;
using System.Data;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_PRODUCT_SIZE_MASTER : Form
    {

        Components comp = new Components();
        Connection con = new Connection();
        string query;
        string validate;
        int i = 0;

        public FRM_PRODUCT_SIZE_MASTER()
        {
            InitializeComponent();
        }

        private void AutoNumber()
        {
            string query = "Select f_product_size_master from Master_Prefix_db";
            con.GetId(query, txtProductSizeId);

            string str = "Select f_product_size_id from Product_Size_db Order By ID Desc";
            con.AutoNumber(str, txtProductSizeId);
        }

        private void FRM_PRODUCT_SIZE_MASTER_Load(object sender, EventArgs e)
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
            Control[] boxes = { txtProductSizeNo };
            comp.Clear(boxes);
            grpProductSize.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwDetails.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtProductSizeNo };

            if (comp.validateControls(textBoxes))
            {
                if (btnSave.Text == "F2 Save" && grpProductSize.Text == "Create")
                {
                    validate = "Select * from Product_Size_db where f_product_size_no ='" + txtProductSizeNo.Text + "'";
                    query = "Insert into Product_Size_db (f_product_size_id,f_product_size_no) values ('" + txtProductSizeId.Text + "','" + txtProductSizeNo.Text + "')";
                    con.SaveData(query, validate);
                    ClearAll();
                    AutoNumber();
                }
                else if (btnSave.Text == "F2 Update" && grpProductSize.Text == "Update")
                {
                    validate = "Select * from Product_Size_db where f_product_size_no ='" + txtProductSizeNo.Text + "'";
                    query = "Update Product_Size_db set f_product_size_id ='" + txtProductSizeId.Text + "',f_product_size_no='" + txtProductSizeNo.Text + "' where ID=" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                    con.EditData(query, validate);
                    query = "Update Product_Items_db set f_product_size_id ='" + txtProductSizeId.Text + "',f_product_size_no='" + txtProductSizeNo.Text + "' where f_product_size_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    query = "Update Purchase_Items_db set f_product_size_id ='" + txtProductSizeId.Text + "',f_product_size_no='" + txtProductSizeNo.Text + "' where f_product_size_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    query = "Update Sales_Items_db set f_product_size_id ='" + txtProductSizeId.Text + "',f_product_size_no='" + txtProductSizeNo.Text + "' where f_product_size_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    ClearAll();
                    AutoNumber();
                    btnSave.Text = "F2 Save";
                    grpProductSize.Text = "Create";
                }
                else if (grpProductSize.Text == "Create")
                {
                    btnSave.Text = "F2 Save";
                }
                else if (grpProductSize.Text == "Edit")
                {
                    MessageBox.Show("Please Select A Data❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearAll();
                }
                else if (grpProductSize.Text == "View")
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
            query = "Select ID,f_product_size_id,f_product_size_no from Product_Size_db Order By ID Desc";
            string[] headerText = { "ID", "Product Size ID", "Product Size No" };
            con.GetData(query, dgwDetails, headerText);
            dgwDetails.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grpProductSize.Text == "Update" || grpProductSize.Text == "View")
            {
                query = "Delete From Product_Size_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                con.DeleteData(query, dgwDetails);
            }
            ClearAll();
            displayData();
            AutoNumber();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            grpProductSize.Text = "View";
            if (grpProductSize.Text == "View")
            {
                ClearAll();
                grpProductSize.Text = "View";
                displayData();
            }
            else
            {
                ClearAll();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            grpProductSize.Text = "Edit";
            if (grpProductSize.Text == "Edit")
            {
                ClearAll();
                grpProductSize.Text = "Edit";
                displayData();
            }
            else
            {
                ClearAll();
            }
        }

        private void dgwDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control[] txtBoxes = { txtProductSizeId, txtProductSizeNo };
            if (comp.getDataDgwToTextBox(dgwDetails, txtBoxes))
            {
                if (grpProductSize.Text == "Edit")
                {
                    btnSave.Text = "F2 Update";
                    grpProductSize.Text = "Update";
                    txtProductSizeNo.Focus();
                    dgwDetails.Hide();
                }
                else if (grpProductSize.Text == "View")
                {
                    txtProductSizeNo.Focus();
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
                if (grpProductSize.Text == "Edit")
                {
                    Control[] txtBoxes = { txtProductSizeId, txtProductSizeNo };
                    comp.getDataDgwToTextBox(dgwDetails, txtBoxes);
                    btnSave.Text = "F2 Update";
                    grpProductSize.Text = "Update";
                    dgwDetails.Hide();
                    txtProductSizeNo.Focus();
                }
                else if (grpProductSize.Text == "View")
                {
                    Control[] txtBoxes = { txtProductSizeId, txtProductSizeNo };
                    comp.getDataDgwToTextBox(dgwDetails, txtBoxes);
                    dgwDetails.Hide();
                    txtProductSizeNo.Focus();
                }
                else
                {
                    ClearAll();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ClearAll();
            }
        }

        private void txtProductSizeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgwDetails.Visible && txtProductSizeNo.Text != "")
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

        private void txtProductSizeNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grpProductSize.Text == "Edit" || grpProductSize.Text == "View" && dgwDetails.Visible == true)
                {
                    (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_product_size_no LIKE '%{0}%'", txtProductSizeNo.Text);
                }
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FRM_PRODUCT_SIZE_MASTER_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
            comp.ShortcutKey(Keys.F1, btnNew, e);
            comp.ShortcutKey(Keys.F2, btnSave, e);
            comp.ShortcutKey(Keys.F3, btnEdit, e);
            comp.ShortcutKey(Keys.F4, btnDelete, e);
            comp.ShortcutKey(Keys.F5, btnView, e);
        }
    }
}
