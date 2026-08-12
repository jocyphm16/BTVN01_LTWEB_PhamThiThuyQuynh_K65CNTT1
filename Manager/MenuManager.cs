using System;
using StudentManagement.Models;
using StudentManagement.Services;
using StudentManagement.Views;

namespace StudentManagement.Managers
{
    public class MenuManager
    {
        private readonly StudentService _service;
        private readonly StudentConsoleView _view;

        public MenuManager()
        {
            _service = new StudentService();
            _view = new StudentConsoleView();
        }

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("           QUẢN LÝ SINH VIÊN            ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Hiển thị danh sách");
                Console.WriteLine("3. Tìm sinh viên theo mã");
                Console.WriteLine("4. Tìm sinh viên theo họ tên");
                Console.WriteLine("5. Cập nhật sinh viên");
                Console.WriteLine("6. Xóa sinh viên");
                Console.WriteLine("7. Sắp xếp theo họ tên");
                Console.WriteLine("8. Sắp xếp theo điểm trung bình (Giảm dần)");
                Console.WriteLine("9. Sinh viên có GPA >= 8");
                Console.WriteLine("10. Sinh viên có GPA cao nhất");
                Console.WriteLine("11. Điểm trung bình toàn bộ sinh viên");
                Console.WriteLine("12. Thống kê theo ngành");
                Console.WriteLine("13. Thống kê theo trạng thái");
                Console.WriteLine("0. Thoát");
                Console.WriteLine("========================================");
                Console.Write("Nhập lựa chọn của bạn: ");

                string? choice = Console.ReadLine();

                Console.WriteLine(); // Xuống dòng cho đẹp
                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": ShowAllStudents(); break;
                    case "3": FindById(); break;
                    case "4": FindByName(); break;
                    case "5": UpdateStudent(); break;
                    case "6": DeleteStudent(); break;
                    case "7": SortByName(); break;
                    case "8": SortByGPA(); break;
                    case "9": GetGpaFrom8(); break;
                    case "10": TopGpa(); break;
                    case "11": AverageGpa(); break;
                    case "12": StatByMajor(); break;
                    case "13": StatByStatus(); break;
                    case "0":
                        isRunning = false;
                        _view.ShowMessage("Đã thoát chương trình. Tạm biệt!", ConsoleColor.Green);
                        break;
                    default:
                        _view.ShowMessage("Lựa chọn không hợp lệ, vui lòng nhập lại!", ConsoleColor.Red);
                        break;
                }

