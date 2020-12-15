using QuizK101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizK101.ViewModels
{
    public class QuestionDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ExamID{ get; set; }
        public List<AnswerDto> Answers{ get; set; }
    }
}