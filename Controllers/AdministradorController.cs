using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sirlLab.Models;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace sirlLab.Controllers
{
    public class AdministradorController : Controller
    {
        //
        // GET: /Administrador/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Laboratorios()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Laboratorios(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                Laboratorio lab = new Laboratorio();
                lab.nombre = form["txtNombre"];
                lab.edificio = Int32.Parse(form["selEdif"]);
                lab.area = form["selArea"];

                if (lab.registrarLab(lab) == 1)
                    ViewBag.Status = "Registrado";
            }

            return View();
        }

        private bool connectionOpen;
        private OracleConnection conex;
        // GET: /Home/

        Usuarios prod = new Usuarios();


        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }

        public ActionResult RegistroUsuarios()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            List<Usuarios> l = prod.obtenerProductos();
            return View(l);
        }
        public ActionResult Error(Exception e)
        {
            ViewBag.message = e.Message;
            return View();
        }

        [HttpPost]
        public ActionResult Usuarios(FormCollection f)
        {
            try
            {
            Usuarios p = new Usuarios();
            p.idUsuario = Int32.Parse(f["txtID"]);
            p.password = f["txtPass"];
            p.nombre = f["txtNombre"];
            p.correo = f["txtCorreo"];
            p.tipo = Int32.Parse(f["dplTipo"]);
            p.area = f["txtArea"];


            var oper = Request.Form["operacion"];
            OracleCommand cmd = new OracleCommand();
            
                if (oper != null)
                {
                    switch (oper)
                    {
                        case "POST":
                            prod.operacion(1, p);
                            break;
                        case "DELETE":
                            prod.operacion(2, p);
                            break;
                        case "PUT":
                            prod.operacion(3, p);
                            break;
                        default:
                            ViewBag.Mensaje = "No se inserta";
                            break;
                    }
                }
                List<Usuarios> l = prod.obtenerProductos();
                return View(l);
            }
            catch(Exception e)
            {
                return RedirectToAction("Error", "Administrador",e);
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
    }
}
