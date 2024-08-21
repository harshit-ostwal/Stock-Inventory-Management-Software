using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_STOCK_TRANSFER : Form
    {

        Components comp = new Components();
        Connection con = new Connection();
        DataTable dt = new DataTable();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        string query;
        string validate;

        public FRM_STOCK_TRANSFER()
        {
            InitializeComponent();
            dt.Columns.Add("Product ID", typeof(string));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Product Category ID", typeof(string));
            dt.Columns.Add("Product Category Name", typeof(string));
            dt.Columns.Add("Product Godown ID", typeof(string));
            dt.Columns.Add("Product Godown Name", typeof(string));
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("Product Size ID", typeof(string));
            dt.Columns.Add("Product Size No", typeof(string));
            dt.Columns.Add("Printing Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt2.Columns.Add("Product ID", typeof(string));
            dt2.Columns.Add("Product Name", typeof(string));
            dt2.Columns.Add("Product Category ID", typeof(string));
            dt2.Columns.Add("Product Category Name", typeof(string));
            dt2.Columns.Add("Product Godown ID", typeof(string));
            dt2.Columns.Add("Product Godown Name", typeof(string));
            dgwItems.DataSource = dt;
        }

        private void FRM_STOCK_TRANSFER_Load(object sender, EventArgs e)
        {
            GetGodownData();
            AutoNumber();
        }

        private void AutoNumber()
        {
            string query = "Select f_stock_transfer from Transaction_Prefix_db";
            con.GetId(query, txtStockTransferId);

            string str = "Select f_stock_transfer_id from Stock_Transfer_db Order By ID Desc";
            con.AutoNumber(str, txtStockTransferId);
        }

        private void CalculateTotal()
        {
            double totalQuantity = 0;
            if (dgwItems.RowCount == 0)
            {
                lblTotalQuantity.Text = "0";
                lblTotalProducts.Text = "0";
            }


            for (int i = 0; i < dgwItems.Rows.Count; i++)
            {
                if (dgwItems.Rows[i].Cells[10].Value != null)
                { totalQuantity += double.Parse(dgwItems.Rows[i].Cells[10].Value.ToString()); }

                lblTotalProducts.Text = dgwItems.RowCount.ToString();
                lblTotalQuantity.Text = Math.Round(totalQuantity, 2).ToString();
            }
        }

        private void GetGodownData()
        {
            cmbFromGodown.Items.Clear();
            cmbToGodown.Items.Clear();
            query = "Select f_godown_name from Godown_db";
            con.GetItems(query, cmbFromGodown);
            con.GetItems(query, cmbToGodown);
        }

        private void displayProductData()
        {
            query = "Select f_product_id, f_product_name, f_product_category_name,f_godown_name FROM Product_db where f_godown_name='" + cmbFromGodown.Text + "'";
            string[] headerText = { "Product ID", "Product Name", "Category Name", "Godown Name" };
            con.GetData(query, dgwProduct, headerText);
        }

        private void ClearAll()
        {
            comp.Clear(new Control[] { cmbFromGodown, cmbToGodown, txtProductId, txtProductName, txtPrintingName, txtProductCategoryName, txtGodownName, txtBarcode, txtProductSizeId, cmbProductSizeNo, txtProductCategoryName, txtQuantity });
            grpProduct.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwProduct.Hide();
            dgwItems.Show();
            dt.Clear();
            isRemoveClicked = false;
            btnAdd.Enabled = true;
            dt2.Clear();
            lblTotalQuantity.Text = "0";
            lblTotalProducts.Text = "0";
        }

        private void ClearProduct()
        {
            comp.Clear(new Control[] { txtProductName, txtProductId, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtProductSizeId, txtPrintingName, txtQuantity });
            displayProductData();
            dgwProduct.Hide();
            btnAdd.Enabled = true;
            CalculateTotal();
            barcode = false;
        }
        private void getItems()
        {
            cmbProductSizeNo.Items.Clear();
            query = "Select f_product_size_no from Product_Items_db Where f_product_id='" + txtProductId.Text + "' and f_product_name='" + txtProductName.Text + "'";
            con.GetItems(query, cmbProductSizeNo);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearProduct();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgwItems.SelectedRows.Count > 0)
            {
                query = "select f_product_category_id from Product_Category_db where f_product_category_name='" + txtProductCategoryName.Text + "'";
                string categoryId = con.FetchData(query);
                query = "select f_godown_id from Godown_db where f_godown_name='" + txtGodownName.Text + "'";
                string godownId = con.FetchData(query);
                var selectedRow = dgwItems.SelectedRows[0];
                selectedRow.Cells[0].Value = txtProductId.Text;
                selectedRow.Cells[1].Value = txtProductName.Text;
                selectedRow.Cells[2].Value = categoryId;
                selectedRow.Cells[3].Value = txtProductCategoryName.Text;
                selectedRow.Cells[4].Value = godownId;
                selectedRow.Cells[5].Value = txtGodownName.Text;
                selectedRow.Cells[6].Value = txtBarcode.Text;
                selectedRow.Cells[7].Value = txtProductSizeId.Text;
                selectedRow.Cells[8].Value = cmbProductSizeNo.Text;
                selectedRow.Cells[9].Value = txtPrintingName.Text;
                selectedRow.Cells[10].Value = txtQuantity.Text;
                ClearProduct();
            }
            else
            {
                MessageBox.Show("Data Not Found? You Can't Edit", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        DataTable dt2 = new DataTable();
        bool isRemoveClicked = false;

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgwItems.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Do You Wanna Delete?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (grpProduct.Text == "Edit" || grpProduct.Text == "Update")
                {
                    foreach (DataGridViewRow row in dgwItems.SelectedRows)
                    {
                        dt2.Rows.Add(row.Cells[10].Value.ToString(), row.Cells[6].Value.ToString());
                    }
                    isRemoveClicked = true;
                }
                dgwItems.Rows.RemoveAt(dgwItems.SelectedRows[0].Index);
                ClearProduct();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            query = "select f_product_category_id from Product_Category_db where f_product_category_name='" + txtProductCategoryName.Text + "'";
            string categoryId = con.FetchData(query);
            query = "select f_godown_id from Godown_db where f_godown_name='" + txtGodownName.Text + "'";
            string godownId = con.FetchData(query);
            Control[] textBoxes = { txtProductId, txtProductName, cmbProductSizeNo, txtBarcode, txtQuantity };
            if (comp.validateControls(textBoxes))
            {
                bool itemExists = false;
                foreach (DataGridViewRow row in dgwItems.Rows)
                {
                    if (row.Cells[0].Value.ToString() == txtProductId.Text && row.Cells[8].Value.ToString() == cmbProductSizeNo.Text)
                    {
                        double lastQty = double.Parse(row.Cells[10].Value.ToString());
                        double newQty = lastQty + double.Parse(txtQuantity.Text);
                        row.Cells[10].Value = newQty.ToString();
                        itemExists = true;
                        ClearProduct();
                        break;
                    }
                }
                if (MessageBox.Show("Do You Wanna Add One More Details?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!itemExists)
                    {
                        dt.Rows.Add(txtProductId.Text, txtProductName.Text, categoryId, txtProductCategoryName.Text, godownId, txtGodownName.Text, txtBarcode.Text, txtProductSizeId.Text, cmbProductSizeNo.Text, txtPrintingName.Text, txtQuantity.Text);
                        ClearProduct();
                    }

                    txtProductName.Focus();
                }
                else
                {
                    if (!itemExists)
                    {
                        dt.Rows.Add(txtProductId.Text, txtProductName.Text, categoryId, txtProductCategoryName.Text, godownId, txtGodownName.Text, txtBarcode.Text, txtProductSizeId.Text, cmbProductSizeNo.Text, txtPrintingName.Text, txtQuantity.Text);
                        ClearProduct();
                    }
                    btnSave.Focus();
                }
            }
        }

        private void dgwProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (barcode)
                {
                    if (comp.getDgwToTextBox(dgwProduct, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName, txtBarcode, txtProductSizeId, cmbProductSizeNo, txtPrintingName }))
                    {
                        txtQuantity.Focus();
                        displayProductData();
                        dgwProduct.Hide();
                    }
                    else
                    {
                        ClearProduct();
                    }
                }
                else
                {
                    if (comp.getDgwToTextBox(dgwProduct, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName }))
                    {
                        cmbProductSizeNo.Focus();
                        displayProductData();
                        getItems();
                        dgwProduct.Hide();
                    }
                    else
                    {
                        ClearProduct();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ClearProduct();
            }
        }

        private void dgwProduct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (barcode)
            {
                if (comp.getDgwToTextBox(dgwProduct, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName, txtBarcode, txtProductSizeId, cmbProductSizeNo, txtPrintingName }))
                {
                    txtQuantity.Focus();
                    dgwProduct.Hide();
                    displayProductData();
                    barcode = false;
                }
                else
                {
                    ClearProduct();
                }
            }
            else
            {
                if (comp.getDgwToTextBox(dgwProduct, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName }))
                {
                    cmbProductSizeNo.Focus();
                    getItems();
                    displayProductData();
                    dgwProduct.Hide();
                }
                else
                {
                    ClearProduct();
                }
            }
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.Show();
                (dgwProduct.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_product_name LIKE '%{0}%'", txtProductName.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwProduct.Visible && !string.IsNullOrEmpty(txtProductName.Text))
                {
                    dgwProduct.Focus();
                }
                else
                {
                    dgwProduct.Hide();
                    SendKeys.Send("{TAB}");
                }
            }
        }

        private void cmbFromGodown_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayProductData();
        }

        private void Enter_Key_Press(object sender, KeyEventArgs e)
        {
            comp.Enter(sender, e);
        }

        private TextBox dummyTextBox = new TextBox();
        private void dgwItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getItemsDgwToTextBox(dgwItems, new Control[] { txtProductId, txtProductName, dummyTextBox, txtProductCategoryName, dummyTextBox, txtGodownName, txtBarcode, txtProductSizeId, cmbProductSizeNo, txtPrintingName, txtQuantity }))
            {
                txtProductName.Focus();
                btnAdd.Enabled = false;
                dgwProduct.Hide();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            comp.Close(this);
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }

        private void FRM_STOCK_TRANSFER_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
            comp.ShortcutKey(Keys.F1, btnNew, e);
            comp.ShortcutKey(Keys.F2, btnNew, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void SaveItems()
        {
            string query;
            string godownID;
            bool isFirstOccurrence = true;
            TextBox ProductID = new TextBox();
            string previousProductID = null;

            if (dgwItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgwItems.Rows.Count; i++)
                {
                    string productID = dgwItems.Rows[i].Cells[0].Value.ToString();

                    query = "insert into Stock_Transfer_Items_db (f_stock_transfer_id,f_stock_transfer_date,f_product_id,f_product_name,f_product_category_id,f_product_category_name,f_godown_id,f_godown_name,f_product_size_id,f_product_size_no,f_barcode,f_printing_name,f_quantity) Values ('" + txtStockTransferId.Text + "', '" + txtStockTransferDate.Text + "', '" + dgwItems.Rows[i].Cells[0].Value + "', '" + dgwItems.Rows[i].Cells[1].Value + "', '" + dgwItems.Rows[i].Cells[2].Value + "', '" + dgwItems.Rows[i].Cells[3].Value + "', '" + dgwItems.Rows[i].Cells[4].Value + "', '" + dgwItems.Rows[i].Cells[5].Value + "', '" + dgwItems.Rows[i].Cells[7].Value + "', '" + dgwItems.Rows[i].Cells[8].Value + "', '" + dgwItems.Rows[i].Cells[6].Value + "', '" + dgwItems.Rows[i].Cells[9] + "', '" + dgwItems.Rows[i].Cells[10].Value + "')";
                    con.SaveOrEditItems(query);

                    query = "Select f_godown_id from Godown_db where f_godown_name='" + cmbToGodown.SelectedItem?.ToString() + "'";
                    godownID = con.FetchData(query);

                    query = "Select f_quantity from Product_Items_db where f_product_id='" + dgwItems.Rows[i].Cells[0].Value + "' and f_barcode='" + dgwItems.Rows[i].Cells[6].Value + "'";
                    con.SaveOrEditItems(query);

                    int CurrentQuantity = Convert.ToInt32(con.FetchData(query));

                    int StockTransferQuantity = Convert.ToInt32(dgwItems.Rows[i].Cells[10].Value);

                    int NewQuantity = CurrentQuantity - StockTransferQuantity;

                    query = "Update Product_Items_db set f_quantity='" + NewQuantity + "' where f_barcode='" + dgwItems.Rows[i].Cells[6].Value + "' and f_product_id='" + dgwItems.Rows[i].Cells[0].Value + "'";
                    con.SaveOrEditItems(query);

                    query = "Select f_product_master from Master_Prefix_db";
                    con.GetId(query, ProductID);

                    query = "Select f_product_id from Product_db Order By ID Desc";
                    con.AutoNumber(query, ProductID);

                    if (isFirstOccurrence || productID != previousProductID)
                    {
                        dt2.Rows.Add(ProductID.Text, dgwItems.Rows[i].Cells[1].Value.ToString(), dgwItems.Rows[i].Cells[2].Value.ToString(), dgwItems.Rows[i].Cells[3].Value.ToString(), godownID, cmbToGodown.SelectedItem?.ToString());
                        previousProductID = productID;
                        isFirstOccurrence = false;
                    }

                    query = "insert into Product_Items_db (f_product_id,f_product_name,f_product_size_id,f_product_size_no,f_barcode,f_printing_name,f_quantity) Values ('" + ProductID.Text + "','" + dgwItems.Rows[i].Cells[1].Value + "','" + dgwItems.Rows[i].Cells[7].Value + "','" + dgwItems.Rows[i].Cells[8].Value + "','" + dgwItems.Rows[i].Cells[6].Value + "','" + dgwItems.Rows[i].Cells[9].Value + "','" + StockTransferQuantity + "')";
                    con.SaveOrEditItems(query);

                    if (dgwItems.Rows.Count - 1 == i)
                    {
                        MessageBox.Show("Data Creation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                foreach (DataRow dr in dt2.Rows)
                {
                    query = "insert into Product_db (f_product_id,f_product_name,f_product_category_id,f_product_category_name,f_godown_id,f_godown_name) Values ('" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + dr[4] + "','" + dr[5] + "')";
                    con.SaveOrEditItems(query);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Control[] textBoxes = { cmbFromGodown, cmbToGodown };

                if (comp.validateControls(textBoxes))
                {
                    if (dgwItems.Rows.Count > 0)
                    {
                        if (MessageBox.Show("Do You Wanna Save Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            validate = "Select * from Stock_Transfer_db where f_stock_transfer_id='" + txtStockTransferId.Text + "'";
                            query = "Insert into Stock_Transfer_db (f_stock_transfer_id,f_stock_transfer_date,f_from_godown,f_to_godown) values ('" + txtStockTransferId.Text + "','" + txtStockTransferDate.Text + "','" + cmbFromGodown.SelectedItem.ToString() + "','" + cmbToGodown.SelectedItem.ToString() + "')";
                            con.SaveOrEditItems(query);
                            SaveItems();
                            AutoNumber();
                            ClearAll();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Add The Stock Transfering Products❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ClearProduct();
                    }
                }
                else
                {
                    btnSave.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Stock Transfering?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            query = "Select Product_db.f_product_id,Product_db.f_product_name,Product_db.f_product_category_name,Product_db.f_godown_name,Product_Items_db.f_barcode,Product_Items_db.f_product_size_id, Product_Items_db.f_product_size_no,Product_Items_db.f_printing_name FROM Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id Where Product_Items_db.f_barcode='" + txtBarcode.Text + "'";
            getDetails(txtBarcode, query);
        }

        private bool barcode = false;

        private void getDetails(TextBox txtBox, string queryDetails)
        {
            try
            {
                if (txtBox.Text != string.Empty)
                {
                    DataSet d2s = new DataSet();
                    OleDbDataAdapter ad = new OleDbDataAdapter(queryDetails, Main);
                    ad.Fill(d2s);
                    if (d2s.Tables[0].Rows.Count == 1)
                    {
                        DataRow row = d2s.Tables[0].Rows[0];
                        txtProductId.Text = row[0].ToString();
                        txtProductName.Text = row[1].ToString();
                        txtProductCategoryName.Text = row[2].ToString();
                        txtGodownName.Text = row[3].ToString();
                        txtBarcode.Text = row[4].ToString();
                        txtProductSizeId.Text = row[5].ToString();
                        cmbProductSizeNo.Text = row[6].ToString();
                        txtPrintingName.Text = row[7].ToString();
                        txtQuantity.Focus();
                        dgwProduct.Hide();
                    }
                    else if (d2s.Tables[0].Rows.Count > 1)
                    {
                        dgwProduct.Show();
                        query = "Select Product_db.f_product_id,Product_db.f_product_name,Product_db.f_product_category_name,Product_db.f_godown_name,Product_Items_db.f_barcode,Product_Items_db.f_product_size_id, Product_Items_db.f_product_size_no,Product_Items_db.f_printing_name FROM Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id Where Product_Items_db.f_barcode='" + txtBarcode.Text + "'";
                        string[] headerText = { "Product ID", "Product Name", "Category Name", "Godown Name", "Barcode", "Product Size ID", "Product Size No", "Printing Name" };
                        con.GetData(query, dgwProduct, headerText);
                        dgwProduct.Focus();
                        barcode = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Data Not Found?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbProductSizeNo_Leave(object sender, EventArgs e)
        {
            try
            {
                string query = "Select f_product_size_id,f_barcode,f_printing_name,f_quantity from Product_Items_db where f_product_size_no='" + cmbProductSizeNo.Text + "' and f_product_id='" + txtProductId.Text + "'";
                TextBox[] txtBoxes = { txtProductSizeId, txtBarcode, txtPrintingName };
                con.FetchData(txtBoxes, query);
            }
            catch
            {

            }
        }
    }
}
