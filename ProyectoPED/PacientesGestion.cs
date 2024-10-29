﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ProyectoPED
{
    public partial class PacientesGestion : Form
    {
        public PacientesGestion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dui = textBox1.Text;
            string nombre = textBox2.Text;
            int edad = 0;
            if (!int.TryParse(textBox3.Text, out edad))
            {
                MessageBox.Show("Edad inválida. Debe ser un número entero.");
                return;
            }

            string telefono = textBox4.Text;
            string sexo = comboBox1.Text;
            decimal peso = 0;
            if (!decimal.TryParse(textBox5.Text, out peso))
            {
                MessageBox.Show("Peso inválido. Debe ser un número decimal.");
                return;
            }

            string tipoSangre = comboBox2.Text;
            decimal estatura = 0;
            if (!decimal.TryParse(textBox6.Text, out estatura))
            {
                MessageBox.Show("Estatura inválida. Debe ser un número decimal.");
                return;
            }

            // Validar el formato del DUI (ejemplo: 00000000-0)
            if (!Regex.IsMatch(dui, @"^\d{8}-\d$"))
            {
                MessageBox.Show("Formato de DUI inválido. Debe ser 00000000-0.");
                return;
            }

            // Validar el formato del nombre (Nombre Apellido)
            if (!Regex.IsMatch(nombre, @"^[a-zA-Z]+\s[a-zA-Z]+$"))
            {
                MessageBox.Show("Formato de nombre inválido. Debe ser Nombre Apellido.");
                return;
            }

            // Validar el formato del teléfono (0000-0000)
            if (!Regex.IsMatch(telefono, @"^\d{4}-\d{4}$"))
            {
                MessageBox.Show("Formato de teléfono inválido. Debe ser 0000-0000.");
                return;
            }

            // Validar el sexo (solo Masculino o Femenino)
            if (sexo != "Masculino" && sexo != "Femenino")
            {
                MessageBox.Show("Sexo inválido. Debe ser Masculino o Femenino.");
                return;
            }

            // Validar el tipo de sangre (tipos de sangre comunes)
            List<string> tiposSangreValidos = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            if (!tiposSangreValidos.Contains(tipoSangre))
            {
                MessageBox.Show("Tipo de sangre inválido.");
                return;
            }

            // Si pasa todas las validaciones, procedemos a insertar el paciente
            Paciente paciente = new Paciente
            {
                dui = dui,
                nombre = nombre,
                edad = edad,
                telefono = telefono,
                sexo = sexo,
                peso = peso,
                tipoSangre = tipoSangre,
                estatura = estatura
            };

            int result = PacienteFun.AgregarPaciente(paciente);

            if (result > 0)
            {
                MessageBox.Show("Paciente ingresado exitosamente.");
                LimpiarCampos(); // Método para limpiar los campos después de la inserción
                recargarScreen(); // Método para recargar la pantalla
            }
            else
            {
                MessageBox.Show("Error al guardar al paciente.");
            }

            /**
            if (id != null)
            {
                int result = PacienteFun.ModificarPaciente(paciente);


                if (result > 0)
                {
                    MessageBox.Show("Paciente Modificado Exitosamente");

                }
                else
                {
                    MessageBox.Show("Error al Modificar al Paciente");
                }

                recargarScreen();
            }
            else
            {
                paciente.id = id;
                int result = PacienteFun.AgregarPaciente(paciente);


                if (result > 0)
                {
                    MessageBox.Show("Paciente Ingresado Exitosamente");

                }
                else
                {
                    MessageBox.Show("Error al Guardar al Paciente");
                }

                recargarScreen();
            }

            **/

            dataGridView1.DataSource = PacienteFun.PacienteRegistro();
        }

        private void LimpiarCampos()
        {
            // Limpiar todos los campos del formulario
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            comboBox1.SelectedIndex = -1; // Limpiar selección en el combo box
            textBox5.Text = string.Empty;
            comboBox2.SelectedIndex = -1; // Limpiar selección en el combo box
            textBox6.Text = string.Empty;
        }

        public void recargarScreen()
        {
            dataGridView1.DataSource = PacienteFun.PacienteRegistro();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            this.Hide();
            selectActionAdmin seleccionarAccion = new selectActionAdmin();
            seleccionarAccion.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            editPaciente editPaciente = new editPaciente();
            editPaciente.Show();
        }

        private void PacientesGestion_Load(object sender, EventArgs e)
        {
            recargarScreen();
        }
    }
}

