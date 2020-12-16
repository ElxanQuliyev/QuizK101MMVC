namespace QuizK101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        QuestionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.QuestionID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        CorrectAnswerID = c.Int(nullable: false),
                        ExamID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exams", t => t.ExamID)
                .Index(t => t.ExamID);
            
            CreateTable(
                "dbo.MyAnswers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnswerID = c.Int(),
                        QuizID = c.Int(),
                        QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.QuestionID)
                .ForeignKey("dbo.Quiz", t => t.QuizID)
                .Index(t => t.QuizID)
                .Index(t => t.QuestionID);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        ExamTime = c.Time(nullable: false, precision: 7),
                        QuestionCount = c.Int(nullable: false),
                        SeeAnswer = c.Boolean(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Quiz",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(),
                        UserID = c.String(maxLength: 128),
                        QuizStart = c.DateTime(nullable: false),
                        QuizEnd = c.DateTime(nullable: false),
                        QuizTime = c.Time(nullable: false, precision: 7),
                        QuizCount = c.Int(nullable: false),
                        Score = c.Int(),
                        QuizActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exams", t => t.ExamID)
                .Index(t => t.ExamID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false, maxLength: 300),
                        SubjectCode = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Exams", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.MyAnswers", "QuizID", "dbo.Quiz");
            DropForeignKey("dbo.Quiz", "ExamID", "dbo.Exams");
            DropForeignKey("dbo.Questions", "ExamID", "dbo.Exams");
            DropForeignKey("dbo.MyAnswers", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Answers", "QuestionID", "dbo.Questions");
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.Quiz", new[] { "ExamID" });
            DropIndex("dbo.Exams", new[] { "SubjectID" });
            DropIndex("dbo.MyAnswers", new[] { "QuestionID" });
            DropIndex("dbo.MyAnswers", new[] { "QuizID" });
            DropIndex("dbo.Questions", new[] { "ExamID" });
            DropIndex("dbo.Answers", new[] { "QuestionID" });
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Subjects");
            DropTable("dbo.Quiz");
            DropTable("dbo.Exams");
            DropTable("dbo.MyAnswers");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
