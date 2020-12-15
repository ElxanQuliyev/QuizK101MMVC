using QuizK101.Models;
using QuizK101.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizK101.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            HomeVM vm = new HomeVM();
            vm.Subjects = db.Subjects.ToList();
            return View(vm);
        }
        public ActionResult ExamList(int? id)
        {
            if (id == null) return HttpNotFound();
            var examList = db.Exams.Where(x => x.SubjectID == id).ToList();
            HomeVM vm = new HomeVM();
            vm.Exams = examList;
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}