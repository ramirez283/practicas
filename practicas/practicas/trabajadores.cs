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
    public partial class trabajadores : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");
        OleDbDataAdapter da;
        DataTable dt;
        int trabajador_id = 0;
        OleDbCommand comando;

        public trabajadores()
        {
            InitializeComponent();
        }

        public void cargarTabla(DataGridView dgvTrabajadores)
        {
            try
            {
                string consulta = "SELECT trabajadores.trab_id AS ID, trabajadores.trab_nombre AS Nombre, trabajadores.trab_puesto AS Puesto, trabajadores.trab_telefono AS Teléfono, trabajadores.trab_direccion AS Dirección FROM trabajadores ORDER BY trabajadores.trab_id;";
                dt = new DataTable();
                da = new OleDbDataAdapter(consulta, conexion);
                da.Fill(dt);
                dgvTrabajadores.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo llenar la tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void cargarBusqueda(DataGridView dgvTrabajadores, string cadenaBusqueda)
        {
            try
            {
                string consulta = "SELECT trabajadores.trab_id AS ID, trabajadores.trab_nombre AS Nombre, trabajadores.trab_puesto AS Puesto, trabajadores.trab_telefono AS Teléfono, trabajadores.trab_direccion AS Dirección FROM trabajadores WHERE(((trabajadores.trab_nombre)Like '%" + cadenaBusqueda + "%')) ORDER BY trabajadores.trab_id;";
                dt = new DataTable();
                da = new OleDbDataAdapter(consulta, conexion);
                da.Fill(dt);
                dgvTrabajadores.DataSource = dt;
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

        private void trabajadores_Load(object sender, EventArgs e)
        {
            cargarTabla(dgvTrabajadores);
        }

        private void dgvTrabajadores_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            trabajador_id = Convert.ToInt32(dgvTrabajadores.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtNombre.Text = dgvTrabajadores.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPuesto.Text = dgvTrabajadores.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtTelefono.Text = dgvTrabajadores.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDireccion.Text = dgvTrabajadores.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (txtNombre.Text != "" || txtPuesto.Text != "" || txtTelefono.Text != "" || txtDireccion.Text != "")
            {
                string actualizarMaterial = "update trabajadores SET trab_nombre = '" + txtNombre.Text + "', trab_puesto = '" + txtPuesto.Text + "', trab_telefono = '" + txtTelefono.Text + "', trab_direccion = '" + txtDireccion.Text + "' where trab_id = " + trabajador_id + ";";
                comando = new OleDbCommand(actualizarMaterial, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha actualizado el registro", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarTabla(dgvTrabajadores);
                trabajador_id = 0;
                txtNombre.Text = "";
                txtPuesto.Text = "";
                txtTelefono.Text = "";
                txtDireccion.Text = "";
            }
            else
            {
                MessageBox.Show("No se pudo actualizar, Completa todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conexion.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (txtNombre.Text != "" || txtPuesto.Text != "" || txtTelefono.Text != "" || txtDireccion.Text != "")
            {
                string agregarProv = "INSERT INTO trabajadores(trab_nombre, trab_puesto, trab_telefono, trab_direccion) Values('" + txtNombre.Text + "', '" + txtPuesto.Text + "', '" + txtTelefono.Text + "', '" + txtDireccion.Text + "');";
                comando = new OleDbCommand(agregarProv, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se agrego el nuevo Trabajador", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarTabla(dgvTrabajadores);
                trabajador_id = 0;
                txtNombre.Text = "";
                txtPuesto.Text = "";
                txtTelefono.Text = "";
                txtDireccion.Text = "";
            }
            else
            {
                MessageBox.Show("No se pudo agregar, Completa todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conexion.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (trabajador_id != 0)
            {
                DialogResult result = MessageBox.Show("¿Desea eliminar el registro?", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string eliminarProv = "DELETE FROM trabajadores where trab_id=" + trabajador_id + ";";
                    comando = new OleDbCommand(eliminarProv, conexion);
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Se eliminó el registro correctamente", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarTabla(dgvTrabajadores);
                    trabajador_id = 0;
                    txtNombre.Text = "";
                    txtPuesto.Text = "";
                    txtTelefono.Text = "";
                    txtDireccion.Text = "";
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            cargarBusqueda(dgvTrabajadores, txtBuscar.Text);
            if (txtBuscar.Text == "")
            {
                cargarTabla(dgvTrabajadores);
            }
        }
    }
}
