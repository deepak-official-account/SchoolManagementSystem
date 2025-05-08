using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystem.API.Models
{
    public class Student
    {
        [Key]
        public int RollNo { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [Required]
        [Range(5, 100)]
        public int Age { get; set; }
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }

        public Student()
        {

        }

        public Student(int rollNo, string name, string surname, int age, string phoneNumber)
        {
            RollNo = rollNo;
            Name = name;
            Surname = surname;
            Age = age;
            PhoneNumber = phoneNumber;
        }

    }
}
