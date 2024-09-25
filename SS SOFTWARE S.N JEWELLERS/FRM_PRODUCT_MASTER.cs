using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_PRODUCT_MASTER : Form
    {
        Components comp = new Components();
        Connection con = new Connection();
        DataTable dt = new DataTable();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        string query;
        string validate;
        int i = 0;

        public FRM_PRODUCT_MASTER()
        {
            InitializeComponent();
            dt.Columns.Add("Product Size No", typeof(string));
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("Printing Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dgwItems.DataSource = dt;
        }

        private void AutoNumber()
        {
            string query = "Select f_product_master from Master_Prefix_db";
            con.GetId(query, txtProductId);

            string str = "Select f_product_id from Product_db Order By ID Desc";
            con.AutoNumber(str, txtProductId);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void FRM_PRODUCT_MASTER_Load(object sender, EventArgs e)
        {
            AutoNumber();
            getItems();
        }

        private void ClearAll()
        {
            comp.Clear(new Control[] { txtProductName, cmbProductCategoryName, cmbGodownName, cmbProductSizeNo, txtBarcode, txtPrintingName, txtQuantity });
            grpProduct.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwDetails.Hide();
            txtPrintingName.Text = txtProductName.Text;
            dgwItems.Show();
            dt.Clear();
            btnAdd.Enabled = true;
        }

        private void ClearItems()
        {
            comp.Clear(new Control[] { cmbProductSizeNo, txtBarcode, txtPrintingName, txtQuantity });
            txtPrintingName.Text = txtProductName.Text;
            btnAdd.Enabled = true;
        }

        private void ClearItems2()
        {
            comp.Clear(new Control[] { cmbProductSizeNo });
            txtPrintingName.Text = txtProductName.Text;
            btnAdd.Enabled = true;
        }

        private void SaveItems()
        {
            foreach (DataRow row in dt.Rows)
            {
                query = "insert into Product_Items_db (f_product_id,f_product_name,f_product_size_no,f_barcode,f_printing_name,f_quantity) Values ('" + txtProductId.Text + "', '" + txtProductName.Text + "', '" + row[0] + "', '" + row[1] + "', '" + row[2] + "', '" + row[3] + "')";
                con.SaveOrEditItems(query);
            }
        }

        private void getItems()
        {
            cmbProductSizeNo.Items.Clear();
            cmbProductCategoryName.Items.Clear();
            cmbGodownName.Items.Clear();
            string query;
            query = "Select f_product_category_name from Product_Category_db";
            con.GetItems(query, cmbProductCategoryName);
            query = "Select f_godown_name from Godown_db";
            con.GetItems(query, cmbGodownName);
            query = "Select f_product_size_no from Product_Size_db";
            con.GetItems(query, cmbProductSizeNo);
        }

        private void UpdateItems()
        {
            query = "delete from Product_Items_db where f_product_id = '" + txtProductId.Text + "'";
            con.SaveOrEditItems(query);
            foreach (DataRow row in dt.Rows)
            {
                query = "insert into Product_Items_db (f_product_id,f_product_name,f_product_size_no,f_barcode,f_printing_name,f_quantity) Values ('" + txtProductId.Text + "', '" + txtProductName.Text + "', '" + row[0] + "', '" + row[1] + "', '" + row[2] + "', '" + row[3] + "')";
                con.SaveOrEditItems(query);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtProductName, cmbProductCategoryName };

            if (comp.validateControls(textBoxes))
            {
                if (btnSave.Text == "F2 Save" && grpProduct.Text == "Create")
                {
                    validate = "Select * from Product_db where f_product_name ='" + txtProductName.Text + "'";
                    query = "Insert into Product_db (f_product_id,f_product_name,f_product_category_name,f_godown_name) values ('" + txtProductId.Text + "','" + txtProductName.Text + "','" + cmbProductCategoryName.Text + "','" + cmbGodownName.Text + "')";
                    if (con.SaveData(query, validate))
                    {
                        SaveItems();
                    }
                    ClearAll();
                    AutoNumber();
                }
                else if (btnSave.Text == "F2 Update" && grpProduct.Text == "Update")
                {
                    validate = "Select * from Product_db where f_product_name ='" + txtProductName.Text + "'";
                    query = "Update Product_db set f_product_id ='" + txtProductId.Text + "',f_product_name='" + txtProductName.Text + "',f_product_category_name='" + cmbProductCategoryName.Text + "',f_godown_name ='" + cmbGodownName.Text + "' where ID=" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                    if (con.EditData(query, validate))
                    {
                        UpdateItems();
                    }
                    query = "Update Purchase_Items_db set f_product_id ='" + txtProductId.Text + "',f_product_name='" + txtProductName.Text + "' where f_product_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    query = "Update Sales_Items_db set f_product_id ='" + txtProductId.Text + "',f_product_name='" + txtProductName.Text + "' where f_product_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    ClearAll();
                    AutoNumber();
                    btnSave.Text = "F2 Save";
                    grpProduct.Text = "Create";
                }
                else if (grpProduct.Text == "Create")
                {
                    btnSave.Text = "F2 Save";
                }
                else if (grpProduct.Text == "Edit")
                {
                    MessageBox.Show("Please Select A Data❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearAll();
                }
                else if (grpProduct.Text == "View")
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
            else
            {
                btnSave.Focus();
            }
        }

        private void displayData()
        {
            query = "Select ID,f_product_id,f_product_name,f_product_category_name,f_godown_name from Product_db Order By ID Desc";
            string[] headerText = { "ID", "Product ID", "Product Name", "Category Name", "Godown Name" };
            con.GetData(query, dgwDetails, headerText);
            dgwItems.Hide();
            dgwDetails.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grpProduct.Text == "Update" || grpProduct.Text == "View")
            {
                query = "Delete From Product_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                if (con.DeleteData(query, dgwDetails))
                {
                    query = "delete from Product_Items_db where f_product_id = '" + dgwDetails.SelectedRows[i].Cells[1].Value.ToString() + "'";
                    con.SaveOrEditItems(query);
                }
                ClearAll();
                displayData();
                AutoNumber();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            grpProduct.Text = "View";
            if (grpProduct.Text == "View")
            {
                ClearAll();
                grpProduct.Text = "View";
                displayData();
            }
            else
            {
                ClearAll();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            grpProduct.Text = "Edit";
            if (grpProduct.Text == "Edit")
            {
                ClearAll();
                grpProduct.Text = "Edit";
                displayData();
            }
            else
            {
                ClearAll();
            }
        }

        private void dgwDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!comp.getDataDgwToTextBox(dgwDetails, new Control[] { txtProductId, txtProductName, cmbProductCategoryName, cmbGodownName })) return;

            if (grpProduct.Text == "Edit")
            {
                btnSave.Text = "F2 Update";
                grpProduct.Text = "Update";
            }
            displayItems();
            txtProductName.Focus();
            dgwDetails.Hide();
            dgwItems.Show();
        }

        private void displayItems()
        {
            query = "Select f_product_size_no,f_barcode,f_printing_name,f_quantity FROM Product_Items_db WHERE f_product_id = '" + txtProductId.Text + "'";
            DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter(query, Main);
            ad.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dt.Rows.Add(row[0], row[1], row[2], row[3]);
            }
        }

        private void dgwItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgwItems.SelectedRows.Count == 0) return;

            comp.getItemsDgwToTextBox(dgwItems, new Control[] { cmbProductSizeNo, txtBarcode, txtPrintingName, txtQuantity });
            btnAdd.Enabled = false;
            cmbProductSizeNo.Focus();
        }

        private void FRM_PRODUCT_MASTER_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
            comp.ShortcutKey(Keys.F1, btnNew, e);
            comp.ShortcutKey(Keys.F2, btnSave, e);
            comp.ShortcutKey(Keys.F3, btnEdit, e);
            comp.ShortcutKey(Keys.F4, btnDelete, e);
            comp.ShortcutKey(Keys.F5, btnView, e);
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgwDetails.Visible && !string.IsNullOrEmpty(txtProductName.Text))
                {
                    dgwDetails.Focus();
                }
                else
                {
                    dgwDetails.Hide();
                    dgwItems.Show();
                    SendKeys.Send("{TAB}");
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grpProduct.Text == "Edit" || grpProduct.Text == "View" && dgwDetails.Visible == true)
                {
                    (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_product_name LIKE '%{0}%'", txtProductName.Text);
                }
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgwDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comp.getDataDgwToTextBox(dgwDetails, new Control[] { txtProductId, txtProductName, cmbProductCategoryName, cmbGodownName }))
                {
                    if (grpProduct.Text == "Edit")
                    {
                        btnSave.Text = "F2 Update";
                        grpProduct.Text = "Update";
                    }
                    displayItems();
                    txtProductName.Focus();
                    dgwDetails.Hide();
                    dgwItems.Show();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgwItems.SelectedRows.Count > 0)
            {
                var selectedRow = dgwItems.SelectedRows[0];
                selectedRow.Cells[0].Value = cmbProductSizeNo.Text;
                selectedRow.Cells[1].Value = txtBarcode.Text;
                selectedRow.Cells[2].Value = txtPrintingName.Text;
                selectedRow.Cells[3].Value = txtQuantity.Text;
                ClearItems();
            }
            else
            {
                MessageBox.Show("Data Not Found? You Can't Edit", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool itemExists = false;
            foreach (DataGridViewRow row in dgwItems.Rows)
            {
                if (row.Cells[0].Value.ToString() == txtProductId.Text)
                {
                    double lastQty = double.Parse(row.Cells[3].Value.ToString());
                    double newQty = lastQty + double.Parse(txtQuantity.Text);
                    row.Cells[3].Value = newQty;
                    itemExists = true;
                    break;
                }
            }
            if (MessageBox.Show("Do You Wanna Add One More Details?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!itemExists)
                {
                    dt.Rows.Add(cmbProductSizeNo.SelectedItem.ToString(), txtBarcode.Text, txtPrintingName.Text, txtQuantity.Text);
                    ClearItems2();
                }
                cmbProductSizeNo.Focus();
            }
            else
            {
                dt.Rows.Add(cmbProductSizeNo.SelectedItem.ToString(), txtBarcode.Text, txtPrintingName.Text, txtQuantity.Text);
                ClearItems();
                btnSave.Focus();
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

        private void Enter_Key_Press(object sender, KeyEventArgs e)
        {
            comp.Enter(sender, e);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearItems();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgwItems.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Do You Wanna Delete?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dt.Rows.RemoveAt(dgwItems.SelectedRows[0].Index);
                ClearItems();
            }
        }

        private async void cmbProductSizeNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (comp.validateControls(new Control[] { txtProductName, cmbProductCategoryName }))
                {
                    txtBarcode.Text = await comp.GetBarcode(txtProductName, cmbProductCategoryName, cmbProductSizeNo);
                }
            }
            catch
            {

            }
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            txtPrintingName.Text = txtProductName.Text;
        }

        private void txtPrintingName_Leave(object sender, EventArgs e)
        {
            if (txtPrintingName.Text == string.Empty)
            {
                txtPrintingName.Text = txtProductName.Text;
            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            if (txtQuantity.Text == string.Empty)
            {
                txtQuantity.Text = "0";
            }
        }
    }
}
