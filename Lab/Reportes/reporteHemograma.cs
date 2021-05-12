using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab.Reportes
{
    public partial class reporteHemograma : Form
    {
        public reporteHemograma()
        {
            InitializeComponent();
        }

        private int getEdad(DateTime fecha_nacimiento)
        {
            int EDAD;
            int dia = DateTime.Today.Day;
            int mes = DateTime.Today.Month;
            int anio = DateTime.Today.Year;
            int diaNac = fecha_nacimiento.Day;
            int mesNac = fecha_nacimiento.Month;
            int anioNac = Convert.ToInt32(fecha_nacimiento.Year.ToString().Substring(2, 2));
            int currentYearMod = anio % 100;
            if (anioNac >= 0 && anioNac <= currentYearMod) { anioNac += 2000; }
            else { anioNac += 1900; }
            int age = anio - anioNac;
            if (mes > mesNac) { EDAD = age; }
            else if (mes == mesNac)
            {
                if (dia >= diaNac)
                {
                    EDAD = age;
                }
                else
                {
                    age--;
                    EDAD = age;
                }
            }
            else
            {
                age--;
                EDAD = age;
            }
            return EDAD;
        }

        public paciente paciente;
        public hemograma hemograma;
        private hemograma hemogramaModel = new hemograma();
        private void setData() {
            //DATOS DE PACIENTE
            txtNomPaciente.Text = paciente.nombre;
            txtCodigo.Text = paciente.codigo;
            txtGenero.Text = paciente.genero;
            txtFecha.Text = DateTime.Today.ToShortDateString();
            txtEdad.Text = getEdad(paciente.fecha_nacimiento).ToString();


            //DATOS EXAMEN
            txtGlobulosRojos.Text = hemograma.globulos_rojos;
            txtHemoglobina.Text = hemograma.hemoglobina;
            txtHematocrito.Text = hemograma.hematocrito;
            txtVGM.Text = hemograma.vgm;
            txtHCM.Text = hemograma.hcm;
            txtCHCM.Text = hemograma.chcm;

            //LEUCOCITOS
            txtLeucocitos.Text = hemograma.leucocitos;
            txtNeutroSeg.Text = hemograma.neutrofilos_segmentados;
            txtNeutroBanda.Text = hemograma.neutrofilos_en_banda;
            txtLinfocitos.Text = hemograma.linfocitos;
            txtEosinofilo.Text = hemograma.eosinofilo;
            txtBasofilo.Text = hemograma.basofilo;
            txtMonocitos.Text = hemograma.monocitos;

            //OTROS
            txtPlaquetas.Text = hemograma.plaquetas;
            txtMacroplaquetas.Text = hemograma.macroplaquetas;
            rtxtObservaciones.Text = hemograma.observaciones;
        }

        private void reporteHemograma_Load(object sender, EventArgs e)
        {
            setData();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(groupBox1.ClientRectangle.Width, groupBox1.ClientRectangle.Height);
            groupBox1.DrawToBitmap(bmp, groupBox1.ClientRectangle);
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog ppd = new PrintPreviewDialog();
            PrintDocument Pd = new PrintDocument();
            Pd.PrintPage += printDocument1_PrintPage;
            ppd.Document = Pd;
            ppd.ShowDialog();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;

            //DATOS DE PACIENTE
            txtNomPaciente.Enabled = true;
            txtCodigo.Enabled = true;
            txtGenero.Enabled = true;
            txtFecha.Enabled = true;
            txtEdad.Enabled = true;

            //DATOS EXAMEN
            txtGlobulosRojos.Enabled = true;
            txtHemoglobina.Enabled = true;
            txtHematocrito.Enabled = true;
            txtVGM.Enabled = true;           
            txtHCM.Enabled = true;           
            txtCHCM.Enabled = true;

            //LEUCOCITOS
            txtLeucocitos.Enabled = true;
            txtNeutroSeg.Enabled = true;
            txtNeutroBanda.Enabled = true;
            txtLinfocitos.Enabled = true;
            txtEosinofilo.Enabled = true;
            txtBasofilo.Enabled = true;
            txtMonocitos.Enabled = true;

            //OTROS
            txtPlaquetas.Enabled = true;
            txtMacroplaquetas.Enabled = true;
            rtxtObservaciones.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            //DATOS DE PACIENTE
            txtNomPaciente.Enabled = false;
            txtCodigo.Enabled = false;
            txtGenero.Enabled = false;
            txtFecha.Enabled = false;
            txtEdad.Enabled = false;

            //DATOS EXAMEN
            txtGlobulosRojos.Enabled = false;
            txtHemoglobina.Enabled = false;
            txtHematocrito.Enabled = false;
            txtVGM.Enabled = false;
            txtHCM.Enabled = false;
            txtCHCM.Enabled = false;

            //LEUCOCITOS
            txtLeucocitos.Enabled = false;
            txtNeutroSeg.Enabled = false;
            txtNeutroBanda.Enabled = false;
            txtLinfocitos.Enabled = false;
            txtEosinofilo.Enabled = false;
            txtBasofilo.Enabled = false;
            txtMonocitos.Enabled = false;

            //OTROS
            txtPlaquetas.Enabled = false;
            txtMacroplaquetas.Enabled = false;
            rtxtObservaciones.Enabled = false;

            setData();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!rtxtObservaciones.Text.Trim().Equals(""))
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    hemogramaModel.id_hemograma = hemograma.id_hemograma;

                    //DATOS EXAMEN
                    hemogramaModel.globulos_rojos = txtGlobulosRojos.Text.Trim();
                    hemogramaModel.hemoglobina = txtHemoglobina.Text.Trim();
                    hemogramaModel.hematocrito = txtHematocrito.Text.Trim();
                    hemogramaModel.vgm = txtVGM.Text.Trim();
                    hemogramaModel.hcm = txtHCM.Text.Trim();
                    hemogramaModel.chcm = txtCHCM.Text.Trim();

                    //LEUCOCITOS
                    hemogramaModel.leucocitos = txtLeucocitos.Text.Trim();
                    hemogramaModel.neutrofilos_segmentados = txtNeutroSeg.Text.Trim();
                    hemogramaModel.neutrofilos_en_banda = txtNeutroBanda.Text.Trim();
                    hemogramaModel.linfocitos = txtLinfocitos.Text.Trim();
                    hemogramaModel.eosinofilo = txtEosinofilo.Text.Trim();
                    hemogramaModel.basofilo = txtBasofilo.Text.Trim();
                    hemogramaModel.monocitos = txtMonocitos.Text.Trim();

                    //OTROS
                    hemogramaModel.plaquetas = txtPlaquetas.Text.Trim();
                    hemogramaModel.macroplaquetas = txtMacroplaquetas.Text.Trim();
                    hemogramaModel.observaciones = rtxtObservaciones.Text.Trim();
                    DB.Entry(hemogramaModel).State = EntityState.Modified;
                    DB.SaveChanges();
                    MessageBox.Show("El examen fue modificado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnModificar.Enabled = true;
            }
            else {
                MessageBox.Show("Debe llenar al menos el campo de observaciones si no tiene muestra el paciente", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
