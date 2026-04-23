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

            // US1 & US5: Calculate total marks using Func + Anonymous Method (delegate keyword)
            Func<Student, int> calculateTotalMarks = delegate (Student s)
            {
                return s.Scores.Sum() + s.Attendance + s.Participation;
            };

            // Apply total marks calculation
            foreach (var student in students)
            {
                student.TotalMarks = calculateTotalMarks(student);
            }

            // US2 & US6: Display student details using Action + Lambda (=> syntax)
            Action<Student> displayStudent = s =>
            {
                Console.WriteLine($"Student: {s.Name}");
                Console.WriteLine($"Scores: {string.Join(", ", s.Scores)}");
                Console.WriteLine($"Attendance: {s.Attendance}%");
                Console.WriteLine($"Participation: {s.Participation}");
                Console.WriteLine($"Total Marks: {s.TotalMarks}");
                Console.WriteLine(new string('-', 30));
            };

            Console.WriteLine("--- Student Evaluation Details ---");
            foreach (var student in students)
            {
                displayStudent(student);
            }

            // US3: Check if a student is eligible (marks > 50) using Predicate + Lambda
            Predicate<Student> isEligible = s => s.Scores.Average() > 50;

            // Display eligible students (using the predicate)
            Console.WriteLine("\n--- Eligible Students (Average Marks > 50) ---");
            foreach (var student in students)
            {
                if (isEligible(student))
                {
                    Console.WriteLine($"- {student.Name} (Average: {student.Scores.Average():F2})");
                }
            }

            // US4: Filter students scoring above 75 using Predicate + List.FindAll()
            Predicate<Student> isTopPerformer = s => s.Scores.Average() > 75;
            List<Student> topStudents = students.FindAll(isTopPerformer);

            Console.WriteLine("\n--- Top Performers (Average Marks > 75) ---");
            foreach (var student in topStudents)
            {
                Console.WriteLine($"- {student.Name} (Average: {student.Scores.Average():F2})");
            }
        }
    }
}
