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
    public partial class AddExamenes : Form
    {
        public AddExamenes()
        {
            InitializeComponent();
        }

        public int id_hemograma = 0;
        public int id_heces = 0;
        public int id_orina = 0;

        paciente pacienteModel = new paciente();
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            dgvBusqueda.Rows.Clear();

            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                var query = from paciente in DB.paciente where paciente.nombre.Contains(txtBuscar.Text) select paciente;

                foreach (var paciente in query.ToList())
                {
                    dgvBusqueda.Rows.Add(paciente.nombre);

                    pacienteModel.id_paciente = paciente.id_paciente;
                    pacienteModel.id_campaña = paciente.id_campaña;
                    pacienteModel.nombre = paciente.nombre;
                    pacienteModel.edad = paciente.edad;
                    pacienteModel.codigo = paciente.codigo;
                    pacienteModel.genero = paciente.genero;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hemograma vHemograma = new Hemograma();
            AddOwnedForm(vHemograma);
            vHemograma.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Orina vOrina = new Orina();
            AddOwnedForm(vOrina);
            vOrina.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Heces vHeces = new Heces();
            AddOwnedForm(vHeces);
            vHeces.Show();
        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            dgvBusqueda.Rows.Clear();

            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                var query = from paciente in DB.paciente where paciente.nombre.Contains(txtBuscar.Text) select paciente;

                foreach (var paciente in query.ToList())
                {
                    dgvBusqueda.Rows.Add(paciente.nombre);

                    pacienteModel.id_paciente = paciente.id_paciente;
                    pacienteModel.id_campaña = paciente.id_campaña;
                    pacienteModel.nombre = paciente.nombre;
                    pacienteModel.edad = paciente.edad;
                    pacienteModel.codigo = paciente.codigo;
                    pacienteModel.genero = paciente.genero;
                }
            }
        }

        private void AddExamenes_Load(object sender, EventArgs e)
        {


            dgvBusqueda.EnableHeadersVisualStyles = false;
            dgvBusqueda.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1682a7");
            dgvBusqueda.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
    }
}
