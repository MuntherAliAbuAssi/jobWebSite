using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jobWebSite.Models
{
    public class JobsViewModel
    {
        public string  JobTitle { get; set; }
        // حتى نجلب الأشخاص الذين تقدموا للوظيفة
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}