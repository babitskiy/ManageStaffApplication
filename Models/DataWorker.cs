using ManageStaff.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageStaff.Models
{
    public static class DataWorker
    {
        // получить все отделы
        public static List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }

        // получить все позиции
        public static List<Position> GetAllPositions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }

        // получить всех сотрудников
        public static List<Employee> GetAllEmployees()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Employees.ToList();
                return result;
            }
        }

        // создать отдел
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем существует ли такой отдел
                bool checkIsExist = db.Departments.Any(e => e.Name == name);
                if (!checkIsExist)
                {
                    Department newDepartment = new Department { Name = name };
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    result = "Сделано";
                }
                return result;
            }
        }

        // создать позицию
        public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем существует ли такая позиция с такой зарплатой
                bool checkIsExist = db.Positions.Any(e => e.Name == name && e.Salary == salary);
                if (!checkIsExist)
                {
                    Position newPosition = new Position
                    {
                        Name = name,
                        Salary = salary,
                        MaxNumber = maxNumber,
                        DepartmentId = department.Id
                    };
                    db.Positions.Add(newPosition);
                    db.SaveChanges();
                    result = "Сделано";
                }
                return result;
            }
        }

        // создать сотрудника
        public static string CreateEmployee(string name, string surName, string phone, Position position)
        {
            string result = "Уже существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                // проверяем существует ли такой сотрудник
                bool checkIsExist = db.Employees.Any(e => e.Name == name && e.SurName == surName && e.Position == position);
                if (!checkIsExist)
                {
                    Employee newEmployee = new Employee
                    {
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Employees.Add(newEmployee);
                    db.SaveChanges();
                    result = "Сделано";
                }
                return result;
            }
        }

        // удалить отдел
        public static string DeleteDepartment(Department department)
        {
            string result = "Такого отдела не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = "Отдел " + department.Name + " успешно удалён!";
            }
            return result;
        }

        // удалить позицию
        public static string DeletePosition(Position position)
        {
            string result = "Такой позиции не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = "Позиция " + position.Name + " успешно удалена!";
            }
            return result;
        }

        // удалить сотрудника
        public static string DeleteEmployee(Employee employee)
        {
            string result = "Такого сотрудника не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                result = "Сотрудник " + employee.Name + " успешно удалён!";
            }
            return result;
        }

        // изменить отдел
        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Такого отдела не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                db.SaveChanges();
                result = "Отдел " + department.Name + " успешно изменён!";
            }
            return result;
        }

        // изменить позицию
        public static string EditPosition(Position oldPosition, string newName, int newMaxNumber, decimal newSalary, Department newDepartment)
        {
            string result = "Такой позиции не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxNumber = newMaxNumber;
                position.DepartmentId = newDepartment.Id; 
                db.SaveChanges();
                result = "Позиция " + position.Name + " успешно изменена!";
            }
            return result;
        }

        // изменить сотрудника
        public static string EditEmployee(Employee oldEmployee, string newName, string newSurName, string newPhone, Position newPosition)
        {
            string result = "Такого сотрудника не существует!";
            using (ApplicationContext db = new ApplicationContext())
            {
                Employee employee = db.Employees.FirstOrDefault();
                db.SaveChanges();
                result = "Сотрудник " + employee.Name + " успешно удалён!";
            }
            return result;
        }
    }
}
