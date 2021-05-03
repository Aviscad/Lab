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
    public partial class reporteHeces : Form
    {
        public reporteHeces()
        {
            InitializeComponent();
        }


        public paciente paciente;
        public heces heces;
        private void reporteHeces_Load(object sender, EventArgs e)
        {
            //DATOS PACIENTE
            txtNomPaciente.Text = paciente.nombre;
            txtCodigo.Text = paciente.codigo;
            txtGenero.Text = paciente.genero;
            txtFecha.Text = DateTime.Today.ToShortDateString();
            txtEdad.Text = getEdad(paciente.fecha_nacimiento).ToString();

            //DATOS HECES
            txtColor.Text = heces.color;
            txtConsistencia.Text = heces.consistencia;
            txtMucus.Text = heces.mucus;
            txtHematies.Text = heces.hematies;
            txtLeucocitos.Text = heces.leucocitos;
            txtMacrofagos.Text = heces.macrofagos;
            txtMacrofagos.Text = heces.restos_alimenticios_macroscopicos;

            //PROTOZOARIOS ACTIVOS
            try
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var getProtozoariosActivos = DB.protozoarios_activos.Where(m => m.id_protozoarios_activos == heces.id_protozoarios_activos).FirstOrDefault();
                    txtEntamoebaHis_Activo.Text = getProtozoariosActivos.entamoeba_histolitica;
                    txtEntamoebaCol_Activo.Text = getProtozoariosActivos.entamoeba_coli;
                    txtEndolimax_Activo.Text = getProtozoariosActivos.endolimax_nana;
                    txtGuiardia_Activo.Text = getProtozoariosActivos.guiardia_lamblia;
                    txtTrichomonas_Activos.Text = getProtozoariosActivos.trichomonas_hominis;
                    txtChilomastix_Activo.Text = getProtozoariosActivos.chilomastrix_mesnili;
                    txtBlostocistis_Activo.Text = getProtozoariosActivos.blostocistis_hominis;
                }
            }
            catch (Exception)
            {
                throw;
            }

            //PROTOZOARIOS QUISTES
            try
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var getProtozoariosActivos = DB.protozoarios_activos.Where(m => m.id_protozoarios_activos == heces.id_protozoarios_activos).FirstOrDefault();
                    txtEntamoebaHis_Quistes.Text = getProtozoariosActivos.entamoeba_histolitica;
                    txtEntamoebaCol_Quistes.Text = getProtozoariosActivos.entamoeba_coli;
                    txtEndolimax_Quistes.Text = getProtozoariosActivos.endolimax_nana;
                    txtGuiardia_Quistes.Text = getProtozoariosActivos.guiardia_lamblia;
                    txtTrichomonas_Quistes.Text = getProtozoariosActivos.trichomonas_hominis;
                    txtChilomastix_Quistes.Text = getProtozoariosActivos.chilomastrix_mesnili;
                    txtBlostocistis_Quistes.Text = getProtozoariosActivos.blostocistis_hominis;
                }
            }
            catch (Exception)
            {
                throw;
            }

            //METAZOARIOS
            txtTrichuris.Text = heces.trichuris_trichiura;
            txtAscaris.Text = heces.ascaris_lumbricoides;
            txtUncinaria.Text = heces.uncinaria;
            txtStrongy.Text = heces.strongyloides_stercoralis;
            txtEnterobius.Text = heces.entorobius_vermicularis;
            txtTaenias.Text = heces.taenias_sp;

            rtxtObservaciones.Text = heces.observaciones;
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
