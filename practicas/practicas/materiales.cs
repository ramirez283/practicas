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
    public partial class materiales : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");
        OleDbDataAdapter da;
        DataTable dt;
        int mat_id = 0;
        OleDbCommand comando;
        int proveedor_id;

        public materiales()
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

        public void cargarBusqueda(DataGridView dgvMateriales, string busqueda)
        {
            try
            {
                string consulta = "SELECT materiales_prov.mat_id AS ID, materiales_prov.mat_nombre AS Nombre, materiales_prov.mat_precio AS Precio, materiales_prov.mat_medicion AS [Unidad de medida], proveedores.prov_nombre AS Proveedor, materiales_prov.mat_descripcion AS Descripción FROM proveedores INNER JOIN materiales_prov ON proveedores.prov_id = materiales_prov.mat_prov_id where materiales_prov.mat_nombre like '%" + busqueda + "%'  ORDER BY materiales_prov.mat_id;";
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

        public DataTable cargarProveedores()
        {
            conexion.Open();
            string consulta = "SELECT prov_id, prov_nombre FROM proveedores order by prov_id;";
            dt = new DataTable();
            da = new OleDbDataAdapter(consulta, conexion);
            da.Fill(dt);

            conexion.Close();
            return dt;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void materiales_Load(object sender, EventArgs e)
        {
            cargarTabla(dgvMateriales);
            DataTable dtC = cargarProveedores();
            cbProveedor.DataSource = dtC;
            cbProveedor.ValueMember = "prov_id";
            cbProveedor.DisplayMember = "prov_nombre";

        }

        private void dgvMateriales_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            mat_id = Convert.ToInt32(dgvMateriales.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtMaterial.Text = dgvMateriales.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPrecio.Text = dgvMateriales.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMedida.Text = dgvMateriales.Rows[e.RowIndex].Cells[3].Value.ToString();
            cbProveedor.Text = dgvMateriales.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDescipcion.Text = dgvMateriales.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            proveedor_id = cbProveedor.SelectedIndex +1;
            if (txtMaterial.Text != "" || txtDescipcion.Text != "" || txtMedida.Text != "" || txtPrecio.Text != "" || cbProveedor.Text != "")
            {
                string actualizarMaterial = "update materiales_prov SET mat_nombre = '" + txtMaterial.Text + "', mat_precio = '" + txtPrecio.Text + "', mat_medicion = '" + txtMedida.Text + "', mat_prov_id = " + proveedor_id + ", mat_descripcion = '" + txtDescipcion.Text + "' where mat_id = " + mat_id + ";";
                comando = new OleDbCommand(actualizarMaterial, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha actualizado el registro", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarTabla(dgvMateriales);
                mat_id = 0;
                txtMaterial.Text = "";
                txtPrecio.Text = "";
                txtMedida.Text = "";
                txtDescipcion.Text = "";
                cbProveedor.Text = "";
            }
            else
            {
                MessageBox.Show("No se pudo actualizar, Completa todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexion.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (mat_id != 0)
            {
                DialogResult result = MessageBox.Show("¿Desea eliminar el registro?", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string eliminarProv = "DELETE FROM materiales_prov where mat_id=" + mat_id + ";";
                    comando = new OleDbCommand(eliminarProv, conexion);
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Se eliminó el registro correctamente", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarTabla(dgvMateriales);
                    mat_id = 0;
                    txtMaterial.Text = "";
                    txtPrecio.Text = "";
                    txtMedida.Text = "";
                    txtDescipcion.Text = "";
                    cbProveedor.Text = "";
                }
                else if (result == DialogResult.No)
                {
                    MessageBox.Show("NO se elimino ningún registro", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("No se seleccionó ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            conexion.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            proveedor_id = cbProveedor.SelectedIndex + 1;
            if (txtMaterial.Text != "" || txtPrecio.Text != "" || txtMedida.Text != "" || txtDescipcion.Text != "" || cbProveedor.Text != "")
            {
                string agregarProv = "INSERT INTO materiales_prov(mat_nombre, mat_precio, mat_medicion, mat_prov_id, mat_descripcion) Values('"+ txtMaterial.Text +"', '" + txtPrecio.Text + "', '" + txtMedida.Text + "', " + proveedor_id + ", '" + txtDescipcion.Text + "');";
                comando = new OleDbCommand(agregarProv, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se agrego el nuevo Material", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarTabla(dgvMateriales);
                mat_id = 0;
                txtMaterial.Text = "";
                txtPrecio.Text = "";
                txtMedida.Text = "";
                txtDescipcion.Text = "";
                cbProveedor.Text = "";
            }
            else
            {
                MessageBox.Show("No se pudo agregar, Completa todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conexion.Close();
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
