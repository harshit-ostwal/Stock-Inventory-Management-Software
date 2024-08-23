using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_VIEW_REPORTS : Form
    {
        ReportDocument report;
        string fileName = "";
        Components comp = new Components();

        public FRM_VIEW_REPORTS(ReportDocument report, string fileName)
        {
            InitializeComponent();
            this.report = report;
            this.fileName = fileName;
        }

        private void FRM_VIEW_REPORTS_Load(object sender, EventArgs e)
        {
            cryView.ReportSource = report;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int numberOfCopies = Convert.ToInt32(txtNoOfCopies.Value);
                if (numberOfCopies <= 0)
                {
                    MessageBox.Show("Please Enter No.Of.Copies. Do You Wanna Print? 📄", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                report.PrintToPrinter(numberOfCopies, false, 0, 0);
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Printing?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                    saveFileDialog.Title = "SS SOFTWARE";
                    saveFileDialog.FileName = fileName + ".pdf";

                    if (MessageBox.Show("Do You Want To Export As PDF? 📊", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, saveFileDialog.FileName);
                            MessageBox.Show("PDF Exported Successfully! ✅", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Exporting PDF?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
                    saveFileDialog.Title = "SS SOFTWARE";
                    saveFileDialog.FileName = fileName + ".xls";

                    if (MessageBox.Show("Do You Want To Export As Excel? 📊", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, saveFileDialog.FileName);
                            MessageBox.Show("Excel Exported Successfully! ✅", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Exporting Excel?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FRM_VIEW_REPORTS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
