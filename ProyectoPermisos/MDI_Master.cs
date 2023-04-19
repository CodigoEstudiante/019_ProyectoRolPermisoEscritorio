using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ProyectoPermisos.CapaDatos;
using ProyectoPermisos.CapaModelo;

namespace ProyectoPermisos
{
    public partial class MDI_Master : Form
    {
        private int idusuario;

        public MDI_Master(int idusuario_esperado = 0)
        {
            InitializeComponent();

            idusuario = idusuario_esperado;
        }

        private void MDI_Master_Load(object sender, EventArgs e)
        {
            List<CapaModelo.Menu> Permisos_Esperados = CD_Usuario.ObtenerPermisos(idusuario);

            MenuStrip MiMenu = new MenuStrip();

            //ITERAMOS CADA MENU DE NUESTRA LISTA
            foreach (CapaModelo.Menu objMenu in Permisos_Esperados) {

                ToolStripMenuItem menuPadre = new ToolStripMenuItem(objMenu.Nombre);
                menuPadre.TextImageRelation = TextImageRelation.ImageAboveText;

                string rutaImagen = Path.GetFullPath(Path.Combine(Application.StartupPath, @"../../") + @objMenu.Icono);

                menuPadre.Image = new Bitmap(rutaImagen);
                menuPadre.ImageScaling = ToolStripItemImageScaling.None;

                //ITERAMOS LOS SUBMENUS DENTRO DE OBJMENU
                foreach (SubMenu objSubMenu in objMenu.ListaSubMenu) {

                    ToolStripMenuItem menuHijo = new ToolStripMenuItem(objSubMenu.Nombre,null,click_en_menu,objSubMenu.NombreFormulario);


                    menuPadre.DropDownItems.Add(menuHijo);
                }


                MiMenu.Items.Add(menuPadre);
            }


            //AGREGAMOS EL CONTROL AL FORMULARIO
            this.MainMenuStrip = MiMenu;
            Controls.Add(MiMenu);
        }

        private void click_en_menu(object sender, System.EventArgs e) {

            ToolStripMenuItem menuSeleccionado = (ToolStripMenuItem)sender;

            //NOS AYUDA A VALIDAR SI EXISTE ELEMENTOS EN NUESTRO PROYECTO - OBTENEMOS PROYECTO
            //ASSEMBLY Obtiene el proceso de nuestro ejecutable
            Assembly asm = Assembly.GetEntryAssembly();

            Type elemento = asm.GetType(asm.GetName().Name + "." + menuSeleccionado.Name);

            if (elemento == null)
            {
                MessageBox.Show("Formulario no encontrado");
            }
            else {

                //IMPORTANTE CAMBIAR EL TEXTO DEL FORMULARIO CUANDO SE CREA , TIENE QUE SER DIFERENTE AL NAME
                Form FormularioCreado = (Form)Activator.CreateInstance(elemento);


                int encontrado = this.MdiChildren.Where(x => x.Name == FormularioCreado.Name).ToList().Count();

                if (encontrado != 0)
                {
                   
                    ((Form)this.MdiChildren.Where(x => x.Name == FormularioCreado.Name).FirstOrDefault()).WindowState = FormWindowState.Normal;
                    ((Form)this.MdiChildren.Where(x => x.Name == FormularioCreado.Name).FirstOrDefault()).Activate();

                }
                else {

                    FormularioCreado.MdiParent = this;
                    FormularioCreado.Show();
                }

            }


        }

    }
}
