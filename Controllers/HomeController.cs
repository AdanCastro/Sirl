using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
namespace sirlLab.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        AdministradorController admView = new AdministradorController();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            return RedirectToAction("Index", "Administrador");
        }



    }
}
