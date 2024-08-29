using System;
using System.Data;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    public partial class FRM_GODOWN_MASTER : Form
    {

        Components comp = new Components();
        Connection con = new Connection();
        string query;
        string validate;
        int i = 0;

        public FRM_GODOWN_MASTER()
        {
            InitializeComponent();
        }

        private void AutoNumber()
        {
            string query = "Select f_godown_master from Master_Prefix_db";
            con.GetId(query, txtGodownId);

            string str = "Select f_godown_id from Godown_db Order By ID Desc";
            con.AutoNumber(str, txtGodownId);
        }

        private void FRM_GODOWN_MASTER_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            comp.Minimize(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            comp.Close(this);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            Control[] boxes = { txtGodownName };
            comp.Clear(boxes);
            grpGodown.Text = "Create";
            btnSave.Text = "F2 Save";
            dgwDetails.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Control[] textBoxes = { txtGodownName };

            if (comp.validateControls(textBoxes))
            {
                if (btnSave.Text == "F2 Save" && grpGodown.Text == "Create")
                {
                    validate = "Select * from Godown_db where f_godown_name ='" + txtGodownName.Text + "'";
                    query = "Insert into Godown_db (f_godown_id,f_godown_name) values ('" + txtGodownId.Text + "','" + txtGodownName.Text + "')";
                    con.SaveData(query, validate);
                    ClearAll();
                    AutoNumber();
                }
                else if (btnSave.Text == "F2 Update" && grpGodown.Text == "Update")
                {
                    validate = "Select * from Godown_db where f_godown_name ='" + txtGodownName.Text + "'";
                    query = "Update Godown_db set f_godown_id ='" + txtGodownId.Text + "',f_godown_name='" + txtGodownName.Text + "' where ID=" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                    con.EditData(query, validate);
                    query = "Update Product_db set f_godown_id ='" + txtGodownId.Text + "',f_godown_name='" + txtGodownName.Text + "' where f_godown_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    query = "Update Purchase_Items_db set f_godown_id ='" + txtGodownId.Text + "',f_godown_name='" + txtGodownName.Text + "' where f_godown_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    query = "Update Sales_Items_db set f_godown_id ='" + txtGodownId.Text + "',f_godown_name='" + txtGodownName.Text + "' where f_godown_id='" + dgwDetails.SelectedRows[i].Cells[1].Value + "'";
                    con.SaveOrEditItems(query);
                    ClearAll();
                    AutoNumber();
                    btnSave.Text = "F2 Save";
                    grpGodown.Text = "Create";
                }
                else if (grpGodown.Text == "Create")
                {
                    btnSave.Text = "F2 Save";
                }
                else if (grpGodown.Text == "Edit")
                {
                    MessageBox.Show("Please Select A Data❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearAll();
                }
                else if (grpGodown.Text == "View")
                {
                    MessageBox.Show("You Are In The View Mode❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearAll();
                }
                else
                {
                    btnSave.Text = "F2 Save";
                    ClearAll();
                }
            }
        }

        private void displayData()
        {
            query = "Select ID,f_godown_id,f_godown_name from Godown_db Order By ID Desc";
            string[] headerText = { "ID", "Godown ID", "Godown Name" };
            con.GetData(query, dgwDetails, headerText);
            dgwDetails.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grpGodown.Text == "Update" || grpGodown.Text == "View")
            {
                query = "Delete From Godown_db Where ID =" + dgwDetails.SelectedRows[i].Cells[0].Value.ToString() + "";
                con.DeleteData(query, dgwDetails);
            }
            ClearAll();
            displayData();
            AutoNumber();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            grpGodown.Text = "View";
            if (grpGodown.Text == "View")
            {
                ClearAll();
                grpGodown.Text = "View";
                displayData();
                AutoNumber();
            }
            else
            {
                ClearAll();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            grpGodown.Text = "Edit";
            if (grpGodown.Text == "Edit")
            {
                ClearAll();
                grpGodown.Text = "Edit";
                displayData();
            }
            else
            {
                ClearAll();
            }
        }

        private void dgwDetails_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Control[] txtBoxes = { txtGodownId, txtGodownName };
            if (comp.getDataDgwToTextBox(dgwDetails, txtBoxes))
            {
                if (grpGodown.Text == "Edit")
                {
                    btnSave.Text = "F2 Update";
                    grpGodown.Text = "Update";
                    txtGodownName.Focus();
                    dgwDetails.Hide();
                }
                else if (grpGodown.Text == "View")
                {
                    txtGodownName.Focus();
                    dgwDetails.Hide();
                }
            }
            else
            {
                ClearAll();
            }
        }

        private void dgwDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (grpGodown.Text == "Edit")
                {
                    Control[] txtBoxes = { txtGodownId, txtGodownName };
                    comp.getDataDgwToTextBox(dgwDetails, txtBoxes);
                    btnSave.Text = "F2 Update";
                    grpGodown.Text = "Update";
                    dgwDetails.Hide();
                    txtGodownName.Focus();
                }
                else if (grpGodown.Text == "View")
                {
                    Control[] txtBoxes = { txtGodownId, txtGodownName };
                    comp.getDataDgwToTextBox(dgwDetails, txtBoxes);
                    dgwDetails.Hide();
                    txtGodownName.Focus();
                }
                else
                {
                    ClearAll();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                ClearAll();
            }
        }

        private void txtGodownName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgwDetails.Visible && txtGodownName.Text != "")
                {
                    dgwDetails.Focus();
                }
                else if (!dgwDetails.Visible)
                {
                    dgwDetails.Hide();
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    dgwDetails.Hide();
                    SendKeys.Send("{TAB}");
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txtGodownName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (grpGodown.Text == "Edit" || grpGodown.Text == "View" && dgwDetails.Visible == true)
                {
                    (dgwDetails.DataSource as DataTable).DefaultView.RowFilter = string.Format("f_godown_name LIKE '%{0}%'", txtGodownName.Text);
                }
            }
            catch
            {
                MessageBox.Show("No Record Found❔", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FRM_GODOWN_MASTER_KeyDown(object sender, KeyEventArgs e)
        {
            comp.Close(this, e);
            comp.ShortcutKey(Keys.F1, btnNew, e);
            comp.ShortcutKey(Keys.F2, btnSave, e);
            comp.ShortcutKey(Keys.F3, btnEdit, e);
            comp.ShortcutKey(Keys.F4, btnDelete, e);
            comp.ShortcutKey(Keys.F5, btnView, e);
        }
    }
}
