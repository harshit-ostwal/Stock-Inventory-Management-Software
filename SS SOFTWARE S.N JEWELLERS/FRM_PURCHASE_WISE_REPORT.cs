using CrystalDecisions.CrystalReports.Engine;
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
    public partial class FRM_PURCHASE_WISE_REPORT : Form
    {

        Components comp = new Components();
        DataSet ds = new DataSet();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        Connection con = new Connection();
        string query;
        CRY_PURCHAE_WISE_REPORT cr = new CRY_PURCHAE_WISE_REPORT();

        public FRM_PURCHASE_WISE_REPORT()
        {
            InitializeComponent();
        }

        private void FRM_PURCHASE_WISE_REPORT_Load(object sender, EventArgs e)
        {
            displayData();
        }

        private void displayData()
        {
            query = "Select f_invoice_no,f_invoice_date,f_supplier_id,f_supplier_name,f_gstin,f_mobile_no from Purchase_db Order By ID Desc";
            string[] headerText = { "Invoice No", "Invoice Date", "Supplier ID", "Supplier Name", "Gstin", "Mobile No" };
            con.GetData(query, dgwPurchase, headerText);
        }

        private void btnPurchaseWiseReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Load All Purchase Wise ⏲?\n It Might Take A Bit Of Time To Load All The Records. ⏳", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
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
                    AND Purchase_db.f_invoice_no = @PurchaseInvoiceNo 
                    AND Purchase_db.f_supplier_name = @SupplierName";

                    using (OleDbConnection con = new OleDbConnection(Main))
                    {
                        ad = new OleDbDataAdapter(query, con);
                        ad.SelectCommand.Parameters.AddWithValue("@From", txtFromDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@To", txtToDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@PurchaseInvoiceNo", txtPurchaseInvoiceNo.Text);
                        ad.SelectCommand.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);

                        ds.Clear();
                        ad.Fill(ds);
                        ds.WriteXmlSchema("Purchase Wise Report.xml");
                        cr.SetDataSource(ds);

                        FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, $"{txtFromDate.Text} To {txtToDate.Text} - Purchase Wise Report");
                        View_Daily_Reports.ShowDialog();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error While Loading Purchase Wise Report?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FRM_PURCHASE_WISE_REPORT_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void txtSupplierName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwPurchase.Show();
                (dgwPurchase.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_supplier_name LIKE '%{0}%'", txtSupplierName.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPurchaseInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwPurchase.Show();
                (dgwPurchase.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_invoice_no LIKE '%{0}%'", txtPurchaseInvoiceNo.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        TextBox Dummy = new TextBox();

        private void dgwPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comp.getDgwToTextBox(dgwPurchase, new Control[] { txtPurchaseInvoiceNo, Dummy, Dummy, txtSupplierName }))
                {
                    dgwPurchase.Hide();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgwPurchase.Hide();
            }
        }

        private void dgwPurchase_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getDgwToTextBox(dgwPurchase, new Control[] { txtPurchaseInvoiceNo, Dummy, Dummy, txtSupplierName }))
            {
                dgwPurchase.Hide();
            }
        }

        private void txtPurchaseInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwPurchase.Visible && !string.IsNullOrEmpty(txtPurchaseInvoiceNo.Text))
                {
                    dgwPurchase.Focus();
                }
                else
                {
                    dgwPurchase.Hide();
                    txtPurchaseInvoiceNo.Focus();
                }
            }
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwPurchase.Visible && !string.IsNullOrEmpty(txtSupplierName.Text))
                {
                    dgwPurchase.Focus();
                }
                else
                {
                    dgwPurchase.Hide();
                    txtSupplierName.Focus();
                }
            }
        }
    }
}
