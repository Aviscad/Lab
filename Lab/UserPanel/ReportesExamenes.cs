﻿using Lab.Reportes;
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
    public partial class ReportesExamenes : Form
    {
        public ReportesExamenes()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            reporteOrina toReporteOrina = new reporteOrina();
            paciente getPaciente = new paciente();
            orina getOrina = new orina();

            if (txtBuscar.Text.Trim().Equals(""))
            {
                MessageBox.Show("No se ha seleccionado ningun paciente","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                int idPaciente = pacienteModel.id_paciente;

                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var pacienteHelper = DB.paciente.Where(m => m.id_paciente == idPaciente).FirstOrDefault();
                    try
                    {
                        var orinaHelper = DB.examenes.Where(m => m.id_paciente == idPaciente).FirstOrDefault();
                        var getOrinaHelper = DB.orina.Where(m => m.id_orina == orinaHelper.id_orina).FirstOrDefault();
                        getPaciente = pacienteHelper;
                        getOrina = getOrinaHelper;
                        AddOwnedForm(toReporteOrina);
                        toReporteOrina.paciente = getPaciente;
                        toReporteOrina.orina = getOrina;
                        toReporteOrina.Show();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("El paciente: " + pacienteHelper.nombre + " no tiene registrado un examen de orina","Error!!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
        }

        paciente pacienteModel = new paciente();
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim().Length == 0)
            {
                dgvBusqueda.Rows.Clear();
            }
            else
            {
                dgvBusqueda.Rows.Clear();

                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var query = from paciente in DB.paciente where paciente.nombre.Contains(txtBuscar.Text) select paciente;

                    foreach (var paciente in query.ToList())
                    {
                        dgvBusqueda.Rows.Add(paciente.id_paciente, paciente.nombre);

                        pacienteModel.id_paciente = paciente.id_paciente;
                        pacienteModel.id_campaña = paciente.id_campaña;
                        pacienteModel.nombre = paciente.nombre;
                        pacienteModel.fecha_nacimiento = paciente.fecha_nacimiento;
                        pacienteModel.codigo = paciente.codigo;
                        pacienteModel.genero = paciente.genero;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reporteHemograma toReporteHemograma = new reporteHemograma();
            paciente getPaciente = new paciente();
            hemograma getHemograma = new hemograma();

            if (txtBuscar.Text.Trim().Equals(""))
            {
                MessageBox.Show("No se ha seleccionado ningun paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int idPaciente = pacienteModel.id_paciente;

                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var pacienteHelper = DB.paciente.Where(m => m.id_paciente == idPaciente).FirstOrDefault();
                    try
                    {
                        var hemogramaHelper = DB.examenes.Where(m => m.id_paciente == idPaciente).FirstOrDefault();
                        var getHemogramaHelper = DB.hemograma.Where(m => m.id_hemograma == hemogramaHelper.id_hemograma).FirstOrDefault();
                        getPaciente = pacienteHelper;
                        getHemograma = getHemogramaHelper;
                        AddOwnedForm(toReporteHemograma);
                        toReporteHemograma.paciente = getPaciente;
                        toReporteHemograma.hemograma = getHemograma;
                        toReporteHemograma.Show();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("El paciente: " + pacienteHelper.nombre + " no tiene registrado un hemograma", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reporteHeces toReporteHeces = new reporteHeces();
            paciente getPaciente = new paciente();
            heces getHeces = new heces();

            if (txtBuscar.Text.Trim().Equals(""))
            {
                MessageBox.Show("No se ha seleccionado ningun paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int idPaciente = pacienteModel.id_paciente;

                using (laboratorio_pEntities DB = new laboratorio_pEntities())
                {
                    var pacienteHelper = DB.paciente.Where(m => m.id_paciente == idPaciente).FirstOrDefault();
                    try
                    {
                        var hecesHelper = DB.examenes.Where(m => m.id_paciente == idPaciente).FirstOrDefault();
                        var getHecesHelper = DB.heces.Where(m => m.id_heces == hecesHelper.id_heces).FirstOrDefault();
                        getPaciente = pacienteHelper;
                        getHeces = getHecesHelper;
                        AddOwnedForm(toReporteHeces);
                        toReporteHeces.paciente = getPaciente;
                        toReporteHeces.heces = getHeces;
                        toReporteHeces.Show();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("El paciente: " + pacienteHelper.nombre + " no tiene registrado un examen de heces", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
