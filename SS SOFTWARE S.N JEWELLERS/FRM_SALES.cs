using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_SALES : Form
    {

        Components comp = new Components();
        Connection con = new Connection();
        DataTable dt = new DataTable();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        string query;
        string validate;
        int i = 0;

        public FRM_SALES()
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
            dt2.Columns.Add("Barcode", typeof(string));
            dt2.Columns.Add("Quantity", typeof(int));
            dgwItems.DataSource = dt;
        }

        private void FRM_SALES_Load(object sender, EventArgs e)
        {
            AutoNumber();
            displaySupplierData();
            displayProductData();
        }

        private void ClearAll()
        {
            comp.Clear(new Control[] { txtCustomerName, txtInvoiceDate, txtCustomerId, txtArea, txtMobileNo, txtProductId, txtProductName, txtPrintingName, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtProductCategoryName, txtQuantity });
            grpProduct.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwCustomer.Hide();
            dgwProduct.Hide();
            dgwDetails.Hide();
            dgwItems.Show();
            dt.Clear();
            isRemoveClicked = false;
            btnAdd.Enabled = true;
            dt2.Clear();
            txtQuantity.Text = "1";
            lblTotalQuantity.Text = "0";
            lblTotalProducts.Text = "0";
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
            string query = "Select f_sales from Transaction_Prefix_db";
            con.GetId(query, txtInvoiceNo);

            string str = "Select f_invoice_no from Sales_db Order By ID Desc";
            con.AutoNumber(str, txtInvoiceNo);
        }

        private void displaySupplierData()
        {
            query = "Select f_customer_id,f_customer_name,f_area,f_mobile_no from Customer_db Order By ID Desc";
            string[] headerText = { "Customer ID", "Customer Name", "Area", "Mobile No" };
            con.GetData(query, dgwCustomer, headerText);
        }

        private void displayProductData()
        {
            query = "Select f_product_id, f_product_name, f_product_category_name,f_godown_name FROM Product_db";
            string[] headerText = { "Product ID", "Product Name", "Category Name", "Godown Name" };
            con.GetData(query, dgwProduct, headerText);
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwCustomer.Show();
                (dgwCustomer.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_customer_name LIKE '%{0}%'", txtCustomerName.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearCustomer()
        {
            comp.Clear(new Control[] { txtCustomerName, txtCustomerId, txtArea, txtMobileNo });
            dgwCustomer.Hide();
            isRemoveClicked = false;
            dt2.Clear();
        }

        private void ClearProduct()
        {
            comp.Clear(new Control[] { txtBarcode, txtProductName, txtProductId, txtProductCategoryName, txtGodownName, cmbProductSizeNo, txtPrintingName, txtQuantity });
            displayProductData();
            txtQuantity.Text = "1";
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

        private void dgwCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comp.getDgwToTextBox(dgwCustomer, new Control[] { txtCustomerId, txtCustomerName, txtArea, txtMobileNo }))
                {
                    txtBarcode.Focus();
                    dgwCustomer.Hide();
                }
                else
                {
                    ClearCustomer();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ClearCustomer();
            }
        }

        private void dgwCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getDgwToTextBox(dgwCustomer, new Control[] { txtCustomerId, txtCustomerName, txtArea, txtMobileNo }))
            {
                txtBarcode.Focus();
                dgwCustomer.Hide();
            }
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwCustomer.Visible && !string.IsNullOrEmpty(txtCustomerName.Text))
                {
                    dgwCustomer.Focus();
                }
                else
                {
                    txtBarcode.Focus();
                    dgwCustomer.Hide();
                }
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
                if (txtCustomerName.Text != string.Empty)
                {
                    dgwProduct.Show();
                    (dgwProduct.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_product_name LIKE '%{0}%'", txtProductName.Text);
                }
                else
                {
                    txtCustomerName.Focus();
                }
            }
            catch (Exception)
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

        private void Enter_Key_Press(object sender, KeyEventArgs e)
        {
            comp.Enter(sender, e);
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
                        dt2.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[7].Value.ToString());
                    }
                    isRemoveClicked = true;
                }
                dgwItems.Rows.RemoveAt(dgwItems.SelectedRows[0].Index);
                ClearProduct();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
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
                            btnSave.Focus();
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
            catch
            {
                btnSave.Focus();
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
            query = "Select ID,f_invoice_no,f_invoice_date,f_customer_id,f_customer_name,f_area,f_mobile_no from Sales_db Order By ID Desc";
            string[] headerText = { "ID", "Invoice ID", "Invoice Name", "Customer ID", "Customer Name", "Area", "Mobile No" };
            con.GetData(query, dgwDetails, headerText);
            dgwItems.Hide();
            dgwDetails.Show();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            grpProduct.Text = "View";
            if (grpProduct.Text == "View")
            {
                ClearAll();
                grpProduct.Text = "View";
                displayData();
                AutoNumber();
            }
            else
            {
                ClearAll();
            }
        }

        private void SaveItems()
        {
            foreach (DataRow row in dt.Rows)
            {
                query = "insert into Sales_Items_db (f_invoice_no,f_invoice_date,f_product_id,f_product_name,f_product_category_name,f_godown_name,f_barcode,f_product_size_no,f_printing_name,f_quantity) Values ('" + txtInvoiceNo.Text + "', '" + txtInvoiceDate.Text + "', '" + row[0] + "', '" + row[1] + "', '" + row[2] + "', '" + row[3] + "', '" + row[4] + "', '" + row[5] + "', '" + row[6] + "', '" + row[7] + "')";
                con.SaveOrEditItems(query);

                using (OleDbConnection con = new OleDbConnection(Main))
                {
                    con.Open();
                    query = "select f_quantity from Product_Items_db where f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    double lastQty = Convert.ToDouble(cmd.ExecuteScalar());

                    double newQty = Convert.ToDouble(row[7]);
                    double totalQty = lastQty - newQty;

                    query = "Update Product_Items_db set f_quantity='" + totalQty + "' where f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                    OleDbCommand updateCmd = new OleDbCommand(query, con);
                    updateCmd.ExecuteNonQuery();
                }
            }
        }

        int productQty = 0, salesLastQty = 0, salesNewQty = 0, totalNewQty = 0;

        private void UpdateItems()
        {
            using (OleDbConnection connection = new OleDbConnection(Main))
            {
                connection.Open();
                if (isRemoveClicked == false)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string query = "select f_quantity from Product_Items_db where f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand cmd = new OleDbCommand(query, connection);
                        productQty = Convert.ToInt32(cmd.ExecuteScalar());

                        query = "select f_quantity from Sales_Items_db where f_invoice_no='" + txtInvoiceNo.Text + "' and f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand cmd1 = new OleDbCommand(query, connection);
                        salesLastQty = Convert.ToInt32(cmd1.ExecuteScalar());

                        salesNewQty = Convert.ToInt32(row[7]);

                        int newQty = productQty + salesLastQty;
                        totalNewQty = newQty - salesNewQty;

                        query = "Update Product_Items_db set f_quantity='" + totalNewQty + "' where f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand updateCmd = new OleDbCommand(query, connection);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    foreach (DataRow row in dt2.Rows)
                    {
                        string query = "select f_quantity from Product_Items_db where f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand cmd = new OleDbCommand(query, connection);
                        productQty = Convert.ToInt32(cmd.ExecuteScalar());

                        query = "select f_quantity from Sales_Items_db where f_invoice_no='" + txtInvoiceNo.Text + "' and f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand cmd1 = new OleDbCommand(query, connection);
                        salesLastQty = Convert.ToInt32(cmd1.ExecuteScalar());

                        salesNewQty = Convert.ToInt32(row[0]);

                        int newQty = productQty + salesLastQty;

                        query = "Update Product_Items_db set f_quantity='" + newQty + "' where f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand updateCmd = new OleDbCommand(query, connection);
                        updateCmd.ExecuteNonQuery();
                    }
                }

                query = "delete from Sales_Items_db where f_invoice_no = '" + txtInvoiceNo.Text + "'";
                OleDbCommand deleteCmd = new OleDbCommand(query, connection);
                deleteCmd.ExecuteNonQuery();

                foreach (DataRow row in dt.Rows)
                {
                    query = "insert into Sales_Items_db (f_invoice_no,f_invoice_date,f_product_id,f_product_name,f_product_category_name,f_godown_name,f_barcode,f_product_size_no,f_printing_name,f_quantity) Values ('" + txtInvoiceNo.Text + "', '" + txtInvoiceDate.Text + "', '" + row[0] + "', '" + row[1] + "', '" + row[2] + "', '" + row[3] + "', '" + row[4] + "', '" + row[5] + "', '" + row[6] + "', '" + row[7] + "')";
                    OleDbCommand insertCmd = new OleDbCommand(query, connection);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtInvoiceNo, txtInvoiceDate, txtCustomerName };

            if (comp.validateControls(textBoxes))
            {
                if (dgwItems.Rows.Count > 0)
                {
                    if (btnSave.Text == "F2 Save" && grpProduct.Text == "Create")
                    {
                        validate = "Select * from Sales_db where f_invoice_no='" + txtInvoiceNo.Text + "'";
                        query = "Insert into Sales_db (f_invoice_no,f_invoice_date,f_customer_id,f_customer_name,f_area,f_mobile_no) values ('" + txtInvoiceNo.Text + "','" + txtInvoiceDate.Text + "','" + txtCustomerId.Text + "','" + txtCustomerName.Text + "','" + txtArea.Text + "','" + txtMobileNo.Text + "')";
                        if (con.SaveData(query, validate))
                        {
                            SaveItems();
                        }
                        ClearAll();
                        AutoNumber();
                    }
                    else if (btnSave.Text == "F2 Update" && grpProduct.Text == "Update")
                    {
                        validate = "Select * from Sales_db where f_invoice_no='" + txtInvoiceNo.Text + "'";
                        query = "Update Sales_db set f_invoice_no ='" + txtInvoiceNo.Text + "',f_invoice_date='" + txtInvoiceDate.Text + "' ,f_customer_id='" + txtCustomerId.Text + "',f_customer_name='" + txtCustomerName.Text + "',f_area ='" + txtArea.Text + "',f_mobile_no='" + txtMobileNo.Text + "' where ID=" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                        if (con.EditData(query, validate))
                        {
                            UpdateItems();
                        }
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
                    MessageBox.Show("Please Add The Selling Products❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ClearProduct();
                }
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grpProduct.Text == "View" || grpProduct.Text == "View")
            {
                query = "Delete From Sales_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                if (con.DeleteData(query, dgwDetails))
                {
                    using (OleDbConnection connection = new OleDbConnection(Main))
                    {
                        connection.Open();
                        foreach (DataGridViewRow row in dgwItems.Rows)
                        {
                            dt2.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[7].Value.ToString());
                        }
                        foreach (DataRow row in dt2.Rows)
                        {
                            string query = "select f_quantity from Product_Items_db where f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
                            OleDbCommand cmd = new OleDbCommand(query, connection);
                            productQty = Convert.ToInt32(cmd.ExecuteScalar());

                            query = "select f_quantity from Sales_Items_db where f_invoice_no='" + txtInvoiceNo.Text + "' and f_barcode='" + row[1] + "'";
                            OleDbCommand cmd1 = new OleDbCommand(query, connection);
                            salesLastQty = Convert.ToInt32(cmd1.ExecuteScalar());

                            salesNewQty = Convert.ToInt32(row[2]);

                            int newQty = productQty + salesLastQty;

                            query = "Update Product_Items_db set f_quantity='" + newQty + "' where f_barcode='" + row[1] + "'";
                            OleDbCommand updateCmd = new OleDbCommand(query, connection);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    query = "delete from Sales_Items_db where f_invoice_no = '" + dgwDetails.SelectedRows[i].Cells[1].Value.ToString() + "'";
                    con.SaveOrEditItems(query);
                }
                ClearAll();
                displayData();
                AutoNumber();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void displayItems()
        {
            query = "Select f_product_id,f_product_name,f_product_category_name,f_godown_name,f_barcode,f_product_size_no,f_printing_name,f_quantity FROM Sales_Items_db WHERE f_invoice_no = '" + txtInvoiceNo.Text + "'";
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
            if (!comp.getDataDgwToTextBox(dgwDetails, new Control[] { txtInvoiceNo, txtInvoiceDate, txtCustomerId, txtCustomerName, txtArea, txtMobileNo })) return;

            if (grpProduct.Text == "Edit")
            {
                btnSave.Text = "F2 Update";
                grpProduct.Text = "Update";
            }
            displayItems();
            txtCustomerName.Focus();
            dgwDetails.Hide();
            dgwCustomer.Hide();
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

        private void FRM_SALES_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
            comp.ShortcutKey(Keys.F1, btnNew, e);
            comp.ShortcutKey(Keys.F2, btnSave, e);
            comp.ShortcutKey(Keys.F3, btnEdit, e);
            comp.ShortcutKey(Keys.F4, btnDelete, e);
            comp.ShortcutKey(Keys.F5, btnView, e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            comp.Close(this);
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }
    }
}
