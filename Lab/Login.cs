using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab
{
    public partial class login : Form
    {
        public static int id = 0; //ID del usuario que inicio sesion
        public login()
        {
            InitializeComponent();
        }

        private string hasing(string password) {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);

            return Convert.ToBase64String(encrypted_bytes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Get Username and Password
            string u = txtUsername.Text.Trim();
            String p = txtPass.Text.Trim();
            if (u.Equals("") || p.Equals(""))
            {
                MessageBox.Show("Por favor rellene los campos","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else {
                //Check username and password on DB
                using (laboratorio_pEntities db = new laboratorio_pEntities())
                {
                    var query = from usuario in db.usuario
                                where usuario.nombre_usuario == u
                                select usuario;

                    var currentUser = query.ToList();

                    if (currentUser.Count > 0)
                    {
                        if (hasing(p).Equals(currentUser[0].contraseña))
                        {
                            MessageBox.Show("In");
                            String userType = currentUser[0].tipo.ToString().Trim();

                            id = currentUser[0].id_usuario;

                            if (userType.Equals("admin"))
                            {
                                MessageBox.Show("Admin Type");
                                new AdminPanel.Admin().Show();
                                this.Hide();
                            }
                            else if (userType.Equals("user"))
                            {
                                MessageBox.Show("User type");
                                new UserPanel.User().Show();
                                this.Hide();
                            }
                        }
                        else {
                            MessageBox.Show("out");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El nombre de usuario o contraseña estan incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }   
        }
    }
}
