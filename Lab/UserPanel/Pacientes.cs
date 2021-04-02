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
    public partial class Pacientes : Form
    {
        public Pacientes()
        {
            InitializeComponent();
        }

        private void Pacientes_Load(object sender, EventArgs e)
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities()) 
            {
                var query = from campaña in DB.campaña select campaña;
                var campaniaTolist = query.ToList();

                cbbCampania.DataSource = campaniaTolist;
                cbbCampania.DisplayMember = "nombre";
                cbbCampania.ValueMember = "id_campaña";

                cbbGenero.SelectedIndex = 0;
                           
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ID de campa;a seleccionada del combobox
            MessageBox.Show(cbbCampania.SelectedValue.ToString());   
        }
    }
}
