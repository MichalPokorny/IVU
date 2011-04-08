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
    public partial class SimulationControl : UserControl
    {
        private Model Model { get { return Program.Model; } }
        private CarrierControl CarrierControl;
        private StartControl StartControl;
        private EndControl[] EndControls;

        private bool showNodes;
        public bool ShowNodes
        {
            get { return showNodes; }
            set {
                showNodes = value;
                if (Created)
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        Refresh();
                    }));
                }
            }
        }

        public SimulationControl()
        {
            InitializeComponent();
            InitializeWorkplaceComponents();
            addProductButton.DataBindings.Add("Enabled", Model, "StartIsEmpty");

            productSelection.DisplayMember = "FullText";
            productSelection.DataSource = Model.Products;

            Model.LogHandler += new LogHandler(Model_LogHandler);

            showNodes = false;
            background = new SolidBrush(Color.LightGray);
        }

        void Model_LogHandler(string text)
        {
            this.Invoke(new MethodInvoker(()=>
            {
                this.logListBox.Items.Add(text);
                this.logListBox.Invalidate();

                List<string> items = new List<string>();
                foreach (string s in logListBox.Items)
                {
                    items.Add(s);
                }

                while (items.Count > 15) items.RemoveAt(0);
                this.logListBox.Items.Clear();
                foreach (string s in items) logListBox.Items.Add(s);
            }));
        }

        public void InitializeWorkplaceComponents()
        {
            foreach (Workplace w in Model.Workplaces)
            {
                WorkplaceControl wc = new WorkplaceControl(w);
                wc.Parent = this.workPanel;
                wc.Width = 90;
                wc.Height = 90;
                wc.Location = new Point(w.Location.X - wc.Width / 2, w.Location.Y - wc.Height / 2);
                this.workPanel.Controls.Add(wc);
                wc.Show();
            }

            StartControl = new StartControl();
            StartControl.Parent = this.workPanel;
            StartControl.Width = 70;
            StartControl.Height = 70;
            StartControl.Location = 
                new Point(Model.StartLocation.X - StartControl.Width/2,
                    Model.StartLocation.Y - StartControl.Height/2);
            StartControl.Show();

            EndControls = new EndControl[Model.EndCount];
            for (int i = 0; i < Model.EndCount; i++)
            {
                EndControls[i] = new EndControl(i);
                EndControls[i].Parent = this.workPanel;
                EndControls[i].Width = 70;
                EndControls[i].Height = 70;
                EndControls[i].Location = 
                    new Point(Model.EndPoints[i].Location.X - EndControls[i].Width/2,
                        Model.EndPoints[i].Location.Y - EndControls[i].Height /2);
            }

            foreach (Swapspace swapspace in Model.Swapspaces)
            {
                SwapspaceControl swap = new SwapspaceControl(swapspace);
                swap.Width = 90; swap.Height = 90;
                swap.Parent = this.workPanel; swap.Location = new Point(swapspace.Location.X - swap.Width / 2, swapspace.Location.Y - swap.Height / 2);
                this.workPanel.Controls.Add(swap);
                swap.Show();
            }

            CarrierControl = new CarrierControl();
            CarrierControl.Parent = this.workPanel;
            CarrierControl.Width = 50;
            CarrierControl.Height = 50;
            CarrierControl.Location = new Point(Model.CarrierPositionP.X - CarrierControl.Width / 2, Model.CarrierPositionP.Y - CarrierControl.Height / 2);
            CarrierControl.Show();
            Model.CarrierPositionChanged += new EventHandler(CarrierPositionChanged);
        }

        private void CarrierPositionChanged(object sender, EventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                CarrierControl.Location = 
                    new Point(Model.CarrierPositionP.X-CarrierControl.Width/2,
                        Model.CarrierPositionP.Y-CarrierControl.Height/2);
            }));
        }

        Brush background;

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //g.FillRectangle(background, ClientRectangle);

            foreach (PathEdge edge in Model.PathEdges)
            {
                TextureBrush b = new TextureBrush(Properties.Resources.kolej);
                Matrix mtx = b.Transform;
                mtx.Translate(edge.B.Location.X, edge.B.Location.Y);
                mtx.Rotate(90.0f + (float)((edge.Angle) * (180.0 / Math.PI)));
                b.Transform = mtx;
                Pen p = new Pen(b, 20);
                g.DrawLine(p, edge.A.Location, edge.B.Location);
            }
            int i = 0;
            if (ShowNodes)
            {
                foreach (PathNode node in Model.PathNodes)
                {
                    //g.FillEllipse(Brushes.DarkGray, node.Location.X - 5, node.Location.Y - 5, 10, 10);
                    //g.DrawEllipse(Pens.Black, node.Location.X - 5, node.Location.Y - 5, 10, 10);
                    g.DrawString((i++).ToString(), new Font(DefaultFont, FontStyle.Bold), Brushes.LemonChiffon, node.Location);
                }
            }
        }

        private void addProductButton_Click(object sender, EventArgs e)
        {
            Model.AddToStart((Product)productSelection.SelectedItem, (int)productCount.Value);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
