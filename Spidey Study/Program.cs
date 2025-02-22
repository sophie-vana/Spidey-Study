using SpiderManPomodoroApp;
using System;
using System.Windows.Forms;

namespace Spidey_Study
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread] // Marks the thread as single-threaded apartment (required for WinForms)
        static void Main()
        {
            Application.EnableVisualStyles(); // Enables visual styles for controls (modern appearance)
            Application.SetCompatibleTextRenderingDefault(false); // Sets the default text rendering to GDI+
            Application.Run(new Form1()); // Starts the application and opens the main form (Form1)
        }
    }
}
