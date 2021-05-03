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
    public partial class reporteOrina : Form
    {
        public reporteOrina()
        {
            InitializeComponent();
        }


        public paciente paciente;
        public orina orina;
        private void reporteOrina_Load(object sender, EventArgs e)
        {
            //DATOS PACIENTE
            txtNomPaciente.Text = paciente.nombre;
            txtCodigo.Text = paciente.codigo;
            txtGenero.Text = paciente.genero;
            txtFecha.Text = DateTime.Today.ToShortDateString();
            txtEdad.Text = getEdad(paciente.fecha_nacimiento).ToString();

            //DATOS ORINA

            //EXAMEN FISICOQUIMICO
            txtColor.Text = orina.color;
            txtAspecto.Text = orina.aspecto;
            txtDensidad.Text = orina.densidad;
            txtPh.Text = orina.ph;
            txtProteinas.Text = orina.proteinas;
            txtGlucosa.Text = orina.glucosa;
            txtSangreOculta.Text = orina.sangre_oculta;
            txtCuerCeton.Text = orina.cuerpos_cetonicos;
            txtUrobilinogeno.Text = orina.urobilinogeno;
            txtBilirrubina.Text = orina.bilirrubina;
            txtNitritos.Text = orina.nitritos;
            txtHemoglobina.Text = orina.hemoglobina;
            txtEsteriasaLeuc.Text = orina.esteriasa_leucocitaria;

            //EXAMEN MICROSCOPICO
            txtCilindrosGranulosos.Text = orina.cilindros_granulosos;
            txtLeucocitarios.Text = orina.leucocitarios;
            txtHematicos.Text = orina.hematicos;
            txtHialinos.Text = orina.hialinos;

            //OTROS
            txtHematies.Text = orina.hematies;
            txtLeucocitos.Text = orina.leucocitos;
            txtCelulasEpiteliales.Text = orina.celulas_epiteliales;
            txtCristales.Text = orina.celulas_epiteliales;
            txtParasitos.Text = orina.parasitos;
            rtxtObservaciones.Text = orina.observaciones;
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
