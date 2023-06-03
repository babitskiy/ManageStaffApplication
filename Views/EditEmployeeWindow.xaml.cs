using ManageStaff.Models;
using ManageStaff.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageStaff.Views
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        public EditEmployeeWindow(Employee employeeToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            DataManageVM.SelectedEmployee = employeeToEdit;
            DataManageVM.EmployeeName = employeeToEdit.Name;
            DataManageVM.EmployeeSurName = employeeToEdit.SurName;
            DataManageVM.EmployeePhone = employeeToEdit.Phone;
        }
        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
