using ManageStaff.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManageStaff.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewEmployeeWindow.xaml
    /// </summary>
    public partial class AddNewEmployeeWindow : Window
    {
        public AddNewEmployeeWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
