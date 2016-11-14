using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace sirlLab.Models
{
    public class Laboratorio
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int edificio { get; set; }
        public string area { get; set; }
        
        OracleConex con = new OracleConex();
        OracleCommand cmd = new OracleCommand();

        public Laboratorio() { }

        public int registrarLab(Laboratorio lab)
        {
            int i;

            try
            {                
                cmd.CommandText = "INSERT INTO LABORATORIO (LAB_NOMBRE, LAB_EDIFICIO, LAB_AREA) VALUES ('" + lab.nombre + "', " + lab.edificio + ", '" + lab.area + "')";
                cmd.Connection = con.getConnection();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            return i;
        }
    }
}