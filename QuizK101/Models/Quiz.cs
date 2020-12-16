namespace QuizK101.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Quiz")]
    public partial class Quiz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quiz()
        {
            MyAnswers = new HashSet<MyAnswer>();
        }

        public int ID { get; set; }

        public int? ExamID { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        public DateTime QuizStart { get; set; }

        public DateTime QuizEnd { get; set; }

        public TimeSpan QuizTime { get; set; }

        public int QuizCount { get; set; }

        public int? Score { get; set; }
        public bool QuizActive{ get; set; }


        public virtual Exam Exam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MyAnswer> MyAnswers { get; set; }
    }
}
