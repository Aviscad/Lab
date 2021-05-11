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
    public partial class Pacientes : Form
    {
        public Pacientes()
        {
            InitializeComponent();
        }

        private void Clear()
        {
            txtNombre.Text = txtCodigo.Text = "";
            btnEliminar.Enabled = false;
            btnGuardar.Text = "Guardar";
            btnGuardar.Image = Properties.Resources.guardar;
            pacienteModel.id_paciente = 0;

            cbbCampania.SelectedIndex = 0;
            cbbGenero.SelectedIndex = 0;
        }

        paciente pacienteModel = new paciente();

        private void Pacientes_Load(object sender, EventArgs e)
        {
            //Fill campaña comboBox
            using (laboratorio_pEntities DB = new laboratorio_pEntities()) 
            {
                var query = from campaña in DB.campaña select campaña;
                var campaniaTolist = query.ToList();
                cbbCampania.DataSource = campaniaTolist;
                cbbCampania.DisplayMember = "nombre";
                cbbCampania.ValueMember = "id_campaña";
                cbbGenero.SelectedIndex = 0;     
            }
            fillRows();
            Clear();

            dgvPacientes.EnableHeadersVisualStyles = false;
            dgvPacientes.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1682a7");
            dgvPacientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim().Equals("")  )
            {
                MessageBox.Show("Debe proporcionar toda la información del paciente antes de guardar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtNombre.Text, @"^[A-Z\sÑ]+$")) //regEx solo letras
                {
                    MessageBox.Show("El Nombre del paciente debe contener solo letras", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else 
                {
                        pacienteModel.nombre = txtNombre.Text.Trim();
                        pacienteModel.fecha_nacimiento = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
                        pacienteModel.codigo = txtCodigo.Text.Trim();
                        pacienteModel.genero = cbbGenero.SelectedItem.ToString().Trim();
                        pacienteModel.id_campaña = Convert.ToInt32(cbbCampania.SelectedValue.ToString().Trim());

                        using (laboratorio_pEntities DB = new laboratorio_pEntities())
                        {
                            if (pacienteModel.id_paciente == 0){
                                DB.paciente.Add(pacienteModel);
                                MessageBox.Show("Registro agregado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            } else {
                                DB.Entry(pacienteModel).State = EntityState.Modified;
                                MessageBox.Show("Registro actualizado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            DB.SaveChanges();
                        }
                        fillRows();
                        Clear();
                }
            }
        }

        private void fillRows() {
            dgvPacientes.Rows.Clear();
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {

                foreach (var paciente in DB.paciente)
                {
                    var query = from campaña in DB.campaña where campaña.id_campaña == paciente.id_campaña select campaña;
                    var getCampania = query.ToList();

                    dgvPacientes.Rows.Add(paciente.id_paciente, paciente.nombre, paciente.fecha_nacimiento.ToShortDateString(), getCampania[0].nombre, paciente.codigo, paciente.genero);
                }
            }
        }

        private void dgvPacientes_DoubleClick(object sender, EventArgs e)
        {
            pacienteModel.id_paciente = Convert.ToInt32(dgvPacientes.CurrentRow.Cells[0].Value);

            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                try
                {
                    pacienteModel = DB.paciente.Where(x => x.id_paciente == pacienteModel.id_paciente).FirstOrDefault();
                    
                    txtNombre.Text = pacienteModel.nombre;
                    dateTimePicker1.Value = Convert.ToDateTime(pacienteModel.fecha_nacimiento);
                    txtCodigo.Text = pacienteModel.codigo;

                    cbbCampania.SelectedValue = pacienteModel.id_campaña;
                    if (pacienteModel.genero.Equals("Masculino	"))
                    {
                        cbbGenero.SelectedIndex = 0;
                    }
                    else if(pacienteModel.genero.Equals("Femenino")){
                        cbbGenero.SelectedIndex = 1;
                    }
                    btnGuardar.Text = "Modificar";
                    btnGuardar.Image = Properties.Resources.editar;
                    btnEliminar.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Estas seguro de eliminar este registro, luego de hacerlo no se podra recuperar la información?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var entry = DB.Entry(pacienteModel);
                    if (entry.State == EntityState.Detached)
                        DB.paciente.Attach(pacienteModel);
                    DB.paciente.Remove(pacienteModel);
                    DB.SaveChanges();

                    Clear();
                    fillRows();

                    MessageBox.Show("Registro eliminado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
