using System;

public class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public double Grade { get; set; }

    public void DisplayDetails()
    {
        Console.WriteLine($"ID: {ID}, Name: {Name}, Age: {Age}, Grade: {Grade} - {(IsPassing() ? "Passing" : "Failing")}");
    }

    public bool IsPassing()
    {
        return Grade >= 60;
    }

    public static string CapitalizeWords(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        string[] words = input.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
        }
        return string.Join(" ", words);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Student[] students = new Student[50];
        int studentCount = 0;
        string choice;

        do
        {
            Console.WriteLine("Welcome to the Student Management System");
            Console.WriteLine("1. Add a New Student");
            Console.WriteLine("2. View All Students");
            Console.WriteLine("3. Search for a Student by ID");
            Console.WriteLine("4. Remove a Student by ID");
            Console.WriteLine("5. Update a Student's Grade");
            Console.WriteLine("6. Sort Students by Grade");
            Console.WriteLine("7. Display Average Grade");
            Console.WriteLine("8. Count Passing Students");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent(students, ref studentCount);
                    break;
                case "2":
                    ViewAllStudents(students, studentCount);
                    break;
                case "3":
                    SearchStudentById(students, studentCount);
                    break;
                case "4":
                    RemoveStudentById(students, ref studentCount);
                    break;
                case "5":
                    UpdateStudentGrade(students, studentCount);
                    break;
                case "6":
                    SortStudentsByGrade(students, studentCount);
                    break;
                case "7":
                    DisplayAverageGrade(students, studentCount);
                    break;
                case "8":
                    CountPassingStudents(students, studentCount);
                    break;
                case "9":
                    Console.WriteLine("Exiting the application.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        } while (choice != "9");
    }

 static void AddStudent(Student[] students, ref int studentCount)
{
    if (studentCount >= students.Length)
    {
        Console.WriteLine("Cannot add more students. The array is full.");
        return;
    }

    int id;
    while (true)
    {
        Console.Write("Enter Student ID: ");
        if (int.TryParse(Console.ReadLine(), out id))
        {
            // Check if the ID already exists
            bool idExists = false;
            for (int i = 0; i < studentCount; i++)
            {
                
                if (students[i].ID == id)
                {
                    idExists = true;
                    break;
                }
            }

            if (idExists)
            {
                Console.WriteLine("This ID already exists. Please enter a different ID.");
            }
            else
            {
                break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for the Student ID.");
        }
    }

    string name;
    while (true)
    {
        Console.Write("Enter Student Name: ");
        name = Console.ReadLine();
        if (IsAlphabetic(name))
        {
            name = Student.CapitalizeWords(name);
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a name with alphabetic characters only.");
        }
    }

    int age;
    while (true)
    {
        Console.Write("Enter Student Age: ");
        if (int.TryParse(Console.ReadLine(), out age))
        {
            if (age >= 0 && age <= 150)
            {
                break;
            }
             else
             {
                Console.WriteLine("Invalid input. Please enter an age between 0 and 150.");
             }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for the Student Age.");
        }
    }

    double grade;
    while (true)
    {
        Console.Write("Enter Student Grade: ");
        if (double.TryParse(Console.ReadLine(), out grade))
        {
            if (grade >= 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Grade cannot be negative.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the Student Grade.");
        }
    }

    students[studentCount] = new Student { ID = id, Name = name, Age = age, Grade = grade };
    studentCount++;
    Console.WriteLine("Student added successfully!");
}


static bool IsAlphabetic(string input)
{
    foreach (char c in input)
    {
        if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
        {
            return false;
        }
    }
    return true;
}


    static void ViewAllStudents(Student[] students, int studentCount)
    {
        for (int i = 0; i < studentCount; i++)
        {
            students[i].DisplayDetails();
        }
    }

    static void SearchStudentById(Student[] students, int studentCount)
    {
        Console.Write("Enter Student ID to search: ");
        int id = int.Parse(Console.ReadLine());

        for (int i = 0; i < studentCount; i++)
        {
            if (students[i].ID == id)
            {
                students[i].DisplayDetails();
                return;
            }
        }
        Console.WriteLine("Student not found.");
    }

    static void RemoveStudentById(Student[] students, ref int studentCount)
    {
        Console.Write("Enter Student ID to remove: ");
        int id = int.Parse(Console.ReadLine());

        for (int i = 0; i < studentCount; i++)
        {
            if (students[i].ID == id)
            {
                for (int j = i; j < studentCount - 1; j++)
                {
                    students[j] = students[j + 1];
                }
                studentCount--;
                Console.WriteLine("Student removed successfully!");
                return;
            }
        }
        Console.WriteLine("Student not found.");
    }

   static void UpdateStudentGrade(Student[] students, int studentCount)
{
    Console.Write("Enter Student ID to update grade: ");
    int id = int.Parse(Console.ReadLine());

    for (int i = 0; i < studentCount; i++)
    {
        if (students[i].ID == id)
        {
            double grade;
            while (true)
            {
                Console.Write("Enter new grade: ");
                if (double.TryParse(Console.ReadLine(), out grade))
                {
                    if (grade >= 0)
                    {
                        students[i].Grade = grade;
                        Console.WriteLine("Grade updated successfully!");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Grade cannot be negative.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for the grade.");
                }
            }
        }
    }
    Console.WriteLine("Student not found.");
}

   static void SortStudentsByGrade(Student[] students, int studentCount)
{
    for (int i = 0; i < studentCount - 1; i++)
    {
        for (int j = i + 1; j < studentCount; j++)
        {
            if (students[j].Grade > students[i].Grade)
            {
                Student temp = students[i];
                students[i] = students[j];
                students[j] = temp;
            }
        }
    }
    Console.WriteLine("Students sorted by grade.");
        ViewAllStudents(students, studentCount);
}


    static void DisplayAverageGrade(Student[] students, int studentCount)
    {
        double total = 0;
        for (int i = 0; i < studentCount; i++)
        {
            total += students[i].Grade;
        }
        Console.WriteLine($"Average Grade: {total / studentCount}");
    }

    static void CountPassingStudents(Student[] students, int studentCount)
    {
        int count = 0;
        for (int i = 0; i < studentCount; i++)
        {
            if (students[i].IsPassing())
            {
                count++;
            }
        }
        Console.WriteLine($"Number of Passing Students: {count}");
    }
}


