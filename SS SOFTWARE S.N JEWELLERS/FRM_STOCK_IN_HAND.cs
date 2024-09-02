using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_STOCK_IN_HAND : Form
    {

        Components comp = new Components();
        DataSet ds = new DataSet();
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        DataTable dt = new DataTable();
        ReportDocument cr = new ReportDocument();

        public FRM_STOCK_IN_HAND()
        {
            InitializeComponent();
            dt.Columns.Add("Product ID", typeof(string));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Product Category Name", typeof(string));
            dt.Columns.Add("Product Godown Name", typeof(string));
            dt.Columns.Add("Product Size No", typeof(string));
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("Printing Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            cr.Load(Application.StartupPath + "/REPORTS/CRY_STOCK_IN_HAND.rpt");
            dgwDetails.DataSource = dt;
        }

        private void CalculateTotal()
        {
            double totalQuantity = 0;
            if (dgwDetails.RowCount == 0)
            {
                lblTotalQuantity.Text = "0";
            }

            for (int i = 0; i < dgwDetails.Rows.Count; i++)
            {
                if (dgwDetails.Rows[i].Cells[7].Value != null)
                { totalQuantity += double.Parse(dgwDetails.Rows[i].Cells[7].Value.ToString()); }

                lblTotalQuantity.Text = Math.Round(totalQuantity, 2).ToString();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Load Stock ⏲?", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = @"
    SELECT 
        Product_db.f_product_id, 
        Product_db.f_product_name, 
        Product_db.f_product_category_name, 
        Product_db.f_godown_name, 
        Product_Items_db.f_product_size_no, 
        Product_Items_db.f_barcode, 
        Product_Items_db.f_printing_name, 
        Product_Items_db.f_quantity 
    FROM 
        Product_db 
    INNER JOIN 
        Product_Items_db ON Product_db.f_product_id = Product_Items_db.f_product_id";

                string filter = "";

                string searchType = cmbSearchType.SelectedItem?.ToString();
                string searchTerm = txtSearch.Text.Trim();

                if (!string.IsNullOrEmpty(searchType) && !string.IsNullOrEmpty(searchTerm))
                {
                    string[] terms = searchTerm.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(term => term.Trim())
                                                .ToArray();

                    switch (searchType)
                    {
                        case "Product Name":
                            filter = string.Join(" OR ", terms.Select(term => string.Format("[Product_Items_db].[f_product_name] LIKE '%%{0}%%'", term)));
                            break;
                        case "Category Name":
                            filter = string.Join(" OR ", terms.Select(term => string.Format("[Product_db].[f_product_category_name] LIKE '%%{0}%%'", term)));
                            break;
                        case "Godown Name":
                            filter = string.Join(" OR ", terms.Select(term => string.Format("[Product_db].[f_godown_name] LIKE '%%{0}%%'", term)));
                            break;
                        case "Product Size No":
                            filter = string.Join(" OR ", terms.Select(term => string.Format("[Product_Items_db].[f_product_size_no] LIKE '%%{0}%%'", term)));
                            break;
                        case "Barcode":
                            filter = string.Join(" OR ", terms.Select(term => string.Format("[Product_Items_db].[f_barcode] LIKE '%%{0}%%'", term)));
                            break;
                        default:
                            MessageBox.Show("Please select a valid search type.", "Invalid Search Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                    }

                    query += " WHERE " + filter;
                }
                OleDbDataAdapter ad = new OleDbDataAdapter(query, Main);
                ds.Clear();
                dt.Rows.Clear();
                ad.Fill(ds);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dt.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);
                }
                CalculateTotal();
            }
        }

        private void ShowData()
        {
            string query = "SELECT Product_db.f_product_id, Product_db.f_product_name, Product_db.f_product_category_name, Product_db.f_godown_name, Product_Items_db.f_product_size_no, Product_Items_db.f_barcode, Product_Items_db.f_printing_name, Product_Items_db.f_quantity FROM Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id";
            ad = new OleDbDataAdapter(query, Main);
            ds.Clear();
            dt.Rows.Clear();
            ad.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dt.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);
            }
            CalculateTotal();
        }


        private void btnShowAllStock_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (MessageBox.Show("Do You Want To Load All Stock ⏲?\n It Might Take A Bit Of Time To Load All The Records. ⏳", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ShowData();
            }
        }

        private void ClearAll()
        {
            cmbSearchType.SelectedIndex = -1;
            txtSearch.Text = string.Empty;
            dt.Rows.Clear();
            CalculateTotal();
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ds.WriteXmlSchema("Reports.xml");
            cr.SetDataSource(ds);
            FRM_VIEW_REPORTS View_Daily_Reports = new FRM_VIEW_REPORTS(cr, "Stock In Hand");
            View_Daily_Reports.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            comp.Close(this);
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }

        private void FRM_STOCK_IN_HAND_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void EnterKEY(object sender, KeyEventArgs e)
        {
            comp.Enter(sender, e);
        }
    }
}
