﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AnalysisController : Controller
    {
        public ActionResult Index(string code)
        {
            ViewBag.code = code;
            return View();
        }

        public ActionResult dapan()
        {
            return View();
        }

        public ActionResult sector()
        {
            return View();
        }

        public ActionResult singlename()
        {
            return View();
        }
    }
}