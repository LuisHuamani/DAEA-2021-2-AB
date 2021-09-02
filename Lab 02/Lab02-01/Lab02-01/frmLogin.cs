using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_01
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        string usuario = "Luis Huamani";
        string clave = "123456789";

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            string user = txtUsuario.Text;
            string password = txtClave.Text;
            if (user.Equals(usuario))
            {
                if (password.Equals(clave))
                {
                    principalMDI principal = new principalMDI();
                    principal.Show();
                    this.Hide();
                }

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
