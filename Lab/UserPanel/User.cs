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

        private void User_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void User_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Campania().Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Pacientes().Show();
        }
    }
}
