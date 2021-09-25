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

namespace Lab05
{
    public partial class frmPerson : Form
    {
        SqlConnection con;
        public frmPerson()
        {
            InitializeComponent();
        }

        private void frmPerson_Load(object sender, EventArgs e)
        {
            String str = "Server=DESKTOP-PTAILR8\\LOCAL;DataBase=School;Integrated Security=true";
            con = new SqlConnection(str);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            con.Open();
            String sql = "select * from Person";
            SqlCommand cmd = new SqlCommand(sql,con);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            dgvListado.DataSource = dt;
            dgvListado.Refresh();
            con.Close();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            con.Open();
            string sp = "InsertPerson";
            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", txtNombre.Text);
            cmd.Parameters.AddWithValue("@LastName", txtApellido.Text);
            cmd.Parameters.AddWithValue("@HireDate", dtpContrato.Value);
            cmd.Parameters.AddWithValue("@EnrollmentDate", dtpInscripcion.Value);

            int codigo = Convert.ToInt32(cmd.ExecuteScalar());
            MessageBox.Show("Se ha registrado con exito a la persona con el codigo: " + codigo);
            con.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            con.Open();
            String sp = "UpdatePerson";
            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PersonID", txtCodigo.Text);
            cmd.Parameters.AddWithValue("@FirstName", txtNombre.Text);
            cmd.Parameters.AddWithValue("@LastName", txtApellido.Text);
            cmd.Parameters.AddWithValue("@Hiredate", dtpContrato.Value);
            cmd.Parameters.AddWithValue("@EnrollmentDate", dtpInscripcion.Value);

            int resultado = cmd.ExecuteNonQuery();      
            if (resultado>0)
            {
                MessageBox.Show("Se ha modificado el registro correctamente");
            }
            con.Close();
        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvListado.SelectedRows.Count>0)
            {
                txtCodigo.Text = dgvListado.SelectedRows[0].Cells[0].Value.ToString();
                txtNombre.Text = dgvListado.SelectedRows[0].Cells[1].Value.ToString();
                txtApellido.Text = dgvListado.SelectedRows[0].Cells[2].Value.ToString();
                dtpContrato.Text = dgvListado.SelectedRows[0].Cells[3].Value.ToString();
                dtpInscripcion.Text = dgvListado.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                String sp = "DeletePerson";
                SqlCommand cmd = new SqlCommand(sp, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonID", txtCodigo.Text);

                int resultado = cmd.ExecuteNonQuery();

                if (resultado > 0)
                {
                    MessageBox.Show("Se ha eliminado el registro");
                }
                con.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(""+ex);
            }           
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            con.Open();    
            String FirstName = txtNombre.Text;

            SqlCommand cmd = new SqlCommand();            
            cmd.CommandText = "BuscarPersonaNombre";
            cmd.CommandType = CommandType.StoredProcedure;                
            cmd.Connection = con;                
            SqlParameter param = new SqlParameter();               
            param.ParameterName = "@FirstName";                
            param.SqlDbType = SqlDbType.NVarChar;                
            param.Value = FirstName;
                
            cmd.Parameters.Add(param);
                
            SqlDataReader reader = cmd.ExecuteReader();               
            DataTable dt = new DataTable();                
            dt.Load(reader);               
            dgvListado.DataSource = dt;                
            dgvListado.Refresh();
            con.Close();
        }
    }
}
