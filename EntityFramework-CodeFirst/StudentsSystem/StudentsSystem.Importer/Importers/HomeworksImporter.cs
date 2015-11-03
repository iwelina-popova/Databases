namespace StudentsSystem.Importer.Importers
{
    using System;
    using System.Linq;
    using StudentsSystem.Data;
    using StudentsSystem.Model;

    public class HomeworksImporter : IImporter
    {
        public void Seed(int count)
        {
            Console.Write("Importing homeworks:");

            var db = new StudentsSystemDbContext();
            var allStudentIds = db
                .Students
                .Select(s => s.Id)
                .ToList();
            for (int i = 0; i < count; i++)
            {
                var studentId = RandomGenerator.GetRandomNumber(0, allStudentIds.Count - 1);
                var homework = new Homework
                {
                    Content = RandomGenerator.GetRandomString(50, 150),
                    TimeSent = RandomGenerator.GetRandomDate(before: DateTime.Now),
                    StudentId = allStudentIds[studentId]
                };

                db.Homeworks.Add(homework);

                if (i % 10 == 0)
                {
                    Console.Write(".");
                }

                if (i % 100 == 0)
                {
                    db.SaveChanges();
                    db.Dispose();
                    db = new StudentsSystemDbContext();
                }
            }

            db.SaveChanges();
            Console.WriteLine();
        }
    }
}
