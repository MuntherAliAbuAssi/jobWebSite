using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jobWebSite.Models;
namespace jobWebSite.Models
{
    // مودل مرتبط بعلاقة متعدد لمتعدد بين جدول الوظيفة واليوزر
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public DateTime ApplyDate { get; set; }
        public int JobId { get; set; } // معرف الوظيفة جبتو عن طريق السيشن لعرض تفاصيل الوظيفة
        public string UserID { get; set; } // من خلال ميثود موجودة في identity اسمها getUserID التي تسمح لنا 
        // العلاقة متعدد 
        // من أجل الربط بين جدولين جدول الوظيفة والمستخدم
        public virtual Job job { get; set; } // جدول الوظيفة 
        // virtual  يعني حتى نستفيد من مزايا الكلاس اللي أنا عملو
        public virtual ApplicationUser user { get; set; }// جدول اليوزر
    }

}