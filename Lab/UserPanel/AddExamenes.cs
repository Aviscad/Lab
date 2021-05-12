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

            if (comboBox1.SelectedIndex == 0)
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var query = from paciente in DB.paciente where paciente.nombre.Contains(txtBuscar.Text) select paciente;

                    foreach (var paciente in query.ToList())
                    {
                        var getCampania = (from campaña in DB.campaña where campaña.id_campaña == paciente.id_campaña select campaña).FirstOrDefault();
                        dgvBusqueda.Rows.Add(paciente.id_paciente, paciente.nombre, paciente.codigo, getCampania.nombre, paciente.fecha_nacimiento.ToShortDateString(), paciente.genero);
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var query = from paciente in DB.paciente where paciente.codigo.StartsWith(txtBuscar.Text) select paciente;

                    foreach (var paciente in query.ToList())
                    {
                        var getCampania = (from campaña in DB.campaña where campaña.id_campaña == paciente.id_campaña select campaña).FirstOrDefault();
                        dgvBusqueda.Rows.Add(paciente.id_paciente, paciente.nombre, paciente.codigo, getCampania.nombre, paciente.fecha_nacimiento.ToShortDateString(), paciente.genero);
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    if (txtBuscar.Text.Trim().Length > 0)
                    {
                            var campania = (from campaña in DB.campaña where campaña.nombre.ToUpper().StartsWith(txtBuscar.Text.Trim()) select campaña).FirstOrDefault();

                            if (campania != null)
                            {
                                var query = from paciente in DB.paciente where paciente.id_campaña == campania.id_campaña select paciente;
                                foreach (var paciente in query.ToList())
                                {
                                    var getCampania = (from campaña in DB.campaña where campaña.id_campaña == paciente.id_campaña select campaña).FirstOrDefault();
                                    dgvBusqueda.Rows.Add(paciente.id_paciente, paciente.nombre, paciente.codigo, getCampania.nombre, paciente.fecha_nacimiento.ToShortDateString(), paciente.genero);
                                }
                            }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pacienteModel.id_paciente != 0)
            {
                if (id_hemograma == 0) {
                    Hemograma vHemograma = new Hemograma();
                    AddOwnedForm(vHemograma);
                    vHemograma.Show();
                }
                else{
                    MessageBox.Show("El examen de hemograma ya fue guardado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente primero! " +
                    "\n 1. Buscar al paciente en la barra de busqueda \n 2. En la tabla de resultados dar doble click en el nombre del paciente.",
                    "Error!!!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pacienteModel.id_paciente != 0)
            {
                if (id_orina == 0)
                {
                    Orina vOrina = new Orina();
                    AddOwnedForm(vOrina);
                    vOrina.Show();
                }
                else {
                    MessageBox.Show("El examen de orina ya fue guardado","Advertencia",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("Seleccione un paciente primero! " +
                     "\n 1. Buscar al paciente en la barra de busqueda \n 2. En la tabla de resultados dar doble click en el nombre del paciente.",
                     "Error!!!",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pacienteModel.id_paciente != 0)
            {
                if (id_heces == 0)
                {
                    Heces vHeces = new Heces();
                    AddOwnedForm(vHeces);
                    vHeces.Show();
                }
                else {
                    MessageBox.Show("El examen de heces ya fue guardado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente primero! " +
                    "\n 1. Buscar al paciente en la barra de busqueda \n 2. En la tabla de resultados dar doble click en el nombre del paciente.",
                    "Error!!!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
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
            comboBox1.SelectedIndex = 0;
            dgvBusqueda.EnableHeadersVisualStyles = false;
            dgvBusqueda.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1682a7");
            dgvBusqueda.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                examenes newExamen = new examenes();
                if (id_heces == 0 || id_hemograma == 0 || id_orina == 0)
                {
                    MessageBox.Show("Complete los datos de todos los examenes \n Para llenar los datos de los examenes hacer lo siguiente:" +
                        "\n     1. Dar click en los botones de Agregar Examen." +
                        "\n     2. Llenar los datos correspondientes del examen." +
                        "\n     3. Dar click en el boton guardar.","Advertencia!!!",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    newExamen.id_paciente = pacienteModel.id_paciente;
                    newExamen.id_orina = id_orina;
                    newExamen.id_heces = id_heces;
                    newExamen.id_hemograma = id_hemograma;
                    newExamen.fecha = DateTime.Today;

                    DB.examenes.Add(newExamen);
                    DB.SaveChanges();

                    MessageBox.Show("Guardado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBuscar.Text = "";
                    dgvBusqueda.Rows.Clear();
                }
            }
        }

        private void dgvBusqueda_DoubleClick(object sender, EventArgs e)
        {
            pacienteModel.id_paciente = Convert.ToInt32(dgvBusqueda.CurrentRow.Cells[0].Value);

            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                try
                {
                    pacienteModel = DB.paciente.Where(x => x.id_paciente == pacienteModel.id_paciente).FirstOrDefault();
                    MessageBox.Show("Paciente seleccionado: " + pacienteModel.nombre,"Información!!!",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            dgvBusqueda.Rows.Clear();
        }
    }
}
