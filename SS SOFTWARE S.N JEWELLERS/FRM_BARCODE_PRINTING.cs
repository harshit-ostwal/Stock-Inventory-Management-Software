using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_BARCODE_PRINTING : Form
    {
        Connection con = new Connection();
        Components comp = new Components();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        string query;
        int i = 0;

        public FRM_BARCODE_PRINTING()
        {
            InitializeComponent();
        }

        private void FRM_BARCODE_PRINTING_Load(object sender, EventArgs e)
        {
            displayDetails();
        }

        private void displayDetails()
        {
            query = "Select f_product_id,f_product_name from Product_db";
            string[] headerText = { "Product ID", "Product Name" };
            con.GetData(query, dgwDetails, headerText);
        }

        private void txtProductId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwDetails.Show();
                if (dgwDetails.Visible == true)
                {
                    (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_product_id LIKE '%{0}%'", txtProductId.Text);
                }
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwDetails.Show();
                if (dgwDetails.Visible == true)
                {
                    (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_product_name LIKE '%{0}%'", txtProductName.Text);
                }
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProductId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgwDetails.Visible && !string.IsNullOrEmpty(txtProductId.Text))
                {
                    dgwDetails.Focus();
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
                    SendKeys.Send("{TAB}");
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void ClearAll()
        {
            comp.Clear(new Control[] { txtProductId, txtProductName, txtPrintingName, cmbProductSizeNo, txtBarcode, txtPrintingName, txtQuantity });
            dgwDetails.Hide();
            btnAdd.Enabled = true;
        }

        private void getItems()
        {
            cmbProductSizeNo.Items.Clear();
            query = "Select f_product_size_no from Product_Items_db Where f_product_id='" + txtProductId.Text + "' and f_product_name='" + txtProductName.Text + "'";
            con.GetItems(query, cmbProductSizeNo);
        }

        private void dgwDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comp.getDgwToTextBox(dgwDetails, new Control[] { txtProductId, txtProductName }))
                {
                    cmbProductSizeNo.Focus();
                    getItems();
                    dgwDetails.Hide();
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

        private void dgwDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getDgwToTextBox(dgwDetails, new Control[] { txtProductId, txtProductName }))
            {
                cmbProductSizeNo.Focus();
                getItems();
                dgwDetails.Hide();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgwItems.Rows.Count != 0)
            {
                dgwItems.SelectedRows[i].Cells[0].Value = cmbProductSizeNo.SelectedItem.ToString();
                dgwItems.SelectedRows[i].Cells[1].Value = txtBarcode.Text;
                dgwItems.SelectedRows[i].Cells[2].Value = txtPrintingName.Text;
                dgwItems.SelectedRows[i].Cells[3].Value = txtQuantity.Text;
                ClearAll();
            }
            else
            {
                MessageBox.Show("Data Not Found? You Can't Edit", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearAll();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgwItems.Rows.Count != 0)
            {
                if (MessageBox.Show("Do You Wanna Delete?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int rowIndex = dgwItems.SelectedRows[i].Cells[0].RowIndex;
                    dgwItems.Rows.RemoveAt(rowIndex);
                    ClearAll();
                }
            }
            else
            {
                MessageBox.Show("Data Not Found? You Can't Delete", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearAll();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtProductId, txtProductName, cmbProductSizeNo, txtBarcode, txtPrintingName, txtQuantity };
            if (comp.validateControls(textBoxes))
            {
                bool itemExists = false;
                foreach (DataGridViewRow row in dgwItems.Rows)
                {
                    if (row.Cells[0].Value.ToString() == txtProductId.Text && row.Cells[8].Value.ToString() == cmbProductSizeNo.SelectedItem.ToString())
                    {
                        double lastQty = double.Parse(row.Cells[10].Value.ToString());
                        double newQty = lastQty + double.Parse(txtQuantity.Text);
                        row.Cells[10].Value = newQty.ToString();
                        itemExists = true;
                        ClearAll();
                        break;
                    }
                }
                if (MessageBox.Show("Do You Wanna Add One More Details?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (!itemExists)
                    {
                        dgwItems.Rows.Add(cmbProductSizeNo.SelectedItem.ToString(), txtBarcode.Text, txtPrintingName.Text, txtQuantity.Text);
                        ClearAll();
                    }
                }
                else
                {
                    if (!itemExists)
                    {
                        dgwItems.Rows.Add(cmbProductSizeNo.SelectedItem.ToString(), txtBarcode.Text, txtPrintingName.Text, txtQuantity.Text);
                        ClearAll();
                        btnPrint.Focus();
                    }
                }
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


        private void Enter_Key_Press(object sender, KeyEventArgs e)
        {
            comp.Enter(sender, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportDocument cr = new ReportDocument();
            cr.Load(Application.StartupPath + "/REPORTS/CRY_BARCODE_PRINTING.rpt");
            FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, "Barcode Printing");
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Size No");
            dt.Columns.Add("Barcode");
            dt.Columns.Add("Printing Name");
            foreach (DataGridViewRow dgwDetails in dgwItems.Rows)
            {
                int quantity = Convert.ToInt32(dgwDetails.Cells[3].Value);
                for (int i = 0; i < quantity; i++)
                {
                    dt.Rows.Add(dgwDetails.Cells[0].Value, dgwDetails.Cells[1].Value, dgwDetails.Cells[2].Value);
                }
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Barcode Printing.xml");
            cr.SetDataSource(ds);
            View_Daily_Reports.ShowDialog();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            txtBarcode.Enabled = !txtBarcode.Enabled;
            txtBarcode.Clear();
            txtBarcode.Focus();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            txtPrintingName.Enabled = !txtPrintingName.Enabled;
            txtPrintingName.Focus();
        }

        private void txtPrintingName_Leave(object sender, EventArgs e)
        {
            txtPrintingName.Enabled = false;
        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            query = "Select f_product_id,f_product_name,f_product_size_no,f_barcode,f_printing_name,f_quantity from Product_Items_db where f_barcode='" + txtBarcode.Text + "'";
            getDetails(txtBarcode, query);
            txtBarcode.Enabled = false;
        }

        private void getDetails(TextBox txtBox, string queryDetails)
        {
            try
            {
                if (txtBox.Text != string.Empty)
                {
                    DataSet ds = new DataSet();
                    OleDbDataAdapter ad = new OleDbDataAdapter(queryDetails, Main);
                    ad.Fill(ds);
                    DataRow row = ds.Tables[0].Rows[0];
                    txtProductId.Text = row[0].ToString();
                    txtProductName.Text = row[1].ToString();
                    cmbProductSizeNo.Text = row[2].ToString();
                    txtBarcode.Text = row[3].ToString();
                    txtPrintingName.Text = row[4].ToString();
                }
            }
            catch
            {
                MessageBox.Show("Data Not Found?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgwItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MessageBox.Show("Do You Wanna Edit?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                btnAdd.Enabled = false;
                cmbProductSizeNo.SelectedItem = dgwItems.SelectedRows[i].Cells[0].Value.ToString();
                txtBarcode.Text = dgwItems.SelectedRows[i].Cells[1].Value.ToString();
                txtPrintingName.Text = dgwItems.SelectedRows[i].Cells[2].Value.ToString();
                txtQuantity.Text = dgwItems.SelectedRows[i].Cells[3].Value.ToString();
                cmbProductSizeNo.Focus();
            }
            else
            {
                ClearAll();
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

        private void FRM_BARCODE_PRINTING_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void cmbProductSizeNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
