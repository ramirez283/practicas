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
    public partial class inicioform : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");
        public inicioform()
        {
            InitializeComponent();
            
        }

        public static class EnviarId
        {
            public static int id;
        }

        private void salirbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iniciarbtn_Click(object sender, EventArgs e)
        {
            //OleDbConnection conectar = new OleDbConnection();
            conexion.Open();

            string consulta = "select us_id, us_usuario, us_password from usuarios where us_usuario='" + usuariotxt.Text + "' and us_password='" + passwordtxt.Text + "';";
            string tipuUsuario = "select us_tipo from usuarios where us_usuario='" + usuariotxt.Text + "' and us_tipo=1";
            string obtenerId = "select us_id from usuarios where us_usuario='" + usuariotxt.Text + "' and us_password='" + passwordtxt.Text + "';";

            //string obtenerId = "SELECT trabajadores.trab_id FROM trabajadores INNER JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador WHERE(((usuarios.us_usuario) = '" + usuariotxt.Text + "') AND((usuarios.us_password) = '" + passwordtxt.Text + "')); ";


            OleDbCommand comando = new OleDbCommand(consulta, conexion);
            OleDbDataReader leerUsuario;
            leerUsuario = comando.ExecuteReader();

            OleDbCommand tipo = new OleDbCommand(tipuUsuario, conexion);
            OleDbDataReader leerTipo;
            leerTipo = tipo.ExecuteReader();

            OleDbCommand id = new OleDbCommand(obtenerId, conexion);


            bool usuarioValido = leerUsuario.HasRows;
            bool tipoUsurioAdmin = leerTipo.HasRows;


            if (usuariotxt.Text == "" || passwordtxt.Text == "")
            {
                MessageBox.Show("Ingresa el usuario y la contraseña correcta", "Inisio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(usuarioValido)
            {
                if (tipoUsurioAdmin)
                {
                    admin_inicio adminInicioForm = new admin_inicio();
                    //EnviarId.id = comando.Parameters.AddWithValue("@id", Convert.ToInt32);
                    EnviarId.id = Convert.ToInt32(id.ExecuteScalar());
                    adminInicioForm.Show();
                }
                else
                {
                    user_inicio userInicioForm = new user_inicio();
                    EnviarId.id = Convert.ToInt32(id.ExecuteScalar());
                    userInicioForm.Show();
                }

            }
            else
            {
                MessageBox.Show("Ingresa el usuario y la contraseña correcta", "Inisio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usuariotxt.Focus();
            }
            usuariotxt.Text = "";
            passwordtxt.Text = "";
            conexion.Close();
        }

        /*private void inicioform_Load(object sender, EventArgs e)
        {
            try
            {
                conexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C: /Users/Pablo/source/repos/practicas/produccion.accdb";
                MessageBox.Show("Conectado con exito", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception)
            {
                MessageBox.Show("No se Pudo Conectar a la base de datos", "Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }*/

        private void passwordtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                iniciarbtn.PerformClick();
        }
    }
}
