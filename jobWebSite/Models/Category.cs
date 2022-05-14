using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jobWebSite.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [DisplayName("نوع الوظيفة")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("وصف النوع")]
        public string  CategoryDesc { get; set; }
        // virtual علشان في تقدم في البيانات يعني بيانات الوصف في الأسم وهكذا
        // تسمح لنا بتفعيل الليزي لودينج
        public virtual ICollection<Job> Jobs { get; set; }
    }
}