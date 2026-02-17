using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ValidationDemo.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}
