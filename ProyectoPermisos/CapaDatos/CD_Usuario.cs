using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using ProyectoPermisos.CapaModelo;
using System.Xml;
using System.Xml.Linq;

namespace ProyectoPermisos.CapaDatos
{
    public class CD_Usuario
    {


        public static int Loguear(string usuario, string clave) {

            int idusuario = 0;

            using (SqlConnection cn = new SqlConnection(Conexion.cn)) {

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_LoginUsuario", cn);
                    cmd.Parameters.AddWithValue("Usuario", usuario);
                    cmd.Parameters.AddWithValue("Clave", clave);
                    cmd.Parameters.Add("IdUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    idusuario = Convert.ToInt32(cmd.Parameters["IdUsuario"].Value);

                }
                catch {
                    idusuario = 0;
                }

            }

            return idusuario;

        }

        public static List<Menu> ObtenerPermisos(int P_IdUsuario) {

            List<Menu> Permisos = new List<Menu>();

            using (SqlConnection cn = new SqlConnection(Conexion.cn))
            {

                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ObtenerPermisos", cn);
                    cmd.Parameters.AddWithValue("IdUsuario", P_IdUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();


                    XmlReader leerXML = cmd.ExecuteXmlReader();

                    while (leerXML.Read()) {

                        XDocument doc = XDocument.Load(leerXML);

                        if (doc.Element("PERMISOS") != null) {

                            Permisos = doc.Element("PERMISOS").Element("DetalleMenu") == null ? new List<Menu>() :

                                       (from menu in doc.Element("PERMISOS").Element("DetalleMenu").Elements("Menu")
                                        select new Menu()
                                        {
                                            Nombre = menu.Element("Nombre").Value,
                                            Icono = menu.Element("Icono").Value,
                                            ListaSubMenu = menu.Element("DetalleSubMenu") == null ? new List<SubMenu>() :
                                            (from submenu in menu.Element("DetalleSubMenu").Elements("SubMenu")
                                             select new SubMenu() {
                                                 Nombre = submenu.Element("Nombre").Value,
                                                 NombreFormulario = submenu.Element("NombreFormulario").Value
                                             }
                                            ).ToList()
                                        }).ToList();

                        }

                    }

                }
                catch
                {
                    Permisos = new List<Menu>();
                }

            }

            return Permisos;
        }



    }
}
