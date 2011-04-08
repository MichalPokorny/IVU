namespace PasovaVyroba
{
    partial class StartControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.contents = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // contents
            // 
            this.contents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contents.Location = new System.Drawing.Point(0, 0);
            this.contents.Name = "contents";
            this.contents.Size = new System.Drawing.Size(150, 150);
            this.contents.TabIndex = 0;
            this.contents.Text = "label1";
            this.contents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.contents);
            this.Name = "StartControl";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label contents;
    }
}
