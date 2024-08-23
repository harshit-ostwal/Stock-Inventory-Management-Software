using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.OleDb;
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
                cr.Load(Application.StartupPath + "/REPORTS/CRY_STOCK_IN_HAND.rpt");
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

                // Retrieving search type and search term
                string searchType = cmbSearchType.SelectedItem?.ToString();
                string searchTerm = txtSearch.Text.Trim();

                // Append filter based on search type
                if (!string.IsNullOrEmpty(searchType) && !string.IsNullOrEmpty(searchTerm))
                {
                    // Sanitize the search term to prevent SQL injection
                    searchTerm = searchTerm.Replace("'", "''");

                    switch (searchType)
                    {
                        case "Product Name":
                            query += " AND Product_db.f_product_name LIKE '%" + searchTerm + "%'";
                            break;
                        case "Category Name":
                            query += " AND Product_db.f_product_category_name LIKE '%" + searchTerm + "%'";
                            break;
                        case "Godown Name":
                            query += " AND Product_db.f_godown_name LIKE '%" + searchTerm + "%'";
                            break;
                        case "Product Size No":
                            query += " AND Product_Items_db.f_product_size_no LIKE '%" + searchTerm + "%'";
                            break;
                        default:
                            MessageBox.Show("Please select a valid search type.", "Invalid Search Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                    }
                }

                // Execute the query
                OleDbDataAdapter ad = new OleDbDataAdapter(query, Main);
                ds.Clear();
                ad.Fill(ds);

                // Clear existing rows in the DataTable before loading new data
                dt.Rows.Clear();

                // Populate the DataTable with the results
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dt.Rows.Add(row["f_product_id"], row["f_product_name"], row["f_product_category_name"], row["f_godown_name"], row["f_product_size_no"], row["f_barcode"], row["f_printing_name"], row["f_quantity"]);
                }
                CalculateTotal();
            }
            ds.WriteXmlSchema("Reports.xml");
            cr.SetDataSource(ds);
        }


        private void btnShowAllStock_Click(object sender, EventArgs e)
        {
            ClearAll();
            if (MessageBox.Show("Do You Want To Load All Stock ⏲?\n It Might Take A Bit Of Time To Load All The Records. ⏳", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "SELECT Product_db.f_product_id, Product_db.f_product_name, Product_db.f_product_category_name, Product_db.f_godown_name, Product_Items_db.f_product_size_no, Product_Items_db.f_barcode, Product_Items_db.f_printing_name, Product_Items_db.f_quantity FROM Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id";
                ad = new OleDbDataAdapter(query, Main);
                ds.Clear();
                ad.Fill(ds);
                dt.Rows.Clear();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dt.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);
                }
                CalculateTotal();
            }
            ds.WriteXmlSchema("Reports.xml");
            cr.SetDataSource(ds);
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
    }
}
