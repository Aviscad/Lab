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
                    dgvBusqueda.Rows.Add(paciente.id_paciente, paciente.nombre);

                    pacienteModel.id_paciente = paciente.id_paciente;
                    pacienteModel.id_campaña = paciente.id_campaña;
                    pacienteModel.nombre = paciente.nombre;
                    pacienteModel.fecha_nacimiento = paciente.fecha_nacimiento;
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
                    pacienteModel.fecha_nacimiento = paciente.fecha_nacimiento;
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                examenes newExamen = new examenes();

                /*********          OJO:   **************/
                //if(id_heces == 0) <-- VALIDACION DE IDS DE EXAMENES QUE NO VENGAN EN 0 O NULL

                /*********            **************/

                if (id_heces == 0 || id_hemograma == 0 || id_orina == 0)
                {
                    MessageBox.Show("Complete los datos de todos los examenes");
                }
                else {
                    newExamen.id_paciente = pacienteModel.id_paciente;
                    newExamen.id_orina = id_orina;
                    newExamen.id_heces = id_heces;
                    newExamen.id_hemograma = id_hemograma;
                    newExamen.fecha = DateTime.Today;

                    DB.examenes.Add(newExamen);
                    DB.SaveChanges();

                    MessageBox.Show("Guardado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
