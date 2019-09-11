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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Register_Cancel(object sender, RoutedEventArgs e)
        {
            Authorization w = new Authorization();
            w.Show();
            Close();
        }

        private void Button_Register_End(object sender, RoutedEventArgs e)
        {
            if (RegisterLogin.Text != "" && RegisterPassword.Password != "" && RegisterEmail.Text != "")
            {
                var temp = WorkinhWithUserMethods.RegistrationNewUser(RegisterLogin.Text, RegisterPassword.Password, RegisterEmail.Text);
                if(temp == true)
                {
                    MainWindow w = new MainWindow(WorkinhWithUserMethods.Authorization(RegisterLogin.Text, RegisterPassword.Password));
                    w.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("This user is already existing", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                TextOutput.Text = "FILL THE GAPS";
            }
        }
    }
}
