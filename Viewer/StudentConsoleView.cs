using System;
using System.Collections.Generic;
using StudentManagement.Models;
using StudentManagement.Validators;

namespace StudentManagement.Views
{
    public class StudentConsoleView
    {
        public void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // Hiển thị danh sách dạng bảng
        public void DisplayStudentList(List<Student> students)
        {
            if (students == null || students.Count == 0)
            {
                ShowMessage("Danh sách trống!", ConsoleColor.Yellow);
                return;
            }

            Console.WriteLine(new string('-', 110));
            Console.WriteLine($"{"Mã SV",-10} | {"Họ tên",-20} | {"Ngày sinh",-12} | {"Giới tính",-10} | {"Email",-22} | {"SĐT",-12} | {"Ngành",-15} | {"GPA",-4} | {"Trạng thái"}");
            Console.WriteLine(new string('-', 110));

            foreach (var s in students)
            {
                Console.WriteLine($"{s.StudentId,-10} | {s.FullName,-20} | {s.DateOfBirth.ToString("dd/MM/yyyy"),-12} | {s.Gender,-10} | {s.Email,-22} | {s.PhoneNumber,-12} | {s.Major,-15} | {s.GPA,-4} | {s.StudyStatus}");
            }
            Console.WriteLine(new string('-', 110));
        }

        public void DisplayStudent(Student student)
        {
            DisplayStudentList(new List<Student> { student });
        }

        // Nhập chuỗi có validate không rỗng
        public string InputString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (StudentValidator.IsValidString(input))
                {
                    return input!;
                }
                ShowMessage("Lỗi: Dữ liệu không được để trống!", ConsoleColor.Red);
            }
        }

        // Nhập ngày sinh
        public DateTime InputDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (StudentValidator.IsValidDateOfBirth(input, out DateTime date))
                {
                    return date;
                }
                ShowMessage("Lỗi: Ngày sinh phải đúng định dạng dd/MM/yyyy!", ConsoleColor.Red);
            }
        }

        // Nhập Email
        public string InputEmail(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (StudentValidator.IsValidEmail(input))
                {
                    return input!;
                }
                ShowMessage("Lỗi: Email không đúng định dạng!", ConsoleColor.Red);
            }
        }

        // Nhập GPA
        public double InputGPA(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (double.TryParse(input, out double gpa) && StudentValidator.IsValidGPA(gpa))
                {
                    return gpa;
                }
                ShowMessage("Lỗi: Điểm trung bình phải là số từ 0 đến 10!", ConsoleColor.Red);
            }
        }
    }
}