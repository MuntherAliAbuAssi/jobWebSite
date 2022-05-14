using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jobWebSite.Models
{
    public class Job
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("اسم الوظيفة")]
        public string JobTitle { get; set; }
        [DisplayName("وصف الوظيفة")]
        [AllowHtml]
        public string JobContent { get; set; }
        [DisplayName("صورة الوظيفة")]
        public string JobImage { get; set; }
        // يحدد الشخص الذي قام بإضافة هذه الوظيفة
        public string UserID { get; set; }
        //العلاقة واحد لمتعدد الأكبر يهاجر إلى الأصغر
        [DisplayName("نوع الوظيفة")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        // علشان نعرف مين اللي نشر الوظيفة علاقة واحد لمتعدد
        // من أجل السماح بعملية الربط بين جدول الوظائف واليوزر
        public ApplicationUser User { get; set; }
        // عملية المتعدد لمتعدد
        public virtual ICollection<ApplyForJob> ApplyForJob { get; set; }
    }
}