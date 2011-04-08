namespace PasovaVyroba
{
    partial class ProductEditationForm
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
            System.Windows.Forms.Button button1;
            System.Windows.Forms.Button button3;
            System.Windows.Forms.Button button4;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.productListBox = new System.Windows.Forms.ListBox();
            this.actionListBox = new System.Windows.Forms.ListBox();
            this.cas = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.jmeno = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cislo = new System.Windows.Forms.TextBox();
            this.poradi = new System.Windows.Forms.NumericUpDown();
            this.stanoviste = new System.Windows.Forms.NumericUpDown();
            button1 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.poradi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stanoviste)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            button1.Dock = System.Windows.Forms.DockStyle.Fill;
            button1.Location = new System.Drawing.Point(3, 383);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(140, 23);
            button1.TabIndex = 2;
            button1.Text = "Přidat";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            button3.Dock = System.Windows.Forms.DockStyle.Fill;
            button3.Location = new System.Drawing.Point(295, 383);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(140, 23);
            button3.TabIndex = 4;
            button3.Text = "Přidat";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            button4.Dock = System.Windows.Forms.DockStyle.Fill;
            button4.Location = new System.Drawing.Point(441, 383);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(141, 23);
            button4.TabIndex = 5;
            button4.Text = "Odebrat";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(295, 409);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(140, 26);
            label1.TabIndex = 6;
            label1.Text = "Pořadí:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = System.Windows.Forms.DockStyle.Fill;
            label2.Location = new System.Drawing.Point(295, 435);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(140, 26);
            label2.TabIndex = 7;
            label2.Text = "Stanoviště:";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = System.Windows.Forms.DockStyle.Fill;
            label3.Location = new System.Drawing.Point(295, 461);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(140, 26);
            label3.TabIndex = 8;
            label3.Text = "Čas:";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.productListBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.actionListBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(button3, 2, 1);
            this.tableLayoutPanel1.Controls.Add(button4, 3, 1);
            this.tableLayoutPanel1.Controls.Add(label1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(label2, 2, 3);
            this.tableLayoutPanel1.Controls.Add(label3, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.poradi, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.stanoviste, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.cas, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(button1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.jmeno, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cislo, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(585, 487);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // productListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.productListBox, 2);
            this.productListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productListBox.FormattingEnabled = true;
            this.productListBox.Location = new System.Drawing.Point(3, 3);
            this.productListBox.Name = "productListBox";
            this.productListBox.Size = new System.Drawing.Size(286, 374);
            this.productListBox.TabIndex = 0;
            this.productListBox.SelectedIndexChanged += new System.EventHandler(this.productListBox_SelectedIndexChanged);
            // 
            // actionListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.actionListBox, 2);
            this.actionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionListBox.FormattingEnabled = true;
            this.actionListBox.Location = new System.Drawing.Point(295, 3);
            this.actionListBox.Name = "actionListBox";
            this.actionListBox.Size = new System.Drawing.Size(287, 374);
            this.actionListBox.TabIndex = 1;
            this.actionListBox.SelectedIndexChanged += new System.EventHandler(this.actionListBox_SelectedIndexChanged);
            // 
            // cas
            // 
            this.cas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cas.Location = new System.Drawing.Point(441, 464);
            this.cas.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.cas.Name = "cas";
            this.cas.Size = new System.Drawing.Size(141, 20);
            this.cas.TabIndex = 11;
            this.cas.ValueChanged += new System.EventHandler(this.cas_ValueChanged);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(149, 383);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Odebrat";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 409);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 26);
            this.label4.TabIndex = 12;
            this.label4.Text = "Jméno:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // jmeno
            // 
            this.jmeno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jmeno.Location = new System.Drawing.Point(149, 412);
            this.jmeno.Name = "jmeno";
            this.jmeno.Size = new System.Drawing.Size(140, 20);
            this.jmeno.TabIndex = 13;
            this.jmeno.TextChanged += new System.EventHandler(this.jmeno_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 435);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 26);
            this.label5.TabIndex = 14;
            this.label5.Text = "Číslo:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cislo
            // 
            this.cislo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cislo.Location = new System.Drawing.Point(149, 438);
            this.cislo.Name = "cislo";
            this.cislo.Size = new System.Drawing.Size(140, 20);
            this.cislo.TabIndex = 15;
            this.cislo.TextChanged += new System.EventHandler(this.cislo_TextChanged);
            // 
            // poradi
            // 
            this.poradi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.poradi.Location = new System.Drawing.Point(441, 412);
            this.poradi.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.poradi.Name = "poradi";
            this.poradi.Size = new System.Drawing.Size(141, 20);
            this.poradi.TabIndex = 9;
            this.poradi.ValueChanged += new System.EventHandler(this.poradi_ValueChanged);
            // 
            // stanoviste
            // 
            this.stanoviste.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stanoviste.Location = new System.Drawing.Point(441, 438);
            this.stanoviste.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.stanoviste.Name = "stanoviste";
            this.stanoviste.Size = new System.Drawing.Size(141, 20);
            this.stanoviste.TabIndex = 10;
            this.stanoviste.ValueChanged += new System.EventHandler(this.stanoviste_ValueChanged);
            // 
            // ProductEditationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 487);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProductEditationForm";
            this.Text = "Úpravy výrobků";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductEditationForm_FormClosing);
            this.Load += new System.EventHandler(this.ProductEditationForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.poradi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stanoviste)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox productListBox;
        private System.Windows.Forms.ListBox actionListBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown cas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox jmeno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox cislo;
        private System.Windows.Forms.NumericUpDown poradi;
        private System.Windows.Forms.NumericUpDown stanoviste;
    }
}