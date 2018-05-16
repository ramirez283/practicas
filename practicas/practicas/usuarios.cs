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
    public partial class usuarios : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");
        OleDbDataAdapter da;
        DataTable dt;
        int us_id = 0;
        OleDbCommand comando;
        int tipo_id;

        public usuarios()
        {
            InitializeComponent();
        }

        public void cargarTabla(DataGridView dgvUsuarios)
        {
            try
            {
                string consulta = "SELECT usuarios.us_Id AS ID, usuarios.us_usuario AS Usuario, trabajadores.trab_nombre AS Nombre, tipos_usuarios.tipo_us_usuario AS [Tipo de usuario] FROM tipos_usuarios INNER JOIN(trabajadores INNER JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador) ON tipos_usuarios.tipo_us_id = usuarios.us_tipo ORDER BY usuarios.us_Id;";
                dt = new DataTable();
                da = new OleDbDataAdapter(consulta, conexion);
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo llenar la tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public void cargarBusqueda(DataGridView dgvUsuarios, string cadenaBusqueda)
        {
            try
            {
                string consulta = "SELECT usuarios.us_Id AS ID, usuarios.us_usuario AS Usuario, trabajadores.trab_nombre AS Nombre, tipos_usuarios.tipo_us_usuario AS [Tipo de usuario] FROM tipos_usuarios INNER JOIN(trabajadores INNER JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador) ON tipos_usuarios.tipo_us_id = usuarios.us_tipo WHERE(((usuarios.us_usuario)Like '%" + cadenaBusqueda + "%')) ORDER BY usuarios.us_Id;";
                dt = new DataTable();
                da = new OleDbDataAdapter(consulta, conexion);
                da.Fill(dt);
                dgvUsuarios.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo llenar la tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public DataTable cargarTiposUsuario()
        {
            conexion.Open();
            string consulta = "SELECT * FROM tipos_usuarios order by tipo_us_id;";
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

        private void usuarios_Load(object sender, EventArgs e)
        {
            cargarTabla(dgvUsuarios);
            DataTable dtC = cargarTiposUsuario();
            cbTiposUsuarios.DataSource = dtC;
            cbTiposUsuarios.ValueMember = "tipo_us_id";
            cbTiposUsuarios.DisplayMember = "tipo_us_usuario";
        }

        private void dgvUsuarios_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            us_id = Convert.ToInt32(dgvUsuarios.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtNombre.Text = dgvUsuarios.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTrabajador.Text = dgvUsuarios.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbTiposUsuarios.Text = dgvUsuarios.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            tipo_id = cbTiposUsuarios.SelectedIndex + 1;
            if (txtNombre.Text != "" || txtTrabajador.Text != "" || tipo_id != 0)
            {
                string actualizarUsuario = "update usuarios SET us_tipo = " + tipo_id + " where us_id= "+us_id +";";
                comando = new OleDbCommand(actualizarUsuario, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha actualizado el registro", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarTabla(dgvUsuarios);
                us_id = 0;
            }
            else
            {
                MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexion.Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (us_id != 0)
            {
                DialogResult result = MessageBox.Show("¿Desea eliminar el registro?", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string eliminarProv = "DELETE FROM usuarios where us_id=" + us_id + ";";
                    comando = new OleDbCommand(eliminarProv, conexion);
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Se eliminó el registro correctamente", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarTabla(dgvUsuarios);
                    us_id = 0;
                    txtNombre.Text = "";
                    txtTrabajador.Text = "";
                    cbTiposUsuarios.Text = "";
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
            agregar_usuario nuevoUsuario = new agregar_usuario();
            nuevoUsuario.Show();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            cargarBusqueda(dgvUsuarios, txtBuscar.Text);
            if (txtBuscar.Text == "")
            {
                cargarTabla(dgvUsuarios);
            }
        }
    }
}
