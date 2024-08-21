using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_HOME : Form
    {

        Components comp = new Components();
        Connection con = new Connection();

        public FRM_HOME()
        {
            InitializeComponent();
        }

        private void dateTimer_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss ");
        }

        private void FRM_HOME_Load(object sender, EventArgs e)
        {
            dateTimer.Start();
            string query = "Select f_company_name from Company_db";
            lblCompanyName.Text = con.FetchData(query);
            string query1 = "Select f_email_id from Company_db";
            lblEmail.Text = con.FetchData(query1);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_SETTINGS());
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            TogglePanelVisibility(pnlTransactions);
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            TogglePanelVisibility(pnlRecords);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            TogglePanelVisibility(pnlReports);
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_BARCODE_PRINTING());
            HideAllPanels();
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_CALCULATOR());
            HideAllPanels();
        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
            TogglePanelVisibility(pnlMaster);
        }

        private void TogglePanelVisibility(Panel panel)
        {
            if (panel.Visible)
            {
                panel.Visible = false;
            }
            else
            {
                HideAllPanels();
                panel.Visible = true;
            }
        }

        private void HideAllPanels()
        {
            pnlTransactions.Visible = false;
            pnlRecords.Visible = false;
            pnlReports.Visible = false;
            pnlImportExport.Visible = false;
            pnlMaster.Visible = false;
        }

        private void OpenForm(Form frm)
        {
            frm.ShowDialog();
        }

        private void btnSizeMaster_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_PRODUCT_SIZE_MASTER());
        }

        private void btnSupplierMaster_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_SUPPLIER_MASTER());
        }

        private void btnProductMaster_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_PRODUCT_MASTER());
        }

        private void btnCategoryMaster_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_PRODUCT_CATEGORY_MASTER());
        }

        private void btnGodownMaster_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_GODOWN_MASTER());
        }

        private void btnCustomerMaster_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_CUSTOMER_MASTER());
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_SALES());
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_PURCHASE());
        }

        private void btnStockInHand_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_STOCK_IN_HAND());
        }

        private void btnCustomerReport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_CUSTOMER_REPORT());
        }

        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_SALES_REPORT());
        }

        private void btnStockReport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_STOCK_IN_OUT_REPORT());
        }

        private void btnStockTransferReport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_STOCK_TRANSFER_REPORT());
        }

        private void btnSupplierReport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_SUPPLIER_REPORT());
        }

        private void btnPurchaseReport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_PURCHASE_REPORT());
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            comp.ExitApplication();
        }

        private void btnCompanyMaster_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_COMPANY_MASTER());
        }

        private void FRM_HOME_KeyDown(object sender, KeyEventArgs e)
        {
            comp.ExitApplication(this, e);
            comp.ShortcutKey(Keys.F1, bunifuButton3, e);
            comp.ShortcutKey(Keys.F2, bunifuButton2, e);
            comp.ShortcutKey(Keys.F3, bunifuButton1, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            comp.ExitApplication();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            FRM_LOGIN Login = new FRM_LOGIN();
            Login.Show();
        }

        private void btnStockTransfer_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_STOCK_TRANSFER());
        }

        private void btnImportExport_Click(object sender, EventArgs e)
        {
            TogglePanelVisibility(pnlImportExport);
        }

        private void btnProductImportExport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_PRODUCT_IMPORT_EXPORT());
        }

        private void btnSupplierImportExport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_SUPPLIER_IMPORT_EXPORT());
        }

        private void btnCustomerImportExport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_CUSTOMER_IMPORT_EXPORT());
        }

        private void btnSalesWiseReport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_SALES_WISE_REPORT());
        }

        private void btnPurchaseWiseReport_Click(object sender, EventArgs e)
        {
            OpenForm(new FRM_PURCHASE_WISE_REPORT());
        }
    }
}
