// StudentManager.cs
// Contains logic for managing student data (add, update, delete, group, summary)
using StudentManagementSystem.Models;
using StudentManagementSystem.Exceptions;
using System.Numerics;
namespace StudentManagementSystem.Manager
{
    class StudentManager
    {
        // List to store student records
        private List<Student> students = new List<Student>();

        // Delegate and event to notify when more than 5 students are added
        public delegate void StudentLimitReachedEventHandler();
        public event StudentLimitReachedEventHandler StudentLimitReached;

        public void EventReached()
        {
            Console.WriteLine("Event: More that 5 students");
        }


        /// <summary>
        /// Adds a new student to the list.
        /// Throws InvalidStudentException if the student data is invalid.
        /// Triggers event if student count exceeds 5.
        /// </summary>
        public void AddStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) || student.Marks < 0)
            {
                throw new InvalidStudentException("Invalid student data");
            }
            students.Add(student);

            if (students.Count > 5)
            {
                StudentLimitReached?.Invoke();
            }
        }

        // Updates an existing student record based on Id.
        public void UpdateStudent(int id, Student updatedStudent)
        {
            var student = students.FirstOrDefault(x => x.Id == id);
            if (student != null)
            {
                student.Name = updatedStudent.Name;
                student.Age = updatedStudent.Age;
                student.Department = updatedStudent.Department;
                student.Marks = updatedStudent.Marks;
                student.Level = updatedStudent.Level;
                student.Address = updatedStudent.Address;
            }
        }

        // Deletes a student from the list based on Id.
        public void DeleteStudent(int id)
        {
            students.RemoveAll(s => s.Id == id);
        }

        // Lists all students and their information.
        public void ListStudents()
        {
            foreach (var s in students)
            {
                Console.WriteLine($"Name: {s.Name}\nAge: {s.Age}\nDepartment: {s.Department}\nMarks: {s.Marks}\nLevel: {s.Level}\nAddress: {s.Address.GetFullAddress()}");
            }
        }

        // Displays the student with the highest marks.
        public void ViewTopScorer()
        {
            var topScorer = students.OrderByDescending(s => s.Marks).FirstOrDefault();
            if (topScorer != null)
            {
                Console.WriteLine($"Top Scorer: {topScorer.Name}\nMarks: {topScorer.Marks}");
            }
        }

        // Groups students by their department and shows count per group.
        public void GroupByDepartment()
        {
            var groups = students.GroupBy(g => g.Department);
            foreach (var group in groups)
            {
                Console.WriteLine($"Department: {group.Key}, Count: {group.Count()}");
            }
        }

        // Calculates and returns the total and average marks of all students.
        public (double total, double average) GetSummary()
        {
            var total = students.Sum(s => s.Marks);
            var average = students.Count > 0 ? total / students.Count : 0;
            return (total, average);
        }
    }
}