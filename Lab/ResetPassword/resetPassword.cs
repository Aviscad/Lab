using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab.ResetPassword
{
    public partial class resetPassword : Form
    {
        public resetPassword()
        {
            InitializeComponent();
        }

        const string USUARIO = "lab.clinicoescobar@gmail.com";
        const string PASSWORD = "labescobar2021";

        private string randomCode;
        private usuario userModel = new usuario();
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!txtUserToReset.Text.Trim().Equals(""))
            {
                string user = txtUserToReset.Text.Trim();
                using (laboratorio_pEntities db = new laboratorio_pEntities())
                {
                    var query = from usuario in db.usuario
                                where usuario.nombre_usuario == user
                                select usuario;

                    var currentUser = query.ToList();
                    if (currentUser.Count > 0)
                    {
                        userModel = currentUser.FirstOrDefault();
                        MessageBox.Show("Contactese con el administrador para que le brinde el codigo de reseteo de contraseña", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //ENVIAR MAIL
                        Random getCode = new Random();
                        randomCode = getCode.Next(999999).ToString();

                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress(USUARIO);
                        mail.To.Add(USUARIO);
                        mail.Subject = "Reseteo de Contraseña";
                        mail.Body = "El usuario: " + user + " solicito un reseteo de contraseña. \nEl codigo es: " + randomCode + ".";
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.Port = 587;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(USUARIO, PASSWORD);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);

                        groupBox3.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("El usuario: " + txtUserToReset.Text.Trim() + " no existe!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                }
            else {
                MessageBox.Show("Debe colocar el nombre de usuario","Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string hashing(string password)
        {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);

            return Convert.ToBase64String(encrypted_bytes);
        }


        private void resetPassword_Load(object sender, EventArgs e)
        {

        }

        private void btnCheckCode_Click(object sender, EventArgs e)
        {
            if (!txtResetCode.Text.Trim().Equals(""))
            {
                string c = txtResetCode.Text.Trim();
                if (c.Equals(randomCode))
                {
                    MessageBox.Show("Código Correcto! Ingrese nueva contraseña","Información",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    groupBox1.Enabled = true;
                }
                else {
                    MessageBox.Show("Código incorrecto, verifique e intente de nuevo!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                MessageBox.Show("Debe colocar el código de verificación","Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (txtPass.Text.Trim().Equals(txtRePass.Text.Trim()))
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    userModel.contraseña = hashing(txtPass.Text.Trim());
                    DB.Entry(userModel).State = EntityState.Modified;
                    DB.SaveChanges();
                }
                MessageBox.Show("Contraseña Actualizada!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else {
                MessageBox.Show("Las contraseñas no coninciden, verifique e intente de nuevo", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
