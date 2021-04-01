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
            userModel.id_usuario = 0;
        }


        private string Salt_x_Hash(string password) {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            userModel.nombre_usuario = txtUsuario.Text.Trim();
            userModel.tipo = cbbTipo.SelectedItem.ToString();


            String newP = Salt_x_Hash(txtCont.Text.Trim());
            userModel.contraseña = newP;
            MessageBox.Show(newP.Length.ToString());



            using (laboratorio_pEntities DB = new laboratorio_pEntities()) {
                if (userModel.id_usuario == 0)
                    DB.usuario.Add(userModel);
                else
                    DB.Entry(userModel).State = EntityState.Modified;
                DB.SaveChanges();
            }

            fillRows();
            Clear();
        }
    }
}
