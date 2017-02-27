namespace TaskList.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskList.Data.Entity;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskList.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TaskList.Data.Context context)
        {
            // Category
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 1, CategoryName = "Strategy" });
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 2, CategoryName = "Urgency" });
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 3, CategoryName = "Cost Benefit" });
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 4, CategoryName = "Infrastructure Opportunity" });
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 5, CategoryName = "Breadth of Impact" });
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 6, CategoryName = "Product Impact" });
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 7, CategoryName = "Risk Reduction" });
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 8, CategoryName = "Policy" });
            context.Categories.AddOrUpdate(new CategoryLookup { CategoryId = 9, CategoryName = "Funding" });

            // Weight
            context.Weight.AddOrUpdate(new Weight { WeightValue = 1, WeightId = 1 });
            context.Weight.AddOrUpdate(new Weight { WeightValue = 2, WeightId = 2 });
            context.Weight.AddOrUpdate(new Weight { WeightValue = 3, WeightId = 3 });
            context.Weight.AddOrUpdate(new Weight { WeightValue = 4, WeightId = 4 });

            // Status
            context.Status.AddOrUpdate(new Status { StatusId = 1, StatusName = "Pending" });
            context.Status.AddOrUpdate(new Status { StatusId = 2, StatusName = "Approved" });
            context.Status.AddOrUpdate(new Status { StatusId = 3, StatusName = "NotApproved" });

            // Question
            context.Questions.AddOrUpdate(new QuestionLookup { QuestionId = 1, QuestionName = "Does the Proposed Change Affect", CategoryId = 1 });
            context.Questions.AddOrUpdate(new QuestionLookup { QuestionId = 2, QuestionName = "Mandate", CategoryId = 2 });
            context.Questions.AddOrUpdate(new QuestionLookup { QuestionId = 3, QuestionName = "Timeline", CategoryId = 2 });

            // Answer
            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 1, AnswerName = "All Campuses", QuestionId = 1, WeightId = 3 });
            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 2, AnswerName = "Lawrence Campus", QuestionId = 1, WeightId = 2 });
            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 3, AnswerName = "KUMC / Wichita", QuestionId = 1, WeightId = 2 });

            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 4, AnswerName = "Is the proposed project a mandate from the State of kansas or other regulatory agency?", QuestionId = 2, WeightId = 3 });
            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 5, AnswerName = "Is the proposed project a mandate from campus executive leadership?", QuestionId = 2, WeightId = 2 });
            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 6, AnswerName = "Is the proposed project a mandate from Project Team members?", QuestionId = 2, WeightId = 1 });

            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 7, AnswerName = "Does the proposed project have a required implementation within 1-3 months?", QuestionId = 3, WeightId = 4 });
            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 8, AnswerName = "Does the proposed project have a required implementation within 3 - 6 months?", QuestionId = 3, WeightId = 3 });
            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 9, AnswerName = "Does the proposed project have a required implementation within 6 -12 months?", QuestionId = 3, WeightId = 2 });
            context.Answers.AddOrUpdate(new AnswerLookup { AnswerId = 10, AnswerName = "Does the proposed project no required implementation timeframe?", QuestionId = 3, WeightId = 1 });

            // Multiplier
            context.Multiplier.AddOrUpdate(new MultiplierLookup { MultiplierId = 1, MultiplierValue = 1 });
            context.Multiplier.AddOrUpdate(new MultiplierLookup { MultiplierId = 2, MultiplierValue = 2 });
            context.Multiplier.AddOrUpdate(new MultiplierLookup { MultiplierId = 3, MultiplierValue = 3 });
            context.Multiplier.AddOrUpdate(new MultiplierLookup { MultiplierId = 4, MultiplierValue = 4 });
            context.Multiplier.AddOrUpdate(new MultiplierLookup { MultiplierId = 5, MultiplierValue = 5 });
            context.Multiplier.AddOrUpdate(new MultiplierLookup { MultiplierId = 6, MultiplierValue = 6 });
            context.Multiplier.AddOrUpdate(new MultiplierLookup { MultiplierId = 7, MultiplierValue = 7 });
            context.Multiplier.AddOrUpdate(new MultiplierLookup { MultiplierId = 8, MultiplierValue = 8 });

            base.Seed(context);
        }
    }
}
