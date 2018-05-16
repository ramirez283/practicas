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
    public partial class admin_inicio : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");

        public admin_inicio()
        {
            InitializeComponent();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void admin_inicio_Load(object sender, EventArgs e)
        {
            //lbUsuario.Text = Convert.ToString(inicioform.EnviarId.id);
            
            conexion.Open();
            string nombreUsuario = ("SELECT  trabajadores.trab_nombre FROM trabajadores INNER JOIN usuarios ON trabajadores.trab_id = usuarios.us_id_trabajador where trabajadores.trab_id ="+ inicioform.EnviarId.id + " ;");
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
            admin_proveedores adminProveedor = new admin_proveedores();
            adminProveedor.Show();
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            materiales mostrarMateriales = new materiales();
            mostrarMateriales.Show();
        }

        private void btnTrabajadores_Click(object sender, EventArgs e)
        {
            trabajadores mostrarTrabajadores = new trabajadores();
            mostrarTrabajadores.Show();
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            usuarios mostrarUsuarios = new usuarios();
            mostrarUsuarios.Show();
        }
    }
}
