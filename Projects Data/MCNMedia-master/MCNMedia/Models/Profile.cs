﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCNMedia_Dev.Models
{
    public class Profile
    {
        public Church Churches { get; set; }
        //public Camera Cameras { get; set; }
        //public MediaChurch Media { get; set; }
        //public Schedule Schedules { get; set; }
        //public Recording Recordings { get; set; }
        public Announcement Announcement { get; set; }
        public NewsLetter newsletter { get; set; }
        public Notice notice { get; set; }

        public  ChurchDonation churchDonation { get; set; }

        // Lists
        public IEnumerable<Church> ChurchList { get; set; }
        public List<WebsiteMedia> mediaList { get; set; }
        public IEnumerable<Camera> CameraList { get; set; }
        public IEnumerable<MediaChurch> VideoList { get; set; }
        public IEnumerable<MediaChurch> SlideshowList { get; set; }
        public IEnumerable<MediaChurch> PictureList { get; set; }
        public IEnumerable<NewsLetter> ChurchNewsLetterList { get; set; }
        public IEnumerable<Schedule> ScheduleList { get; set; }
        public IEnumerable<Recording> RecordingList { get; set; }
        public MediaChurch mediaChurch { get; set; }
        public List<MediaChurch> MediaChurchList { get; set; }
        public IEnumerable<Notice> NoticeList { get; set; }
        public IEnumerable<Schedule> NowScheduleList { get; set; }
        public IEnumerable<Schedule> NextScheduleList { get; set; }
        public IEnumerable<Schedule> ScheduleListDay0 { get; set; }
        public IEnumerable<Schedule> ScheduleListDay1 { get; set; }
        public IEnumerable<Schedule> ScheduleListDay2 { get; set; }
        public IEnumerable<Schedule> ScheduleListDay3 { get; set; }
        public IEnumerable<Schedule> ScheduleListDay4 { get; set; }
        public IEnumerable<Schedule> ScheduleListDay5 { get; set; }
        public IEnumerable<Schedule> ScheduleListDay6 { get; set; }
        public IEnumerable<Schedule> UpcomingSchedule { get; set; }
    }
}
