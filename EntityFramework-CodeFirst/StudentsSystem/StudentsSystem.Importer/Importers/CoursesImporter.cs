﻿namespace StudentsSystem.Importer.Importers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StudentsSystem.Data;
    using StudentsSystem.Model;

    public class CoursesImporter : IImporter
    {
        public void Seed(int count)
        {
            Console.Write("Importing courses:");

            var db = new StudentsSystemDbContext();
            var allStudents = db
                .Students
                .OrderBy(s => Guid.NewGuid())
                .ToList();
            var allHomeworks = db
                .Homeworks
                .OrderBy(h => Guid.NewGuid())
                .ToList();

            for (int i = 0; i < count; i++)
            {
                var currentStudentInCourse = RandomGenerator.GetRandomNumber(10, 30);
                var currentStartIndex = RandomGenerator.GetRandomNumber(0,
                    allStudents.Count - currentStudentInCourse - 1);
                var studentsList = new HashSet<Student>();
                for (int j = 0; j < currentStudentInCourse; j++)
                {
                    studentsList.Add(allStudents[currentStartIndex]);
                    currentStartIndex++;
                }

                var currentHomeworsInCourse = RandomGenerator.GetRandomNumber(5, 15);
                currentStartIndex = RandomGenerator.GetRandomNumber(0,
                    allHomeworks.Count - currentHomeworsInCourse - 1);
                var homeworksList = new HashSet<Homework>();
                for (int j = 0; j < currentHomeworsInCourse; j++)
                {
                    homeworksList.Add(allHomeworks[currentStartIndex]);
                    currentStartIndex++;
                }

                var course = new Course
                {
                    Name = RandomGenerator.GetRandomString(10, 100),
                    Description = RandomGenerator.GetRandomString(50, 500),
                    Students = studentsList,
                    Homeworks = homeworksList
                };

                db.Courses.Add(course);

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
