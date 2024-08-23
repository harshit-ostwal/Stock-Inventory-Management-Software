using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_CUSTOMER_REPORT : Form
    {
        Components comp = new Components();
        DataSet ds = new DataSet();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        Connection con = new Connection();
        string query;
        ReportDocument cr = new ReportDocument();

        public FRM_CUSTOMER_REPORT()
        {
            InitializeComponent();
        }

        private void btnCustomerReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Load All Customer ⏲?\n It Might Take A Bit Of Time To Load All The Records. ⏳", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cr.Load(Application.StartupPath + "/REPORTS/CRY_CUSTOMER_REPORT.rpt");
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
                    AND Sales_db.f_customer_id = @CustomerId 
                    AND Sales_db.f_customer_name = @CustomerName";

                    using (OleDbConnection con = new OleDbConnection(Main))
                    {
                        ad = new OleDbDataAdapter(query, con);
                        ad.SelectCommand.Parameters.AddWithValue("@From", txtFromDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@To", txtToDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text);
                        ad.SelectCommand.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text);

                        ds.Clear();
                        ad.Fill(ds);
                        ds.WriteXmlSchema("Customer Report.xml");
                        cr.SetDataSource(ds);

                        FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, $"{txtFromDate.Text} To {txtToDate.Text} - Sales Report");
                        View_Daily_Reports.ShowDialog();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error While Loading Customer Report?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FRM_CUSTOMER_REPORT_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void FRM_CUSTOMER_REPORT_Load(object sender, EventArgs e)
        {
            displayData();
        }

        private void displayData()
        {
            query = "Select f_customer_id,f_customer_name,f_area,f_mobile_no from Customer_db Order By ID Desc";
            string[] headerText = { "Customer ID", "Customer Name", "Area", "Mobile No" };
            con.GetData(query, dgwCustomer, headerText);
        }

        private void txtCustomerId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgwCustomer.Show();
                (dgwCustomer.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_customer_id LIKE '%{0}%'", txtCustomerId.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void dgwCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comp.getDgwToTextBox(dgwCustomer, new Control[] { txtCustomerId, txtCustomerName }))
                {
                    dgwCustomer.Hide();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgwCustomer.Hide();
            }
        }

        private void dgwCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comp.getDgwToTextBox(dgwCustomer, new Control[] { txtCustomerId, txtCustomerName }))
            {
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
                    dgwCustomer.Hide();
                    btnCustomerReport.Focus();
                }
            }
        }

        private void txtCustomerId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgwCustomer.Visible && !string.IsNullOrEmpty(txtCustomerId.Text))
                {
                    dgwCustomer.Focus();
                }
                else
                {
                    dgwCustomer.Hide();
                    txtCustomerName.Focus();
                }
            }
        }
    }
}
