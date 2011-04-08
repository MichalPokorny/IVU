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
    public partial class ProductEditationForm : Form
    {
        private Product SelectedProduct
        {
            get { return productListBox.SelectedItem as Product; }
        }

        private Action SelectedAction
        {
            get { return actionListBox.SelectedItem as Action; }
        }

        private Model Model
        {
            get { return Program.Model; }
        }

        public ProductEditationForm()
        {
            InitializeComponent();

            //comboBox1.Items.Clear();
            //foreach (var x in Model.WorkplaceIndexes) comboBox1.Items.Add(x);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model.Products.Add(new Product("Nový produkt", "X1000"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (productListBox.SelectedItem == null) return;
            Model.Products.Remove(SelectedProduct);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SelectedProduct == null) return;
            SelectedProduct.Actions.Add(new Action(1, SelectedProduct.FreeNextOrder, 1));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (SelectedProduct == null || SelectedAction == null) return;
            SelectedProduct.Actions.Remove(SelectedAction);
        }

        private void ProductEditationForm_Load(object sender, EventArgs e)
        {
            productListBox.DisplayMember = "FullText";
            productListBox.DataSource = Model.Products;
            actionListBox.DisplayMember = "FullText";
        }

        private void productListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            jmeno.Enabled = cislo.Enabled = actionListBox.Enabled = (SelectedProduct != null);
            if (SelectedProduct != null)
            {
                jmeno.DataBindings.Clear();
                jmeno.DataBindings.Add("Text", SelectedProduct, "Name");
                cislo.DataBindings.Clear();
                cislo.DataBindings.Add("Text", SelectedProduct, "Code");
                actionListBox.DataSource = SelectedProduct.Actions;

                jmeno.DataBindings[0].ReadValue(); cislo.DataBindings[0].ReadValue();
            }
        }

        private void actionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cas.Enabled = stanoviste.Enabled = poradi.Enabled = (SelectedAction != null);
            if (SelectedAction != null)
            {
                cas.DataBindings.Clear();
                cas.DataBindings.Add("Value", SelectedAction, "Time");
                stanoviste.DataBindings.Clear();
                stanoviste.DataBindings.Add("Value", SelectedAction, "Place");
                poradi.DataBindings.Clear();
                poradi.DataBindings.Add("Value", SelectedAction, "Order");

                cas.DataBindings[0].ReadValue(); stanoviste.DataBindings[0].ReadValue();
                poradi.DataBindings[0].ReadValue();
            }
        }

        private void jmeno_TextChanged(object sender, EventArgs e)
        {
            //if (SelectedProduct!=null && jmeno.DataBindings.Count > 0) jmeno.DataBindings[0].WriteValue();
        }

        private void cislo_TextChanged(object sender, EventArgs e)
        {
            //if (SelectedProduct != null && cislo.DataBindings.Count > 0) cislo.DataBindings[0].WriteValue();
        }

        private void ProductEditationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Model.Save();
        }

        private void poradi_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedAction != null && poradi.DataBindings.Count > 0) poradi.DataBindings[0].WriteValue();
        }

        private void stanoviste_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedAction != null && stanoviste.DataBindings.Count > 0) stanoviste.DataBindings[0].WriteValue();
        }

        private void cas_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedAction != null && cas.DataBindings.Count > 0) cas.DataBindings[0].WriteValue();
        }
    }
}
