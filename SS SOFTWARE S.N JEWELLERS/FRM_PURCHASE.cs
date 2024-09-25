using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_PURCHASE : Form
    {
        Components comp = new Components();
        Connection con = new Connection();
        DataTable dt = new DataTable();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        string query;
        string validate;
        int i = 0;
        static ReportDocument cr = new ReportDocument();
        FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, "Barcode Printing");

        public FRM_PURCHASE()
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
            dt2.Columns.Add("Printing Name", typeof(string));

            dt3.Columns.Add("Product ID", typeof(string));
            dt3.Columns.Add("Barcode", typeof(string));
            dt3.Columns.Add("Quantity", typeof(int));
            dt3.Columns.Add("Printing Name", typeof(string));
            dgwItems.DataSource = dt;
        }

        private void FRM_PURCHASE_Load(object sender, EventArgs e)
        {
            AutoNumber();
            displaySupplierData();
            displayProductData();
        }

        private void ClearAll()
        {
            comp.Clear(new Control[] { txtSupplierName, txtInvoiceDate, txtSupplierId, txtGstin, txtMobileNo, txtProductId, txtProductName, txtPrintingName, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtProductCategoryName, txtQuantity });
            grpProduct.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwSupplier.Hide();
            dgwProduct.Hide();
            dgwDetails.Hide();
            dgwItems.Show();
            dt.Clear();
            isRemoveClicked = false;
            btnAdd.Enabled = true;
            dt3.Clear();
            dt2.Clear();
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
            string query = "Select f_purchase from Transaction_Prefix_db";
            con.GetId(query, txtInvoiceNo);

            string str = "Select f_invoice_no from Purchase_db Order By ID Desc";
            con.AutoNumber(str, txtInvoiceNo);
        }

        private void displaySupplierData()
        {
            query = "Select f_supplier_id,f_supplier_name,f_gstin,f_mobile_no from Supplier_db Order By ID Desc";
            string[] headerText = { "Supplier ID", "Supplier Name", "Gstin", "Mobile No" };
            con.GetData(query, dgwSupplier, headerText);
        }

        private void displayProductData()
        {
            query = "Select f_product_id, f_product_name, f_product_category_name,f_godown_name FROM Product_db";
            string[] headerText = { "Product ID", "Product Name", "Category Name", "Godown Name" };
            con.GetData(query, dgwProduct, headerText);
        }

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwSupplier.Show();
                (dgwSupplier.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_supplier_name LIKE '%{0}%'", txtSupplierName.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSupplier()
        {
            comp.Clear(new Control[] { txtSupplierName, txtSupplierId, txtGstin, txtMobileNo });
            dgwSupplier.Hide();
            isRemoveClicked = false;
            dt2.Clear();
            dt3.Clear();
        }

        private void ClearProduct()
        {
            comp.Clear(new Control[] { txtProductName, txtProductId, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtPrintingName, txtQuantity });
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

        private void getItems()
        {
            cmbProductSizeNo.Items.Clear();
            query = "Select f_product_size_no from Product_Items_db Where f_product_id='" + txtProductId.Text + "' and f_product_name='" + txtProductName.Text + "'";
            con.GetItems(query, cmbProductSizeNo);
        }

        private void dgwSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comp.getDgwToTextBox(dgwSupplier, new Control[] { txtSupplierId, txtSupplierName, txtGstin, txtMobileNo }))
                {
                    txtProductName.Focus();
                    dgwSupplier.Hide();
                }
                else
                {
                    ClearSupplier();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ClearSupplier();
            }
        }

        private void dgwSupplier_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getDgwToTextBox(dgwSupplier, new Control[] { txtSupplierId, txtSupplierName, txtGstin, txtMobileNo }))
            {
                txtProductName.Focus();
                dgwSupplier.Hide();
            }
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwSupplier.Visible && !string.IsNullOrEmpty(txtSupplierName.Text))
                {
                    dgwSupplier.Focus();
                }
                else
                {
                    txtProductName.Focus();
                    dgwSupplier.Hide();
                }
            }
        }

        private void dgwProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (barcode)
                {
                    if (comp.getDgwToTextBox(dgwProduct, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtPrintingName }))
                    {
                        txtQuantity.Focus();
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
                if (comp.getDgwToTextBox(dgwProduct, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtPrintingName }))
                {
                    txtQuantity.Focus();
                    dgwProduct.Hide();
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
                if (txtSupplierName.Text != string.Empty)
                {
                    dgwProduct.Show();
                    (dgwProduct.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_product_name LIKE '%{0}%'", txtProductName.Text);
                }
                else
                {
                    txtSupplierName.Focus();
                }
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
                    getItems();
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
            if (txtPrintingName.Text == string.Empty)
            {
                txtPrintingName.Text = txtProductName.Text;
            }
            if (dgwItems.SelectedRows.Count > 0)
            {
                var selectedRow = dgwItems.SelectedRows[0];
                if (selectedRow.Cells[5].Value.ToString() != cmbProductSizeNo.Text)
                {
                    dt3.Rows.Add(selectedRow.Cells[0].Value.ToString(), selectedRow.Cells[4].Value.ToString(), selectedRow.Cells[7].Value.ToString(), selectedRow.Cells[6].Value.ToString());
                }
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
        DataTable dt3 = new DataTable();
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
                        dt2.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[7].Value.ToString(), row.Cells[6].Value.ToString());
                    }
                    isRemoveClicked = true;
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
            Control[] textBoxes = { txtProductId, txtProductName, cmbProductSizeNo, txtBarcode, txtQuantity };
            if (comp.validateControls(textBoxes))
            {
                bool itemExists = false;
                foreach (DataGridViewRow row in dgwItems.Rows)
                {
                    if (row.Cells[0].Value.ToString() == txtProductId.Text && row.Cells[5].Value.ToString() == cmbProductSizeNo.Text)
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
                        ClearProduct2();
                    }

                    txtProductName.Focus();
                }
                else
                {
                    if (!itemExists)
                    {
                        dt.Rows.Add(txtProductId.Text, txtProductName.Text, txtProductCategoryName.Text, txtGodownName.Text, txtBarcode.Text, cmbProductSizeNo.Text, txtPrintingName.Text, txtQuantity.Text);
                        ClearProduct();
                    }
                    txtProductName.Focus();
                }
            }
        }

        private void dgwItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getItemsDgwToTextBox(dgwItems, new Control[] { txtProductId, txtProductName, txtProductCategoryName, txtGodownName, txtBarcode, cmbProductSizeNo, txtPrintingName, txtQuantity }))
            {
                txtProductName.Focus();
                btnAdd.Enabled = false;
                dgwProduct.Hide();
            }
        }

        private void displayData()
        {
            query = "Select ID,f_invoice_no,f_invoice_date,f_supplier_id,f_supplier_name,f_gstin,f_mobile_no from Purchase_db Order By ID Desc";
            string[] headerText = { "ID", "Invoice No", "Invoice Date", "Supplier ID", "Supplier Name", "Gstin", "Mobile No" };
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
                query = "insert into Purchase_Items_db (f_invoice_no,f_invoice_date,f_product_id,f_product_name,f_product_category_name,f_godown_name,f_barcode,f_product_size_no,f_printing_name,f_quantity) Values ('" + txtInvoiceNo.Text + "', '" + txtInvoiceDate.Text + "', '" + row[0] + "', '" + row[1] + "', '" + row[2] + "', '" + row[3] + "', '" + row[4] + "', '" + row[5] + "', '" + row[6] + "', '" + row[7] + "')";
                con.SaveOrEditItems(query);

                using (OleDbConnection con = new OleDbConnection(Main))
                {
                    con.Open();
                    query = "select f_quantity from Product_Items_db where f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    double lastQty = Convert.ToDouble(cmd.ExecuteScalar());

                    double newQty = Convert.ToDouble(row[7]);
                    double totalQty = lastQty + newQty;

                    query = "Update Product_Items_db set f_quantity='" + totalQty + "',f_printing_name='" + row[6] + "' where f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                    OleDbCommand updateCmd = new OleDbCommand(query, con);
                    updateCmd.ExecuteNonQuery();
                }
            }
        }

        int productQty = 0, purchaseLastQty = 0, purchaseNewQty = 0, totalNewQty = 0;

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

                        query = "select f_quantity from Purchase_Items_db where f_invoice_no='" + txtInvoiceNo.Text + "' and f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand cmd1 = new OleDbCommand(query, connection);
                        purchaseLastQty = Convert.ToInt32(cmd1.ExecuteScalar());

                        purchaseNewQty = Convert.ToInt32(row[7]);

                        int newQty = productQty - purchaseLastQty;
                        totalNewQty = newQty + purchaseNewQty;

                        query = "Update Product_Items_db set f_quantity='" + totalNewQty + "',f_printing_name='" + row[6] + "' where f_barcode='" + row[4] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand updateCmd = new OleDbCommand(query, connection);
                        updateCmd.ExecuteNonQuery();
                    }
                    foreach (DataRow row in dt3.Rows)
                    {
                        string query = "select f_quantity from Product_Items_db where f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand cmd = new OleDbCommand(query, connection);
                        productQty = Convert.ToInt32(cmd.ExecuteScalar());

                        query = "select f_quantity from Purchase_Items_db where f_invoice_no='" + txtInvoiceNo.Text + "' and f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand cmd1 = new OleDbCommand(query, connection);
                        purchaseLastQty = Convert.ToInt32(cmd1.ExecuteScalar());

                        purchaseNewQty = Convert.ToInt32(row[2]);

                        int newQty = productQty - purchaseLastQty;

                        query = "Update Product_Items_db set f_quantity='" + newQty + "',f_printing_name='" + row[3] + "' where f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
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

                        query = "select f_quantity from Purchase_Items_db where f_invoice_no='" + txtInvoiceNo.Text + "' and f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand cmd1 = new OleDbCommand(query, connection);
                        purchaseLastQty = Convert.ToInt32(cmd1.ExecuteScalar());

                        purchaseNewQty = Convert.ToInt32(row[2]);

                        int newQty = productQty - purchaseLastQty;

                        query = "Update Product_Items_db set f_quantity='" + newQty + "',f_printing_name='" + row[3] + "' where f_barcode='" + row[1] + "' and f_product_id='" + row[0] + "'";
                        OleDbCommand updateCmd = new OleDbCommand(query, connection);
                        updateCmd.ExecuteNonQuery();
                    }
                }

                query = "delete from Purchase_Items_db where f_invoice_no = '" + txtInvoiceNo.Text + "'";
                OleDbCommand deleteCmd = new OleDbCommand(query, connection);
                deleteCmd.ExecuteNonQuery();

                foreach (DataRow row in dt.Rows)
                {
                    query = "insert into Purchase_Items_db (f_invoice_no,f_invoice_date,f_product_id,f_product_name,f_product_category_name,f_godown_name,f_barcode,f_product_size_no,f_printing_name,f_quantity) Values ('" + txtInvoiceNo.Text + "', '" + txtInvoiceDate.Text + "', '" + row[0] + "', '" + row[1] + "', '" + row[2] + "', '" + row[3] + "', '" + row[4] + "', '" + row[5] + "', '" + row[6] + "', '" + row[7] + "')";
                    OleDbCommand insertCmd = new OleDbCommand(query, connection);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }

        private void barcodePrinting()
        {
            if (MessageBox.Show("Do You Want To Print Qr Code", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                cr.Load(Application.StartupPath + "/REPORTS/CRY_BARCODE_PRINTING.rpt");
                dt.Columns.Add("Size No");
                dt.Columns.Add("Barcode");
                dt.Columns.Add("Printing Name");

                foreach (DataGridViewRow dgwBarcode in dgwItems.Rows)
                {
                    int quantity = Convert.ToInt32(dgwBarcode.Cells[7].Value);
                    for (int i = 0; i < quantity; i++)
                    {
                        dt.Rows.Add(dgwBarcode.Cells[5].Value, dgwBarcode.Cells[4].Value, dgwBarcode.Cells[6].Value);
                    }
                }
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("Barcode Printing.xml");
                cr.SetDataSource(ds);
                View_Daily_Reports.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtInvoiceNo, txtInvoiceDate, txtSupplierName };

            if (comp.validateControls(textBoxes))
            {
                if (btnSave.Text == "F2 Save" && grpProduct.Text == "Create")
                {
                    validate = "Select * from Purchase_db where f_invoice_no='" + txtInvoiceNo.Text + "'";
                    query = "Insert into Purchase_db (f_invoice_no,f_invoice_date,f_supplier_id,f_supplier_name,f_gstin,f_mobile_no) values ('" + txtInvoiceNo.Text + "','" + txtInvoiceDate.Text + "','" + txtSupplierId.Text + "','" + txtSupplierName.Text + "','" + txtGstin.Text + "','" + txtMobileNo.Text + "')";
                    if (con.SaveData(query, validate))
                    {
                        SaveItems();
                        barcodePrinting();
                    }
                    ClearAll();
                    AutoNumber();
                }
                else if (btnSave.Text == "F2 Update" && grpProduct.Text == "Update")
                {
                    validate = "Select * from Purchase_db where f_invoice_no='" + txtInvoiceNo.Text + "'";
                    query = "Update Purchase_db set f_invoice_no ='" + txtInvoiceNo.Text + "',f_invoice_date='" + txtInvoiceDate.Text + "' ,f_supplier_id='" + txtSupplierId.Text + "',f_supplier_name='" + txtSupplierName.Text + "',f_gstin='" + txtGstin.Text + "',f_mobile_no='" + txtMobileNo.Text + "' where ID=" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                    if (con.EditData(query, validate))
                    {
                        UpdateItems();
                        if (dgwDetails.Rows.Count >= 1)
                        {
                            barcodePrinting();
                        }
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
                btnSave.Focus();
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
            if (grpProduct.Text == "Update")
            {
                query = "Delete From Purchase_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
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

                            query = "select f_quantity from Purchase_Items_db where f_invoice_no='" + txtInvoiceNo.Text + "' and f_barcode='" + row[1] + "'";
                            OleDbCommand cmd1 = new OleDbCommand(query, connection);
                            purchaseLastQty = Convert.ToInt32(cmd1.ExecuteScalar());

                            purchaseNewQty = Convert.ToInt32(row[2]);

                            int newQty = productQty - purchaseLastQty;

                            query = "Update Product_Items_db set f_quantity='" + newQty + "' where f_barcode='" + row[1] + "'";
                            OleDbCommand updateCmd = new OleDbCommand(query, connection);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    query = "delete from Purchase_Items_db where f_invoice_no = '" + dgwDetails.SelectedRows[i].Cells[1].Value.ToString() + "'";
                    con.SaveOrEditItems(query);
                }
                ClearAll();
                AutoNumber();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void displayItems()
        {
            query = "Select f_product_id,f_product_name,f_product_category_name,f_godown_name,f_barcode,f_product_size_no,f_printing_name,f_quantity FROM Purchase_Items_db WHERE f_invoice_no = '" + txtInvoiceNo.Text + "'";
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
            if (!comp.getDataDgwToTextBox(dgwDetails, new Control[] { txtInvoiceNo, txtInvoiceDate, txtSupplierId, txtSupplierName, txtGstin, txtMobileNo })) return;

            if (grpProduct.Text == "Edit")
            {
                btnSave.Text = "F2 Update";
                grpProduct.Text = "Update";
            }
            displayItems();
            CalculateTotal();
            txtSupplierName.Focus();
            dgwDetails.Hide();
            dgwSupplier.Hide();
            dgwItems.Show();
        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            query = "Select Product_db.f_product_id,Product_db.f_product_name,Product_db.f_product_category_name,Product_db.f_godown_name,Product_Items_db.f_barcode, Product_Items_db.f_product_size_no,Product_Items_db.f_printing_name FROM Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id Where Product_Items_db.f_barcode='" + txtBarcode.Text + "'";
            getDetails(txtBarcode, query);
        }

        private bool barcode = false;

        private void getDetails(TextBox txtBox, string queryDetails)
        {
            try
            {
                if (txtBox.Text != string.Empty && txtProductName.Text == string.Empty)
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
                        cmbProductSizeNo.Text = row[5].ToString();
                        txtPrintingName.Text = row[6].ToString();
                        txtQuantity.Focus();
                        dgwProduct.Hide();
                    }
                    else if (d2s.Tables[0].Rows.Count > 1)
                    {
                        dgwProduct.Show();
                        query = "Select Product_db.f_product_id,Product_db.f_product_name,Product_db.f_product_category_name,Product_db.f_godown_name,Product_Items_db.f_barcode,Product_Items_db.f_product_size_no,Product_Items_db.f_printing_name FROM Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id Where Product_Items_db.f_barcode='" + txtBarcode.Text + "'";
                        string[] headerText = { "Product ID", "Product Name", "Category Name", "Godown Name", "Barcode", "Product Size No", "Printing Name" };
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

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

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

        private void FRM_PURCHASE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (dgwProduct.Visible == true || dgwSupplier.Visible == true)
                {
                    dgwProduct.Visible = false;
                    dgwSupplier.Visible = false;
                }
                else if (dgwProduct.Visible == false && dgwSupplier.Visible == false)
                {
                    comp.Close(this, e);
                }
            }
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
