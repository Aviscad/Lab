using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab.AdminPanel
{
    public partial class Admin : Form
    {

        usuario userModel = new usuario();
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            fillRows();
            Clear();
            cbbTipo.SelectedIndex = 0;
        }


        private void fillRows() {
            dgvUsuarios.Rows.Clear();
            using (laboratorio_pEntities DB = new laboratorio_pEntities()) {
                foreach (var user in DB.usuario)
                {
                    dgvUsuarios.Rows.Add(user.id_usuario, user.nombre_usuario, user.contraseña, user.tipo);
                }
            }
        }
        private void Clear()
        {
            txtCont.Text = txtUsuario.Text = txtReCont.Text = "";
            btnEliminar.Enabled = false;
            btnGuardar.Text = "Guardar";
            userModel.id_usuario = 0;
        }

        private string hasing(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);

            return Convert.ToBase64String(encrypted_bytes);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim().Equals("") || txtCont.Text.Trim().Equals("") ||
                txtReCont.Text.Trim().Equals(""))
            {
                MessageBox.Show("Rellene todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {

                userModel.nombre_usuario = txtUsuario.Text.Trim();
                userModel.tipo = cbbTipo.SelectedItem.ToString();

                /*
                 * NOTA: ACTUALIZAR CONTRASE;A SE MUESTRA CON LA PASSWORD HASHED EN EL TEXTBOX;
                 */

                if (txtCont.Text.Trim().Equals(txtReCont.Text.Trim()))
                {
                    userModel.contraseña = hasing(txtCont.Text.Trim());

                    using (laboratorio_pEntities DB = new laboratorio_pEntities())
                    {
                        if (userModel.id_usuario == 0)
                        {
                            DB.usuario.Add(userModel);
                            MessageBox.Show("Registro agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DB.Entry(userModel).State = EntityState.Modified;
                            MessageBox.Show("Registro actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        DB.SaveChanges();
                    }
                    fillRows();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvUsuarios_DoubleClick(object sender, EventArgs e)
        {
            userModel.id_usuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells[0].Value);

            string
                newUsername = txtUsuario.Text.Trim(),
                newPassword = txtCont.Text.Trim();

            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                try
                {
                    userModel = DB.usuario.Where(x => x.id_usuario == userModel.id_usuario).FirstOrDefault();
                    txtUsuario.Text = userModel.nombre_usuario;
                    if (userModel.tipo.Equals("admin")) { cbbTipo.SelectedIndex = 0; }
                    else if (userModel.tipo.Equals("user")) { cbbTipo.SelectedIndex = 1; }

                    btnGuardar.Text = "Modificar";
                    btnEliminar.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de eliminar este registro, luego de hacerlo no se podra recuperar la información?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var entry = DB.Entry(userModel);
                    if (entry.State == EntityState.Detached)
                        DB.usuario.Attach(userModel);
                    DB.usuario.Remove(userModel);
                    DB.SaveChanges();

                    Clear();
                    fillRows();

                    MessageBox.Show("Registro eliminado","Información",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
