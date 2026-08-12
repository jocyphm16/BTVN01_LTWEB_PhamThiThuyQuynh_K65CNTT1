// See https://aka.ms/new-console-template for more information
using StudentManagement.Managers;

namespace StudentManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Console.InputEncoding = System.Text.Encoding.UTF8;

            // Khởi chạy trình quản lý Menu
            MenuManager menu = new MenuManager();
            menu.Run();
        }
    }
}
