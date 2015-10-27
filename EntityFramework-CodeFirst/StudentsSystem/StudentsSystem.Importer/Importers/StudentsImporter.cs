namespace StudentsSystem.Importer.Importers
{
    using System;
    using StudentsSystem.Data;
    using StudentsSystem.Model;

    public class StudentsImporter : IImporter
    {
        public void Seed(int count)
        {
            var db = new StudentsSystemDbContext();
            for (int i = 0; i < count; i++)
            {
                var student = new Student
                {
                    Name = RandomGenerator.GetRandomString(10, 50),
                    Number = RandomGenerator.GetRandomString(10, 10)
                };

                db.Students.Add(student);

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
