using System;

namespace StudentResultsSystem
{
    class Program
    {
        // Course names
        static string[] courseNames = {
            "Programming with C#",
            "Database Systems",
            "Computer Networks",
            "Web Development",
            "Mathematics for Computing"
        };

        // Student data arrays (supports up to 3 students as required)
        static string[] studentNames = new string[3];
        static string[] studentIDs = new string[3];
        static string[] studentPrograms = new string[3];
        static string[] studentLevels = new string[3];
        static double[,] scores = new double[3, 5];

        static bool dataEntered = false;

        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine();
                Console.WriteLine("===== STUDENT RESULTS PROCESSING SYSTEM =====");
                Console.WriteLine("1. Enter Student Results");
                Console.WriteLine("2. View Student Report");
                Console.WriteLine("3. Exit");
                Console.Write("\nChoose an option: ");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.Write("Invalid option. Please enter 1, 2, or 3: ");
                }

                switch (choice)
                {
                    case 1:
                        EnterStudentResults();
                        break;
                    case 2:
                        ViewStudentReport();
                        break;
                    case 3:
                        Console.WriteLine("\nThank you for using the Student Results Processing System.");
                        break;
                }

            } while (choice != 3);
        }

        static void EnterStudentResults()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"\n--- Enter details for Student {i + 1} ---");

                Console.Write("Enter full name: ");
                studentNames[i] = Console.ReadLine();

                Console.Write("Enter student ID: ");
                studentIDs[i] = Console.ReadLine();

                Console.Write("Enter programme: ");
                studentPrograms[i] = Console.ReadLine();

                Console.Write("Enter level: ");
                studentLevels[i] = Console.ReadLine();

                Console.WriteLine();

                for (int j = 0; j < 5; j++)
                {
                    double score;
                    bool valid = false;

                    do
                    {
                        Console.Write($"Enter score for {courseNames[j]}: ");
                        if (double.TryParse(Console.ReadLine(), out score) && score >= 0 && score <= 100)
                        {
                            valid = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid score. Score must be between 0 and 100.");
                        }
                    } while (!valid);

                    scores[i, j] = score;
                }
            }

            dataEntered = true;
            Console.WriteLine("\nStudent results entered successfully!");
        }

        static void ViewStudentReport()
        {
            if (!dataEntered)
            {
                Console.WriteLine("\nNo student data found. Please enter student results first (Option 1).");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("===== STUDENT RESULTS REPORT =====");

            double classTotal = 0;
            int bestStudentIndex = 0;
            int lowestStudentIndex = 0;
            double bestAvg = -1;
            double lowestAvg = 101;

            for (int i = 0; i < 3; i++)
            {
                double total = 0;
                for (int j = 0; j < 5; j++)
                    total += scores[i, j];

                double average = total / 5.0;
                string grade = GetGrade(average);
                string status = average >= 50 ? "Passed" : "Failed";

                classTotal += average;

                if (average > bestAvg) { bestAvg = average; bestStudentIndex = i; }
                if (average < lowestAvg) { lowestAvg = average; lowestStudentIndex = i; }

                Console.WriteLine();
                Console.WriteLine($"Student Name : {studentNames[i]}");
                Console.WriteLine($"Student ID   : {studentIDs[i]}");
                Console.WriteLine($"Programme    : {studentPrograms[i]}");
                Console.WriteLine($"Level        : {studentLevels[i]}");
                Console.WriteLine();

                for (int j = 0; j < 5; j++)
                    Console.WriteLine($"  {courseNames[j],-30}: {scores[i, j]}");

                Console.WriteLine();
                Console.WriteLine($"  Total Score  : {total}");
                Console.WriteLine($"  Average Score: {average:F1}");
                Console.WriteLine($"  Grade        : {grade}");
                Console.WriteLine($"  Status       : {status}");
                Console.WriteLine(new string('-', 45));
            }

            // Bonus: Class summary
            double classAverage = classTotal / 3.0;
            Console.WriteLine();
            Console.WriteLine($"Class Average          : {classAverage:F1}");
            Console.WriteLine($"Best Student           : {studentNames[bestStudentIndex]} (Avg: {bestAvg:F1})");
            Console.WriteLine($"Student with Lowest Avg: {studentNames[lowestStudentIndex]} (Avg: {lowestAvg:F1})");
            Console.WriteLine();
        }

        static string GetGrade(double average)
        {
            if (average >= 80) return "A";
            if (average >= 70) return "B";
            if (average >= 60) return "C";
            if (average >= 50) return "D";
            return "F";
        }
    }
}