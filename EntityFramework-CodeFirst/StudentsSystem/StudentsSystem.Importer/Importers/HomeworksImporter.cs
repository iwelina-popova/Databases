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
            var db = new StudentsSystemDbContext();
            var allStudentIds = db
                .Students
                .Select(st => st.Id)
                .ToList();

            for (int i = 0; i < count; i++)
            {
                var homework = new Homework
                {
                    Content = RandomGenerator.GetRandomString(50, 150),
                    TimeSent = RandomGenerator.GetRandomDate(before: DateTime.Now),
                    StudentId = allStudentIds[RandomGenerator.GetRandomNumber(0, allStudentIds.Count - 1)]
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
        }
    }
}
