using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProyectoPermisos.CapaDatos;

namespace ProyectoPermisos
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int idusuario_esperado = CD_Usuario.Loguear(txtusuario.Text, txtclave.Text);

            if (idusuario_esperado != 0)
            {
                this.Hide();

                MDI_Master mdi = new MDI_Master(idusuario_esperado);
                mdi.Show();
            }
            else {

                MessageBox.Show("Usuario no encontrado");
            }


        }
    }
}
