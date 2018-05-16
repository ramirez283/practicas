using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace practicas
{
    public partial class user_materiales : Form
    {

        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");
        OleDbDataAdapter da;
        DataTable dt;

        public user_materiales()
        {
            InitializeComponent();
        }

        public void cargarTabla(DataGridView dgvMateriales)
        {
            try
            {
                string consulta = "SELECT materiales_prov.mat_id AS ID, materiales_prov.mat_nombre AS Nombre, materiales_prov.mat_precio AS Precio, materiales_prov.mat_medicion AS [Unidad de medida], proveedores.prov_nombre AS Proveedor, materiales_prov.mat_descripcion AS Descripción FROM proveedores INNER JOIN materiales_prov ON proveedores.prov_id = materiales_prov.mat_prov_id ORDER BY materiales_prov.mat_id;";
                dt = new DataTable();
                da = new OleDbDataAdapter(consulta, conexion);
                da.Fill(dt);
                dgvMateriales.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo llenar la tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void cargarBusqueda(DataGridView dgvMateriales, string cadenaBusqueda)
        {
            try
            {
                string consulta = "SELECT materiales_prov.mat_id AS ID, materiales_prov.mat_nombre AS Nombre, materiales_prov.mat_precio AS Precio, materiales_prov.mat_medicion AS [Unidad de medida], proveedores.prov_nombre AS Proveedor, materiales_prov.mat_descripcion AS Descripción FROM proveedores INNER JOIN materiales_prov ON proveedores.prov_id = materiales_prov.mat_prov_id where materiales_prov.mat_nombre like '%" + cadenaBusqueda + "%'  ORDER BY materiales_prov.mat_id;";
                dt = new DataTable();
                da = new OleDbDataAdapter(consulta, conexion);
                da.Fill(dt);
                dgvMateriales.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo llenar la tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void user_materiales_Load(object sender, EventArgs e)
        {
            cargarTabla(dgvMateriales);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            cargarBusqueda(dgvMateriales, txtBuscar.Text);

            if (txtBuscar.Text == "")
            {
                cargarTabla(dgvMateriales);
            }
        }
    }
}
