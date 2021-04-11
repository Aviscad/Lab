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


        private void fillRows()
        {
            dgvUsuarios.Rows.Clear();
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
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
            if (txtUsuario.Text.Trim().Equals(""))
            {
                MessageBox.Show("Introduzca el nombre de usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    /* INSERT CON VERIFICACION DE EXISTENCIA DE USUARIO */
                    if (userModel.id_usuario == 0)
                    {
                        var userExist = from usuario in DB.usuario
                                        where usuario.nombre_usuario == txtUsuario.Text.Trim()
                                        select usuario;

                        if (userExist.ToList().Count > 0)
                        {
                            MessageBox.Show("El usuario " + txtUsuario.Text.Trim() + " ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (txtCont.Text.Trim().Equals(txtReCont.Text.Trim()))
                            {
                                userModel.nombre_usuario = txtUsuario.Text.Trim();
                                userModel.tipo = cbbTipo.SelectedItem.ToString();
                                userModel.contraseña = hasing(txtCont.Text.Trim());

                                DB.usuario.Add(userModel);
                                MessageBox.Show("Registro agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    /* UPDATE CON VERIFICACION DE EXISTENCIA DE USUARIO */
                    else
                    {
                        if (userModel.nombre_usuario != txtUsuario.Text.Trim())
                        {
                            var userExist = from usuario in DB.usuario
                                            where usuario.nombre_usuario == txtUsuario.Text.Trim()
                                            select usuario;

                            if (userExist.ToList().Count > 0)
                            {
                                MessageBox.Show("El usuario " + txtUsuario.Text.Trim() + " ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                /* USUARIO NO EXISTE? COMPROBAR SI LA CONTRASE;A SE QUIERE CAMBIAR */
                                if (
                                    !txtCont.Text.Trim().Equals("") &&
                                    !txtReCont.Text.Trim().Equals("")
                                    )
                                {
                                    if (txtCont.Text.Trim().Equals(txtReCont.Text.Trim()))
                                    {
                                        userModel.nombre_usuario = txtUsuario.Text.Trim();
                                        userModel.tipo = cbbTipo.SelectedItem.ToString();
                                        userModel.contraseña = hasing(txtCont.Text.Trim());

                                        DB.Entry(userModel).State = EntityState.Modified;
                                        MessageBox.Show("Registro actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                /* SI LA CONTRASE;A NO SE QUIERE CAMBIAR SOLO MODIFICA NOMBRE DE USUARIO Y TIPO */
                                else
                                {
                                    userModel.nombre_usuario = txtUsuario.Text.Trim();
                                    userModel.tipo = cbbTipo.SelectedItem.ToString();

                                    DB.Entry(userModel).State = EntityState.Modified;
                                    MessageBox.Show("Registro actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        /* SI NO SE QUIERE CAMBIAR EL NOMBRE DE USUARIO DEJAR EL MISMO */
                        else
                        {
                            if (
                                !txtCont.Text.Trim().Equals("") &&
                                !txtReCont.Text.Trim().Equals("")
                                )
                            {
                                if (txtCont.Text.Trim().Equals(txtReCont.Text.Trim()))
                                {
                                    userModel.tipo = cbbTipo.SelectedItem.ToString();
                                    userModel.contraseña = hasing(txtCont.Text.Trim());

                                    DB.Entry(userModel).State = EntityState.Modified;
                                    MessageBox.Show("Registro actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                userModel.tipo = cbbTipo.SelectedItem.ToString();

                                DB.Entry(userModel).State = EntityState.Modified;
                                MessageBox.Show("Registro actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    DB.SaveChanges();
                    fillRows();
                    Clear();
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
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    MessageBox.Show("Registro eliminado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
