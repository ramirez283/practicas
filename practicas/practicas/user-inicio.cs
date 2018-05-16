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
    public partial class user_inicio : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");

        public user_inicio()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void user_inicio_Load(object sender, EventArgs e)
        {
            conexion.Open();
            string nombreUsuario = ("SELECT trabajadores.trab_nombre FROM trabajadores INNER JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador WHERE(((usuarios.us_Id) = " + inicioform.EnviarId.id + "));");
            OleDbCommand usuario = new OleDbCommand(nombreUsuario, conexion);
            lbUsuario.Text = Convert.ToString(usuario.ExecuteScalar());
            conexion.Close();
        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            ajustes_usuarios ajustes = new ajustes_usuarios();
            ajustes.Show();
        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {
            user_proveedor mostrarProveedores = new user_proveedor();
            mostrarProveedores.Show();
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            user_materiales mostrarMateriales = new user_materiales();
            mostrarMateriales.Show();
        }
    }
}
