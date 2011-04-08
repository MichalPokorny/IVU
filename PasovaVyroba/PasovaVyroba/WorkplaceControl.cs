using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PasovaVyroba
{
    public partial class WorkplaceControl : UserControl
    {
        private Workplace _workplace;
        private Workplace Workplace { get { return _workplace; } set { _workplace = value; } }
        private Model Model { get { return Program.Model; } }

        public WorkplaceControl(Workplace workplace)
        {
            Workplace = workplace;
            Workplace.Updated += new EventHandler((a, b) => { this.Invoke(new MethodInvoker(() => { this.Rebind(); })); });
            Workplace.Finished += new EventHandler((a, b) => { this.Invoke(new MethodInvoker(() => { this.Rebind(); })); });
            InitializeComponent();
            this.DoubleBuffered = true;
            Rebind();
            workplaceName.DataBindings.Add("Text", Workplace, "Identification");
            productLabel.DataBindings.Add("Text", Workplace, "Contents");
        }

        private void Rebind()
        {
            int value = (int)(Workplace.Progress * this.progressBar1.Maximum);
            this.progressBar1.Value = (value>this.progressBar1.Maximum)?(this.progressBar1.Maximum):(value);
            Bitmap bmp = null;
            switch (Workplace.State)
            {
                case WorkplaceState.Working:
                    bmp = Properties.Resources.application_x_executable;
                    break;
                case WorkplaceState.NoJob:
                    bmp = Properties.Resources.appointment_new;
                    break;
            }
            icon.Image = bmp;
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle myRect = new Rectangle(1, 1, Width - 2, Height - 2);

            Brush b = null;

            switch (Workplace.State)
            {
                case WorkplaceState.Working:
                    b = Brushes.Yellow;
                    break;
                case WorkplaceState.NoJob:
                    b = SystemBrushes.ControlLightLight;
                    break;
            }
            g.FillRectangle(b, myRect);


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
