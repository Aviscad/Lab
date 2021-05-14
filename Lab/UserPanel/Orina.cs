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
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!rtxtObservaciones.Text.Trim().Equals(""))
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
                    examenOrina.sangre_oculta = txtSangreOculta.Text.Trim();
                    examenOrina.cuerpos_cetonicos = txtCuerCeton.Text.Trim();
                    examenOrina.urobilinogeno = txtUrobilinogeno.Text.Trim();
                    examenOrina.bilirrubina = txtBilirrubina.Text.Trim();
                    examenOrina.nitritos = txtNitritos.Text.Trim();
                    examenOrina.hemoglobina = txtHemoglobina.Text.Trim();
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
                }
            }
            else {
                MessageBox.Show("Debe colocar al menos la observación, en caso de que no trajo muestra.","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            else {
                e.Handled = true;
            }
        }

        private void txtAspecto_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDensidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (
                (e.KeyChar >= 48 && e.KeyChar <=57) ||
                e.KeyChar == 08
                
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 ||
                e.KeyChar == 08)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtProteinas_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtGlucosa_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtSangreOculta_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCuerCeton_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtUrobilinogeno_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtBilirrubina_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNitritos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtHemoglobina_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEsteriasaLeuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= 65) && (e.KeyChar <= 90)) ||
              ((e.KeyChar >= 97) && (e.KeyChar <= 122)) ||
               ((e.KeyChar >= 48) && (e.KeyChar <= 57)) ||
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

        private void txtCilindrosGranulosos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtLeucocitarios_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtHematicos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtHialinos_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCelulasEpiteliales_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCristales_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtParasitos_KeyPress(object sender, KeyPressEventArgs e)
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
