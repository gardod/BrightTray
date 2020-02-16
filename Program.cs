using System;
using System.Threading;
using System.Windows.Forms;

namespace BrightTray
{
    static class Program
    {
        static Mutex mutex = new Mutex(true, "thread:/BrightTray");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // See if mutex gets available signal
            if (mutex.WaitOne(TimeSpan.FromSeconds(1), true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);				
                using (ProcessIcon pi = new ProcessIcon())
                {
                    // Show the system tray icon.	
                    pi.Display();
                    // Make sure the application runs!
                    Application.Run();
                }
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("BrightTray is already running!");
            }


        }
    }
}
