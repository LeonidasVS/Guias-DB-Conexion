using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_C_
{
    public partial class ConexionSQL : Form
    {
        public ConexionSQL()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (idtext.Text.Length > 5)
                {
                    MessageBox.Show($"El ID de usuario debe ser menor a 5 caracteres", "ID de usuario erroneo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    idtext.Text = "";
                    idtext.Focus();
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(idtext.Text))
                    {
                        MessageBox.Show($"Rellena los campos!", "No puedes realizar esta accion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    this.Validate();
                    this.customersBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.northwindDataSet);
                    idtext.Enabled = false;
                    Busqueda.Enabled = true;
                    Agregar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}", "No puedes realizar esta accion", MessageBoxButtons.OK,MessageBoxIcon.Warning);
               
            }
        }

        private void ConexionSQL_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'northwindDataSet.Customers' Puede moverla o quitarla según sea necesario.
            this.customersTableAdapter.Fill(this.northwindDataSet.Customers);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 ir = new Form1();
            ir.ShowDialog();
        }
        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            idtext.Enabled = false; 
        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            customersBindingSource.AddNew();
            idtext.Enabled = true;
            Agregar.Enabled = false;
            Busqueda.Text = "";
            Busqueda.Enabled = false;
            idtext.Focus();
        }

        private void Busqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                var index = customersBindingSource.Find("customerID", Busqueda);

                if (index>0)
                {
                    MessageBox.Show("Datos ENCONTRADOS", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    customersBindingSource.Position = index;
                    return;
                }
                else
                {
                    MessageBox.Show("Datos no encontrados","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            };
        }
    }
}
