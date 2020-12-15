using QuizK101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizK101.ViewModels
{
    public class QuestionViewModel
    {
        public QuestionDto Question { get; set; }

        public int CurrentQuestion = 0;
        public int TotalQuestionCount { get; set; }
        //public int ExamID { get; set; }
        public Exam Exam { get; set; }
        public Quiz Quiz { get; set; }
        public MyAnswer MyAnswerId { get; set; }
    }
}