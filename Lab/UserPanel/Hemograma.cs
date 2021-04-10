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
    public partial class Hemograma : Form
    {
        public Hemograma()
        {
            InitializeComponent();
        }

        paciente pacienteModel = new paciente();
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        public void Clear() {
            txtBuscarText.Text = "";
            dgvResultadosBusqueda.Rows.Clear();
        }

        private void Hemograma_Load(object sender, EventArgs e)
        {
            cbbBuscarPor.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dgvResultadosBusqueda.Rows.Clear();
            if (cbbBuscarPor.SelectedIndex == 0)
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var query = from paciente in DB.paciente where paciente.nombre.Contains(txtBuscarText.Text) select paciente;

                    foreach (var paciente in query.ToList())
                    {
                        dgvResultadosBusqueda.Rows.Add(paciente.nombre);

                        pacienteModel.id_paciente = paciente.id_paciente;
                        pacienteModel.id_campaña = paciente.id_campaña;
                        pacienteModel.nombre = paciente.nombre;
                        pacienteModel.edad = paciente.edad;
                        pacienteModel.codigo = paciente.codigo;
                        pacienteModel.genero = paciente.genero;
                    }

                    
                }
            }
            else
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var query = from paciente in DB.paciente
                                where paciente.codigo.Contains(txtBuscarText.Text) || paciente.codigo == txtBuscarText.Text
                                select paciente;

                    foreach (var paciente in query.ToList())
                    {
                        dgvResultadosBusqueda.Rows.Add(paciente.nombre);

                        pacienteModel.id_paciente = paciente.id_paciente;
                        pacienteModel.id_campaña = paciente.id_campaña;
                        pacienteModel.nombre = paciente.nombre;
                        pacienteModel.edad = paciente.edad;
                        pacienteModel.codigo = paciente.codigo;
                        pacienteModel.genero = paciente.genero;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities()) {

                hemograma newHemograma = new hemograma();

                newHemograma.globulos_rojos = txtGlobulosRojos.Text.Trim();
                newHemograma.hemoglobina = txtHemoglobina.Text.Trim();
                newHemograma.hematocrito = txtHematocrito.Text.Trim();
                newHemograma.vgm = txtVGM.Text.Trim();
                newHemograma.hcm = txtHCM.Text.Trim();

                //Leucocitos
                newHemograma.leucocitos = txtLeucocitos.Text.Trim();
                newHemograma.neutrofilos_segmentados = txtNeutroSeg.Text.Trim();
                newHemograma.neutrofilos_en_banda = txtNeutroBanda.Text.Trim();
                newHemograma.linfocitos = txtLinfocitos.Text.Trim();
                newHemograma.eosinofilo = txtEosinofilo.Text.Trim();
                newHemograma.basofilo = txtBasofilo.Text.Trim();
                newHemograma.monocitos = txtMonocitos.Text.Trim();

                newHemograma.plaquetas = txtPlaquetas.Text.Trim();
                newHemograma.macroplaquetas = txtMacroplaquetas.Text.Trim();

                newHemograma.observaciones = rtxtObservaciones.Text.Trim();


                DB.hemograma.Add(newHemograma);
                DB.SaveChanges();

                int getHemogramaID = newHemograma.id_hemograma;

                /* NOTA: 
                 * LUEGO DE GUARDAR EL EXAMEN AL USAR EL DB.SAVECHANGES() 
                 * SE ACTUALIZA EL MODEL CON EL ID CORRESPONDIENTE
                 */

                examenes newExamen = new examenes();

                newExamen.id_paciente = pacienteModel.id_paciente;
                newExamen.id_hemograma = getHemogramaID;
                newExamen.fecha = DateTime.Today;

                MessageBox.Show(pacienteModel.id_paciente.ToString());

                DB.examenes.Add(newExamen);
                DB.SaveChanges();

                /*
                 * FALTAN LAS VALIDACIONES Y EL RESET DE LOS TXT ( Clear(); )
                 */

            }
        }
    }
}
