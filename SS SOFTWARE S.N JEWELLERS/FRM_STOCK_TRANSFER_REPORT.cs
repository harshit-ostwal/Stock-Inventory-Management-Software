using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_STOCK_TRANSFER_REPORT : Form
    {
        Components comp = new Components();
        DataSet ds = new DataSet();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        CRY_STOCK_TRANSFER_REPORT cr = new CRY_STOCK_TRANSFER_REPORT();

        public FRM_STOCK_TRANSFER_REPORT()
        {
            InitializeComponent();
        }

        private void btnStockTransferReport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Load All Stock Transfer ⏲?\n It Might Take A Bit Of Time To Load All The Records. ⏳", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    TextObject FromDate = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["lblFromDate"];
                    FromDate.Text = txtFromDate.Value.ToString("dd-MM-yyyy");
                    TextObject ToDate = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["lblToDate"];
                    ToDate.Text = txtToDate.Value.ToString("dd-MM-yyyy");
                    string query = "SELECT Stock_Transfer_db.f_stock_transfer_id, Stock_Transfer_db.f_stock_transfer_date, Stock_Transfer_db.f_from_godown, Stock_Transfer_db.f_to_godown,Stock_Transfer_Items_db.f_product_name, Stock_Transfer_Items_db.f_product_category_name, Stock_Transfer_Items_db.f_godown_name, Stock_Transfer_Items_db.f_product_size_no, Stock_Transfer_Items_db.f_barcode, Stock_Transfer_Items_db.f_printing_name, Stock_Transfer_Items_db.f_quantity FROM Stock_Transfer_db INNER JOIN Stock_Transfer_Items_db ON Stock_Transfer_db.f_stock_transfer_id = Stock_Transfer_Items_db.f_stock_transfer_id WHERE Stock_Transfer_db.f_stock_transfer_date BETWEEN @From AND @To";
                    using (OleDbConnection con = new OleDbConnection(Main))
                    {
                        ad = new OleDbDataAdapter(query, con);
                        ad.SelectCommand.Parameters.AddWithValue("@From", txtFromDate.Value.ToString("dd-MM-yyyy"));
                        ad.SelectCommand.Parameters.AddWithValue("@To", txtToDate.Value.ToString("dd-MM-yyyy"));

                        ds.Clear();
                        ad.Fill(ds);
                        ds.WriteXmlSchema("Stock Transfer Report.xml");
                        cr.SetDataSource(ds);

                        FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, $"{txtFromDate.Text} To {txtToDate.Text} - Stock Transfer Report");
                        View_Daily_Reports.ShowDialog();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error While Loading Stock Transfer Report?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FRM_STOCK_TRANSFER_REPORT_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void FRM_STOCK_TRANSFER_REPORT_Load(object sender, EventArgs e)
        {

        }
    }
}
