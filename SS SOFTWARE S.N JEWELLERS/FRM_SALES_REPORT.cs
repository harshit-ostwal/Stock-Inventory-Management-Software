using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_SALES_REPORT : Form
    {
        Components comp = new Components();
        DataSet ds = new DataSet();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        ReportDocument cr = new ReportDocument();

        public FRM_SALES_REPORT()
        {
            InitializeComponent();
        }

        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Load All Sales ⏲?\n It Might Take A Bit Of Time To Load All The Records. ⏳", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cr.Load(Application.StartupPath + "/REPORTS/CRY_SALES_REPORT.rpt");
                    TextObject FromDate = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["lblFromDate"];
                    FromDate.Text = txtFromDate.Value.ToString("dd-MM-yyyy");
                    TextObject ToDate = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["lblToDate"];
                    ToDate.Text = txtToDate.Value.ToString("dd-MM-yyyy");
                    string query = "SELECT Sales_db.f_invoice_no, Sales_db.f_invoice_date, Sales_db.f_customer_name, Sales_db.f_area, Sales_db.f_mobile_no, Sales_Items_db.f_product_name, Sales_Items_db.f_product_category_name, Sales_Items_db.f_godown_name, Sales_Items_db.f_product_size_no, Sales_Items_db.f_barcode, Sales_Items_db.f_printing_name, Sales_Items_db.f_quantity FROM Sales_db INNER JOIN Sales_Items_db ON Sales_db.f_invoice_no = Sales_Items_db.f_invoice_no WHERE Sales_db.f_invoice_date BETWEEN @From AND @To";
                    using (OleDbConnection con = new OleDbConnection(Main))
                    {
                        ad = new OleDbDataAdapter(query, con);
                        ad.SelectCommand.Parameters.AddWithValue("@From", txtFromDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@To", txtToDate.Value.ToString("dd-MM-yyyy"));

                        ds.Clear();
                        ad.Fill(ds);
                        ds.WriteXmlSchema("Sales Report.xml");
                        cr.SetDataSource(ds);

                        FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, $"{txtFromDate.Text} To {txtToDate.Text} - Sales Report");
                        View_Daily_Reports.ShowDialog();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error While Loading Sales Report?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FRM_SALES_REPORT_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }
    }
}
