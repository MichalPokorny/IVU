using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PasovaVyroba
{
    public partial class MainForm : Form
    {
        private Model Model
        {
            get { return Program.Model; }
        }

        public MainForm()
        {
            InitializeComponent();
            zobrazovatČíslaKřižovatekToolStripMenuItem.CheckedChanged += new EventHandler(zobrazovatČíslaKřižovatekToolStripMenuItem_CheckedChanged);
        }

        void zobrazovatČíslaKřižovatekToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            simulationControl1.ShowNodes = zobrazovatČíslaKřižovatekToolStripMenuItem.Checked;
        }

        private void úpravyVýrobkůToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new ProductEditationForm()).ShowDialog();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Model.StartPositionUpdater();
        }
    }
}
