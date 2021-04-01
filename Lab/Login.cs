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
        public login()
        {
            InitializeComponent();
        }

        //private Boolean checkPassword(string password) {
        //    Boolean access = false;
        //    /* Fetch the stored value */
        //    string savedPasswordHash = password;
        //    /* Extract the bytes */
        //    byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
        //    /* Get the salt */
        //    byte[] salt = new byte[16];
        //    Array.Copy(hashBytes, 0, salt, 0, 16);
        //    /* Compute the hash on the password the user entered */
        //    var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        //    byte[] hash = pbkdf2.GetBytes(20);
        //    /* Compare the results */

        //    MessageBox.Show(hash.ToString() + " ... hash");
        //    MessageBox.Show(pbkdf2.ToString() + " ... pbkdf2");

        //    for (int i = 0; i < 20; i++) { 
        //    MessageBox.Show(hashBytes[i + 16].ToString());
        //        MessageBox.Show(hash[i].ToString());
        //        if (hashBytes[i + 16] != hash[i])
        //        access = false;
        //    else
        //        access = true; }
        //    return access;
        //}

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
                                where usuario.nombre_usuario == u && usuario.contraseña ==p
                                select usuario;

                    var currentUser = query.ToList();

                   // MessageBox.Show(currentUser[0].contraseña.ToString());
                   // Boolean f = checkPassword(currentUser[0].contraseña.ToString());

                   // if (f) { MessageBox.Show("yes");}

                    if (currentUser.Count > 0)
                    {
                        //Check user type
                        String userType = currentUser[0].tipo.ToString().Trim();




                        //if (access)
                        //{
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
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Contrase;a incorrecta");
                        //}


                        /* 
                         * NOTA: 
                         * FALTA BUSCAR UNA MANERA PARA CERRAR EL MAIN FORM SIN QUE SE CIERRE LA APLICACION
                         * AL UTILIZAR EL .Hide() SOLO SE OCULTA TEMPORALMENTE, PARA SALIR DE LA APLICACION
                         * HAY QUE FORZAR EL CIERRE CON Application.Exit();
                         * 
                         * FALTA ENCRIPTAR LAS CONTRASEÑAS EN LA DB <--- OJO
                         */
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
