namespace StudentsSystem.Model
{
    using System.ComponentModel.DataAnnotations;

    using System;

    public class Homework
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
    }
}
