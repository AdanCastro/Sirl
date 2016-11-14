using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace sirlLab.Models
{
    public class Usuarios
    {
         public int idUsuario { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public int tipo { get; set; }
        public string area { get; set; }

        private bool connectionOpen;
        private OracleConnection conex;
        OracleCommand cmd = new OracleCommand();


        public Usuarios()
        {
        }

        public Usuarios(int idProd)
        {
            getConnection();
            idUsuario = idProd;
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conex;
                cmd.CommandText = string.Format("select user_password,user_nombre,user_email,user_tipocuenta,user_area from usuarios where user_id={0}", idUsuario);
                OracleDataReader reader = cmd.ExecuteReader();
                try
                {
                    reader.Read();
                    if (reader.IsDBNull(0) == false) password = reader.GetString(2); else password = null;
                    if (reader.IsDBNull(1) == false) nombre = reader.GetString(0); else nombre = null;
                    if (reader.IsDBNull(2) == false) correo = reader.GetString(1); else correo = null;
                    if (reader.IsDBNull(3) == false) tipo = reader.GetInt32(3); else tipo = 0;
                    if (reader.IsDBNull(4) == false) area = reader.GetString(4); else area = null;
                }
                catch (Exception)
                {
                    string mensaje = "Error en la lectura";
                }
            }
            catch (Exception)
            {
                string mensaje = "Verificar la conexión a la BD";
            }
            conex.Close();
        }

        public List<Usuarios> obtenerProductos()
        {
            getConnection();
            List<Usuarios> lista = new List<Usuarios>();
            cmd.Connection = conex;
            cmd.CommandText = string.Format("select * from usuarios");
            OracleDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Usuarios p = new Usuarios();
                        if (reader.IsDBNull(0) == false) p.idUsuario = reader.GetInt32(0); else idUsuario = 0;
                        if (reader.IsDBNull(1) == false) p.password = reader.GetString(1); else password = null;
                        if (reader.IsDBNull(2) == false) p.nombre = reader.GetString(2); else nombre = null;
                        if (reader.IsDBNull(3) == false) p.correo = reader.GetString(3); else correo = null;
                        if (reader.IsDBNull(4) == false) p.tipo = reader.GetInt32(4); else tipo = 0;
                        if (reader.IsDBNull(5) == false) p.area = reader.GetString(5); else area = null;
                        lista.Add(p);
                    }
                }
                reader.Close();
            }
            catch (Exception)
            {
                string mensaje = "Error en la lectura";
            }
            conex.Close();
            return lista;
        }

        public void operacion(int o, Usuarios u)
        {            
            getConnection();
            cmd.Connection = conex;
            switch (o)
            {
                case 1:
                    cmd.CommandText = "insert into usuarios values(" + u.idUsuario + ", '" + u.password + "', '" + u.nombre + "', '" + u.correo + "', " + u.tipo + ", '" + u.area + "')";
                    cmd.ExecuteNonQuery();
                    conex.Close();
                    break;
                case 2:
                    cmd.CommandText = "delete from usuarios where user_id =" + u.idUsuario;
                    cmd.ExecuteNonQuery();
                    conex.Close();
                    break;
                case 3:
                    cmd.CommandText = "update usuarios set user_password='" + u.password + "',user_nombre='" + u.nombre + "',user_email='" + u.correo + "',user_tipo=" + u.tipo + ",user_area='" + u.area + "' where user_id =" + u.idUsuario;
                    cmd.ExecuteNonQuery();
                    conex.Close();
                    break;
            }
            }
        

        private bool openLocalConnection()
        {
            try
            {
                conex.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void getConnection()
        {
            connectionOpen = false;
            conex = new OracleConnection();
            conex.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            if (openLocalConnection())
                connectionOpen = true;
            else
                connectionOpen = false;
        }

        public int registraProducto(Usuarios prod)
        {
            int i;
            getConnection();

            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conex;
                cmd.CommandText = "insert into usuarios values(" + prod.idUsuario + ", '" + prod.password + "', '" + prod.nombre + "', '" + prod.correo + "', " + prod.tipo + ", '" + prod.area + "')";
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            conex.Close();
            return i;
        }
    }
}
