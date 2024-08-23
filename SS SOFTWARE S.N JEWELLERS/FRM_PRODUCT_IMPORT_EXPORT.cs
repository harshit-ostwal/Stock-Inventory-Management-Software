using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_PRODUCT_IMPORT_EXPORT : Form
    {
        readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";
        OleDbDataAdapter ad;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        Connection con = new Connection();
        Components comp = new Components();

        public FRM_PRODUCT_IMPORT_EXPORT()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            dt.Columns.Add("Product ID", typeof(string));
            dt.Columns.Add("Product Name", typeof(string));
            dt.Columns.Add("Product Category Name", typeof(string));
            dt.Columns.Add("Product Godown Name", typeof(string));
            dt.Columns.Add("Product Size No", typeof(string));
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("Printing Name", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt2.Columns.Add("Product ID", typeof(string));
            dt2.Columns.Add("Product Name", typeof(string));
            dt2.Columns.Add("Product Category ID", typeof(string));
            dt2.Columns.Add("Product Category Name", typeof(string));
            dt2.Columns.Add("Product Godown ID", typeof(string));
            dt2.Columns.Add("Product Godown Name", typeof(string));
            dgwDetails.DataSource = dt;
        }

        private void LoadProducts()
        {
            try
            {
                string query = "Select Product_db.f_product_id, Product_db.f_product_name,Product_db.f_product_category_name, Product_db.f_godown_name,Product_Items_db.f_product_size_no, Product_Items_db.f_barcode,Product_Items_db.f_printing_name, Product_Items_db.f_quantity From Product_db Inner Join Product_Items_db On Product_db.f_product_id = Product_Items_db.f_product_id";
                ad = new OleDbDataAdapter(query, Main);
                ds.Clear();
                ad.Fill(ds);
                dt.Rows.Clear();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    dt.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Loading Data?", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FRM_PRODUCT_IMPORT_EXPORT_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnDownloadFormat_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveFileDialog.Title = "SS SOFTWARE";
                saveFileDialog.FileName = "Products - SS SOFTWARE.xlsx";

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
                ExcelWorksheet productMaster = package.Workbook.Worksheets.Add("Product Master");

                for (int col = 1; col <= dt.Columns.Count; col++)
                {
                    productMaster.Cells[1, col].Value = dt.Columns[col - 1].ColumnName;

                    productMaster.Cells[1, col].Style.Font.Bold = true;
                    productMaster.Cells[1, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    if (col == 6 || col == 7)
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
                saveFileDialog.FileName = "Products - SS SOFTWARE.xlsx";

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
                            MessageBox.Show("Products Imported Successfully! ✅", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string productCategoryID;
                string godownID;
                string productSizeID;
                string Barcode;
                TextBox productName = new TextBox();
                ComboBox CategoryName = new ComboBox();
                ComboBox SizeNo = new ComboBox();
                bool isFirstOccurrence = true;
                string previousProductID = null;
                if (MessageBox.Show("Do You Wanna Save Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (dgwDetails.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgwDetails.Rows.Count; i++)
                        {
                            productName.Text = dgwDetails.Rows[i].Cells[1].Value.ToString();
                            CategoryName.Text = dgwDetails.Rows[i].Cells[2].Value.ToString();
                            SizeNo.Text = dgwDetails.Rows[i].Cells[4].Value.ToString();
                            if (dgwDetails.Rows[i].Cells[5].Value.ToString() == string.Empty)
                            {
                                Barcode = comp.GetBarcode(productName, CategoryName, SizeNo);
                                dgwDetails.Rows[i].Cells[5].Value = Barcode;
                            }
                            else
                            {
                                Barcode = dgwDetails.Rows[i].Cells[5].Value.ToString();
                            }
                            string printingName = dgwDetails.Rows[i].Cells[6].Value.ToString();
                            if (string.IsNullOrWhiteSpace(printingName))
                            {
                                dgwDetails.Rows[i].Cells[6].Value = productName.Text;
                            }
                            query = "Select f_product_category_id from Product_Category_db where f_product_category_name='" + dgwDetails.Rows[i].Cells[2].Value + "'";
                            productCategoryID = con.FetchData(query);
                            query = "Select f_godown_id from Godown_db where f_godown_name='" + dgwDetails.Rows[i].Cells[3].Value + "'";
                            godownID = con.FetchData(query);
                            query = "Select f_product_size_id from Product_Size_db where f_product_size_no='" + dgwDetails.Rows[i].Cells[4].Value + "'";
                            productSizeID = con.FetchData(query);

                            if (productCategoryID == string.Empty)
                            {
                                MessageBox.Show("Data Verification Failed. Please Review The Information Provided ❔\nIncorrect Category Name :- " + dgwDetails.Rows[i].Cells[2].Value, "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            if (godownID == string.Empty)
                            {
                                MessageBox.Show("Data Verification Failed. Please Review The Information Provided ❔\nIncorrect Godown Name :- " + dgwDetails.Rows[i].Cells[3].Value, "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            if (productSizeID == string.Empty)
                            {
                                MessageBox.Show("Data Verification Failed. Please Review The Information Provided ❔\nIncorrect Product Size No :- " + dgwDetails.Rows[i].Cells[4].Value, "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (Barcode != string.Empty && productCategoryID != string.Empty && godownID != string.Empty && productSizeID != string.Empty)
                            {
                                string productID = dgwDetails.Rows[i].Cells[0].Value.ToString();

                                if (isFirstOccurrence || productID != previousProductID)
                                {
                                    dt2.Rows.Add(dgwDetails.Rows[i].Cells[0].Value.ToString(), dgwDetails.Rows[i].Cells[1].Value.ToString(), productCategoryID, dgwDetails.Rows[i].Cells[2].Value.ToString(), godownID, dgwDetails.Rows[i].Cells[3].Value.ToString());
                                    previousProductID = productID;
                                    isFirstOccurrence = false;
                                }
                                query = "insert into Product_Items_db (f_product_id,f_product_name,f_product_size_id,f_product_size_no,f_barcode,f_printing_name,f_quantity) Values ('" + dgwDetails.Rows[i].Cells[0].Value + "','" + dgwDetails.Rows[i].Cells[1].Value + "','" + productSizeID + "','" + dgwDetails.Rows[i].Cells[4].Value + "','" + Barcode + "','" + dgwDetails.Rows[i].Cells[6].Value + "','" + dgwDetails.Rows[i].Cells[7].Value + "')";
                                con.SaveOrEditItems(query);
                            }
                        }
                        foreach (DataRow dr in dt2.Rows)
                        {
                            query = "insert into Product_db (f_product_id,f_product_name,f_product_category_id,f_product_category_name,f_godown_id,f_godown_name) Values ('" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + dr[4] + "','" + dr[5] + "')";
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

        private void FRM_PRODUCT_IMPORT_EXPORT_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("[Product Name] LIKE '%{0}%'", txtSearch.Text);
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}