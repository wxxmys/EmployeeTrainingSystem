using EmployeeTrainingEntity;
using EmployeeTrainingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTrainingSystem
{
    public class EntityDbContext:DbContext
    {
        public EntityDbContext() { }
        public EntityDbContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// /教学计划
        /// </summary>
        public DbSet<TeachingPlan> TeachingPlans { get; set; }
        /// <summary>
        /// 教学内容的课件
        /// </summary>
        public DbSet<Courseware> Coursewares { get; set; }
        public DbSet<LiveFiles> LiveFiles { get; set; }
        /// <summary>
        /// 培训申请
        /// </summary>
        public DbSet<TrainRequest> TrainRequest { get; set; }
        /// <summary>
        /// 成员
        /// </summary>
        public DbSet<Member> Member { get; set; }
        /// <summary>
        /// 批改作业
        /// </summary>
        public DbSet<CorrectJob> CorrectJob { get; set; } 
        /// <summary>
        /// 发布作业
        /// </summary>
        public DbSet<Publishjob> Publishjob { get; set; }
        /// <summary>
        /// 老师
        /// </summary>
        public DbSet<Teacher> Teacher { get; set; }
        /// <summary>
        /// 学生
        /// </summary>
        public DbSet<Students> Students { get; set; }
        /// <summary>
        /// 课程
        /// </summary>
        public DbSet<ClassSchedule> ClassSchedule { get; set; }

        //public DbSet<MySchedule> MySchedule { get; set; }//课表
        /// <summary>
        /// 题库
        /// </summary>
        public DbSet<Information> Information { get; set; }
        /// <summary>
        /// 试卷
        /// </summary>
        public DbSet<TestPaper> TestPaper { get; set; }
        /// <summary>
        /// 试卷内容
        /// </summary>
        public DbSet<ExamContent> examContents { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public DbSet<Answer> Answers { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public DbSet<Department> Department { get; set; }

        /// <summary>
        /// 培训计划
        /// </summary>
        public DbSet<TrainPlan> TrainPlan { get; set; }
        /// <summary>
        /// 培训记录
        /// </summary>
        public DbSet<TrainRecord> TrainRecord { get; set; }
        /// <summary>
        /// 培训资源
        /// </summary>
        public DbSet<TrainResource> TrainResource { get; set; }
        /// <summary>
        /// 培训资格证明
        /// </summary>
        public DbSet<TrainQualificationCertificate> TrainQualificationCertificate { get; set; }
        /// <summary>
        /// 学院和班级 空为学院
        /// </summary>
        public DbSet<CollegeClass> CollegeClass { get; set; }
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string connetionString = configuration["ConnectionStrings:Default"];
            optionsBuilder.UseSqlServer(connetionString);
            base.OnConfiguring(optionsBuilder);
        }


        
    }
}
