using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_CUSTOMER_IMPORT_EXPORT : Form
    {
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        Connection con = new Connection();
        Components comp = new Components();

        public FRM_CUSTOMER_IMPORT_EXPORT()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            dt.Columns.Add("Customer ID", typeof(string));
            dt.Columns.Add("Customer Name", typeof(string));
            dt.Columns.Add("Area", typeof(string));
            dt.Columns.Add("Mobile No", typeof(string));
            dgwDetails.DataSource = dt;
        }

        private void LoadProducts()
        {
            try
            {
                string query = "Select f_customer_id,f_customer_name,f_area,f_mobile_no from Customer_db";
                ad = new OleDbDataAdapter(query, Main);
                ds.Clear();
                ad.Fill(ds);
                dt.Rows.Clear();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dt.Rows.Add(row[0], row[1], row[2], row[3]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Loading Data?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FRM_CUSTOMER_IMPORT_EXPORT_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnDownloadFormat_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "SS SOFTWARE";
                saveFileDialog.FileName = "Customers - SS SOFTWARE.xlsx";

                if (MessageBox.Show("Do You Want To Download Excel Sheet Format? 📊", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            ExportExcel(saveFileDialog, false);
                            MessageBox.Show("Excel Exported Successfully! ✅", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("An Error Occured While Downloading Excel Sheet Format?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void ExportExcel(SaveFileDialog saveFileDialog, bool includeData)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet productMaster = package.Workbook.Worksheets.Add("Customer Master");

                for (int col = 1; col <= dt.Columns.Count; col++)
                {
                    productMaster.Cells[1, col].Value = dt.Columns[col - 1].ColumnName;

                    productMaster.Cells[1, col].Style.Font.Bold = true;
                    productMaster.Cells[1, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    if (col == 1)
                    {
                        productMaster.Cells[1, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Green);
                    }
                    else
                    {
                        productMaster.Cells[1, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                    }
                    productMaster.Cells[1, col].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                    productMaster.Cells[1, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    productMaster.Cells[1, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    productMaster.Column(col).Width = 30;
                }

                if (includeData)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            var cell = productMaster.Cells[row + 2, col + 1];
                            cell.Value = dt.Rows[row][col];
                        }
                    }
                }

                productMaster.Row(1).Height = 35;
                productMaster.Cells[1, 1, 1, dt.Columns.Count].AutoFilter = true;

                package.SaveAs(saveFileDialog.FileName);
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "SS SOFTWARE";
                saveFileDialog.FileName = "Customers - SS SOFTWARE.xlsx";

                if (MessageBox.Show("Do You Want To Export Data To Excel Sheet? 📊", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            ExportExcel(saveFileDialog, true);
                            MessageBox.Show("Excel Exported Successfully! ✅", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("An Error Occured While Exporting Data To Excel Sheet?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                openFileDialog.Title = "SS SOFTWARE";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (ExcelPackage package = new ExcelPackage(openFileDialog.FileName))
                        {
                            ExcelWorksheet productMaster = package.Workbook.Worksheets[0];
                            dt.Clear();
                            for (int row = 2; row <= productMaster.Dimension.End.Row; row++)
                            {
                                DataRow dataRow = dt.NewRow();
                                for (int col = 1; col <= productMaster.Dimension.End.Column; col++)
                                {
                                    dataRow[col - 1] = productMaster.Cells[row, col].Text;
                                }
                                dt.Rows.Add(dataRow);
                            }

                            dgwDetails.DataSource = dt;
                            MessageBox.Show("Customers Imported Successfully! ✅", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("An Error Occured While Importing Data From Excel Sheet? 📊" + Ex, "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            LoadProducts();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query;
                TextBox customerID = new TextBox();
                if (MessageBox.Show("Do You Wanna Save Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (dgwDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgwDetails.Rows.Count; i++)
                        {
                            query = "Select f_customer_prefix from Customer_db";
                            con.GetId(query, customerID);
                            string str = "Select f_customer_id from Customer_db Order By ID Desc";
                            con.AutoNumber(str, customerID);
                            dgwDetails.Rows[i].Cells[0].Value = customerID.Text;

                            query = "insert into Customer_db (f_customer_id,f_customer_name,f_area,f_mobile_no) Values ('" + customerID.Text + "','" + dgwDetails.Rows[i].Cells[1].Value + "','" + dgwDetails.Rows[i].Cells[2].Value + "','" + dgwDetails.Rows[i].Cells[3].Value + "')";
                            con.SaveOrEditItems(query);
                        }
                        MessageBox.Show("Data Creation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show("Data Not Found?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        LoadProducts();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Data Creation?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void FRM_CUSTOMER_IMPORT_EXPORT_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Customer Name] LIKE '%{0}%'", txtSearch.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Record Found❔" + ex, "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}