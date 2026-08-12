using System;
using System.Text.RegularExpressions;

namespace StudentManagement.Validators
{
    public class StudentValidator
    {
        // Static member vì logic chỉ xử lý trên đầu vào, không cần lưu state
        public static bool IsValidString(string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // Biểu thức chính quy đơn giản kiểm tra email
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidGPA(double gpa)
        {
            return gpa >= 0.0 && gpa <= 10.0;
        }

        public static bool IsValidDateOfBirth(string? input, out DateTime result)
        {
            return DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out result);
        }
    }
}