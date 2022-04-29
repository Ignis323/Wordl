/*********************************************
 * Lingo
 * A simple word game
 * 
 * Benzi K. Ahamed
 * 2007
 * ******************************************/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lingo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the game
            Application.Run(new Lingo());
        }
    }
}