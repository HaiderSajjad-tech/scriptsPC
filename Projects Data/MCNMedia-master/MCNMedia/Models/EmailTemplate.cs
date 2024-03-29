﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCNMedia_Dev.Models
{
    public class EmailTemplate
    {
        public int MessageId { get; set; }
        public string MessageFor { get; set; }
        public string Subject { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SystemEMail { get; set; }

    }
}