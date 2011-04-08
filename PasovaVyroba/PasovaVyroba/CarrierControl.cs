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
    public partial class CarrierControl : UserControl
    {
        private Model Model
        {
            get { return Program.Model; }
        }

        public CarrierControl()
        {
            InitializeComponent();
            contents.DataBindings.Add("Text", Model, "CarrierContents");
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle myRect = new Rectangle(1, 1, Width - 2, Height - 2);
            g.FillRectangle(Brushes.Cornsilk, myRect);
            //g.FillRectangle(SystemBrushes.ControlLightLight, myRect);

            Pen pen = new Pen(SystemBrushes.ControlDark, 1.0f);
            g.DrawRectangle(pen, myRect);
            myRect = new Rectangle(2, 2, Width - 4, Height - 4);
            pen = new Pen(SystemBrushes.ControlLight, 1.0f);
            g.DrawRectangle(pen, myRect);

        }
    }
}
