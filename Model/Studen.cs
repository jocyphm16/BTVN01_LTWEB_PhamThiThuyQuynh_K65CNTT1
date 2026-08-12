using System;

namespace StudentManagement.Models
{
    public class Student
    {
        // Sử dụng Encapsulation: Dữ liệu chỉ được gán từ bên trong class này
        public string StudentId { get; private set; }
        public string FullName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Gender { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Major { get; private set; }
        public double GPA { get; private set; }
        public string StudyStatus { get; private set; }

        // Constructor để khởi tạo đối tượng
        public Student(string studentId, string fullName, DateTime dateOfBirth, string gender, 
                       string email, string phoneNumber, string major, double gpa, string studyStatus)
        {
            StudentId = studentId;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Major = major;
            GPA = gpa;
            StudyStatus = studyStatus;
        }

        // Method để cập nhật thông tin (Bảo vệ tính đóng gói)
        public void UpdateInfo(string fullName, DateTime dateOfBirth, string gender, 
                               string email, string phoneNumber, string major, double gpa, string studyStatus)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Major = major;
            GPA = gpa;
            StudyStatus = studyStatus;
        }
    }
}