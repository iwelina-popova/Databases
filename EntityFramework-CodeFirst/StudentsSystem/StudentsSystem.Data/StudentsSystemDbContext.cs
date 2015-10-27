namespace StudentsSystem.Data
{
    using System.Data.Entity;
    using StudentsSystem.Model;
    
    public class StudentsSystemDbContext : DbContext
    {
        public StudentsSystemDbContext()
            : base("StudentsSystem")
        {
        }

        public virtual IDbSet<Student> Students { get; set; }

        public virtual IDbSet<Course> Courses { get; set; }

        public virtual IDbSet<Homework> Homeworks { get; set; }
    }
}
