using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using QuizK101.Models;
using QuizK101.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace QuizK101.Controllers
{
    public class QuizController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public QuizController()
        {
        }
        // GET: Quiz
        public ActionResult QuizInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            HomeVM vm = new HomeVM();
            vm.Exam = db.Exams.FirstOrDefault(e => e.ID == id);
            return View(vm);
        }
        private int CreateQuiz(int exmId)
        {
            Quiz quiz = new Quiz();
            quiz.ExamID = exmId;
            quiz.UserID = 2;
            quiz.QuizStart = DateTime.Now;
            //TimeSpan time = new TimeSpan(2000, 0, 0, 0);
            quiz.QuizEnd = DateTime.Now.AddHours(2);
            db.Quizs.Add(quiz);
            db.SaveChanges();
            return quiz.ID;
        }
        [HttpPost]
        public ActionResult QuizInfo(Exam exam)
        {
            int activeQuizId;
            HomeVM vm = new HomeVM();
            if (exam == null) return HttpNotFound();
            Quiz selectedQuiz= db.Quizs.Where(x => x.UserID == 2).FirstOrDefault(x=>x.ExamID==exam.ID);
            if (selectedQuiz == null)
            {
                activeQuizId = CreateQuiz(exam.ID); 
            }
            else if (selectedQuiz.QuizEnd <= DateTime.Now)
            {
                activeQuizId = CreateQuiz(exam.ID);
            }
            else
            {
                activeQuizId = selectedQuiz.ID;
            }
            return RedirectToAction("StartExam",new { quizId=activeQuizId,examId=exam.ID});
        }
        public ActionResult StartExam(int? QuizId, int? examId)
        {
            if (QuizId == null) return HttpNotFound();
            QuestionViewModel vm = new QuestionViewModel();
            vm.Quiz = db.Quizs.FirstOrDefault(x=>x.ID== QuizId);
            if (vm.Quiz == null) return HttpNotFound();
            vm.Exam = db.Exams.FirstOrDefault(x => x.ID==examId);
            if (vm.Exam == null) return HttpNotFound();
            //if (!vm.Quiz.QuizActive) return RedirectToAction("EndQuiz",new { QuizId});
            vm.TotalQuestionCount = db.Questions.Where(x => x.ExamID == vm.Quiz.ExamID).Count();
            return View(vm);
        }
        public ActionResult EndQuiz(int? QuizId)
        {
            int score = 0;
           Quiz qz= db.Quizs.FirstOrDefault(q => q.ID == QuizId);
            if (qz == null) return HttpNotFound();
            EndQuizVm vm = null;
            if (!qz.QuizActive)
            {
                List<MyAnswer> myAnswers = db.MyAnswers.Where(x => x.QuizID == QuizId).ToList();
                foreach (var answ in myAnswers)
                {
                    if (answ.AnswerID == answ.Question.CorrectAnswerID)
                    {
                        score++;
                    }
                }
                qz.Score = score;
                qz.QuizActive = false;
                qz.QuizEnd = DateTime.Now;
                db.SaveChanges();
                vm = new EndQuizVm()
                {
                    Score = (int)qz.Score
                };
            }
            
            return View(vm);
        }

        //[HttpPost]
        public ActionResult QuestionSlide(int? ExamId,int? QuizId, int? skip,int? answerId, int? questionId)
        {
            int chooseAnswerID;
            if (ExamId == null) return HttpNotFound();

            skip = skip.HasValue ? skip.Value > 0 ? skip.Value : 0 : 0;
            QuestionViewModel vm = new QuestionViewModel();
            vm.Quiz = db.Quizs.FirstOrDefault(x => x.ID == QuizId);
            vm.Exam = db.Exams.FirstOrDefault(x => x.ID == ExamId);
                            
            vm.Question = db.Questions.Where(x => x.ExamID == ExamId)
                .OrderBy(x => x.ID)
                .Select(x => new QuestionDto()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Answers = db.Answers
                .Where(ans => ans.QuestionID == x.ID).Select(ans => new AnswerDto()
                {
                    ID = ans.ID,
                    Name = ans.Name
                }).ToList()
                }).Skip(skip.Value).Take(1).First();
            vm.CurrentQuestion += skip.Value;

            vm.TotalQuestionCount = db.Questions.Where(x => x.ExamID == ExamId).Count();
            if (questionId != null && answerId != null)
            {   
                MyAnswer chooseAnswer = db.MyAnswers.FirstOrDefault(x => x.QuestionID == questionId && x.QuizID== QuizId);
                if (chooseAnswer == null)
                {
                    MyAnswer ans = new MyAnswer();
                    ans.QuestionID = questionId;
                    ans.AnswerID = answerId;
                    ans.QuizID = QuizId;
                    db.MyAnswers.Add(ans);
                    db.SaveChanges();
                    chooseAnswerID = (int)ans.AnswerID;
                }
                else
                {
                    chooseAnswer.AnswerID = answerId;
                    db.SaveChanges();
                }
            }
            else
            {
                questionId = vm.Question.ID;
            }
            vm.MyAnswerId = db.MyAnswers.FirstOrDefault(x => x.QuestionID == questionId && x.QuizID == QuizId);

            return PartialView("QuestionSlide",vm);
        }   
    }
}


//vm.Questions = Questions.Select(x => new QuestionDto()
//{
//    ID = x.ID,
//    Name = x.Name,
//    Answers = db.Answers.Where(ans => ans.QuestionID == x.ID).Select(ans => new AnswerDto()
//    {
//        ID = ans.ID,
//        Name = ans.Name
//    }).ToList()
//}).ToList();



//vm.Question = Questions.Select(x=>new QuestionDto()
//{
//    ID = x.ID,
//    Name = x.Name,
//    Answers = db.Answers.Where(ans => ans.QuestionID == x.ID)
//    .Select(ans => new AnswerDto()
//    {
//        ID = ans.ID,
//        Name = ans.Name
//    }).ToList()
//}).OrderBy(x => x.ID).Skip(skip.Value).First();

//var rnd = new Random();
//var randomNumbers = Enumerable.Range(1, 6).OrderBy(x => rnd.Next()).Take(6).ToList();
//vm.QuestionIDS = randomNumbers;