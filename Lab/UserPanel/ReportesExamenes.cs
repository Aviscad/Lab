using Lab.Reportes;
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
    public partial class ReportesExamenes : Form
    {
        public ReportesExamenes()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim().Equals(""))
            {
                MessageBox.Show("No se ha seleccionado ningun paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (examenesModel.id_orina == null)
            {
                MessageBox.Show("No se ha seleccionado ningun examen");
            }
            else
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    reporteOrina toReporteOrina = new reporteOrina();
                    orina getOrina = new orina();
                    try
                    {
                        var datosOrina = DB.orina.Where(m => m.id_orina == examenesModel.id_orina).FirstOrDefault();
                        AddOwnedForm(toReporteOrina);
                        toReporteOrina.paciente = pacienteModel;
                        toReporteOrina.orina = datosOrina;
                        toReporteOrina.Show();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("El paciente: " + pacienteModel.nombre + " no tiene registrado un examen de orina", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        paciente pacienteModel = new paciente();
        examenes examenesModel = new examenes();
        private void txtBuscar_TextChanged(object sender, EventArgs e)
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
                    var campania = (from campaña in DB.campaña where campaña.nombre.ToUpper().StartsWith(txtBuscar.Text.Trim()) select campaña).FirstOrDefault();
                    var query = from paciente in DB.paciente where paciente.id_campaña == campania.id_campaña select paciente;
                    if (txtBuscar.Text.Trim().Length > 0)
                    {

                        foreach (var paciente in query.ToList())
                        {
                            var getCampania = (from campaña in DB.campaña where campaña.id_campaña == paciente.id_campaña select campaña).FirstOrDefault();
                            dgvBusqueda.Rows.Add(paciente.id_paciente, paciente.nombre, paciente.codigo, getCampania.nombre, paciente.fecha_nacimiento.ToShortDateString(), paciente.genero);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim().Equals(""))
            {
                MessageBox.Show("No se ha seleccionado ningun paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (examenesModel.id_hemograma == null) {
                MessageBox.Show("No se ha seleccionado ningun examen");
            }
            else
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    reporteHemograma toReporteHemograma = new reporteHemograma();
                    hemograma getHemograma = new hemograma();
                    try
                    {
                        var datosHemograma = DB.hemograma.Where(m => m.id_hemograma == examenesModel.id_hemograma).FirstOrDefault();
                        AddOwnedForm(toReporteHemograma);
                        toReporteHemograma.paciente = pacienteModel;
                        toReporteHemograma.hemograma = datosHemograma;
                        toReporteHemograma.Show();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("El paciente: " + pacienteModel.nombre + " no tiene registrado un hemograma", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim().Equals(""))
            {
                MessageBox.Show("No se ha seleccionado ningun paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (examenesModel.id_heces == null)
            {
                MessageBox.Show("No se ha seleccionado ningun examen");
            }
            else
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    reporteHeces toReporteHeces = new reporteHeces();
                    heces getHeces = new heces();
                    try
                    {
                        var datosHeces = DB.heces.Where(m => m.id_heces == examenesModel.id_heces).FirstOrDefault();
                        AddOwnedForm(toReporteHeces);
                        toReporteHeces.paciente = pacienteModel;
                        toReporteHeces.heces = datosHeces;
                        toReporteHeces.Show();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("El paciente: " + pacienteModel.nombre + " no tiene registrado un examen de heces", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ReportesExamenes_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            //Cambiar de color header
            dgvBusqueda.EnableHeadersVisualStyles = false;
            dgvBusqueda.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1682a7");
            dgvBusqueda.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1682a7");
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            dgvBusqueda.Rows.Clear();
        }

        private void dgvBusqueda_DoubleClick(object sender, EventArgs e)
        {
            pacienteModel.id_paciente = Convert.ToInt32(dgvBusqueda.CurrentRow.Cells[0].Value);

            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                try
                {
                    pacienteModel = DB.paciente.Where(x => x.id_paciente == pacienteModel.id_paciente).FirstOrDefault();
                    MessageBox.Show("Seleccionado: " + pacienteModel.nombre);


                    /* SELECCIONAR DATOS DE EXAMENES */
                    paciente getPaciente = new paciente();
                    orina getOrina = new orina();

                    dataGridView1.Rows.Clear();
                    var query = DB.examenes.Where(m => m.id_paciente == pacienteModel.id_paciente);
                    var listado = query.ToList();
                    if (listado.Count == 0)
                    {
                        MessageBox.Show("El paciente: " + pacienteModel.nombre + " no tiene registrado ningun examen.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        foreach (var reg in listado)
                        {
                            dataGridView1.Rows.Add(reg.id_examenes, reg.id_orina, reg.id_heces, reg.id_hemograma, reg.fecha.ToShortDateString());
                        }
                    }
                    /* FIN SELECCION DE EXAMENES */

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            examenesModel.id_examenes = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                try
                {     
                    examenesModel = DB.examenes.Where(x => x.id_examenes == examenesModel.id_examenes).FirstOrDefault();
                    MessageBox.Show("Seleccionado el examen con fecha: " + examenesModel.fecha.ToShortDateString());


                    /* SELECCIONAR DATOS DE EXAMENES */
                    paciente getPaciente = new paciente();
                    dataGridView1.Rows.Clear();
                    var query = DB.examenes.Where(m => m.id_paciente == pacienteModel.id_paciente);
                    var listado = query.ToList();
                    foreach (var reg in listado)
                    {
                        dataGridView1.Rows.Add(reg.id_examenes, reg.id_orina, reg.id_heces, reg.id_hemograma, reg.fecha.ToShortDateString());
                    }
                    /* FIN SELECCION DE EXAMENES */
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
