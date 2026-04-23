using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentActivityEvaluation
{
    public class Student
    {
        public string Name { get; set; } = string.Empty;
        public List<int> Scores { get; set; } = new List<int>();
        public int Attendance { get; set; } // Percentage
        public int Participation { get; set; } // Points out of 10
        public int TotalMarks { get; set; }
    }

    // Delegate to handle student evaluation logic
    public delegate void StudentEvaluator(Student student);

    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student { Name = "Alice", Scores = new List<int> { 80, 85, 90 }, Attendance = 95, Participation = 10 },
                new Student { Name = "Bob", Scores = new List<int> { 40, 45, 50 }, Attendance = 75, Participation = 5 },
                new Student { Name = "Charlie", Scores = new List<int> { 60, 70, 65 }, Attendance = 80, Participation = 8 },
                new Student { Name = "David", Scores = new List<int> { 30, 20, 40 }, Attendance = 60, Participation = 2 }
            };

            // Use an anonymous method to:
            // - Calculate total marks
            // - Display student performance
            StudentEvaluator evaluateStudent = delegate (Student student)
            {
                student.TotalMarks = student.Scores.Sum() + student.Attendance + student.Participation;
                
                Console.WriteLine($"Student: {student.Name}");
                Console.WriteLine($"Scores: {string.Join(", ", student.Scores)}");
                Console.WriteLine($"Attendance: {student.Attendance}%");
                Console.WriteLine($"Participation: {student.Participation}");
                Console.WriteLine($"Total Marks: {student.TotalMarks}");
                Console.WriteLine(new string('-', 30));
            };

            Console.WriteLine("--- Student Evaluation Details ---");
            foreach (var student in students)
            {
                evaluateStudent(student);
            }

            // Use a lambda expression to:
            // - Check if a student is eligible (e.g., average marks > 50)
            Func<Student, bool> isEligible = s => s.Scores.Average() > 50;

            // Use a lambda expression to:
            // - Filter a list of students based on performance
            List<Student> eligibleStudents = students.Where(isEligible).ToList();

            Console.WriteLine("\n--- Eligible Students (Average Score > 50) ---");
            foreach (var student in eligibleStudents)
            {
                Console.WriteLine($"- {student.Name} (Average Score: {student.Scores.Average():F2})");
            }
        }
    }
}
