using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentService
    {
        private readonly List<Student> _students;

        public StudentService()
        {
            _students = new List<Student>();
            SeedData();
        }

        // Tạo sẵn 5 sinh viên để test
        private void SeedData()
        {
            _students.Add(new Student("SV01", "Nguyen Van A", new DateTime(2003, 1, 15), "Nam", "a.nguyen@email.com", "0123456789", "IT", 8.5, "Đang học"));
            _students.Add(new Student("SV02", "Tran Thi B", new DateTime(2004, 5, 20), "Nu", "b.tran@email.com", "0987654321", "Kinh te", 7.2, "Đang học"));
            _students.Add(new Student("SV03", "Le Van C", new DateTime(2003, 8, 10), "Nam", "c.le@email.com", "0912345678", "IT", 9.1, "Bảo lưu"));
            _students.Add(new Student("SV04", "Pham Thi D", new DateTime(2002, 12, 5), "Nu", "d.pham@email.com", "0909888777", "Ngon ngu Anh", 6.8, "Đang học"));
            _students.Add(new Student("SV05", "Hoang Van E", new DateTime(2003, 3, 25), "Nam", "e.hoang@email.com", "0933444555", "Kinh te", 8.0, "Đã tốt nghiệp"));
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public List<Student> GetAllStudents()
        {
            return _students.ToList();
        }

        // Dùng Nullable Reference Type (Student?) vì có thể không tìm thấy
        public Student? FindById(string studentId)
        {
            return _students.FirstOrDefault(s => s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase));
        }

        public List<Student> FindByName(string name)
        {
            return _students.Where(s => s.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool DeleteStudent(string studentId)
        {
            var student = FindById(studentId);
            if (student != null)
            {
                _students.Remove(student);
                return true;
            }
            return false;
        }

        public List<Student> SortByName()
        {
            return _students.OrderBy(s => s.FullName).ToList();
        }

        public List<Student> SortByGPA()
        {
            return _students.OrderByDescending(s => s.GPA).ToList();
        }

        public List<Student> GetStudentsWithGPAFrom8()
        {
            return _students.Where(s => s.GPA >= 8.0).ToList();
        }

        public List<Student> GetTopStudents()
        {
            if (!_students.Any()) return new List<Student>();
            double maxGpa = _students.Max(s => s.GPA);
            return _students.Where(s => s.GPA == maxGpa).ToList();
        }

        public double GetAverageGPA()
        {
            if (!_students.Any()) return 0;
            return _students.Average(s => s.GPA);
        }

        public Dictionary<string, int> StatisticsByMajor()
        {
            return _students.GroupBy(s => s.Major)
                            .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> StatisticsByStatus()
        {
            return _students.GroupBy(s => s.StudyStatus)
                            .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}