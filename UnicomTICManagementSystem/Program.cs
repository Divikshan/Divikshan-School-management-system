using System;
using System.Windows.Forms;
using UnicomTICManagementSystem.Utilities; // ✅ இதை மேல Add பண்ணணும்

namespace UnicomTICManagementSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ Database create + tables insert பண்ண இது முக்கியம்
            DatabaseManager.InitializeDatabase();

            Application.Run(new Views.LoginForm());
        }
    }
}
