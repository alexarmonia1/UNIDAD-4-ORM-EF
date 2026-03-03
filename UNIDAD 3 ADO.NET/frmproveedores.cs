
using UNIDAD_3_ADO.NET.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace UNIDAD_3_ADO.NET
{
    public partial class frmproveedores : Form
    {

        string connectionString = "Server=localhost;Database=primeraactividad;Trusted_Connection=True;TrustServerCertificate=True;";
        int idseleccionado = 0;
        public frmproveedores()
        {
            InitializeComponent();
            CargarProveedores();
        }

        private void CargarProveedores()
        {
            using (var db = new PrimeraactividadContext())
            {
                dgvProveedores.DataSource = db.Proveedores.ToList();
            }
        }



        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            using (var db = new PrimeraactividadContext())
            {
                Proveedore nuevo = new Proveedore()
                {
                    Nombreproveedor = txtNombreProveedor.Text,
                    Telefono = txtTelefono.Text,
                    Direccion = txtDireccion.Text,
                };

                db.Proveedores.Add(nuevo);
                db.SaveChanges();
            }

            CargarProveedores();
            MessageBox.Show("proveedor agregado");
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            txtNombreProveedor.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(dgvProveedores.CurrentRow.Cells[0].Value);

            using (var db = new PrimeraactividadContext())
            {
                var proveedore = db.Proveedores.Find(id);

                if (proveedore != null)
                {
                    proveedore.Nombreproveedor = txtNombreProveedor.Text;
                    proveedore.Telefono = txtTelefono.Text;
                    proveedore.Direccion = txtDireccion.Text;

                    db.SaveChanges();
                }
            }
            CargarProveedores();
            MessageBox.Show("proveedor actualizado");
            LimpiarCampos();

        }

        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProveedores.Rows[e.RowIndex];
                idseleccionado = Convert.ToInt32(row.Cells["proveedorID"].Value);
                txtNombreProveedor.Text = row.Cells["nombreproveedor"].Value.ToString();
                txtTelefono.Text = row.Cells["telefono"].Value.ToString();
                txtDireccion.Text = row.Cells["direccion"].Value.ToString();
            }
        }

        private void txtEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvProveedores.CurrentRow.Cells[0].Value);

            using (var db = new PrimeraactividadContext())
            {
                var proveedore = db.Proveedores.Find(id);

                if (proveedore != null)
                {
                    db.Proveedores.Remove(proveedore);
                    db.SaveChanges();
                }
            }

            CargarProveedores();
            MessageBox.Show("proveedor eliminado");
            
            LimpiarCampos();
        }

        private void frmproveedores_Load_1(object sender, EventArgs e)
        {
            CargarProveedores();
        }
    }
}
