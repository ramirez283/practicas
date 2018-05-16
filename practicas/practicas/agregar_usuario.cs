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
    public partial class agregar_usuario : Form
    {

        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");
        OleDbDataAdapter da;
        DataTable dt;
        int us_id = 0;
        OleDbCommand comando;

        //DataGridView dgvUsuarios;

        public agregar_usuario()
        {
            InitializeComponent();
        }

        public void cargarTabla(DataGridView dgvUsuarios)
        {
            try
            {
                string consulta = "SELECT trabajadores.trab_id AS ID, trabajadores.trab_nombre AS Nombre FROM trabajadores LEFT JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador WHERE(((usuarios.us_id_trabajador)Is Null)); ";
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

        public void cargarBusqueda(DataGridView dgvMateriales, string cadenaBusqueda)
        {
            try
            {
                string consulta = "SELECT trabajadores.trab_id AS ID, trabajadores.trab_nombre AS Nombre FROM trabajadores LEFT JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador WHERE(((usuarios.us_id_trabajador)Is Null)) and (((trabajadores.trab_nombre)Like '%" + cadenaBusqueda + "%')) ORDER BY trabajadores.trab_id;";
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

        /*public DataTable cargarTabajadores()
        {
            conexion.Open();
            string consulta = "SELECT trab_id, trab_nombre FROM trabajadores order by trab_id;";
            dt = new DataTable();
            da = new OleDbDataAdapter(consulta, conexion);
            da.Fill(dt);

            conexion.Close();
            return dt;
        }*/


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            conexion.Open();

            string validarUsuario = "select us_id, us_usuario from usuarios where us_usuario = '" + txtNombre.Text + "';";

            comando = new OleDbCommand(validarUsuario, conexion);
            OleDbDataReader leerUsuario;
            leerUsuario = comando.ExecuteReader();

            bool usuarioEncontrado = leerUsuario.HasRows;
            if (us_id == 0)
            {
                MessageBox.Show("Selecciona un trabajador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtNombre.Text == "" || txtpassword.Text == "")
                {
                    MessageBox.Show("Ingresa todos los datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (usuarioEncontrado)
                {
                    MessageBox.Show("El usuario ya existe", "Nombre de Usuario registrado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string agregarUusario = "INSERT INTO usuarios(us_usuario, us_password, us_id_trabajador, us_tipo) Values('" + txtNombre.Text + "', '" + txtpassword.Text + "'," + us_id + ", " + 2 + ");";
                    comando = new OleDbCommand(agregarUusario, conexion);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Se agrego el usuario corectamente", "Nuevo Usuario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarTabla(dgvTrabajadores);
                    txtNombre.Text = "";
                    txtpassword.Text = "";
                    txtTrabajador.Text = "";
                    us_id = 0;
                }
            }

            conexion.Close();

            /*conexion.Open();
            trab_id = cbTrabajador.SelectedIndex + 1;

            string validarUsuario = "select us_usuario from usuarios where us_usuario='" + txtNombre.Text + "';";
            comando = new OleDbCommand(validarUsuario,conexion);
            OleDbDataReader leerUsuario;
            leerUsuario = comando.ExecuteReader();
            bool validarUsuarios = leerUsuario.HasRows;

            string validarTrabajador = "select us_id_trabajador from usuarios where us_id_trabajador=" + trab_id + ";";
            comando = new OleDbCommand(validarTrabajador, conexion);
            OleDbDataReader leerTrabajador;
            leerTrabajador = comando.ExecuteReader();

            bool validarTrabajadores = leerTrabajador.HasRows;




            if (txtNombre.Text != "" && txtpassword.Text != "" && trab_id != 0)
            {
                if (validarUsuarios && validarTrabajadores)
                {
                    MessageBox.Show("El usuario ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string agregarProv = "INSERT INTO usuarios(us_usuario, us_password, us_id_trabajador, us_tipo) Values('" + txtNombre.Text + "', '" + txtpassword.Text + "', " + Convert.ToInt32(validarTrabajador) + ", " + 2 + ");";
                    comando = new OleDbCommand(agregarProv, conexion);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Se agrego el nuevo usuario", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargar.cargarTabla(dgvUsuarios);
                    txtNombre.Text = "";
                    txtpassword.Text = "";
                    cbTrabajador.Text = "";
                }
            }
            else
            {
                MessageBox.Show("No se pudo agregar, Completa todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conexion.Close();

            */
            
        
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            /*usuarios cargar = new usuarios();
            cargar.cargarTabla(dgvUsuarios);*/
            Close();
        }

        private void agregar_usuario_Load(object sender, EventArgs e)
        {
            cargarTabla(dgvTrabajadores);
            /*DataTable dtC = cargarTabajadores();
            cbTrabajador.DataSource = dtC;
            cbTrabajador.ValueMember = "trab_id";
            cbTrabajador.DisplayMember = "trab_nombre";*/
        }

        private void dgvTrabajadores_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            us_id = Convert.ToInt32(dgvTrabajadores.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtTrabajador.Text = dgvTrabajadores.Rows[e.RowIndex].Cells[1].Value.ToString();
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
