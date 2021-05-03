using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab.UserPanel
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private Form formActivado = null;
        private void AbrirFormWrapper(Form FormHijo)
        {
            if (formActivado != null)
            {
                formActivado.Close();
            }

            formActivado = FormHijo;
            FormHijo.TopLevel = false;
            FormHijo.Dock = DockStyle.Fill;
            Wrapper.Controls.Add(FormHijo);
            Wrapper.Tag = FormHijo;
            FormHijo.BringToFront();
            FormHijo.Show();
        }

        public int id_examen = 0;
        public int id_paciente = 0;

        private void User_Load(object sender, EventArgs e)
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities()) 
            {
                var getUserName = (from usuario in DB.usuario
                                where usuario.id_usuario == login.id
                                select usuario.nombre_usuario).FirstOrDefault();
                lblUserLogged.Text = "BIENVENIDO: \n" + getUserName.ToString().ToUpper();
            }

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormWrapper(new Campania());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormWrapper(new Pacientes());
        }

        private void btnHemograma_Click(object sender, EventArgs e)
        {
            AbrirFormWrapper(new Hemograma());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormWrapper(new Campania());
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            AbrirFormWrapper(new Pacientes());
        }
        private void btnHemograma_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea salir?", "Información", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new login().Show();
        }

        private void User_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnOrina_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AbrirFormWrapper(new AddExamenes());
        }

        private void Wrapper_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AbrirFormWrapper(new ReportesExamenes());
        }
    }
}
