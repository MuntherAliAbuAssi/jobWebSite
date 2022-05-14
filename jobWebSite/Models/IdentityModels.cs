using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace jobWebSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; } // يمثل المستخدوم و إذا اردت تمثيل بعص المستخدمين نمثلهم هنا 
       
        // علاقة متعدد لمتعدد مع الجوب في جدول خارجي اللي مكتوب في المجموعة
        public virtual ICollection<ApplyForJob> ApplyForJob { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        // نتعامل معها لإدارة الرولز
        //قمت بإضافته بعد إنشاء الجدول
      
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<jobWebSite.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<jobWebSite.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<jobWebSite.Models.RollViewModel> RollViewModels { get; set; }

        public System.Data.Entity.DbSet<jobWebSite.Models.ApplyForJob> ApplyForJobs { get; set; }

    }
}