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
    public partial class ajustes_usuarios : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");

        public ajustes_usuarios()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ajustes_usuarios_Load(object sender, EventArgs e)
        {
            conexion.Open();
            string obtenerDatos = ("SELECT trabajadores.trab_nombre, trabajadores.trab_telefono, trabajadores.trab_direccion FROM trabajadores INNER JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador where usuarios.us_id = @id;");


            OleDbCommand obtenerUser = new OleDbCommand(obtenerDatos, conexion);
            obtenerUser.Parameters.Add(new OleDbParameter("@id", inicioform.EnviarId.id));
            OleDbDataReader reader = obtenerUser.ExecuteReader();
            
            //obtenerUser.Parameters.AddWithValue(Convert.ToString(inicioform.EnviarId.id),1);
            if (reader.Read())
            {
                txtNombre.Text = reader["trab_nombre"] as string;
                txtTel.Text = reader["trab_telefono"] as string;
                txtDireccion.Text = reader["trab_direccion"] as string;

            }


            conexion.Close();
            //reader.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (txtNombre.Text != "" || txtTel.Text != "" || txtDireccion.Text != "")
            {
                //string actualizarUsuario = "update trabajadores set trab_nombre = '" + txtNombre.Text + "', trab_telefono = '" + txtTel.Text + "', trab_direccion = '" + txtDireccion.Text + "' where trab_id =" + inicioform.EnviarId.id + ";";
                string actualizarUsuario = "UPDATE trabajadores INNER JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador SET trabajadores.trab_nombre = '" + txtNombre.Text + "', trabajadores.trab_telefono = '" + txtTel.Text + "', trabajadores.trab_direccion = '" + txtDireccion.Text + "' WHERE(((usuarios.us_Id) = " + inicioform.EnviarId.id + "));"; 
                OleDbCommand actualizar = new OleDbCommand(actualizarUsuario, conexion);
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Se actualizo correctamente", "Hecho", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se pudo actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conexion.Close();
        }
    }
}
