﻿using System;
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

        private string hashing(string password) {

            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);

            return Convert.ToBase64String(encrypted_bytes);
        }

        private void login_Load(object sender, EventArgs e)
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                var query = from usuario in DB.usuario
                            select usuario;
                if (query.ToList().Count == 0)
                {
                    usuario userModel = new usuario();
                    userModel.nombre_usuario = "admin";
                    userModel.contraseña = "0DPiKuNIrrVmD8IUCuw1hQxNqZc=";
                    userModel.tipo = "admin";

                    DB.usuario.Add(userModel);
                    DB.SaveChanges();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Get Username and Password
            string u = txtUsuario.Text.Trim();
            String p = txtContrasenia.Text.Trim();
            if (u.Equals("") || p.Equals(""))
            {
                MessageBox.Show("Por favor rellene los campos: \n- Nombre de Usuario \n- Contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
            }
            else
            {
                //Check username and password on DB
                using (laboratorio_pEntities db = new laboratorio_pEntities())
                {
                    var query = from usuario in db.usuario
                                where usuario.nombre_usuario == u
                                select usuario;

                    var currentUser = query.ToList();

                    if (currentUser.Count > 0)
                    {
                        if (hashing(p).Equals(currentUser[0].contraseña))
                        {
                            String userType = currentUser[0].tipo.ToString().Trim();
                            id = currentUser[0].id_usuario;

                            if (userType.Equals("admin"))
                            {
                                new AdminPanel.Admin().Show();
                                this.Hide();
                            }
                            else if (userType.Equals("user"))
                            {
                                new UserPanel.User().Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("La contraseña es incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El nombre de usuario o contraseña estan incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ResetPassword.resetPassword().Show();
        }
    }
}
