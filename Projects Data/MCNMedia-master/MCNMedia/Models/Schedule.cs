﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCNMedia_Dev.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int ChurchId { get; set; }
        public string ChurchName { get; set; }
        public string Slug { get; set; }
        public string Town { get; set; }

        [Required]
        public string EventName { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yyyy/MMM/dd}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime EventDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:ddd}")]
        public string EventDay { get; set; }
        [EnumDataType(typeof(DayOfWeek))]
        public EventDay Days { get; set; }
        //  [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm tt}")]
        public DateTime EventTime { get; set; }

        public int RecordDuration { get; set; }
 

        public int CameraId  { get; set; }
        public string CameraName { get; set; }
        public string Password { get; set; }

        public Boolean Record  {get; set;}
        public Boolean IsRepeated { get; set; }
        public int UpdatedBy { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}/{0:hh:mm tt}")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedBy { get; set; }

    }
    public enum EventDay
    {

        [Description("Monday")]
        Monday,
        [Description("Tuesday")]
        Tuesday,
        [Description("Wednesday")]
        Wednesday,
        [Description("Thursday")]
        Thursday,
        [Description("Friday")]
        Friday,
        [Description("Saturday")]
        Saturday,
        [Description("Sunday")]
        Sunday
        
    }
}
