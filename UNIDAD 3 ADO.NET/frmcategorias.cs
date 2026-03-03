
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
    public partial class frmcategorias : Form
    {
        int idCategoriaSeleccionada;
        string connectionString = "Server=localhost;Database=primeraactividad;Trusted_Connection=True;TrustServerCertificate=True;";
        int idSeleccionado = 0;

        public frmcategorias()
        {
            InitializeComponent();
            CargarCategoria();
        }

        private void CargarCategoria()
        {
            using (var db = new PrimeraactividadContext())
            {
                dgvcategorias.DataSource = db.Categorias.ToList();
            }
        }



        private void frmcategorias_Load(object sender, EventArgs e)
        {
            CargarCategoria();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            using (var db = new PrimeraactividadContext())
            {
                Categoria nueva = new Categoria()
                {
                    Nombrecategoria = txtNombreCategoria.Text
                };

                db.Categorias.Add(nueva);
                db.SaveChanges();
            }

            CargarCategoria();
            MessageBox.Show("categoria agregada");
            LimpiarCampos();
            
        }

        private void LimpiarCampos()
        {
            txtNombreCategoria.Clear();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            using (var db = new PrimeraactividadContext())
            {
                var categoria = db.Categorias.Find(idCategoriaSeleccionada);

                if (categoria != null)
                {
                    categoria.Nombrecategoria = txtNombreCategoria.Text;
                    db.SaveChanges();
                }
            }

            CargarCategoria();
            LimpiarCampos();
            MessageBox.Show("Categoría actualizada correctamente");

        }

        private void dgvcategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvcategorias.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(row.Cells["categoriaID"].Value);
                txtNombreCategoria.Text = row.Cells["nombrecategoria"].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvcategorias.CurrentRow.Cells[0].Value);

            using (var db = new PrimeraactividadContext())
            {
                var categoria = db.Categorias.Find(id);

                if (categoria != null)
                {
                    db.Categorias.Remove(categoria);
                    db.SaveChanges();
                }
            }

            CargarCategoria();
            MessageBox.Show("Categoria eliminada");
            LimpiarCampos() ;
        }
    }
}
