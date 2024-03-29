﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCNMedia_Dev.Models
{
    public class GenericModel
    {
        public Church Churches { get; set; }
        public Camera Cameras { get; set; }
        public ChurchLock churchLock { get; set; }
        public RecordingLock recordingLock { get; set; }
        public Testinomial testinomial { get; set; }

        public User Users { get; set; }
        public MediaChurch Media { get; set; }
        public IEnumerable<MediaChurch> Videos { get; set; }
        public IEnumerable<MediaChurch> Pictures { get; set; }
        public IEnumerable<MediaChurch> SlideShow { get; set; }
        public Schedule Schedules { get; set; }
        public NewsLetter ChurchNewsLetter { get; set; }
        public IEnumerable<NewsLetter> ListChurchNewsLetter { get; set; }
        public Recording Recordings { get; set; }

        public AnalyticsModel Analytics { get; set; }
        public Announcement Announcement { get; set; }
        public ChurchDonation ChurchDonations { get; set; }
        public IEnumerable<Announcement> LAnnouncement { get; set; }
        public DashBoardClient DashBoardClients { get; set; }
        public IEnumerable<DashBoardClient> LDashBoardClients { get; set; }
        public Dashboard Dashboards { get; set; }
        public IEnumerable<Dashboard> LDashboards { get; set; }
        public IEnumerable<Dashboard> ListDashboards2 { get; set; }
        public IEnumerable<MediaChurch> ListMedia { get; set; }
        public IEnumerable<Camera> LCameras { get; set; }

        public IEnumerable<AnalyticsModel> AnalyticsList { get; set; }
        public IEnumerable<GoogleAnalyticsProperty> googleAnalytics { get; set; }
        public IEnumerable<User> LUsers { get; set; }

        public IEnumerable<Schedule> LSchedules { get; set; }
        public IEnumerable<Schedule> LSchedulesInHour { get; set; }

        public IEnumerable<Recording> LRecordings { get; set; }
        public PreviewChurch PreviewChurches { get; set; }
        public IEnumerable<PreviewChurch> LPreviewChurches { get; set; }

        
        public Notice Notices { get; set; }
        public IEnumerable<Notice> ListNotice { get; set; }
        public SetUp ChurchSetUp { get; set; }

        public string ResultMessage { get; set; } = "";

        public IEnumerable<Church> ChurchList { get; set; }
        public IEnumerable<Place> CountryList { get; set; }
        public IEnumerable<Place> CountyList { get; set; }

        public IEnumerable<RssFeedModel> RssFeedsList { get; set; }

        public EmailTemplate EmailTemplates { get; set; }
        public IEnumerable<EmailTemplate> LEmailTemplate { get; set; }

    }
}
