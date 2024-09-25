using System;
using System.Data;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_PRODUCT_CATEGORY_MASTER : Form
    {

        Components comp = new Components();
        Connection con = new Connection();
        string query;
        string validate;
        int i = 0;

        public FRM_PRODUCT_CATEGORY_MASTER()
        {
            InitializeComponent();
        }

        private void AutoNumber()
        {
            string query = "Select f_product_category_master from Master_Prefix_db";
            con.GetId(query, txtProductCategoryId);

            string str = "Select f_product_category_id from Product_Category_db Order By ID Desc";
            con.AutoNumber(str, txtProductCategoryId);
        }

        private void FRM_PRODUCT_CATEGORY_MASTER_Load(object sender, EventArgs e)
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
            Control[] boxes = { txtProductCategoryName };
            comp.Clear(boxes);
            grpProductCategory.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwDetails.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtProductCategoryName };

            if (comp.validateControls(textBoxes))
            {
                if (btnSave.Text == "F2 Save" && grpProductCategory.Text == "Create")
                {
                    validate = "Select * from Product_Category_db where f_product_category_name ='" + txtProductCategoryName.Text + "'";
                    query = "Insert into Product_Category_db (f_product_category_id,f_product_category_name) values ('" + txtProductCategoryId.Text + "','" + txtProductCategoryName.Text + "')";
                    con.SaveData(query, validate);
                    ClearAll();
                    AutoNumber();
                }
                else if (btnSave.Text == "F2 Update" && grpProductCategory.Text == "Update")
                {
                    validate = "Select * from Product_Category_db where f_product_category_name ='" + txtProductCategoryName.Text + "'";
                    query = "Update Product_Category_db set f_product_category_id ='" + txtProductCategoryId.Text + "',f_product_category_name='" + txtProductCategoryName.Text + "' where ID=" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                    con.EditData(query, validate);
                    query = "Update Product_db set f_product_category_id ='" + txtProductCategoryId.Text + "',f_product_category_name='" + txtProductCategoryName.Text + "' where f_product_category_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    query = "Update Purchase_Items_db set f_product_category_id ='" + txtProductCategoryId.Text + "',f_product_category_name='" + txtProductCategoryName.Text + "' where f_product_category_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    query = "Update Sales_Items_db set f_product_category_id ='" + txtProductCategoryId.Text + "',f_product_category_name='" + txtProductCategoryName.Text + "' where f_product_category_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    ClearAll();
                    AutoNumber();
                    btnSave.Text = "F2 Save";
                    grpProductCategory.Text = "Create";
                }
                else if (grpProductCategory.Text == "Create")
                {
                    btnSave.Text = "F2 Save";
                }
                else if (grpProductCategory.Text == "Edit")
                {
                    MessageBox.Show("Please Select A Data❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearAll();
                }
                else if (grpProductCategory.Text == "View")
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
            query = "Select ID,f_product_category_id,f_product_category_name from Product_Category_db Order By ID Desc";
            string[] headerText = { "ID", "Product Category ID", "Product Category Name" };
            con.GetData(query, dgwDetails, headerText);
            dgwDetails.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grpProductCategory.Text == "Update" || grpProductCategory.Text == "View")
            {
                query = "Delete From Product_Category_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                con.DeleteData(query, dgwDetails);
            }
            ClearAll();
            displayData();
            AutoNumber();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            grpProductCategory.Text = "View";
            if (grpProductCategory.Text == "View")
            {
                ClearAll();
                grpProductCategory.Text = "View";
                displayData();
            }
            else
            {
                ClearAll();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            grpProductCategory.Text = "Edit";
            if (grpProductCategory.Text == "Edit")
            {
                ClearAll();
                grpProductCategory.Text = "Edit";
                displayData();
            }
            else
            {
                ClearAll();
            }
        }

        private void dgwDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control[] txtBoxes = { txtProductCategoryId, txtProductCategoryName };
            if (comp.getDataDgwToTextBox(dgwDetails, txtBoxes))
            {
                if (grpProductCategory.Text == "Edit")
                {
                    btnSave.Text = "F2 Update";
                    grpProductCategory.Text = "Update";
                    txtProductCategoryName.Focus();
                    dgwDetails.Hide();
                }
                else if (grpProductCategory.Text == "View")
                {
                    txtProductCategoryName.Focus();
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
                if (grpProductCategory.Text == "Edit")
                {
                    Control[] txtBoxes = { txtProductCategoryId, txtProductCategoryName };
                    comp.getDataDgwToTextBox(dgwDetails, txtBoxes);
                    btnSave.Text = "F2 Update";
                    grpProductCategory.Text = "Update";
                    dgwDetails.Hide();
                    txtProductCategoryName.Focus();
                }
                else if (grpProductCategory.Text == "View")
                {
                    Control[] txtBoxes = { txtProductCategoryId, txtProductCategoryName };
                    comp.getDataDgwToTextBox(dgwDetails, txtBoxes);
                    dgwDetails.Hide();
                    txtProductCategoryName.Focus();
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

        private void txtProductCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgwDetails.Visible && txtProductCategoryName.Text != "")
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

        private void txtProductCategoryName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grpProductCategory.Text == "Edit" || grpProductCategory.Text == "View" && dgwDetails.Visible == true)
                {
                    (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_product_category_name LIKE '%{0}%'", txtProductCategoryName.Text);
                }
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FRM_PRODUCT_CATEGORY_MASTER_KeyDown(object sender, KeyEventArgs e)
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
