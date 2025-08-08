// Progarm.cs 
// Description: Entry point of the Student Management System console application.
//              Handles user interaction, menu navigation, and calls StudentManager methods.
using StudentManagementSystem.Models;
using StudentManagementSystem.Manager;
using StudentManagementSystem.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem
{
    class Program
    {
        /// <summary>
        /// Displays menu, takes user input, and performs operations like Add, Update, Delete, etc.
        /// </summary>
        public static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();

            // Subscribe to the event when more than 5 students are added
            // manager.StudentLimitReached += () => Console.WriteLine("Event: More than 5 students added");
            manager.StudentLimitReached += manager.EventReached;

            while (true)
            {
                Console.WriteLine("Choose an Option:");
                Console.WriteLine("\n1. Add student\n2. Update student\n3. Delete student\n4. List all students\n5. View Top scorer\n6. Group by department\n7. Get summary\n8. Exit");
                Console.Write("Select: ");
                int choice = int.Parse(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            // Add student
                            Student s = new Student();

                            Console.Write("ID: ");
                            s.Id = int.Parse(Console.ReadLine());

                            Console.Write("Name: ");
                            s.Name = Console.ReadLine();

                            Console.Write("Age: ");
                            s.Age = int.Parse(Console.ReadLine());

                            Console.Write("Department: ");
                            s.Department = Console.ReadLine();

                            Console.Write("Marks: ");
                            s.Marks = double.Parse(Console.ReadLine());

                            Console.Write("Level (Beginner, Intermediate, Advanced): ");
                            s.Level = Enum.Parse<Level>(Console.ReadLine(), true);


                            Address arr = new Address();

                            Console.Write("Street: ");
                            arr.Street = Console.ReadLine();

                            Console.Write("City: ");
                            arr.City = Console.ReadLine();

                            s.Address = arr;        // Set the struct

                            manager.AddStudent(s);
                            Console.WriteLine("Student added successfully.");
                            break;
                        case 2:
                            // Update student
                            Student updated = new Student();
                            Console.Write("Enter Id to update: ");
                            int updateId = int.Parse(Console.ReadLine());

                            Console.Write("Name: ");
                            updated.Name = Console.ReadLine();

                            Console.Write("Age: ");
                            updated.Age = int.Parse(Console.ReadLine());

                            Console.Write("Department: ");
                            updated.Department = Console.ReadLine();

                            Console.Write("Marks: ");
                            updated.Marks = double.Parse(Console.ReadLine());

                            Console.Write("Level (Beginner, Intermediate, Advanced): ");
                            updated.Level = Enum.Parse<Level>(Console.ReadLine(), true);
                            // updated.Level = (Level)Enum.Parse(typeof(Level), Console.ReadLine(), true);

                            Address a = new Address();

                            Console.Write("Street: ");
                            a.Street = Console.ReadLine();

                            Console.Write("City: ");
                            a.City = Console.ReadLine();

                            updated.Address = a;

                            // // Struct without creating object
                            // Address address;                              
                            // address.Street = Console.ReadLine();
                            // address.City = Console.ReadLine();
                            // updated.Address = address;


                            manager.UpdateStudent(updateId, updated);
                            break;
                        case 3:
                            // Delete student
                            Console.Write("Enter Id to delete: ");
                            int deleteId = int.Parse(Console.ReadLine());

                            manager.DeleteStudent(deleteId);
                            break;
                        case 4:
                            // List all students
                            Console.WriteLine("List of Students");
                            Console.WriteLine("-----------");
                            manager.ListStudents();
                            Console.WriteLine("-----------");
                            break;
                        case 5:
                            // View top scorer
                            Console.WriteLine("Top score");
                            Console.WriteLine("-----------");
                            manager.ViewTopScorer();
                            break;
                        case 6:
                            // Group students by department
                            Console.WriteLine("GroupBy Department");
                            Console.WriteLine("-----------");
                            manager.GroupByDepartment();
                            Console.WriteLine("-----------");
                            break;
                        case 7:
                            // Average marks
                            Console.WriteLine("Average marks");
                            manager.GetSummary();
                            break;
                        case 8:
                            // Exit application
                            return;
                        default:
                            // Invalid
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                catch (InvalidStudentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error occured: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Process Completed");
                }
            }

        }
    }
}