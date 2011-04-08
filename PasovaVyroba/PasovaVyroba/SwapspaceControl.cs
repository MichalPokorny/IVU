using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PasovaVyroba
{
    public partial class SwapspaceControl : UserControl
    {
        private Swapspace _swapspace;
        private Swapspace Swapspace { get { return _swapspace; } set { _swapspace = value; } }
        private Model Model { get { return Program.Model; } }

        public SwapspaceControl(Swapspace swapspace)
        {
            Swapspace = swapspace;
            InitializeComponent();
            this.DoubleBuffered = true;
            productLabel.DataBindings.Add("Text", Swapspace, "Contents");
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle myRect = new Rectangle(1, 1, Width - 2, Height - 2);
            g.FillRectangle(Brushes.Coral, myRect);

            Pen pen = new Pen(SystemBrushes.ControlDark, 1.0f);
            g.DrawRectangle(pen, myRect);
            myRect = new Rectangle(2, 2, Width - 4, Height - 4);
            pen = new Pen(SystemBrushes.ControlLight, 1.0f);
            g.DrawRectangle(pen, myRect);
        }
        /*
        private void WorkplaceControl_Click(object sender, EventArgs e)
        {
            Workplace.Start = DateTime.Now;
            Workplace.State = WorkplaceState.Working;
            Workplace.TotalTime = TimeSpan.FromSeconds(10);
            Model.StartWorkplaceThread(Workplace);
        }
         */
    }
}
