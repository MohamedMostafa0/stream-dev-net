using System;
using System.Windows.Forms;
using MouseDev_Server_Socket.Forms;

namespace MouseDev_Server_Socket
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.ProcessExit += OnExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void OnExit(object sender, EventArgs e)
        {
            //TODO release
        }
    }
}
