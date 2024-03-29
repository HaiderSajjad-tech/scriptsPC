﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MCNMedia_Dev.Models;

namespace MCNMedia_Dev.Repository
{
    public class ScheduleDataAccessLayer
    {
        AwesomeDal.DatabaseConnect _dc;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ScheduleDataAccessLayer()
        {
            _dc = new AwesomeDal.DatabaseConnect();
        }
        //To Add new Schedule record
        public int AddSchedule(Schedule schedules)
        {
            _dc.ClearParameters();
            _dc.AddParameter("SchEventName", schedules.EventName);
            _dc.AddParameter("SchEventDate", schedules.EventDate);
            _dc.AddParameter("SchdEventDay", schedules.EventDay);
            _dc.AddParameter("SchEventTime", schedules.EventTime);
            _dc.AddParameter("SchLength", schedules.RecordDuration);
            _dc.AddParameter("SchRecord", schedules.Record);
            _dc.AddParameter("ChurchId", schedules.ChurchId);
            _dc.AddParameter("IsRepeat", schedules.IsRepeated);
            _dc.AddParameter("SchPassword", schedules.Password);
            _dc.AddParameter("SchCameraId", schedules.CameraId);
            _dc.AddParameter("CreatedBy", schedules.CreatedBy);
            return _dc.Execute("spSchedule_Add");
        }

        public IEnumerable<Schedule> GetSearchSchedule(int ChurchId, DateTime EventDate, string EventDay, int isRecord)
        {
            List<Schedule> Balobj = new List<Schedule>();
            _dc.ClearParameters();
            _dc.AddParameter("ChrId", ChurchId);
            _dc.AddParameter("SchDate", EventDate.ToString("yyyy-MM-dd"));
            _dc.AddParameter("SchDay", EventDay);
            _dc.AddParameter("isRecord", isRecord);

            DataTable dataTable = _dc.ReturnDataTable("spSchedule_Search");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Schedule schedule = new Schedule();
                schedule.ScheduleId = Convert.ToInt32(dataRow["ScheduleId"]);
                schedule.ChurchName = dataRow["ChurchName"].ToString();
                schedule.Town = dataRow["Town"].ToString();
                schedule.Slug = dataRow["Slug"].ToString();
                schedule.EventName = dataRow["ScheduleEventName"].ToString();
                schedule.EventDate = Convert.ToDateTime(dataRow["ScheduleEventDate"].ToString());
                schedule.EventDay = dataRow["ScheduleEventDay"].ToString();
                schedule.EventTime = Convert.ToDateTime(dataRow["ScheduleEventTime"].ToString());
                schedule.CreatedAt = Convert.ToDateTime(dataRow["CreatedAtandUpdateDate"].ToString());

                schedule.Password = dataRow["Password"].ToString();
                schedule.IsRepeated = Convert.ToBoolean(dataRow["IsRepeated"]);
                schedule.RecordDuration = Convert.ToInt32(dataRow["RecordingDuration"]);
                schedule.Record = Convert.ToBoolean(dataRow["Record"]);
                schedule.CameraId = Convert.ToInt32(dataRow["CameraId"]);
                schedule.CameraName = dataRow["CameraName"].ToString();
                Balobj.Add(schedule);
            }
            return Balobj;
        }

