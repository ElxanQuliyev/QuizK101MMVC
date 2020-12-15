namespace QuizK101.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MyAnswer
    {
        public int ID { get; set; }

        public int? AnswerID { get; set; }

        public int? QuizID { get; set; }

        public int? QuestionID { get; set; }

        public virtual Question Question { get; set; }
    }
}
