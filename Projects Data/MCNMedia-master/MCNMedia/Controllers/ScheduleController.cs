﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MCNMedia_Dev._Helper;
using MCNMedia_Dev.Models;
using MCNMedia_Dev.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace MCNMedia_Dev.Controllers
{
    public class ScheduleController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        ScheduleDataAccessLayer scheduleDataAccess = new ScheduleDataAccessLayer();
        ChurchDataAccessLayer chdataAccess = new ChurchDataAccessLayer();
        CameraDataAccessLayer camDataAccess = new CameraDataAccessLayer();

        [HttpGet]
        public IActionResult AddSchedule()
        {
            try
            {
                return View();
            }
            catch (Exception exp)
            {
                ViewBag.ErrorMsg = "Error Occurreds! " + exp.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult AddScheduleNew(int churchId, string eventName, bool isRepeated, DateTime eventDate, string eventDay, string eventTime, bool isRecording, int cameraId, int recordDuration, bool isPassword, string password)
        {
            try
            {
                Schedule sch = new Schedule();
                sch.IsRepeated = isRepeated;
                sch.EventTime = Convert.ToDateTime(eventTime);
                sch.ChurchId = churchId;
                if (sch.IsRepeated == false)
                {
                    if (Convert.ToDateTime(eventDate.ToString("yyyy-MMM-dd")) < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MMM-dd")))
                    {
                        throw new Exception("Invalid Date!" + eventDate + "Kindly Provide a valid date");
                    }
                    sch.EventDate = eventDate;
                    sch.EventDay = sch.EventDate.ToString("dddd");
                }
                else
                {
                    sch.EventDay = eventDay;
                    sch.EventDate = Convert.ToDateTime("1900-01-01 00:00:00");
                }
                sch.EventName = eventName;
                sch.Record = isRecording;

                if (sch.Record == false)
                {
                    sch.CameraId = 0;
                    sch.RecordDuration = 0;
                }
                else
                {
                    sch.CameraId = cameraId;
                    sch.RecordDuration = recordDuration;
                }
                if (isPassword == false)
                {
                    sch.Password = string.Empty;
                }
                else
                {
                    sch.Password = password;
                }
                sch.CreatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserType")))
                {

                    int res = scheduleDataAccess.AddSchedule(sch);
                    return Json(new { success = true, responseText = "The attached file is not supported." });
                }
                return RedirectToAction("Listchurch", "Church");
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = e.Message });
            }
        }

        [HttpGet]
        public ViewResult ListSchedule()
        {
            GenericModel gm = new GenericModel();
            try
            {
                int churchId = -1;
                int record = -1;
                DateTime eventDate = DateTime.Now;
                string eventDay = DateTime.Now.ToString("dddd");
                ViewBag.SchDate = DateTime.Now.ToString("dd-MMM-yyyy");
                ViewBag.SchChurchId = churchId;
                ViewBag.Schrecord = record;

                LoadChurchDDL();
                

                gm.LSchedules = SearchSchedules(churchId, eventDay, eventDate, record);
                return View(gm);
            }
            catch (Exception exp)
            {
                ViewBag.ErrorMsg = "Error Occured !"+ exp.Message;
                return View(gm);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, bool ToggleRecord1, [Bind] Schedule schedule)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserType")))
                {
                    return RedirectToAction("UserLogin", "UserLogin");
                }

                if (schedule.IsRepeated == false)
                {
                    schedule.EventDay = schedule.EventDate.ToString("dddd");
                }
                else if (schedule.IsRepeated == true)
                {
                    schedule.EventDate = Convert.ToDateTime("0001-01-01 00:00:00");
                }
                schedule.ScheduleId = id;
                schedule.Record = ToggleRecord1;
                schedule.UpdatedBy = (int)HttpContext.Session.GetInt32("UserId");
                scheduleDataAccess.UpdateSchedule(schedule);
                return RedirectToAction("ListSchedule");
            }
            catch (Exception e)
            {
                                throw;
            }
        }

        [HttpGet]
        public IActionResult EditSchedule(int id)
        {
            try
            {
                Schedule Schedules = scheduleDataAccess.GetScheduleById(id);
                if (Schedules == null)
                {
                    return NotFound();
                }
                return PartialView("EditSchedule", Schedules);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateSchedule(string eventName, bool isRepeat, DateTime eventDate, string eventDay, string eventTime, bool recordtoggle, int cameraId, int recordDuration, bool passwordtoggle, string password, int scheduleId, int churchId)
        {
            try
            {
                Schedule sch = new Schedule();
                sch.IsRepeated = isRepeat;
                sch.ScheduleId = scheduleId;
                sch.ChurchId = churchId;
                sch.EventTime = Convert.ToDateTime(eventTime);
                if (sch.IsRepeated == false)
                {
                    if (Convert.ToDateTime(eventDate.ToString("yyyy-MMM-dd")) < Convert.ToDateTime(DateTime.Now.ToString("yyyy-MMM-dd")))
                    {
                        throw new Exception("Invalid Date!" + eventDate + "Kindly Provide a valid date");
                    }
                    sch.EventDate = eventDate;
                    sch.EventDay = sch.EventDate.ToString("dddd");
                }
                else
                {
                    sch.EventDay = eventDay;
                    sch.EventDate = Convert.ToDateTime("1900-01-01 00:00:00");
                }
                sch.EventName = eventName;
                sch.Record = recordtoggle;

                if (sch.Record == false)
                {
                    sch.CameraId = 0;
                    sch.RecordDuration = 0;
                }
                else
                {
                    sch.CameraId = cameraId;
                    sch.RecordDuration = recordDuration;
                }
                if (passwordtoggle == false)
                {
                    sch.Password = string.Empty;
                }
                else
                {
                    sch.Password = password;
                }

                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserType")))
                {
                    sch.UpdatedBy = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                    int res = scheduleDataAccess.UpdateSchedule(sch);
                    return Json(new { success = true, responseText = "The attached file is not supported." });
                }
                return RedirectToAction("Listchurch", "Church");
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = e.Message });
            }
        }

        [HttpGet]
        public IActionResult DeleteSch(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserType")))
                {
                    return Json(new { Url = "UserLogin" });
                }
                int UpdatedBy = (int)HttpContext.Session.GetInt32("UserId");
                scheduleDataAccess.DeleteSchedule(id, UpdatedBy);
                return RedirectToAction("ListSchedule");
            }
            catch (Exception e)
            {
                return Json(new { success = false, responseText = e.Message });
            }
        }

        public void LoadChurchDDL()
        {
            try
            {
                Church chr = new Church();
                chr.ChurchId = -1;
                chr.CountyId = -1;
                chr.ClientTypeId = -1;
                chr.ChurchName = "";
                chr.EmailAddress = "";
                chr.Phone = "";
                chr.Town = "";
                chr.CountryId = -1;

                IEnumerable<Church> ChurchList = chdataAccess.GetAllChurch(chr);

                List<SelectListItem> selectListItems = new List<SelectListItem>();
                foreach (var item in ChurchList)
                {
                    string ChurchNameTown = item.ChurchName + " , " + item.Town;
                    selectListItems.Add(new SelectListItem { Text = ChurchNameTown, Value = item.ChurchId.ToString() });
                }
                ViewBag.Church = selectListItems;
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        public JsonResult LoadCameraDDL(int ChurchId)
        {
            try
            {
                List<Camera> countyList = camDataAccess.GetAllCamerasByChurch(ChurchId).ToList();
                return Json(countyList);
            }
            catch (Exception e)
            {
                
                throw;
            }
        }

        /// <summary>
        /// This method is used to get <b>Schedule List</b> basis on provided parameters.
        /// </summary>
        /// <param name="churchId">Church Id</param>
        /// <param name="eventDay">Event Scheduled Day</param>
        /// <param name="eventDate">Event Scheduled Date</param>
        /// <param name="record">Event marked for recording</param>
        /// <returns></returns>
        public IActionResult Search(int churchId, DateTime eventDate, int recordDt)
        {
            try
            {
                LoadChurchDDL();
                string eventDay = eventDate.ToString("dddd");
                GenericModel gm = new GenericModel();
                gm.LSchedules = SearchSchedules(churchId, eventDay, eventDate, recordDt);
                return View("/Views/Schedule/ListSchedule.cshtml", gm);
            }
            catch (Exception e)
            {
              
                throw;
            }
        }



        private List<Schedule> SearchSchedules(int churchId, string eventDay, DateTime eventDate, int record)
        {
            ViewBag.SchDate = eventDate.ToString("dd-MMM-yyyy");
            ViewBag.SchChurchId = churchId;
            ViewBag.Schrecord = record;
            HttpContext.Session.SetInt32("ChurchId", churchId);
            GenericModel gm = new GenericModel();
            return scheduleDataAccess.GetSearchSchedule(churchId, eventDate, eventDay, record).ToList<Schedule>();
        }

        [HttpPost]
        public JsonResult StartRecordingSchedule(int scheduleId)
        {
            Wowza wowza = new Wowza();
            return Json(wowza.StartRecordingBySchedule(scheduleId));
        }

        [HttpPost]
        public JsonResult StopRecordingSchedule(int scheduleId)
        {
            Wowza wowza = new Wowza();
            return Json(wowza.StopRecordingBySchedule(scheduleId));
        }
                           }
}