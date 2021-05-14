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
    public partial class Heces : Form
    {
        public Heces()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int proto_ActivosId = 0;
            int proto_QuistesId = 0;
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                //PROTOZOARIOS ACTIVOS
                protozoarios_activos _Activos = new protozoarios_activos();
                _Activos.entamoeba_histolitica = txtEntamoebaHis_Activo.Text.Trim();
                _Activos.entamoeba_coli = txtEntamoebaCol_Activo.Text.Trim();
                _Activos.endolimax_nana = txtEndolimax_Activo.Text.Trim();
                _Activos.guiardia_lamblia = txtGuiardia_Activo.Text.Trim();
                _Activos.trichomonas_hominis = txtTrichomonas_Activos.Text.Trim();
                _Activos.chilomastrix_mesnili = txtChilomastix_Activo.Text.Trim();
                _Activos.blostocistis_hominis = txtBlostocistis_Activo.Text.Trim();

                DB.protozoarios_activos.Add(_Activos);
                DB.SaveChanges();

                proto_ActivosId = _Activos.id_protozoarios_activos;

                //PROTOZOARIOS QUISTES
                protozoarios_quistes _Quistes = new protozoarios_quistes();
                _Quistes.entamoeba_histolitica = txtEntamoebaHis_Quistes.Text.Trim();
                _Quistes.entamoeba_coli = txtEntamoebaCol_Quistes.Text.Trim();
                _Quistes.endolimax_nana = txtEndolimax_Quistes.Text.Trim();
                _Quistes.guiardia_lamblia = txtGuiardia_Quistes.Text.Trim();
                _Quistes.trichomonas_hominis = txtTrichomonas_Quistes.Text.Trim();
                _Quistes.chilomastrix_mesnili = txtChilomastix_Quistes.Text.Trim();
                _Quistes.blostocistis_hominis = txtBlostocistis_Quistes.Text.Trim();

                DB.protozoarios_quistes.Add(_Quistes);
                DB.SaveChanges();

                proto_QuistesId = _Quistes.id_protozoarios_quistes;

                heces eHeces = new heces();

                eHeces.id_protozoarios_activos = proto_ActivosId;
                eHeces.id_protozoarios_quistes = proto_QuistesId;
                eHeces.color = txtColor.Text.Trim();
                eHeces.consistencia = txtConsistencia.Text.Trim();
                eHeces.mucus = txtMucus.Text.Trim();
                eHeces.hematies = txtHematies.Text.Trim();
                eHeces.leucocitos = txtLeucocitos.Text.Trim();
                eHeces.macrofagos = txtMacrofagos.Text.Trim();
                eHeces.restos_alimenticios_macroscopicos = txtRestosAlimen.Text.Trim();
                eHeces.trichuris_trichiura = txtTrichuris.Text.Trim();
                eHeces.ascaris_lumbricoides = txtAscaris.Text.Trim();
                eHeces.uncinaria = txtUncinaria.Text.Trim();
                eHeces.strongyloides_stercoralis = txtEnterobius.Text.Trim();
                eHeces.entorobius_vermicularis = txtEnterobius.Text.Trim();
                eHeces.taenias_sp = txtTaenias.Text.Trim();
                eHeces.observaciones = rtxtObservaciones.Text.Trim();

                DB.heces.Add(eHeces);
                DB.SaveChanges();

                AddExamenes parent = Owner as AddExamenes;
                parent.id_heces = eHeces.id_heces;

                MessageBox.Show("Examen agregado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
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

        private void txtHematies_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
                ((e.KeyChar >= 48) && (e.KeyChar <= 57)) ||
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
                ((e.KeyChar >= 48) && (e.KeyChar <= 57)) ||
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
    }
}
