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
        int i = 0;
        DataTable dt2 = new DataTable();
        bool remove = false;

        public FRM_STOCK_TRANSFER()
        {
            InitializeComponent();
            dt.Columns.Add("Product ID", typeof(string));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Product Category Name", typeof(string));
            dt.Columns.Add("Product Godown Name", typeof(string));
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("Product Size No", typeof(string));
            dt.Columns.Add("Printing Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt2.Columns.Add("Product ID", typeof(string));
            dt2.Columns.Add("Product Name", typeof(string));
            dt2.Columns.Add("Product Category Name", typeof(string));
            dt2.Columns.Add("Product Godown Name", typeof(string));
            dgwItems.DataSource = dt;
        }

        private void FRM_STOCK_TRANSFER_Load(object sender, EventArgs e)
        {
            AutoNumber();
            displayGodown();
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
                if (dgwItems.Rows[i].Cells[7].Value != null)
                { totalQuantity += double.Parse(dgwItems.Rows[i].Cells[7].Value.ToString()); }

                lblTotalProducts.Text = dgwItems.RowCount.ToString();
                lblTotalQuantity.Text = Math.Round(totalQuantity, 2).ToString();
            }
        }

        private void AutoNumber()
        {
            string query = "Select f_stock_transfer from Transaction_Prefix_db";
            con.GetId(query, txtInvoiceNo);

            string str = "Select f_invoice_no from Stock_Transfer_db Order By ID Desc";
            con.AutoNumber(str, txtInvoiceNo);
        }

        private void displayGodown()
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

        private void getItems()
        {
            cmbProductSizeNo.Items.Clear();
            query = "Select f_product_size_no from Product_Items_db Where f_product_id='" + txtProductId.Text + "' and f_product_name='" + txtProductName.Text + "'";
            con.GetItems(query, cmbProductSizeNo);
        }

        private void ClearAll()
        {
            comp.Clear(new Control[] { cmbFromGodown, cmbToGodown, txtProductId, txtProductName, txtPrintingName, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtProductCategoryName, txtQuantity });
            grpProduct.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwProduct.Hide();
            dgwItems.Show();
            dt.Clear();
            btnAdd.Enabled = true;
            dt2.Clear();
            lblTotalQuantity.Text = "0";
            lblTotalProducts.Text = "0";
        }

        private void ClearProduct()
        {
            comp.Clear(new Control[] { txtProductName, txtProductId, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtPrintingName, txtQuantity });
            cmbProductSizeNo.Items.Clear();
            dgwProduct.Hide();
            btnAdd.Enabled = true;
            CalculateTotal();
            barcode = false;
        }

        private void ClearProduct2()
        {
            cmbProductSizeNo.Text = "";
            txtQuantity.Text = "";
            dgwProduct.Hide();
            btnAdd.Enabled = true;
            CalculateTotal();
            barcode = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearProduct();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgwItems.SelectedRows.Count > 0)
            {
                var selectedRow = dgwItems.SelectedRows[0];
                selectedRow.Cells[0].Value = txtProductId.Text;
                selectedRow.Cells[1].Value = txtProductName.Text;
                selectedRow.Cells[2].Value = txtProductCategoryName.Text;
                selectedRow.Cells[3].Value = txtGodownName.Text;
                selectedRow.Cells[4].Value = txtBarcode.Text;
                selectedRow.Cells[5].Value = cmbProductSizeNo.Text;
                selectedRow.Cells[6].Value = txtPrintingName.Text;
                selectedRow.Cells[7].Value = txtQuantity.Text;
                ClearProduct();
            }
            else
            {
                MessageBox.Show("Data Not Found? You Can't Edit", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgwItems.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Do You Wanna Delete?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (grpProduct.Text == "Edit" || grpProduct.Text == "Update")
                {
                    foreach (DataGridViewRow row in dgwItems.SelectedRows)
                    {
                        dt2.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[7].Value.ToString());
                    }
                    remove = true;
                }
                dgwItems.Rows.RemoveAt(dgwItems.SelectedRows[0].Index);
                ClearProduct();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtPrintingName.Text == string.Empty)
            {
                txtPrintingName.Text = txtProductName.Text;
            }
            query = "select f_quantity from Product_Items_db where f_product_id='" + txtProductId.Text + "' and f_product_size_no='" + cmbProductSizeNo.Text + "'";
            int qty = Convert.ToInt32(con.FetchData(query));
            if (qty > 1)
            {
                Control[] textBoxes = { txtProductId, txtProductName, cmbProductSizeNo, txtBarcode, txtQuantity };
                if (comp.validateControls(textBoxes))
                {
                    bool itemExists = false;
                    foreach (DataGridViewRow row in dgwItems.Rows)
                    {
                        if (row.Cells[0].Value.ToString() == txtProductId.Text && row.Cells[4].Value.ToString() == cmbProductSizeNo.Text)
                        {
                            double lastQty = double.Parse(row.Cells[7].Value.ToString());
                            double newQty = lastQty + double.Parse(txtQuantity.Text);
                            row.Cells[7].Value = newQty.ToString();
                            itemExists = true;
                            ClearProduct();
                            break;
                        }
                    }
                    if (MessageBox.Show("Do You Wanna Add One More Details?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (!itemExists)
                        {
                            dt.Rows.Add(txtProductId.Text, txtProductName.Text, txtProductCategoryName.Text, txtGodownName.Text, txtBarcode.Text, cmbProductSizeNo.Text, txtPrintingName.Text, txtQuantity.Text);
                            ClearProduct();
                        }

                        txtBarcode.Focus();
                    }
                    else
                    {
                        if (!itemExists)
                        {
                            dt.Rows.Add(txtProductId.Text, txtProductName.Text, txtProductCategoryName.Text, txtGodownName.Text, txtBarcode.Text, cmbProductSizeNo.Text, txtPrintingName.Text, txtQuantity.Text);
                            ClearProduct();
                        }
                        txtBarcode.Focus();
                    }
                }
                else
                {
                    btnSave.Focus();
                }
            }
            else
            {
                MessageBox.Show("Low Stock❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgwProduct.Hide();
                ClearProduct();
            }
        }

        private void dgwProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (barcode)
                {
                    query = "select f_quantity from Product_Items_db where f_barcode='" + dgwProduct.SelectedRows[i].Cells[4].Value + "' And f_product_id ='" + dgwProduct.SelectedRows[i].Cells[0].Value + "' And f_product_size_no='" + dgwProduct.SelectedRows[i].Cells[5].Value + "'";
                    int qty = Convert.ToInt32(con.FetchData(query));
                    if (qty > 1)
                    {
                        bool itemExists = false;
                        foreach (DataGridViewRow row in dgwItems.Rows)
                        {
                            if (row.Cells[0].Value.ToString() == dgwProduct.SelectedRows[i].Cells[0].Value.ToString() && row.Cells[5].Value.ToString() == dgwProduct.SelectedRows[i].Cells[5].Value.ToString())
                            {
                                double lastQty = double.Parse(row.Cells[7].Value.ToString());
                                double newQty = lastQty + double.Parse(txtQuantity.Text);
                                row.Cells[7].Value = newQty.ToString();
                                itemExists = true;
                                ClearProduct();
                                break;
                            }
                            else
                            {
                                itemExists = false;
                            }
                        }

                        if (itemExists == false)
                        {
                            dt.Rows.Add(dgwProduct.SelectedRows[i].Cells[0].Value.ToString(), dgwProduct.SelectedRows[i].Cells[1].Value.ToString(), dgwProduct.SelectedRows[i].Cells[2].Value.ToString(), dgwProduct.SelectedRows[i].Cells[3].Value.ToString(), dgwProduct.SelectedRows[i].Cells[4].Value.ToString(), dgwProduct.SelectedRows[i].Cells[5].Value.ToString(), dgwProduct.SelectedRows[i].Cells[6].Value.ToString(), 1);
                            ClearProduct();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Low Stock❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgwProduct.Hide();
                        ClearProduct();
                    }
                }
                else
                {
                    if (comp.getDgwToTextBox(dgwProduct, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName }))
                    {
                        cmbProductSizeNo.Focus();
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
                dt.Rows.Add(dgwProduct.SelectedRows[i].Cells[0].Value.ToString(), dgwProduct.SelectedRows[i].Cells[1].Value.ToString(), dgwProduct.SelectedRows[i].Cells[2].Value.ToString(), dgwProduct.SelectedRows[i].Cells[3].Value.ToString(), dgwProduct.SelectedRows[i].Cells[4].Value.ToString(), dgwProduct.SelectedRows[i].Cells[5].Value.ToString(), dgwProduct.SelectedRows[i].Cells[6].Value.ToString(), dgwProduct.SelectedRows[i].Cells[7].Value.ToString(), 1);
                ClearProduct();
            }
            else
            {
                if (comp.getDgwToTextBox(dgwProduct, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName }))
                {
                    cmbProductSizeNo.Focus();
                    getItems();
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

        private void dgwItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getItemsDgwToTextBox(dgwItems, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtPrintingName, txtQuantity }))
            {
                txtBarcode.Focus();
                dgwProduct.Hide();
                btnAdd.Enabled = false;
            }
        }

        private void displayData()
        {
            query = "Select ID,f_invoice_no,f_invoice_date,f_from_godown,f_to_godown from Stock_Transfer_db Order By ID Desc";
            string[] headerText = { "ID", "Invoice No", "Invoice Date", "From Godown", "To Godown" };
            con.GetData(query, dgwDetails, headerText);
            dgwItems.Hide();
            dgwDetails.Show();
        }

        private void displayItems()
        {
            query = "Select f_product_id,f_product_name,f_product_category_name,f_godown_name,f_barcode,f_product_size_no,f_printing_name,f_quantity FROM Stock_Transfer_Items_db Where f_invoice_no = '" + txtInvoiceNo.Text + "'";
            DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter(query, Main);
            ad.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dt.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);
            }
        }

        private void dgwDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!comp.getDataDgwToTextBox(dgwDetails, new Control[] { txtInvoiceNo, txtInvoiceDate, cmbFromGodown, cmbToGodown })) return;

            if (grpProduct.Text == "Edit")
            {
                btnSave.Text = "F2 Update";
                grpProduct.Text = "Update";
            }
            displayItems();
            cmbFromGodown.Focus();
            dgwDetails.Hide();
            dgwItems.Show();
        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            query = "Select Product_db.f_product_id,Product_db.f_product_name,Product_db.f_product_category_name,Product_db.f_godown_name,Product_Items_db.f_barcode,Product_Items_db.f_product_size_no,Product_Items_db.f_printing_name,Product_Items_db.f_quantity FROM Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id Where Product_Items_db.f_barcode='" + txtBarcode.Text + "'";
            getDetails(txtBarcode, query);
        }

        private bool barcode = false;

        private void getDetails(TextBox txtBox, string queryDetails)
        {
            try
            {
                if (txtBox.Text != string.Empty && txtProductName.Text == String.Empty)
                {
                    DataSet d2s = new DataSet();
                    OleDbDataAdapter ad = new OleDbDataAdapter(queryDetails, Main);
                    ad.Fill(d2s);
                    DataRow row = d2s.Tables[0].Rows[0];
                    bool itemExists = false;
                    if (Convert.ToInt32(row[7].ToString()) > 1)
                    {
                        foreach (DataGridViewRow row2 in dgwItems.Rows)
                        {
                            if (row2.Cells[0].Value.ToString() == row[0].ToString() && row2.Cells[5].Value.ToString() == row[5].ToString() && d2s.Tables[0].Rows.Count == 1)
                            {
                                double lastQty = double.Parse(row2.Cells[7].Value.ToString());
                                double newQty = lastQty + 1;
                                row2.Cells[7].Value = newQty.ToString();
                                itemExists = true;
                                ClearProduct();
                                break;
                            }
                            else
                            {
                                itemExists = false;
                            }

                        }
                        if (d2s.Tables[0].Rows.Count == 1 && itemExists == false)
                        {
                            dt.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), 1);
                            txtBarcode.Focus();
                            ClearProduct();
                            dgwProduct.Hide();
                        }
                        if (d2s.Tables[0].Rows.Count > 1 && itemExists == false)
                        {
                            dgwProduct.Show();
                            query = "Select Product_db.f_product_id,Product_db.f_product_name,Product_db.f_product_category_name,Product_db.f_godown_name,Product_Items_db.f_barcode,Product_Items_db.f_product_size_no,Product_Items_db.f_printing_name FROM Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id Where Product_Items_db.f_barcode='" + txtBarcode.Text + "'";
                            string[] headerText = { "Product ID", "Product Name", "Category Name", "Godown Name", "Barcode", "Product Size No", "Printing Name" };
                            con.GetData(query, dgwProduct, headerText);
                            dgwProduct.Focus();
                            barcode = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Low Stock❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgwProduct.Hide();
                        ClearProduct();
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
                string query = "Select f_barcode,f_printing_name,f_quantity from Product_Items_db where f_product_size_no='" + cmbProductSizeNo.Text + "' and f_product_id='" + txtProductId.Text + "'";
                TextBox[] txtBoxes = { txtBarcode, txtPrintingName };
                con.FetchData(txtBoxes, query);
            }
            catch
            {

            }
        }

        private void FRM_STOCK_TRANSFER_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (dgwProduct.Visible == true)
                {
                    dgwProduct.Visible = false;
                }
                else if (dgwProduct.Visible == false)
                {
                    comp.Close(this, e);
                }
            }
            comp.ShortcutKey(Keys.F1, btnNew, e);
            comp.ShortcutKey(Keys.F2, btnSave, e);
            comp.ShortcutKey(Keys.F3, btnDelete, e);
            comp.ShortcutKey(Keys.F4, btnView, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            comp.Close(this);
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }

        private void cmbFromGodown_Leave(object sender, EventArgs e)
        {
            displayProductData();
        }

        private void Enter_Key_Press(object sender, KeyEventArgs e)
        {
            comp.Enter(sender, e);
        }

        private void SaveItems()
        {
            string query;
            TextBox ProductID = new TextBox();

            foreach (DataRow row in dt.Rows)
            {
                query = "insert into Stock_Transfer_Items_db (f_invoice_no,f_invoice_date,f_product_id,f_product_name,f_product_category_name,f_godown_name,f_barcode,f_product_size_no,f_printing_name,f_quantity) Values ('" + txtInvoiceNo.Text + "', '" + txtInvoiceDate.Text + "', '" + row[0] + "', '" + row[1] + "', '" + row[2] + "', '" + row[3] + "', '" + row[4] + "', '" + row[5] + "', '" + row[6] + "', '" + row[7] + "')";
                con.SaveOrEditItems(query);

                query = "Select Count(*) from Product_db Inner Join Product_Items_db on Product_db.f_product_id = Product_Items_db.f_product_id Where Product_db.f_godown_name='" + cmbToGodown.Text + "' and Product_Items_db.f_barcode='" + row[4] + "'";
                int Count = Convert.ToInt32(con.FetchData(query));

                int StockTransferQuantity = Convert.ToInt32(row[7]);

                query = "Select f_quantity from Product_db Inner Join Product_Items_db on Product_db.f_product_id = Product_Items_db.f_product_id Where Product_db.f_godown_name='" + row[3] + "' and Product_Items_db.f_barcode='" + row[4] + "'";
                int CurrentFromGodownQuantity = Convert.ToInt32(con.FetchData(query));

                if (Count >= 1)
                {
                    query = "Select f_quantity from Product_db Inner Join Product_Items_db on Product_db.f_product_id = Product_Items_db.f_product_id Where Product_db.f_godown_name='" + cmbToGodown.Text + "' and Product_Items_db.f_barcode='" + row[4] + "'";
                    int CurrentToGodownQuantity = Convert.ToInt32(con.FetchData(query));

                    int NewToGodownQuantity = CurrentToGodownQuantity + StockTransferQuantity;

                    int NewFromGodownQuantity = CurrentFromGodownQuantity - StockTransferQuantity;

                    query = "Update Product_Items_db Inner Join Product_db on Product_Items_db.f_product_id = Product_db.f_product_id Set f_quantity= '" + NewFromGodownQuantity + "' Where Product_db.f_godown_name='" + row[3] + "' and Product_Items_db.f_barcode='" + row[4] + "'";
                    con.SaveOrEditItems(query);

                    query = "Update Product_Items_db Inner Join Product_db on Product_Items_db.f_product_id = Product_db.f_product_id Set f_quantity= '" + NewToGodownQuantity + "' Where Product_db.f_godown_name='" + cmbToGodown.Text + "' and Product_Items_db.f_barcode='" + row[4] + "'";
                    con.SaveOrEditItems(query);
                }
                else
                {
                    int NewFromGodownQuantity = CurrentFromGodownQuantity - StockTransferQuantity;

                    query = "Update Product_Items_db Inner Join Product_db on Product_Items_db.f_product_id = Product_db.f_product_id Set f_quantity= '" + NewFromGodownQuantity + "' Where Product_db.f_godown_name='" + row[3] + "' and Product_Items_db.f_barcode='" + row[4] + "'";
                    con.SaveOrEditItems(query);

                    query = "Select f_product_master from Master_Prefix_db";
                    con.GetId(query, ProductID);

                    query = "Select f_product_id from Product_db Order By ID Desc";
                    con.AutoNumber(query, ProductID);

                    query = "Select Count(*) from Product_db Where f_godown_name='" + cmbToGodown.Text + "' and f_product_name='" + row[1] + "'";
                    int Count2 = Convert.ToInt32(con.FetchData(query));

                    query = "Select f_product_id from Product_db Where f_godown_name='" + cmbToGodown.Text + "' and f_product_name='" + row[1] + "'";
                    string productid = con.FetchData(query);

                    if (Count2 == 0)
                    {
                        query = "insert into Product_Items_db (f_product_id,f_product_name,f_product_size_no,f_barcode,f_printing_name,f_quantity) Values ('" + ProductID.Text + "','" + row[1] + "','" + row[5] + "','" + row[4] + "','" + row[6] + "','" + StockTransferQuantity + "')";
                        con.SaveOrEditItems(query);

                        query = "insert into Product_db (f_product_id,f_product_name,f_product_category_name,f_godown_name) Values ('" + ProductID.Text + "','" + row[1] + "','" + row[2] + "','" + cmbToGodown.Text + "')";
                        con.SaveOrEditItems(query);
                    }
                    else
                    {
                        query = "insert into Product_Items_db (f_product_id,f_product_name,f_product_size_no,f_barcode,f_printing_name,f_quantity) Values ('" + productid + "','" + row[1] + "','" + row[5] + "','" + row[4] + "','" + row[6] + "','" + StockTransferQuantity + "')";
                        con.SaveOrEditItems(query);
                    }
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtInvoiceNo, txtInvoiceDate, cmbFromGodown, cmbToGodown };

            if (comp.validateControls(textBoxes))
            {
                validate = "Select * from Stock_Transfer_db where f_invoice_no ='" + txtInvoiceNo.Text + "'";
                query = "Insert into Stock_Transfer_db (f_invoice_no,f_invoice_date,f_from_godown,f_to_godown) values ('" + txtInvoiceNo.Text + "','" + txtInvoiceDate.Text + "','" + cmbFromGodown.Text + "','" + cmbToGodown.Text + "')";

                try
                {
                    if (MessageBox.Show("Do You Wanna Save Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        using (OleDbConnection con = new OleDbConnection(Main))
                        {
                            con.Open();
                            using (OleDbCommand cmd = new OleDbCommand(validate, con))
                            {
                                int Count = Convert.ToInt32(cmd.ExecuteScalar());
                                if (Count > 1)
                                {
                                    MessageBox.Show("Data Already Exist?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    if (MessageBox.Show("Do You Wanna Save Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        cmd.CommandText = query;
                                        cmd.ExecuteNonQuery();
                                        SaveItems();
                                        con.Close();
                                        MessageBox.Show("Data Creation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                    SaveItems();
                                    con.Close();
                                    MessageBox.Show("Data Creation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured While Data Creation?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClearAll();
                AutoNumber();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAll();
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grpProduct.Text == "View")
            {
                try
                {
                    query = "Delete From Stock_Transfer_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                    if (dgwDetails.Rows.Count > 0 && dgwDetails.SelectedRows.Count > 0)
                    {
                        if (MessageBox.Show("Do You Wanna Delete Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (OleDbConnection con1 = new OleDbConnection(Main))
                            {
                                con1.Open();
                                using (OleDbCommand cmd = new OleDbCommand(query, con1))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                con1.Close();
                                for (int i = 0; i < dgwItems.Rows.Count; i++)
                                {
                                    query = "select f_quantity from Product_Items_db where f_barcode='" + dgwItems.Rows[i].Cells[4].Value + "' and f_product_id='" + dgwItems.Rows[i].Cells[0].Value + "'";
                                    int ProductQty = Convert.ToInt32(con.FetchData(query));

                                    query = "Select f_product_id from Product_db Where f_godown_name='" + cmbToGodown.Text + "' and f_product_name='" + dgwItems.Rows[i].Cells[1].Value + "'";
                                    string productid = con.FetchData(query);

                                    query = "select f_quantity from Product_Items_db where f_barcode='" + dgwItems.Rows[i].Cells[4].Value + "' and f_product_id='" + productid + "'";
                                    int NewProductQty = Convert.ToInt32(con.FetchData(query));

                                    query = "select f_quantity from Stock_Transfer_Items_db where f_invoice_no='" + txtInvoiceNo.Text + "' and f_barcode='" + dgwItems.Rows[i].Cells[4].Value + "' and f_product_id='" + dgwItems.Rows[i].Cells[0].Value + "'";
                                    int TransferQty = Convert.ToInt32(con.FetchData(query));

                                    int newQty = ProductQty + TransferQty;

                                    int NewQty = NewProductQty - TransferQty;

                                    query = "Update Product_Items_db set f_quantity='" + newQty + "' where f_barcode='" + dgwItems.Rows[i].Cells[4].Value + "' and f_product_id='" + dgwItems.Rows[i].Cells[0].Value + "'";
                                    con.SaveOrEditItems(query);

                                    query = "Update Product_Items_db set f_quantity='" + NewQty + "' where f_barcode='" + dgwItems.Rows[i].Cells[4].Value + "' and f_product_id='" + productid + "'";
                                    con.SaveOrEditItems(query);
                                }
                                query = "delete from Stock_Transfer_Items_db where f_invoice_no = '" + dgwDetails.SelectedRows[i].Cells[1].Value.ToString() + "'";
                                con.SaveOrEditItems(query);
                                MessageBox.Show("Data Deletion Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Data For Deletion?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured While Data Deletion?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                ClearAll();
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
    }
}