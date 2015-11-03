namespace StudentsSystem.ConsoleClient
{
    using System.Data.Entity;

    using StudentsSystem.Data;
    using StudentsSystem.Data.Migrations;
    using StudentsSystem.Importer.Importers;
    
    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentsSystemDbContext, Configuration>());

            var studentsImporter = new StudentsImporter();
            studentsImporter.Seed(100);

            var homeworksImporter = new HomeworksImporter();
            homeworksImporter.Seed(100);

            var coursesImporter = new CoursesImporter();
            coursesImporter.Seed(100);
        }
    }
}