                if (isRunning)
                {
                    Console.WriteLine("\nNhấn phím Enter bất kỳ để tiếp tục...");
                    Console.ReadLine();
                }
            }
        }

        private void AddStudent()
        {
            _view.ShowMessage("--- THÊM SINH VIÊN MỚI ---", ConsoleColor.Cyan);
            
            string id;
            while (true)
            {
                id = _view.InputString("Nhập mã SV: ");
                if (_service.FindById(id) != null)
                {
                    _view.ShowMessage("Lỗi: Mã sinh viên đã tồn tại!", ConsoleColor.Red);
                }
                else break;
            }

            var newStudent = ReadStudentData(id);
            _service.AddStudent(newStudent);
            _view.ShowMessage("Thêm sinh viên thành công!", ConsoleColor.Green);
        }

        private void ShowAllStudents()
        {
            _view.ShowMessage("--- DANH SÁCH SINH VIÊN ---", ConsoleColor.Cyan);
            var list = _service.GetAllStudents();
            _view.DisplayStudentList(list);
        }

        private void FindById()
        {
            string id = _view.InputString("Nhập mã SV cần tìm: ");
            Student? student = _service.FindById(id);

            if (student != null)
            {
                _view.DisplayStudent(student);
            }
            else
            {
                _view.ShowMessage("Không tìm thấy sinh viên!", ConsoleColor.Yellow);
            }
        }

        private void FindByName()
        {
            string name = _view.InputString("Nhập tên SV cần tìm: ");
            var results = _service.FindByName(name);
            _view.DisplayStudentList(results);
        }

        private void UpdateStudent()
        {
            _view.ShowMessage("--- CẬP NHẬT SINH VIÊN ---", ConsoleColor.Cyan);
            string id = _view.InputString("Nhập mã SV cần cập nhật: ");
            Student? student = _service.FindById(id);

            if (student == null)
            {
                _view.ShowMessage("Không tìm thấy sinh viên để cập nhật!", ConsoleColor.Red);
                return;
            }

            _view.DisplayStudent(student);
            _view.ShowMessage("\nNhập thông tin mới:", ConsoleColor.Cyan);
            
            // Tái sử dụng hàm đọc dữ liệu, nhưng bỏ qua nhập ID
            string name = _view.InputString("Họ tên: ");
            DateTime dob = _view.InputDate("Ngày sinh (dd/MM/yyyy): ");
            string gender = _view.InputString("Giới tính: ");
            string email = _view.InputEmail("Email: ");
            string phone = _view.InputString("Số điện thoại: ");
            string major = _view.InputString("Ngành học: ");
            double gpa = _view.InputGPA("Điểm trung bình (0-10): ");
            string status = _view.InputString("Trạng thái học tập: ");

            // Gọi phương thức update để giữ đúng Encapsulation
            student.UpdateInfo(name, dob, gender, email, phone, major, gpa, status);
            _view.ShowMessage("Cập nhật thành công!", ConsoleColor.Green);
        }

        private void DeleteStudent()
        {
            string id = _view.InputString("Nhập mã SV cần xóa: ");
            if (_service.DeleteStudent(id))
            {
                _view.ShowMessage("Xóa thành công!", ConsoleColor.Green);
            }
            else
            {
                _view.ShowMessage("Không tìm thấy sinh viên để xóa!", ConsoleColor.Red);
            }
        }

        private void SortByName()
        {
            _view.ShowMessage("--- SẮP XẾP THEO TÊN ---", ConsoleColor.Cyan);
            _view.DisplayStudentList(_service.SortByName());
        }

        private void SortByGPA()
        {
            _view.ShowMessage("--- SẮP XẾP THEO GPA (Giảm dần) ---", ConsoleColor.Cyan);
            _view.DisplayStudentList(_service.SortByGPA());
        }

        private void GetGpaFrom8()
        {
            _view.ShowMessage("--- DANH SÁCH SINH VIÊN GPA >= 8 ---", ConsoleColor.Cyan);
            _view.DisplayStudentList(_service.GetStudentsWithGPAFrom8());
        }

        private void TopGpa()
        {
            _view.ShowMessage("--- SINH VIÊN CÓ GPA CAO NHẤT ---", ConsoleColor.Cyan);
            _view.DisplayStudentList(_service.GetTopStudents());
        }

        private void AverageGpa()
        {
            double avg = _service.GetAverageGPA();
            _view.ShowMessage($"Điểm trung bình toàn bộ sinh viên: {Math.Round(avg, 2)}", ConsoleColor.Magenta);
        }

        private void StatByMajor()
        {
            _view.ShowMessage("--- THỐNG KÊ THEO NGÀNH HỌC ---", ConsoleColor.Cyan);
            var stat = _service.StatisticsByMajor();
            foreach (var item in stat)
            {
                Console.WriteLine($"- Ngành {item.Key}: {item.Value} sinh viên");
            }
        }

        private void StatByStatus()
        {
            _view.ShowMessage("--- THỐNG KÊ THEO TRẠNG THÁI ---", ConsoleColor.Cyan);
            var stat = _service.StatisticsByStatus();
            foreach (var item in stat)
            {
                Console.WriteLine($"- {item.Key}: {item.Value} sinh viên");
            }
        }

        // Hàm tiện ích nội bộ để tái sử dụng code khi Thêm Sinh Viên
        private Student ReadStudentData(string id)
        {
            string name = _view.InputString("Họ tên: ");
            DateTime dob = _view.InputDate("Ngày sinh (dd/MM/yyyy): ");
            string gender = _view.InputString("Giới tính: ");
            string email = _view.InputEmail("Email: ");
            string phone = _view.InputString("Số điện thoại: ");
            string major = _view.InputString("Ngành học: ");
            double gpa = _view.InputGPA("Điểm trung bình (0-10): ");
            string status = _view.InputString("Trạng thái học tập: ");

            return new Student(id, name, dob, gender, email, phone, major, gpa, status);
        }
    }
}