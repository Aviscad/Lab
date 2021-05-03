using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private void reporteHemograma_Load(object sender, EventArgs e)
        {
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
    }
}
