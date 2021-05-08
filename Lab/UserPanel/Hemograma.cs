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

        private int idExamen = 0;
        paciente pacienteModel = new paciente();
        public int getExamenId() {
            return idExamen;
        }

        private void Hemograma_Load(object sender, EventArgs e)
        {
            
        }

       
        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {

                hemograma newHemograma = new hemograma();

                newHemograma.globulos_rojos = txtGlobulosRojos.Text.Trim();
                newHemograma.hemoglobina = txtHemoglobina.Text.Trim();
                newHemograma.hematocrito = txtHematocrito.Text.Trim();
                newHemograma.vgm = txtVGM.Text.Trim();
                newHemograma.hcm = txtHCM.Text.Trim();
                newHemograma.chcm = txtCHCM.Text.Trim();

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

                MessageBox.Show("Examen agregado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AddExamenes parent = Owner as AddExamenes;
                parent.id_hemograma = newHemograma.id_hemograma;

                this.Close();

                /*
                 * FALTAN LAS VALIDACIONES Y EL RESET DE LOS TXT ( Clear(); )
                 */

            }
        }
    }
}
