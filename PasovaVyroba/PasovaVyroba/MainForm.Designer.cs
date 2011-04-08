namespace PasovaVyroba
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.úpravyVýrobkůToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nastaveníToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zobrazovatČíslaKřižovatekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationControl1 = new PasovaVyroba.SimulationControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.úpravyVýrobkůToolStripMenuItem,
            this.simulaceToolStripMenuItem,
            this.nastaveníToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1240, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // úpravyVýrobkůToolStripMenuItem
            // 
            this.úpravyVýrobkůToolStripMenuItem.Name = "úpravyVýrobkůToolStripMenuItem";
            this.úpravyVýrobkůToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.úpravyVýrobkůToolStripMenuItem.Text = "Úpravy výrobků";
            this.úpravyVýrobkůToolStripMenuItem.Click += new System.EventHandler(this.úpravyVýrobkůToolStripMenuItem_Click);
            // 
            // simulaceToolStripMenuItem
            // 
            this.simulaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem});
            this.simulaceToolStripMenuItem.Name = "simulaceToolStripMenuItem";
            this.simulaceToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.simulaceToolStripMenuItem.Text = "Simulace";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // nastaveníToolStripMenuItem
            // 
            this.nastaveníToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zobrazovatČíslaKřižovatekToolStripMenuItem});
            this.nastaveníToolStripMenuItem.Name = "nastaveníToolStripMenuItem";
            this.nastaveníToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.nastaveníToolStripMenuItem.Text = "Nastavení";
            // 
            // zobrazovatČíslaKřižovatekToolStripMenuItem
            // 
            this.zobrazovatČíslaKřižovatekToolStripMenuItem.CheckOnClick = true;
            this.zobrazovatČíslaKřižovatekToolStripMenuItem.Name = "zobrazovatČíslaKřižovatekToolStripMenuItem";
            this.zobrazovatČíslaKřižovatekToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.zobrazovatČíslaKřižovatekToolStripMenuItem.Text = "Zobrazovat čísla křižovatek";
            // 
            // simulationControl1
            // 
            this.simulationControl1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.simulationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simulationControl1.Location = new System.Drawing.Point(0, 24);
            this.simulationControl1.Name = "simulationControl1";
            this.simulationControl1.ShowNodes = false;
            this.simulationControl1.Size = new System.Drawing.Size(1240, 643);
            this.simulationControl1.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 667);
            this.Controls.Add(this.simulationControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Pásová výroba";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem úpravyVýrobkůToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private SimulationControl simulationControl1;
        private System.Windows.Forms.ToolStripMenuItem nastaveníToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zobrazovatČíslaKřižovatekToolStripMenuItem;
    }
}

