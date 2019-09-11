using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TransportSchedule;
using TransportSchedule.Authorization;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {

        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_ClickRegister(object sender, RoutedEventArgs e)
        {
            Registration w = new Registration();
            w.Show();
            Close(); 
        }

        private void Button_ClickEnter(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "" || PasswordBox.Password == "")
            {
                MessageBox.Show("FILL THE GAPS", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                TextOutput.Text = "FILL THE GAPS";
            }
            else
            {
                if (WorkinhWithUserMethods.Authorization(LoginBox.Text, PasswordBox.Password) != null)
                {
                    MainWindow w = new MainWindow(WorkinhWithUserMethods.Authorization(LoginBox.Text, PasswordBox.Password));
                    w.Show();
                    Close();
                }
                else
                {
                    TextOutput.Text = "ERROR!";
                    LoginBox.Text = "";
                    PasswordBox.Password = "";
                }
            }
            
        }
    }
}
