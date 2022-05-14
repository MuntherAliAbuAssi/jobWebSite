using jobWebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(jobWebSite.Startup))]
namespace jobWebSite
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreatedDefualtRolesUsers();

        }
        private void CreatedDefualtRolesUsers()
        {
            // حينشأ أشياء بشكل مبدئي زي الأدمين لانو عند رفع المشروع على السيرفر كل بيانات قاعدة البيانات بتكون محذوف
            //مستنسخ من الرول منجر الذي يسمح بادارة الرول ويقوم بالتخزين بالداتا بيز على بواسطة كلاس اسمو الرولستور
            // وايضا في حال الهكر حذف كل قاعدة البيانات ما يقدر ينشأ شيةهى وعند تحديث الصفحة ينشأ الأدمين تلقائيا

               var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            // يسمح بالتحكم بالرول الخاصة باليوزرات
            // يوزر منجر هو اللي بدو يضيف اليوزرات 
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            // أنشائنا الرول الأن عند حذف الرول من كل قاعدة بيانات وعند تشغيل الموقع سيفحص وويضيف 
            if (!roleManger.RoleExists("Admins"))
            {
                // اعطينا الرول
                role.Name = "Admins";
                roleManger.Create(role);
                // أنشا اليوزر
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Khalid";
                user.Email = "Khalid@hotmail.fr";
                var Check = userManager.Create(user, "Khalid12@hotmail.fr");// الحقل الثاني كلمة السر
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admins"); // قمت بإسناد الرول إلى اليوزر
                }
            }
        

        }
    }
}
