using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace sirlLab.Models
{
    public class OracleConex
    {
        private OracleConnection conex;

        public OracleConnection getConnection()
        {
            conex = new OracleConnection();
            conex.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;

            if (conex.State == ConnectionState.Open)
                conex.Close();
            else
                conex.Open();

            return conex;
        }
    }
}