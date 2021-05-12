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
    public partial class reporteOrina : Form
    {
        public reporteOrina()
        {
            InitializeComponent();
        }

        private void setData() {
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

        public paciente paciente;
        public orina orina;
        public orina orinaModel = new orina();
        private void reporteOrina_Load(object sender, EventArgs e)
        {
            setData();
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

        private void button1_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            btnModificar.Enabled = false;
            btnCancelar.Enabled = true;
            //DATOS ORINA

            //EXAMEN FISICOQUIMICO
            txtColor.Enabled = true;          
            txtAspecto.Enabled = true;
            txtDensidad.Enabled = true;
            txtPh.Enabled = true;       
            txtProteinas.Enabled = true;
            txtGlucosa.Enabled = true;            
            txtSangreOculta.Enabled = true;
            txtCuerCeton.Enabled = true;
            txtUrobilinogeno.Enabled = true;
            txtBilirrubina.Enabled = true;
            txtNitritos.Enabled = true;
            txtHemoglobina.Enabled = true;
            txtEsteriasaLeuc.Enabled = true;

            //EXAMEN MICROSCOPICO
            txtCilindrosGranulosos.Enabled = true;
            txtLeucocitarios.Enabled = true;
            txtHematicos.Enabled = true;;
            txtHialinos.Enabled = true;

            //OTROS
            txtHematies.Enabled = true;
            txtLeucocitos.Enabled = true;
            txtCelulasEpiteliales.Enabled = true;
            txtCristales.Enabled = true;
            txtParasitos.Enabled = true;
            rtxtObservaciones.Enabled = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnModificar.Enabled = true;
            btnGuardar.Enabled = false;

            //EXAMEN FISICOQUIMICO
            txtColor.Enabled = false;
            txtAspecto.Enabled = false;
            txtDensidad.Enabled = false;
            txtPh.Enabled = false;
            txtProteinas.Enabled = false;
            txtGlucosa.Enabled = false;
            txtSangreOculta.Enabled = false;
            txtCuerCeton.Enabled = false;
            txtUrobilinogeno.Enabled = false;
            txtBilirrubina.Enabled = false;
            txtNitritos.Enabled = false;
            txtHemoglobina.Enabled = false;
            txtEsteriasaLeuc.Enabled = false;

            //EXAMEN MICROSCOPICO
            txtCilindrosGranulosos.Enabled = false;
            txtLeucocitarios.Enabled = false;
            txtHematicos.Enabled = false; ;
            txtHialinos.Enabled = false;

            //OTROS
            txtHematies.Enabled = false;
            txtLeucocitos.Enabled = false;
            txtCelulasEpiteliales.Enabled = false;
            txtCristales.Enabled = false;
            txtParasitos.Enabled = false;
            rtxtObservaciones.Enabled = false;

            setData();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!rtxtObservaciones.Text.Trim().Equals(""))
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    //DATOS MODIFICADOS DE EXAMEN
                    orinaModel.id_orina = orina.id_orina;

                    //Examen fisicoquimico
                    orinaModel.color = txtColor.Text.Trim();
                    orinaModel.aspecto = txtAspecto.Text.Trim();
                    orinaModel.densidad = txtDensidad.Text.Trim();
                    orinaModel.ph = txtPh.Text.Trim();
                    orinaModel.proteinas = txtProteinas.Text.Trim();
                    orinaModel.glucosa = txtGlucosa.Text.Trim();
                    orinaModel.sangre_oculta = txtSangreOculta.Text.Trim();
                    orinaModel.cuerpos_cetonicos = txtCuerCeton.Text.Trim();
                    orinaModel.urobilinogeno = txtUrobilinogeno.Text.Trim();
                    orinaModel.bilirrubina = txtBilirrubina.Text.Trim();
                    orinaModel.nitritos = txtNitritos.Text.Trim();
                    orinaModel.hemoglobina = txtHemoglobina.Text.Trim();
                    orinaModel.esteriasa_leucocitaria = txtEsteriasaLeuc.Text.Trim();

                    //Examen Microscopio
                    orinaModel.cilindros_granulosos = txtCilindrosGranulosos.Text.Trim();
                    orinaModel.leucocitarios = txtLeucocitarios.Text.Trim();
                    orinaModel.hematicos = txtHematicos.Text.Trim();
                    orinaModel.hialinos = txtHialinos.Text.Trim();

                    //Otros
                    orinaModel.hematies = txtHematies.Text.Trim();
                    orinaModel.leucocitos = txtLeucocitos.Text.Trim();
                    orinaModel.celulas_epiteliales = txtCelulasEpiteliales.Text.Trim();
                    orinaModel.cristales = txtCristales.Text.Trim();
                    orinaModel.parasitos = txtParasitos.Text.Trim();

                    orinaModel.observaciones = rtxtObservaciones.Text.Trim();

                    DB.Entry(orinaModel).State = EntityState.Modified;
                    DB.SaveChanges();
                    MessageBox.Show("El examen fue modificado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnModificar.Enabled = true;
            }
            else {
                MessageBox.Show("Debe llenar al menos el campo de observaciones si no tiene muestra el paciente","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
