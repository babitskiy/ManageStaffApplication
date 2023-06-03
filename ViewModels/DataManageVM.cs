using ManageStaff.Models;
using ManageStaff.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManageStaff.ViewModels
{
    internal class DataManageVM : INotifyPropertyChanged
    {
        // все отделы
        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            }
        }

        // все позиции
        private List<Position> allPositions = DataWorker.GetAllPositions();
        public List<Position> AllPositions
        {
            get { return allPositions; }
            set
            {
                allPositions = value;
                NotifyPropertyChanged("AllPositions");
            }
        }

        // все сотрудники
        private List<Employee> allEmployees = DataWorker.GetAllEmployees();
        public List<Employee> AllEmployees
        {
            get { return allEmployees; }
            set
            {
                allEmployees = value;
                NotifyPropertyChanged("AllEmployees");
            }
        }

        // свойства для отдела
        public static string DepartmentName { get; set; }

        // свойства для позиции
        public static string PositionName { get; set; }
        public static decimal PositionSalary { get; set; }
        public static int PositionMaxNumber { get; set; }
        public Department PositionDepartment { get; set; }

        // свойства для сотрудника
        public static string EmployeeName { get; set; }
        public static string EmployeeSurName { get; set; }
        public static string EmployeePhone { get; set; }
        public Position EmployeePosition { get; set; }

        // свойства для выделенных элементов
        public TabItem SelectedTabItem { get; set; }
        public static Employee SelectedEmployee { get; set; }
        public static Position SelectedPosition { get; set; }
        public static Department SelectedDepartment { get; set; }

        #region COMMANDS TO ADD
        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommand addNewPosition;
        public RelayCommand AddNewPosition
        {
            get
            {
                return addNewPosition ?? new RelayCommand(obj => {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (PositionSalary == 0)
                    {
                        SetRedBlockControl(wnd, "SalaryBlock");
                    }
                    if (PositionMaxNumber == 0)
                    {
                        SetRedBlockControl(wnd, "MaxNumberBlock");
                    }
                    if (PositionDepartment == null)
                    {
                        MessageBox.Show("Укажите отдел");
                    }
                    else
                    {
                        resultStr = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionDepartment);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        private RelayCommand addNewEmployee;
        public RelayCommand AddNewEmployee
        {
            get
            {
                return addNewEmployee ?? new RelayCommand(obj => {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (EmployeeName == null || EmployeeName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (EmployeeSurName == null || EmployeeSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "SurNameBlock");
                    }
                    if (EmployeePosition == null)
                    {
                        MessageBox.Show("Укажите позицию");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateEmployee(EmployeeName, EmployeeSurName, EmployeePhone, EmployeePosition);
                        UpdateAllDataView();

                        ShowMessageToUser(resultStr);
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        #endregion

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    // если сотрудник
                    if (SelectedTabItem.Name == "EmployeesTab" && SelectedEmployee != null)
                    {
                        resultStr = DataWorker.DeleteEmployee(SelectedEmployee);
                        UpdateAllDataView();
                    }

                    // если позиция
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        UpdateAllDataView();
                    }

                    // если отдел
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }

                    // обновление
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                });
            }
        }

        #region EDIT COMMANDS
        private RelayCommand editEmployee;
        public RelayCommand EditEmployee
        {
            get
            {
                return editEmployee ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    string noPositionStr = "Не выбрана новая должность";
                    if (SelectedEmployee != null)
                    {
                        if (EmployeePosition != null)
                        {
                            resultStr = DataWorker.EditEmployee(SelectedEmployee, EmployeeName, EmployeeSurName, EmployeePhone, EmployeePosition);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noPositionStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        private RelayCommand editPosition;
        public RelayCommand EditPosition
        {
            get
            {
                return editPosition ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана должность";
                    string noDepartmentStr = "Не выбран новый отдел";

                    if (SelectedPosition != null)
                    {
                        if (PositionDepartment != null)
                        {
                            resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionMaxNumber, PositionSalary, PositionDepartment);

                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noDepartmentStr);
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран отдел";

                    if (SelectedDepartment != null)
                    {
                        resultStr = DataWorker.EditDepartment(SelectedDepartment, DepartmentName);

                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);
                });
            }
        }
        #endregion

        #region COMMANDS TO OPEN WINDOWS
        private RelayCommand openAddNewDepartmentWnd;
        public RelayCommand OpenAddNewDepartmentWnd
        {
            get
            {
                return openAddNewDepartmentWnd ?? new RelayCommand(obj =>
                    {
                        OpenAddDepartmentWindowMethod();
                    }
                    );
            }
        }

        private RelayCommand openAddNewPositionWnd;
        public RelayCommand OpenAddNewPositionWnd
        {
            get
            {
                return openAddNewPositionWnd ?? new RelayCommand(obj =>
                {
                    OpenAddPositionWindowMethod();
                }
                    );
            }
        }

        private RelayCommand openAddNewEmployeeWnd;
        public RelayCommand OpenAddNewEmployeeWnd
        {
            get
            {
                return openAddNewEmployeeWnd ?? new RelayCommand(obj =>
                {
                    OpenAddEmployeeWindowMethod();
                }
                    );
            }
        }

        private RelayCommand editItem;
        public RelayCommand EditItem
        {
            get
            {
                return editItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    // если сотрудник
                    if (SelectedTabItem.Name == "EmployeesTab" && SelectedEmployee != null)
                    {
                        OpenEditEmployeeWindowMethod(SelectedEmployee);
                    }

                    // если позиция
                    if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                    {
                        OpenEditPositionWindowMethod(SelectedPosition);
                    }

                    // если отдел
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMethod(SelectedDepartment);
                    }
                });
            }
        }
        #endregion

        #region METHODS TO OPEN WINDOWS
        // методы открытия окон
        private void OpenAddDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpen(newDepartmentWindow);
        }
        private void OpenAddPositionWindowMethod()
        {
            AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
            SetCenterPositionAndOpen(newPositionWindow);
        }
        private void OpenAddEmployeeWindowMethod()
        {
            AddNewEmployeeWindow newEmployeeWindow = new AddNewEmployeeWindow();
            SetCenterPositionAndOpen(newEmployeeWindow);
        }

        // окна редактирования
        private void OpenEditDepartmentWindowMethod(Department department)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }
        private void OpenEditPositionWindowMethod(Position position)
        {
            EditPositionWindow editPositionWindow = new EditPositionWindow(position);
            SetCenterPositionAndOpen(editPositionWindow);
        }
        private void OpenEditEmployeeWindowMethod(Employee employee)
        {
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(employee);
            SetCenterPositionAndOpen(editEmployeeWindow);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        private void SetRedBlockControl(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        #region UPDATE VIEWS
        private void SetNullValuesToProperties()
        {
            // для сотрудника
            EmployeeName = null;
            EmployeeSurName = null;
            EmployeePhone = null;
            EmployeePosition = null;

            // для позиции
            PositionName = null;
            PositionSalary = 0;
            PositionMaxNumber = 0;
            PositionDepartment = null;

            // для отдела
            DepartmentName = null;
        }
        private void UpdateAllDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllPositionsView();
            UpdateAllEmployeesView();
        }
        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }
        private void UpdateAllPositionsView()
        {
            AllPositions = DataWorker.GetAllPositions();
            MainWindow.AllPositionsView.ItemsSource = null;
            MainWindow.AllPositionsView.Items.Clear();
            MainWindow.AllPositionsView.ItemsSource = AllPositions;
            MainWindow.AllPositionsView.Items.Refresh();
        }
        private void UpdateAllEmployeesView()
        {
            AllEmployees = DataWorker.GetAllEmployees();
            MainWindow.AllEmployeesView.ItemsSource = null;
            MainWindow.AllEmployeesView.Items.Clear();
            MainWindow.AllEmployeesView.ItemsSource = AllEmployees;
            MainWindow.AllEmployeesView.Items.Refresh();
        }
        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
    }
}
