using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskList.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TaskList.Data
{
    public class Context : DbContext
    {
        public DbSet<Status> Status { get; set; }
        public DbSet<Weight> Weight { get; set; }
        public DbSet<MultiplierLookup> Multiplier { get; set; }
        public DbSet<CategoryLookup> Categories { get; set; }
        public DbSet<QuestionLookup> Questions { get; set; }
        public DbSet<AnswerLookup> Answers { get; set; }
        public DbSet<TaskSet> Tasks { get; set; }
        public DbSet<File_Task> Files { get; set; }
        public DbSet<Task_Category> Task_Categories { get; set; }
        public DbSet<Category_Question_Answer> Category_Questions { get; set; }

        public Context() : base("TaskList") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskSet>().Property(t => t.DateCreated).HasColumnType("datetime2").HasPrecision(0);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

    }
}
