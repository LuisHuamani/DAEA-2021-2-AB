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

namespace Lab06
{
    public partial class manPerson : Form
    {
        SqlConnection con;
        DataSet ds = new DataSet();
        DataTable tablePerson = new DataTable();
        public manPerson()
        {
            InitializeComponent();
        }

        private void manPerson_Load(object sender, EventArgs e)
        {
            String str = "Server=DESKTOP-PTAILR8\\LOCAL;Database=School;Integrated Security=true;";
            con = new SqlConnection(str);
            /*
            con.Open();
            MessageBox.Show("Conexion: " + con.State);
            */
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            String sql = "select * from Person";
            SqlCommand cmd = new SqlCommand(sql, con);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            adapter.Fill(ds, "Person");

            tablePerson = ds.Tables["Person"];

            dgvListado.DataSource = "";
            dgvListado.Refresh();
            dgvListado.DataSource = tablePerson;
            dgvListado.Refresh();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("InsertPerson", con);
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50, "LastName");
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50, "FirstName");
            cmd.Parameters.Add("@HireDate", SqlDbType.Date).SourceColumn = "HireDate";
            cmd.Parameters.Add("@EnrollmentDate", SqlDbType.Date).SourceColumn = "EnrollmentDate";

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = cmd;
            adapter.InsertCommand.CommandType = CommandType.StoredProcedure;

            DataRow fila = tablePerson.NewRow();
            fila["LastName"] = txtApellido.Text;
            fila["FirstName"] = txtNombre.Text;
            fila["HireDate"] = dtpContrato.Text;
            fila["EnrollmentDate"] = dtpInscripcion.Text;

            tablePerson.Rows.Add(fila);

            adapter.Update(tablePerson);
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UpdatePerson", con);

            cmd.Parameters.Add("@PersonID", SqlDbType.VarChar).SourceColumn = "PersonID";
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar).SourceColumn = "LastName";
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).SourceColumn = "FirstName";
            cmd.Parameters.Add("@HireDate", SqlDbType.Date).SourceColumn = "HireDate";
            cmd.Parameters.Add("@EnrollmentDate", SqlDbType.Date).SourceColumn = "EnrollmentDate";

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = cmd;
            adapter.UpdateCommand.CommandType = CommandType.StoredProcedure;

            DataRow[] fila = tablePerson.Select("PersonID = '" + txtCodigo.Text + "'");
            fila[0]["LastName"] = txtApellido.Text;
            fila[0]["FirstName"] = txtNombre.Text;
            fila[0]["HireDate"] = dtpContrato.Text;
            fila[0]["EnrollmentDate"] = dtpInscripcion.Text;

            adapter.Update(tablePerson);
        }

        private void dgvListado_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvListado.SelectedRows.Count > 0)
            {
                txtCodigo.Text = dgvListado.SelectedRows[0].Cells[0].Value.ToString();
                txtApellido.Text = dgvListado.SelectedRows[0].Cells[1].Value.ToString();
                txtNombre.Text = dgvListado.SelectedRows[0].Cells[2].Value.ToString();

                string hireDate = dgvListado.SelectedRows[0].Cells[3].Value.ToString();
                if (String.IsNullOrEmpty(hireDate))
                {
                    dtpContrato.Checked = false;
                }
                else
                {
                    dtpContrato.Text = hireDate;
                }
                string enrollmentDate = dgvListado.SelectedRows[0].Cells[4].Value.ToString();
                if (String.IsNullOrEmpty(enrollmentDate))
                {
                    dtpInscripcion.Checked = false;
                }
                else
                {
                    dtpInscripcion.Text = enrollmentDate;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DeletePerson", con);
            cmd.Parameters.Add("@PersonID", SqlDbType.Int).SourceColumn = "PersonID";

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.DeleteCommand = cmd;
            adapter.DeleteCommand.CommandType = CommandType.StoredProcedure;

            DataRow[] fila = tablePerson.Select("PersonID = '" + txtCodigo.Text + "'");

            tablePerson.Rows.Remove(fila[0]);

            adapter.Update(tablePerson);
        }

        private void btnOApellido_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(tablePerson);
            dv.Sort = "LastName ASC";
            dgvListado.DataSource = dv;
        }

        private void btnOCodigo_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(tablePerson);
            dv.RowFilter = "PersonID = '" + txtCodigo.Text + "'"; 
            dgvListado.DataSource = dv;
        }
    }
}
