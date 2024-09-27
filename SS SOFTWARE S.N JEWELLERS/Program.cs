using System;
using System.Threading;
using System.Windows.Forms;

namespace SS_SOFTWARE_S.N_JEWELLERS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new System.Threading.Mutex(false, "SS SOFTWARE");
            try
            {
                if (mutex.WaitOne(0, false))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    DateTime dt1 = DateTime.Now;
                    DateTime dt2 = DateTime.Parse("01/04/2025");
                    if (dt1.Date >= dt2.Date)
                    {
                        MessageBox.Show("🔒 Application License Expired 🔒,\n🔄 Please Renew Your License to Continue. 🔄", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Application.Run(new FRM_LOGIN());
                    }
                }
                else
                {
                    MessageBox.Show("SS SOFTWARE IS ALREADY RUNNING!!!", "SS SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.Close();
                    mutex = null;
                }
            }
        }
    }
}
