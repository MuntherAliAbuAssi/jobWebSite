using jobWebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace jobWebSite.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Details(int jobId)
        {
            var job = db.Jobs.Find(jobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jobId;
            return View(job);
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View(); // عبارة عن رسالة سيرسلها الشخص المتقدم للوظيفة
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var userId = User.Identity.GetUserId(); // بترجع الاي دي الخاص اليوزر المتصل حاليا
            var jobId = (int)Session["JobId"]; // حيجيب الاي دي الخاص بالوظيفة من صفحة التفاصيل عن طريق السشن
            // اذهب الى الجدول العلاقات وابحث عن اي عنصر الاي دي الخاص به اموجود حاليا 
            var check = db.ApplyForJobs.Where(a => a.JobId == jobId && a.UserID == userId).ToList();
            if (check.Count() < 1)
            {
                var job = new ApplyForJob();
                job.UserID = userId;
                job.JobId = jobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "تمت الإضافة بنجاح";
            }
            else
            {
                ViewBag.Result = "عذراً ،لقد  تقدمت إلى الوظيفة";

            }

            return View();
        }
        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserID == UserId);
            return View(jobs.ToList());
        }
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.UserID == UserId
                       select app; //سيتم تخزين اسم اليوزر والوظائف اللي تقدمو إليها
                                   //عملية القروب كل وظيفة تحت المتقدمين
                                   // انشا مودل جديد اسمو جوب فيو مودل على تحتوي على الأشخاصص المتقدمين للوظيفة للوظيفة
                                   //ناتج عملية القروب في الكلاس الجديد
            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };
            return View(grouped.ToList());
        }

        /*
         

         */
        [Authorize]
        public ActionResult DetailsOfJob(int Id)
        {
            var job = db.ApplyForJobs.Find(Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = Id;
            return View(job);
        }

        [Authorize]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            ApplyForJob applyForJob = db.ApplyForJobs.Find(Id);
            if (applyForJob == null)
            {
                return HttpNotFound();
            }
            return View(applyForJob);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }
        // GET: Rolls/Delete/5
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var job = db.ApplyForJobs.Find(Id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Rolls/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                var myjob = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(myjob);
                db.SaveChanges();
                ViewBag.Result2 = "تمت عملية الحف بنجاح";
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModlel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("munther.assi.2001@gmail.com", "0592172026");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("munther.assi.2001@gmail.com"));
            mail.Subject = contact.Subject;
            string body = "اسم المرسل :" + contact.Name + "<br>" +
                          "بريد المرسل :" + contact.Email + "<br>" +
                          "عنوان الرسالة :" + contact.Subject + "<br>" +
                          "نص الرسالة :" + contact.Message;
            mail.Body = contact.Message;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDesc.Contains(searchName)).ToList();
            return View(result);

        }
    }
}