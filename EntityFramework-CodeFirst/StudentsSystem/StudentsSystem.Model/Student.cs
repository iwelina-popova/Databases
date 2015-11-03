namespace StudentsSystem.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        private ICollection<Course> courses; 

        public Student()
        {
            this.courses = new HashSet<Course>();
        }
        
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Number { get; set; }

        public virtual ICollection<Course> Courses
        {
            get { return this.courses; }
            set { this.courses = value; }
        }
    }
}
