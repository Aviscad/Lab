using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab.UserPanel
{
    public partial class Orina : Form
    {
        public Orina()
        {
            InitializeComponent();
        }
        paciente pacienteModel = new paciente();
        private void Orina_Load(object sender, EventArgs e)
        {
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                orina examenOrina = new orina();

                //Examen fisicoquimico
                examenOrina.color = txtColor.Text.Trim();
                examenOrina.aspecto = txtAspecto.Text.Trim();
                examenOrina.densidad = txtDensidad.Text.Trim();
                examenOrina.ph = txtPh.Text.Trim();
                examenOrina.proteinas = txtProteinas.Text.Trim();
                examenOrina.glucosa = txtGlucosa.Text.Trim();
                examenOrina.sangre_oculta =txtSangreOculta.Text.Trim();
                examenOrina.cuerpos_cetonicos = txtCuerCeton.Text.Trim();
                examenOrina.urobilinogeno = txtUrobilinogeno.Text.Trim();
                examenOrina.bilirrubina = txtBilirrubina.Text.Trim();
                examenOrina.nitritos = txtNitritos.Text.Trim();
                examenOrina.hemoglobina =txtHemoglobina.Text.Trim();
                examenOrina.esteriasa_leucocitaria = txtEsteriasaLeuc.Text.Trim();

                //Examen Microscopio
                examenOrina.cilindros_granulosos = txtCilindrosGranulosos.Text.Trim();
                examenOrina.leucocitarios = txtLeucocitarios.Text.Trim();
                examenOrina.hematicos = txtHematicos.Text.Trim();
                examenOrina.hialinos = txtHialinos.Text.Trim();

                //Otros
                examenOrina.hematies = txtHematies.Text.Trim();
                examenOrina.leucocitos = txtLeucocitos.Text.Trim();
                examenOrina.celulas_epiteliales = txtCelulasEpiteliales.Text.Trim();
                examenOrina.cristales = txtCristales.Text.Trim();
                examenOrina.parasitos = txtParasitos.Text.Trim();
                examenOrina.observaciones = rtxtObservaciones.Text.Trim();

                //insert examen
                DB.orina.Add(examenOrina);
                DB.SaveChanges();

                AddExamenes parent = Owner as AddExamenes;
                parent.id_orina = examenOrina.id_orina;

                MessageBox.Show("Examen agregado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
                /*
                 * FALTAN LAS VALIDACIONES Y EL RESET DE LOS TXT ( Clear(); )
                 */

            }
        }
    }
}
