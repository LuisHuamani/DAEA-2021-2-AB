using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab03
{
    public partial class frmConDB : Form
    {
        public SqlConnection conn;
        public frmConDB()
        {
            InitializeComponent();
        }

        private void frmConDB_Load(object sender, EventArgs e)
        {
            btnCurso.Enabled = false;
            btnPersona.Enabled = false;
            btnDesconectar.Enabled = false;
            btnEstado.Enabled = false;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            //Declaramos variables para conexcion
            
            String servidor = txtServidor.Text;
            String bd = txtBaseDatos.Text;
            String user = txtUsuario.Text;
            String pwd = txtContraseña.Text;

            String str = "Server =" + servidor + ";DataBase = " + bd + ";";

            if (chkAutenticacion.Checked)
            {
                str += "Integrated Security = true";
            }
            else 
            {
                str += "User Id = " + user + ";Password = " + pwd + ";";
            }

            //Abrir conexcion con la BD

            try
            {
                conn = new SqlConnection(str);
                conn.Open();
                MessageBox.Show("Conectado Satisfactoriamente");
                btnDesconectar.Enabled = true;
                btnCurso.Enabled = true;
                btnPersona.Enabled = true;
                btnEstado.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error al conectar al servidor: \n" + ex.ToString());
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if(conn.State == ConnectionState.Open)
                {
                    MessageBox.Show("Estado del servidor:" + conn.State + "\nVersion del Servidor: " 
                        + conn.ServerVersion + "\nBase de Datos: " + conn.Database);
                }
                else
                {
                    MessageBox.Show("Estado del servidor: " + conn.State);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Imposible determinar el estado del servidor: \n" + ex.ToString());
            }
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {
                if(conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    MessageBox.Show("Conexcion cerrada satisfactoriamente");
                    btnDesconectar.Enabled = false;
                    btnCurso.Enabled = false;
                    btnPersona.Enabled = false;
                }
                else
                {
                    MessageBox.Show("La conexcion ya esta cerrada");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocurrio un error al cerrar la conexcion: \n" + ex.ToString());
            }
        }

        private void chkAutenticacion_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAutenticacion.Checked)
            {
                txtUsuario.Enabled = false;
                txtContraseña.Enabled = false;
            }
            else
            {
                txtUsuario.Enabled = true;
                txtContraseña.Enabled = true;
            }
        }

        private void btnPersona_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona(conn);
            persona.Show();
        }

        private void btnCurso_Click(object sender, EventArgs e)
        {
            Curso curso = new Curso(conn);
            curso.Show();
        }
    }
}
