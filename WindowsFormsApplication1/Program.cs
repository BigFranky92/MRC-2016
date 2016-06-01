using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EmailSender.sendEmail(1, 1, 1, "mrc.gruppo@libero.it");
            EmailSender.sendEmail(1, 1, 1, "fabiopalumbo@msn.com");
            Application.Run(new Form1());
        }
    }
}
