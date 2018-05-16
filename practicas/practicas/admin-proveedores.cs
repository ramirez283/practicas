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
    public partial class admin_proveedores : Form
    {
        OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/Pablo/source/repos/practicas/produccion.accdb");
        OleDbDataAdapter da;
        DataTable dt;
        int prov_id = 0;
        OleDbCommand comando;
        public admin_proveedores()
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

        public void cargarBusqueda(DataGridView dgvProveedores,string cadenaBusqueda)
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

        private void admin_proveedores_Load(object sender, EventArgs e)
        {
            cargarTabla(dgvProveedores);
        }

        private void dgvProveedores_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            prov_id = Convert.ToInt32(dgvProveedores.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtNombre.Text = dgvProveedores.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtTelefono.Text = dgvProveedores.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtCorreo.Text = dgvProveedores.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtRfc.Text = dgvProveedores.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtDireccion.Text = dgvProveedores.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (txtNombre.Text != "" || txtTelefono.Text != "" || txtCorreo.Text != "" || txtRfc.Text!="" || txtDireccion.Text!="")
            {
                string actualizarProveedor = "update proveedores SET prov_nombre = '" + txtNombre.Text + "', prov_tel = '" + txtTelefono.Text + "', prov_correo = '" + txtCorreo.Text + "', prov_direccion = '" + txtDireccion.Text + "', prov_rfc = '" + txtRfc.Text + "' where prov_id = " + prov_id +";";
                comando = new OleDbCommand(actualizarProveedor,conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se ha actualizado el registro","Actualizado",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cargarTabla(dgvProveedores);
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
            if (txtNombre.Text != "" || txtTelefono.Text != "" || txtCorreo.Text != "" || txtRfc.Text != "" || txtDireccion.Text != "")
            {
                string agregarProv = "INSERT INTO proveedores(prov_nombre,prov_tel,prov_correo,prov_direccion,prov_rfc) Values('"+ txtNombre.Text + "','" + txtTelefono.Text + "','" + txtCorreo.Text + "','" + txtDireccion.Text + "','" + txtRfc.Text + "');";
                comando = new OleDbCommand(agregarProv, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Se agrego el nuevo Proveedor", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarTabla(dgvProveedores);
                txtNombre.Text = "";
                txtTelefono.Text = "";
                txtCorreo.Text = "";
                txtRfc.Text = "";
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
            if (prov_id != 0)
            {
                DialogResult result = MessageBox.Show("¿Desea eliminar el registro?", "Eliminado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string eliminarProv = "DELETE FROM proveedores where prov_id=" + prov_id + ";";
                    comando = new OleDbCommand(eliminarProv, conexion);
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Se eliminó el registro correctamente", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarTabla(dgvProveedores);
                    prov_id = 0; ;
                    txtNombre.Text = "";
                    txtTelefono.Text = "";
                    txtCorreo.Text = "";
                    txtRfc.Text = "";
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

        private void btnMateriales_Click(object sender, EventArgs e)
        {
            materiales mostrarMateriales = new materiales();
            mostrarMateriales.Show();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            //string busqueda = "SELECT trabajadores.* FROM trabajadores WHERE(((trabajadores.trab_nombre)Like '*" + txtBuscar.Text + "*')); ";
            
            //cargarBusqueda(dgvProveedores, txtBuscar.Text);
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
    }
}
