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
            txtCont.Text = txtUsuario.Text = "";
            btnEliminar.Enabled = false;
            btnGuardar.Text = "Guardar";
            userModel.id_usuario = 0;
        }


        //private string Salt_x_Hash(string password) {
        //    byte[] salt;
        //    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        //    var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        //    byte[] hash = pbkdf2.GetBytes(20);

        //    byte[] hashBytes = new byte[36];
        //    Array.Copy(salt, 0, hashBytes, 0, 16);
        //    Array.Copy(hash, 0, hashBytes, 16, 20);

        //    string savedPasswordHash = Convert.ToBase64String(hashBytes);
        //    return savedPasswordHash;
        //}

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            userModel.nombre_usuario = txtUsuario.Text.Trim();
            userModel.tipo = cbbTipo.SelectedItem.ToString();

            if (txtCont.Text.Trim().Equals(txtReCont.Text.Trim()))
            {
                userModel.contraseña = txtCont.Text.Trim();

                //String newP = Salt_x_Hash(txtCont.Text.Trim());
                //userModel.contraseña = newP;
                //MessageBox.Show(newP.Length.ToString());

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
            else {
                MessageBox.Show("Las contraseñas no coinciden!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
                    txtCont.Text = userModel.contraseña;
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
