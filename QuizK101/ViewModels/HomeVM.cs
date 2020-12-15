using QuizK101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizK101.ViewModels
{
    public class HomeVM
    {
        public Quiz Quiz { get; set; }
        public List<Subject> Subjects{ get; set; }
        public List<Exam> Exams { get; set; }
        public Exam Exam { get; set; }

    }
}