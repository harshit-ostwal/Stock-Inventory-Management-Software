﻿using Bunifu.UI.WinForms.BunifuTextbox;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    class Connection
    {
        // Main Database Path File Location Access Database
        public readonly string Main = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Main_db.accdb;Jet OLEDB:Database Password=SS9975";

        // Settings Database Path File Location Access Database
        public readonly string Settings = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source =" + Application.StartupPath + "/DATABASE/Settings_db.accdb;Jet OLEDB:Database Password=SS9975";

        // Initialize The Connection
        public OleDbCommand cmd = new OleDbCommand();
        public DataSet ds = new DataSet();

        public async void Login(BunifuTextBox txtUserName, BunifuTextBox txtPassword, NotifyIcon notifyIcon, Form Login)
        {
            string query = "Select f_user_name,f_password FROM Login_db Where f_user_name = @username AND f_password = @password";

            using (OleDbConnection con = new OleDbConnection(Settings))
            {
                try
                {
                    await con.OpenAsync();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                        using (OleDbDataReader dr = (OleDbDataReader)await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                notifyIcon.ShowBalloonTip(100);
                                MessageBox.Show("SignIn Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Login.Hide();
                                FRM_HOME Home = new FRM_HOME();
                                Home.Show();
                                return;
                            }
                        }
                    }
                    con.Close();
                }
                catch (Exception err)
                {
                    MessageBox.Show("An Error Occured While SignIn❔👎" + err, "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            MessageBox.Show("Incorrect User Name/Password❔👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public async void GetId(string query, TextBox txtBox)
        {
            using (OleDbConnection con = new OleDbConnection(Settings))
            {
                try
                {
                    await con.OpenAsync();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        using (OleDbDataReader dr = (OleDbDataReader)await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                txtBox.Text = dr.GetValue(0).ToString();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public async void AutoNumber(string query, TextBox txtBox)
        {
            string prefixLen = "select f_prefix_length from Admin_db";
            int length = Convert.ToInt32(FetchAdminData(prefixLen));
            using (OleDbConnection con = new OleDbConnection(Main))
            {
                try
                {
                    await con.OpenAsync();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        var maxid = await cmd.ExecuteScalarAsync() as string;
                        if (maxid == null)
                        {
                            txtBox.Text = txtBox.Text + "0001";
                        }
                        else
                        {
                            double i = double.Parse(maxid.Substring(length));
                            i++;
                            txtBox.Text = string.Format("" + txtBox.Text + "" + "{0:0000}", i++);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public async void GetItems(string query, ComboBox cmbBoxes)
        {
            using (OleDbConnection con = new OleDbConnection(Main))
            {
                try
                {
                    await con.OpenAsync();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        using (OleDbDataReader dr = (OleDbDataReader)await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                cmbBoxes.Items.Add(dr[0].ToString());
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public string FetchData(string query)
        {
            using (OleDbConnection con = new OleDbConnection(Main))
            {
                try
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        object data = cmd.ExecuteScalar();
                        return data != null ? data.ToString() : string.Empty;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return string.Empty;
                }
            }
        }

        public string FetchAdminData(string query)
        {
            using (OleDbConnection con = new OleDbConnection(Settings))
            {
                try
                {
                    con.Open();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        object data = cmd.ExecuteScalar();
                        return data != null ? data.ToString() : string.Empty;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return string.Empty;
                }
            }
        }

        public async void FetchAdminData(Control[] controls, string query)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(Settings))
                {
                    await con.OpenAsync();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        using (OleDbDataReader dr = (OleDbDataReader)await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                for (int i = 0; i < controls.Length; i++)
                                {
                                    controls[i].Text = dr[i].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void FetchData(Control[] controls, string query)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(Main))
                {
                    await con.OpenAsync();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        using (OleDbDataReader dr = (OleDbDataReader)await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                for (int i = 0; i < controls.Length; i++)
                                {
                                    controls[i].Text = dr[i].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void SaveOrEditItems(string query)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(Main))
                {
                    await con.OpenAsync();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void AdminSaveOrEdit(string query)
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(Settings))
                {
                    await con.OpenAsync();
                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured ?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void GetData(string query, DataGridView dgw, string[] headerText)
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter(query, Main);
            await Task.Run(() => ad.Fill(ds));
            dgw.DataSource = ds.Tables[0];
            for (int i = 0; i < dgw.Columns.Count; i++)
            {
                dgw.Columns[i].HeaderText = headerText[i];
            }
        }

        public void BackUp()
        {
            string ThisDB = Application.StartupPath + "\\DATABASE\\Main_db.accdb";
            string SP = Application.StartupPath + "\\BACKUP\\";
            string Destitnation = SP + "\\Main_db " + DateTime.Now.ToString(" dd-MM-yyyy hh-mm-ss") + ".bak";
            File.Copy(ThisDB, Destitnation);
        }

        public bool SaveData(string query, string validate)
        {
            try
            {
                if (MessageBox.Show("Do You Wanna Save Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using (OleDbConnection con = new OleDbConnection(Main))
                    {
                        con.Open();
                        using (OleDbCommand cmd = new OleDbCommand(validate, con))
                        {
                            int Count = Convert.ToInt32(cmd.ExecuteScalar());
                            if (Count > 1)
                            {
                                MessageBox.Show("Data Already Exist?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (MessageBox.Show("Do You Wanna Save Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                {
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    MessageBox.Show("Data Creation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                                con.Close();
                                MessageBox.Show("Data Creation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Data Creation?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool EditData(string query, string validate)
        {
            try
            {
                if (MessageBox.Show("Do You Wanna Update Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (OleDbConnection con = new OleDbConnection(Main))
                    {
                        con.Open();
                        using (OleDbCommand cmd = new OleDbCommand(validate, con))
                        {
                            int Count = Convert.ToInt32(cmd.ExecuteScalar());
                            if (Count > 1)
                            {
                                MessageBox.Show("Data Already Exist?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (MessageBox.Show("Do You Wanna Update Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    cmd.CommandText = query;
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    MessageBox.Show("Data Updation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return true;
                                }
                            }
                            else
                            {
                                cmd.CommandText = query;
                                cmd.ExecuteNonQuery();
                                con.Close();
                                MessageBox.Show("Data Updation Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Data Updation?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteData(string query, DataGridView dgw)
        {
            try
            {
                if (dgw.Rows.Count > 0 && dgw.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Do You Wanna Delete Data❔", "SS SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (OleDbConnection con = new OleDbConnection(Main))
                        {
                            con.Open();
                            using (OleDbCommand cmd = new OleDbCommand(query, con))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            con.Close();
                            MessageBox.Show("Data Deletion Successfull👍", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Data For Deletion?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured While Data Deletion?👎", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}