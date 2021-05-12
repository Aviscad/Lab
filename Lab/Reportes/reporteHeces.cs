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
    public partial class reporteHeces : Form
    {
        public reporteHeces()
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

            //DATOS HECES
            txtColor.Text = heces.color;
            txtConsistencia.Text = heces.consistencia;
            txtMucus.Text = heces.mucus;
            txtHematies.Text = heces.hematies;
            txtLeucocitos.Text = heces.leucocitos;
            txtMacrofagos.Text = heces.macrofagos;
            txtRestosAlimen.Text = heces.restos_alimenticios_macroscopicos;

            //PROTOZOARIOS ACTIVOS
            try
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var query = DB.protozoarios_activos.Where(m => m.id_protozoarios_activos == heces.id_protozoarios_activos);

                    if (query.ToList().Count() > 0) {
                        var getProtozoariosActivos = query.FirstOrDefault();
                  
                        txtEntamoebaHis_Activo.Text = getProtozoariosActivos.entamoeba_histolitica;
                        txtEntamoebaCol_Activo.Text = getProtozoariosActivos.entamoeba_coli;
                        txtEndolimax_Activo.Text = getProtozoariosActivos.endolimax_nana;
                        txtGuiardia_Activo.Text = getProtozoariosActivos.guiardia_lamblia;
                        txtTrichomonas_Activos.Text = getProtozoariosActivos.trichomonas_hominis;
                        txtChilomastix_Activo.Text = getProtozoariosActivos.chilomastrix_mesnili;
                        txtBlostocistis_Activo.Text = getProtozoariosActivos.blostocistis_hominis;

                        //Objeto PROTOZOARIOS ACTIVOS
                        protoActivos_Model = getProtozoariosActivos;
                    }
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
                    var query = DB.protozoarios_quistes.Where(m => m.id_protozoarios_quistes == heces.id_protozoarios_quistes);
                    if (query.ToList().Count() > 0) {
                        var getProtozoariosQuistes = query.FirstOrDefault();
                        txtEntamoebaHis_Quistes.Text = getProtozoariosQuistes.entamoeba_histolitica;
                        txtEntamoebaCol_Quistes.Text = getProtozoariosQuistes.entamoeba_coli;
                        txtEndolimax_Quistes.Text = getProtozoariosQuistes.endolimax_nana;
                        txtGuiardia_Quistes.Text = getProtozoariosQuistes.guiardia_lamblia;
                        txtTrichomonas_Quistes.Text = getProtozoariosQuistes.trichomonas_hominis;
                        txtChilomastix_Quistes.Text = getProtozoariosQuistes.chilomastrix_mesnili;
                        txtBlostocistis_Quistes.Text = getProtozoariosQuistes.blostocistis_hominis;

                        //Objeto PROTOZOARIOS ACTIVOS
                        protoQuistes_Model = getProtozoariosQuistes;
                    }
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
        public paciente paciente;
        public heces heces;
        private heces hecesModel = new heces();
        private protozoarios_activos protoActivos_Model = new protozoarios_activos();
        private protozoarios_activos protoActivos_Helper = new protozoarios_activos();
        private protozoarios_quistes protoQuistes_Model = new protozoarios_quistes();
        private protozoarios_quistes protoQuistes_Helper = new protozoarios_quistes();
        private void reporteHeces_Load(object sender, EventArgs e)
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;
            btnModificar.Enabled = false;

            //DATOS HECES
            txtColor.Enabled = true;
            txtConsistencia.Enabled = true;
            txtMucus.Enabled = true;
            txtHematies.Enabled = true;
            txtLeucocitos.Enabled = true;
            txtMacrofagos.Enabled = true;
            txtRestosAlimen.Enabled = true;

            //PROTOZOARIOS ACTIVOS
            txtEntamoebaHis_Activo.Enabled = true;
            txtEntamoebaCol_Activo.Enabled = true;
            txtEndolimax_Activo.Enabled = true;
            txtGuiardia_Activo.Enabled = true;
            txtTrichomonas_Activos.Enabled = true;
            txtChilomastix_Activo.Enabled = true;
            txtBlostocistis_Activo.Enabled = true;

            //PROTOZOARIOS QUISTES
            txtEntamoebaHis_Quistes.Enabled = true;
            txtEntamoebaCol_Quistes.Enabled = true;
            txtEndolimax_Quistes.Enabled = true;
            txtGuiardia_Quistes.Enabled = true;
            txtTrichomonas_Quistes.Enabled = true;
            txtChilomastix_Quistes.Enabled = true;
            txtBlostocistis_Quistes.Enabled = true;

            //METAZOARIOS
            txtTrichuris.Enabled = true;
            txtAscaris.Enabled = true;
            txtUncinaria.Enabled = true;
            txtStrongy.Enabled = true;
            txtEnterobius.Enabled = true;
            txtTaenias.Enabled = true;

            rtxtObservaciones.Enabled = true;
        }

        private void desactivar() {
            btnCancelar.Enabled = false;
            btnGuardar.Enabled = false;
            btnModificar.Enabled = true;

            //DATOS HECES
            txtColor.Enabled = false;
            txtConsistencia.Enabled = false;
            txtMucus.Enabled = false;
            txtHematies.Enabled = false;
            txtLeucocitos.Enabled = false;
            txtMacrofagos.Enabled = false;
            txtRestosAlimen.Enabled = false;

            //PROTOZOARIOS ACTIVOS
            txtEntamoebaHis_Activo.Enabled = false;
            txtEntamoebaCol_Activo.Enabled = false;
            txtEndolimax_Activo.Enabled = false;
            txtGuiardia_Activo.Enabled = false;
            txtTrichomonas_Activos.Enabled = false;
            txtChilomastix_Activo.Enabled = false;
            txtBlostocistis_Activo.Enabled = false;

            //PROTOZOARIOS QUISTES
            txtEntamoebaHis_Quistes.Enabled = false;
            txtEntamoebaCol_Quistes.Enabled = false;
            txtEndolimax_Quistes.Enabled = false;
            txtGuiardia_Quistes.Enabled = false;
            txtTrichomonas_Quistes.Enabled = false;
            txtChilomastix_Quistes.Enabled = false;
            txtBlostocistis_Quistes.Enabled = false;

            //METAZOARIOS
            txtTrichuris.Enabled = false;
            txtAscaris.Enabled = false;
            txtUncinaria.Enabled = false;
            txtStrongy.Enabled = false;
            txtEnterobius.Enabled = false;
            txtTaenias.Enabled = false;

            rtxtObservaciones.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            desactivar();
            setData();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!rtxtObservaciones.Text.Trim().Equals(""))
            {
                hecesModel.id_heces = heces.id_heces;
                
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                    {
                        //DATOS HECES
                        hecesModel.color = txtColor.Text.Trim();
                        hecesModel.consistencia = txtConsistencia.Text.Trim();
                        hecesModel.mucus = txtMucus.Text.Trim();
                        hecesModel.hematies = txtHematies.Text.Trim();
                        hecesModel.leucocitos = txtLeucocitos.Text.Trim();
                        hecesModel.macrofagos = txtMacrofagos.Text.Trim();
                        hecesModel.restos_alimenticios_macroscopicos = txtRestosAlimen.Text.Trim();

                        //PROTOZOARIOS ACTIVOS
                        protoActivos_Helper.id_protozoarios_activos = protoActivos_Model.id_protozoarios_activos;
                        hecesModel.id_protozoarios_activos = protoActivos_Helper.id_protozoarios_activos;

                        protoActivos_Helper.entamoeba_histolitica = txtEntamoebaHis_Activo.Text.Trim();
                        protoActivos_Helper.entamoeba_coli = txtEntamoebaCol_Activo.Text.Trim();
                        protoActivos_Helper.endolimax_nana = txtEndolimax_Activo.Text.Trim();
                        protoActivos_Helper.guiardia_lamblia = txtGuiardia_Activo.Text.Trim();
                        protoActivos_Helper.trichomonas_hominis = txtTrichomonas_Activos.Text.Trim();
                        protoActivos_Helper.chilomastrix_mesnili = txtChilomastix_Activo.Text.Trim();
                        protoActivos_Helper.blostocistis_hominis = txtBlostocistis_Activo.Text.Trim();
                        DB.Entry(protoActivos_Helper).State = EntityState.Modified;
                        DB.SaveChanges();

                        //PROTOZOARIOS QUISTES
                        protoQuistes_Helper.id_protozoarios_quistes = protoQuistes_Model.id_protozoarios_quistes;
                        hecesModel.id_protozoarios_quistes = protoQuistes_Helper.id_protozoarios_quistes;

                        protoQuistes_Helper.entamoeba_histolitica = txtEntamoebaHis_Quistes.Text.Trim();
                        protoQuistes_Helper.entamoeba_coli = txtEntamoebaCol_Quistes.Text.Trim();
                        protoQuistes_Helper.endolimax_nana = txtEndolimax_Quistes.Text.Trim();
                        protoQuistes_Helper.guiardia_lamblia = txtGuiardia_Quistes.Text.Trim();
                        protoQuistes_Helper.trichomonas_hominis = txtTrichomonas_Quistes.Text.Trim();
                        protoQuistes_Helper.chilomastrix_mesnili = txtChilomastix_Quistes.Text.Trim();
                        protoQuistes_Helper.blostocistis_hominis = txtBlostocistis_Quistes.Text.Trim();
                        DB.Entry(protoQuistes_Helper).State = EntityState.Modified;
                        DB.SaveChanges();

                        //METAZOARIOS
                        hecesModel.trichuris_trichiura = txtTrichuris.Text.Trim();
                        hecesModel.ascaris_lumbricoides = txtAscaris.Text.Trim();
                        hecesModel.uncinaria = txtUncinaria.Text.Trim();
                        hecesModel.strongyloides_stercoralis = txtStrongy.Text.Trim();
                        hecesModel.entorobius_vermicularis = txtEnterobius.Text.Trim();
                        hecesModel.taenias_sp = txtTaenias.Text.Trim();

                        hecesModel.observaciones = rtxtObservaciones.Text.Trim();
                        DB.Entry(hecesModel).State = EntityState.Modified;
                        DB.SaveChanges();
                        MessageBox.Show("El examen fue modificado correctamente!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                
                desactivar();
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnModificar.Enabled = true;
            }
            else {
                MessageBox.Show("Debe llenar al menos el campo de observaciones si no tiene muestra el paciente", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
                ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
                (e.KeyChar == 08) ||
                e.KeyChar == 164 ||
                e.KeyChar == 165 ||
                Char.IsSeparator(e.KeyChar)
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtMucus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtMacrofagos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtConsistencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
              ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
              (e.KeyChar == 08) ||
              e.KeyChar == 164 ||
              e.KeyChar == 165 ||
              Char.IsSeparator(e.KeyChar)
              )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtHematies_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
                ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
                (e.KeyChar == 08) ||
                e.KeyChar == 164 ||
                e.KeyChar == 45 ||
                e.KeyChar == 46 ||
                e.KeyChar == 165 ||
                Char.IsSeparator(e.KeyChar)
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtLeucocitos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
                ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
                (e.KeyChar == 08) ||
                e.KeyChar == 164 ||
                e.KeyChar == 45 ||
                e.KeyChar == 46 ||
                e.KeyChar == 165 ||
                Char.IsSeparator(e.KeyChar)
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtRestosAlimen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
                ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
                (e.KeyChar == 08) ||
                e.KeyChar == 164 ||
                e.KeyChar == 165 ||
                Char.IsSeparator(e.KeyChar)
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtEntamoebaHis_Activo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtEntamoebaCol_Activo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtEndolimax_Activo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtGuiardia_Activo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtTrichomonas_Activos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtChilomastix_Activo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtBlostocistis_Activo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtEntamoebaHis_Quistes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtEntamoebaCol_Quistes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtEndolimax_Quistes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtGuiardia_Quistes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtTrichomonas_Quistes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtChilomastix_Quistes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtBlostocistis_Quistes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtUncinaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtTaenias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtTrichuris_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtAscaris_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtStrongy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtEnterobius_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
               ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               (e.KeyChar == 08) ||
               e.KeyChar == 164 ||
               e.KeyChar == 46 ||
               e.KeyChar == 43 ||
               e.KeyChar == 45 ||
               e.KeyChar == 165 ||
               Char.IsSeparator(e.KeyChar)
               )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
