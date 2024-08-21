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
    public partial class FRM_SALES_WISE_REPORT : Form
    {

        Components comp = new Components();
        DataSet ds = new DataSet();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        Connection con = new Connection();
        string query;
        CRY_SALES_WISE_REPORT cr = new CRY_SALES_WISE_REPORT();

        public FRM_SALES_WISE_REPORT()
        {
            InitializeComponent();
        }

        private void FRM_SALES_WISE_REPORT_Load(object sender, EventArgs e)
        {
            displayData();
        }

        private void displayData()
        {
            query = "Select f_invoice_no,f_invoice_date,f_customer_id, f_customer_name, f_area, f_mobile_no from Sales_db Order By ID Desc";
            string[] headerText = { "Invoice No", "Invoice Date", "Supplier ID", "Supplier Name", "Gstin", "Mobile No" };
            con.GetData(query, dgwSales, headerText);
        }

        private void btnSalesWiseReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Load All Sales Wise ⏲?\n It Might Take A Bit Of Time To Load All The Records. ⏳", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    TextObject FromDate = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["lblFromDate"];
                    FromDate.Text = txtFromDate.Value.ToString("dd-MM-yyyy");
                    TextObject ToDate = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["lblToDate"];
                    ToDate.Text = txtToDate.Value.ToString("dd-MM-yyyy");
                    string query = @"
               SELECT 
                    Sales_db.f_invoice_date,
                    Sales_db.f_invoice_no,
                    Sales_db.f_customer_id,
                    Sales_db.f_customer_name,
                    Sales_db.f_area,
                    Sales_db.f_mobile_no,
                    Sales_Items_db.f_product_name, 
                    Sales_Items_db.f_product_category_name, 
                    Sales_Items_db.f_godown_name, 
                    Sales_Items_db.f_product_size_no, 
                    Sales_Items_db.f_quantity
                FROM 
                    Sales_db
                INNER JOIN 
                    Sales_Items_db ON Sales_db.f_invoice_no = Sales_Items_db.f_invoice_no
                WHERE 
                    Sales_db.f_invoice_date BETWEEN @From AND @To 
                    AND Sales_db.f_invoice_no = @SalesInvoiceNo 
                    AND Sales_db.f_customer_name = @CustomerName";

                    using (OleDbConnection con = new OleDbConnection(Main))
                    {
                        ad = new OleDbDataAdapter(query, con);
                        ad.SelectCommand.Parameters.AddWithValue("@From", txtFromDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@To", txtToDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@SalesInvoiceNo", txtSalesInvoiceNo.Text);
                        ad.SelectCommand.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);

                        ds.Clear();
                        ad.Fill(ds);
                        ds.WriteXmlSchema("Sales Wise Report.xml");
                        cr.SetDataSource(ds);

                        FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, $"{txtFromDate.Text} To {txtToDate.Text} - Sales Wise Report");
                        View_Daily_Reports.ShowDialog();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error While Loading Sales Wise Report?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FRM_SALES_WISE_REPORT_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwSales.Show();
                (dgwSales.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_customer_name LIKE '%{0}%'", txtCustomerName.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSalesInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwSales.Show();
                (dgwSales.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_invoice_no LIKE '%{0}%'", txtSalesInvoiceNo.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        TextBox Dummy = new TextBox();

        private void dgwSales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comp.getDgwToTextBox(dgwSales, new Control[] { txtSalesInvoiceNo, Dummy, Dummy, txtCustomerName }))
                {
                    dgwSales.Hide();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgwSales.Hide();
            }
        }

        private void dgwSales_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getDgwToTextBox(dgwSales, new Control[] { txtSalesInvoiceNo, Dummy, Dummy, txtCustomerName }))
            {
                dgwSales.Hide();
            }
        }

        private void txtSalesInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwSales.Visible && !string.IsNullOrEmpty(txtSalesInvoiceNo.Text))
                {
                    dgwSales.Focus();
                }
                else
                {
                    dgwSales.Hide();
                    txtSalesInvoiceNo.Focus();
                }
            }
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwSales.Visible && !string.IsNullOrEmpty(txtCustomerName.Text))
                {
                    dgwSales.Focus();
                }
                else
                {
                    dgwSales.Hide();
                    txtCustomerName.Focus();
                }
            }
        }
    }
}
