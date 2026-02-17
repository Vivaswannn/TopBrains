using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ValidationDemo.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public int Age { get; set; }
        public List<Enrollment>  Enrollments { get; set; }
        public string Email { get; internal set; }
    }
}
