using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAIS.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult BadRequest()
        {
            Response.StatusCode = 400;
            return View();
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}