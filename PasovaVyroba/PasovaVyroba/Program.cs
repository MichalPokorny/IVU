using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PasovaVyroba
{
    static class Program
    {
        static private Model model = null;
        static public Model Model
        {
            get
            {
                if (model == null)
                {
                    model = new Model();
                    model.Load();
                }
                return model;
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            Model.StopAllThreads();
        }
    }
}
