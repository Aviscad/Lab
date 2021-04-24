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
    public partial class Campania : Form
    {
        campaña campaniaModel = new campaña();
        public Campania()
        {
            InitializeComponent();
        }


        int id = 0; //ID de usuario global
        private void Campania_Load(object sender, EventArgs e)
        {
            fillRows();
            //ID de usuario logeado para agregarlo en la creacion de la campaña
            id = login.id;


            //Cambiar de color header
            dgvCampania.EnableHeadersVisualStyles = false;
            dgvCampania.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1682a7");
            dgvCampania.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                campaniaModel.nombre = txtNomCampania.Text.Trim();
                campaniaModel.fecha = DateTime.Today;
                campaniaModel.id_usuario = id;

                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    if (txtNomCampania.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("Rellene todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else {

                        var checkCampania = from campaña in DB.campaña
                                            where campaña.nombre == campaniaModel.nombre
                                            select campaña;

                        if (checkCampania.ToList().Count > 0)
                        {
                            MessageBox.Show("La campaña " + campaniaModel.nombre + " ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else {
                            if (campaniaModel.id_campaña == 0)
                            {
                                var query = from campaña in DB.campaña where campaña.nombre == txtNomCampania.Text.Trim() select campaña;
                                var campaniaCheck = query.ToList();

                                if (campaniaCheck.Count > 0)
                                {
                                    MessageBox.Show("La campaña '" + txtNomCampania.Text.Trim() + "' ya existe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    DB.campaña.Add(campaniaModel); //insert
                                    MessageBox.Show("Registro agregado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                var query = from campaña in DB.campaña where campaña.nombre == txtNomCampania.Text.Trim() select campaña;
                                var campaniaCheck = query.ToList();

                                if (campaniaCheck.Count > 0)
                                {
                                    MessageBox.Show("La campaña '" + txtNomCampania.Text.Trim() + "' ya existe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    DB.Entry(campaniaModel).State = EntityState.Modified; //update
                                    MessageBox.Show("Registro actualizado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            DB.SaveChanges();
                            fillRows();
                            Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void fillRows()
        {
            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                dgvCampania.Rows.Clear();
                foreach (var row in DB.campaña)
                {
                    var query = from usuario in DB.usuario where usuario.id_usuario == row.id_usuario select usuario;
                    var currentUser = query.ToList();
                    string[] dateHelper = row.fecha.ToString().Split(' ');
                    dgvCampania.Rows.Add(row.id_campaña, row.nombre, dateHelper[0], currentUser[0].nombre_usuario);
                }
            }
        }

        private void Clear()
        {
            txtNomCampania.Text = "";
            btnEliminar.Enabled = false;
            btnGuardar.Text = "Guardar";
            campaniaModel.id_campaña = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dgvCampania_DoubleClick(object sender, EventArgs e)
        {
            campaniaModel.id_campaña = Convert.ToInt32(dgvCampania.CurrentRow.Cells[0].Value);

            string newUsername = txtNomCampania.Text.Trim();

            using (laboratorio_pEntities DB = new laboratorio_pEntities())
            {
                try
                {
                    campaniaModel = DB.campaña.Where(x => x.id_campaña == campaniaModel.id_campaña).FirstOrDefault();
                    txtNomCampania.Text = campaniaModel.nombre;
                    btnGuardar.Text = "Modificar";
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
                    var entry = DB.Entry(campaniaModel);
                    if (entry.State == EntityState.Detached)
                        DB.campaña.Attach(campaniaModel);
                    DB.campaña.Remove(campaniaModel);
                    DB.SaveChanges();

                    Clear();
                    fillRows();

                    MessageBox.Show("Registro eliminado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Campania_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
