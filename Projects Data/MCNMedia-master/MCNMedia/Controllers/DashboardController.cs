﻿using System;
using System.Linq;
using MCNMedia_Dev._Helper;
using MCNMedia_Dev.Models;
using MCNMedia_Dev.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MCNMedia_Dev.Controllers
{
    public class DashboardController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IWebHostEnvironment environment;

        public DashboardController(IWebHostEnvironment _environment)
        {
            environment = _environment;
        }
        DashboardDataAccessLayer dashboardData = new DashboardDataAccessLayer();
        DashBoardClientDataAccessLayer clientdashboardData = new DashBoardClientDataAccessLayer();
        ChurchDataAccessLayer churchDataAccess = new ChurchDataAccessLayer();
       

        public IActionResult Dashboard(int chrid, DateTime eventDate)
        {
            GenericModel gm = new GenericModel();
            try
            {
                if (eventDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                {
                    ViewBag.SchDate = DateTime.Now.ToString("dd-MMM-yyyy");
                    eventDate = DateTime.Now;
                }
                else
                {
                    ViewBag.SchDate = eventDate.ToString("dd-MMM-yyyy");
                }
               
                gm.Dashboards = dashboardData.GetDashboardInfo();
                gm.ListDashboards2 = dashboardData.GetDashboardCountry_Churches();
                gm.LDashBoardClients = clientdashboardData.GetDashboardClientInfo(-1);
               
              
                return View(gm);
            }
            catch (Exception exp)
            {
                ViewBag.ErrorMsg = "Error Occurreds! " + exp.Message;
                return View(gm);
            }


            
            
        }

        [HttpGet]
        public JsonResult GoogleAnalyticsDashboard(DateTime eventDate)
        {
            GenericModel gm = new GenericModel();
            try
            {
                if (eventDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
                {
                    ViewBag.SchDate = DateTime.Now.ToString("dd-MMM-yyyy");
                    eventDate = DateTime.Now;
                }
                else
                {
                    ViewBag.SchDate = eventDate.ToString("dd-MMM-yyyy");
                }

                
                gm.AnalyticsList = churchDataAccess.GetAllChurchForAnalytics(eventDate, eventDate);
                GoogleAnalytics googleantics = new GoogleAnalytics(environment);
                gm.googleAnalytics = googleantics.GoogleAnalytics_GetAll(eventDate);
                

                return Json(gm.googleAnalytics);
            }
            catch (Exception exp)
            {
                ViewBag.ErrorMsg = "Error Occurreds! " + exp.Message;
                return Json(new { success = false, responseText = exp.Message });
            }




        }

    }
}
