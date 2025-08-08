// Student.cs
// Defines the Student class model
namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public double Marks { get; set; }
        public Level Level { get; set; }
        public Address Address { get; set; }
    }
}