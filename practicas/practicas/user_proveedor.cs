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
    public partial class user_proveedor : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");
        OleDbDataAdapter da;
        DataTable dt;

        public user_proveedor()
        {
            InitializeComponent();
        }

        public void cargarTabla(DataGridView dgvProveedores)
        {
            try
            {
                string consulta = "SELECT proveedores.prov_id as ID, proveedores.prov_nombre as Nombre, proveedores.prov_tel as Teléfono, proveedores.prov_correo as Correo, proveedores.prov_rfc as RFC, proveedores.prov_direccion as Dirección, * FROM proveedores ORDER BY proveedores.prov_id; ";
                dt = new DataTable();
                da = new OleDbDataAdapter(consulta, conexion);
                da.Fill(dt);
                dgvProveedores.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo llenar la tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void cargarBusqueda(DataGridView dgvProveedores, string cadenaBusqueda)
        {
            try
            {
                string consulta = "SELECT proveedores.prov_id as ID, proveedores.prov_nombre as Nombre, proveedores.prov_tel as Teléfono, proveedores.prov_correo as Correo, proveedores.prov_rfc as RFC, proveedores.prov_direccion as Dirección FROM proveedores WHERE(((proveedores.prov_nombre)Like '%" + cadenaBusqueda + "%')) ORDER BY proveedores.prov_id;";
                //SELECT trabajadores.* FROM trabajadores WHERE(((trabajadores.trab_nombre)Like '*" + txtBuscar.Text + "*')); 
                dt = new DataTable();
                da = new OleDbDataAdapter(consulta, conexion);
                da.Fill(dt);
                dgvProveedores.DataSource = dt;
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            conexion.Open();
            cargarBusqueda(dgvProveedores, txtBuscar.Text);
            if (txtBuscar.Text == "")
            {
                cargarTabla(dgvProveedores);
            }
            conexion.Close();
        }

        private void user_proveedor_Load(object sender, EventArgs e)
        {
            cargarTabla(dgvProveedores);
        }
    }
}