        //Get the details of a particular Schedule
        public Schedule GetScheduleById(int id)
        {
            Schedule schedule = new Schedule();

            _dc.ClearParameters();
            _dc.AddParameter("SchId", id);
            DataTable dataTable = _dc.ReturnDataTable("spSchedule_GetById");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                schedule.ScheduleId = Convert.ToInt32(dataRow["ScheduleId"]);
                schedule.ChurchId = Convert.ToInt32(dataRow["ChurchId"]);
                schedule.EventName = dataRow["ScheduleEventName"].ToString();
                schedule.EventDate = Convert.ToDateTime(dataRow["ScheduleEventDate"].ToString());
                schedule.EventDay = (dataRow["ScheduleEventDay"].ToString());
                schedule.EventTime = Convert.ToDateTime(dataRow["ScheduleEventTime"].ToString());
                schedule.RecordDuration = Convert.ToInt32(dataRow["RecordingDuration"]);
                schedule.Record = Convert.ToBoolean(dataRow["Record"]);
                schedule.Password = dataRow["Password"].ToString();
                schedule.IsRepeated = Convert.ToBoolean(dataRow["IsRepeated"]);
                schedule.CameraId = Convert.ToInt32(dataRow["CameraId"]);
            }
            return schedule;
        }

        //To Update the records of a particluar Schedule
        public int UpdateSchedule(Schedule schedule)
        {
            _dc.ClearParameters();
            _dc.AddParameter("SchId", schedule.ScheduleId);
            _dc.AddParameter("SchName", schedule.EventName);
            _dc.AddParameter("SchDate", schedule.EventDate);
            _dc.AddParameter("SchDay", schedule.EventDay);
            _dc.AddParameter("SchTime", schedule.EventTime);
            _dc.AddParameter("recordingDuration", schedule.RecordDuration);
            _dc.AddParameter("record", schedule.Record);
            _dc.AddParameter("UpdateBy", schedule.UpdatedBy);
            _dc.AddParameter("SchPassword", schedule.Password);
            _dc.AddParameter("IsRepeat", schedule.IsRepeated);
            _dc.AddParameter("SchCameraId", schedule.CameraId);
            _dc.AddParameter("SchChurchId", schedule.ChurchId);
            return _dc.Execute("spSchedule_Update");
        }

        //To Delete the record on a particular User
        public bool DeleteSchedule(int id, int UpdateBy)
        {
            _dc.ClearParameters();
            _dc.AddParameter("SchId", id);
            _dc.AddParameter("UpdatedBy", UpdateBy);
            return _dc.ReturnBool("spSchedule_Delete");
        }

        public IEnumerable<Schedule> GetScheduleByChurch(int ChurchId)
        {
            List<Schedule> Balobj = new List<Schedule>();
            _dc.ClearParameters();
            _dc.AddParameter("chId", ChurchId);

            DataTable dataTable = _dc.ReturnDataTable("spChurchSchedule_Get");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Schedule sch = new Schedule();
                sch.EventDay = dataRow["ScheduleEventDay"].ToString();
                sch.EventName = dataRow["Events"].ToString();
                Balobj.Add(sch);
            }
            return Balobj;
        }

        public IEnumerable<Schedule> GetSchedule_Today()
        {
            List<Schedule> Balobj = new List<Schedule>();
            _dc.ClearParameters();

            DataTable dataTable = _dc.ReturnDataTable("spWebsite_Schedule_Today");
            return ConvertScheduleTableToList(dataTable);
        }

        public IEnumerable<Schedule> GetSchedule_WhatsOnNow()
        {
            List<Schedule> Balobj = new List<Schedule>();
            _dc.ClearParameters();
            DataTable dataTable = _dc.ReturnDataTable("spWebsite_Schedule_Today_WhatsOnNow");
            return ConvertScheduleTableToList(dataTable);
        }

        public IEnumerable<Schedule> GetSchedule_UpComing_All()
        {
            return GetSchedule_UpComing(churchId:-1);
        }

        public IEnumerable<Schedule> GetSchedule_UpComingByChurch(int churchId)
        {
            return GetSchedule_UpComing(churchId);
        }

        private IEnumerable<Schedule> GetSchedule_UpComing(int churchId)
        {
            _dc.ClearParameters();
            _dc.AddParameter("Church_Id", churchId);
            DataTable dataTable = _dc.ReturnDataTable("spWebsite_Schedule_UpComingEvents");

            return ConvertScheduleTableToList(dataTable);
        }

        public IEnumerable<Schedule> GetSchedule_UpComingHour()
        {
            _dc.ClearParameters();
            _dc.AddParameter("Church_Id", -1);
            DataTable dataTable = _dc.ReturnDataTable("spWebsite_Schedule_Today_WhatsOnNowInHour");
           
            return ConvertScheduleTableToList(dataTable);
        }

        private IEnumerable<Schedule> ConvertScheduleTableToList(DataTable dataTable)
        {
            Schedule schedule ;
            List<Schedule> schedules = new List<Schedule>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                schedule = BindSchedule(dataRow);
                schedules.Add(schedule);
            }
            return schedules;
        }
        
        private Schedule BindSchedule(DataRow dataRow)
        {
            Schedule schedule = new Schedule();
            schedule.ScheduleId = Convert.ToInt32(dataRow["ScheduleId"]);
            schedule.ChurchId = Convert.ToInt32(dataRow["ChurchId"]);
            schedule.ChurchName = dataRow["ChurchName"].ToString();
            schedule.Town = dataRow["Town"].ToString();
            schedule.Slug = dataRow["Slug"].ToString();
            schedule.EventName = dataRow["ScheduleEventName"].ToString();
            schedule.EventDate = Convert.ToDateTime(dataRow["ScheduleEventDate"].ToString());
            schedule.EventDay = dataRow["ScheduleEventDay"].ToString();
            schedule.EventTime = Convert.ToDateTime(dataRow["ScheduleEventTime"].ToString());
            schedule.IsRepeated = Convert.ToBoolean(dataRow["IsRepeated"].ToString());
            schedule.Record = Convert.ToBoolean(dataRow["Record"].ToString());
            return schedule;
        }

        public DataTable GetScheduleReadyToStart()
        {
            _dc.ClearParameters();
            DataTable dataTable = _dc.ReturnDataTable("spSchedule_ReadyForStart");
            return dataTable;
        }

        public DataTable GetScheduleReadyToStop()
        {
            _dc.ClearParameters();
            DataTable dataTable = _dc.ReturnDataTable("spSchedule_ReadyForStop");
            return dataTable;
        }

        public void UpdateScheduleStatus(int scheduleId, int scheduleStatus)
        {
            _dc.ClearParameters();
            _dc.AddParameter("SchId", scheduleId);
            _dc.AddParameter("SchStatus", scheduleStatus);
            _dc.Execute("spSchedule_UpdateStatus");
        }
       
        public void InsertScheduleLog(int scheduleId, int scheduleStatus)
        {
            _dc.ClearParameters();
            _dc.AddParameter("SchId", scheduleId);
            _dc.AddParameter("SchStatus", scheduleStatus);
            _dc.Execute("spScheduleActivityLog");
        }
      
        public void UpdateScheduleLog(int scheduleId, int scheduleStatus)
        {
            _dc.ClearParameters();
            _dc.AddParameter("SchId", scheduleId);
            _dc.AddParameter("SchStatus", scheduleStatus);
            _dc.Execute("spUpdateScheduleLog");
        }

        public DataTable Schedule_NotPublished_GetByCamera(int cameraId)
        {
            _dc.ClearParameters();
            _dc.AddParameter("CamId", cameraId);
            DataTable dataTable = _dc.ReturnDataTable("spSchedule_NotPublished_GetByCamera");
            return dataTable;
        }

    }
}