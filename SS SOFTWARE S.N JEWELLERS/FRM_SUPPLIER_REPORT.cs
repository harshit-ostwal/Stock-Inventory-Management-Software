using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_SUPPLIER_REPORT : Form
    {
        Components comp = new Components();
        DataSet ds = new DataSet();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        Connection con = new Connection();
        string query;
        ReportDocument cr = new ReportDocument();

        public FRM_SUPPLIER_REPORT()
        {
            InitializeComponent();
        }

        private void btnSupplierReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Load All Supplier ⏲?\n It Might Take A Bit Of Time To Load All The Records. ⏳", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cr.Load(Application.StartupPath + "/REPORTS/CRY_SUPPLIER_REPORT.rpt");
                    TextObject FromDate = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["lblFromDate"];
                    FromDate.Text = txtFromDate.Value.ToString("dd-MM-yyyy");
                    TextObject ToDate = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["lblToDate"];
                    ToDate.Text = txtToDate.Value.ToString("dd-MM-yyyy");
                    string query = @"
                SELECT 
                    Purchase_db.f_invoice_date,
                    Purchase_db.f_invoice_no,
                    Purchase_db.f_supplier_id,
                    Purchase_db.f_supplier_name,
                    Purchase_db.f_gstin,
                    Purchase_db.f_mobile_no,
                    Purchase_Items_db.f_product_name, 
                    Purchase_Items_db.f_product_category_name, 
                    Purchase_Items_db.f_godown_name, 
                    Purchase_Items_db.f_product_size_no, 
                    Purchase_Items_db.f_quantity
                FROM 
                    Purchase_db
                INNER JOIN 
                    Purchase_Items_db ON Purchase_db.f_invoice_no = Purchase_Items_db.f_invoice_no
                WHERE 
                    Purchase_db.f_invoice_date BETWEEN @From AND @To 
                    AND Purchase_db.f_supplier_id = @SupplierId 
                    AND Purchase_db.f_supplier_name = @SupplierName";

                    using (OleDbConnection con = new OleDbConnection(Main))
                    {
                        ad = new OleDbDataAdapter(query, con);
                        ad.SelectCommand.Parameters.AddWithValue("@From", txtFromDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@To", txtToDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@SupplierId", txtSupplierId.Text);
                        ad.SelectCommand.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);

                        ds.Clear();
                        ad.Fill(ds);
                        ds.WriteXmlSchema("Supplier Report.xml");
                        cr.SetDataSource(ds);

                        FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, $"{txtFromDate.Text} To {txtToDate.Text} - Purchase Report");
                        View_Daily_Reports.ShowDialog();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error While Loading Supplier Report?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void FRM_SUPPLIER_REPORT_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void FRM_SUPPLIER_REPORT_Load(object sender, EventArgs e)
        {
            displayData();
        }

        private void displayData()
        {
            query = "Select f_supplier_id,f_supplier_name,f_gstin,f_mobile_no from Supplier_db Order By ID Desc";
            string[] headerText = { "Supplier ID", "Supplier Name", "Gstin", "Mobile No" };
            con.GetData(query, dgwSupplier, headerText);
        }

        private void txtSupplierId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwSupplier.Show();
                (dgwSupplier.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_supplier_id LIKE '%{0}%'", txtSupplierId.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void dgwSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comp.getDgwToTextBox(dgwSupplier, new Control[] { txtSupplierId, txtSupplierName }))
                {
                    dgwSupplier.Hide();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgwSupplier.Hide();
            }
        }

        private void dgwSupplier_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getDgwToTextBox(dgwSupplier, new Control[] { txtSupplierId, txtSupplierName }))
            {
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
                    dgwSupplier.Hide();
                    btnSupplierReport.Focus();
                }
            }
        }

        private void txtSupplierId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwSupplier.Visible && !string.IsNullOrEmpty(txtSupplierId.Text))
                {
                    dgwSupplier.Focus();
                }
                else
                {
                    dgwSupplier.Hide();
                    txtSupplierName.Focus();
                }
            }
        }
    }
}
