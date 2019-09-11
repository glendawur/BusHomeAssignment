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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TransportSchedule;
using TransportSchedule.Authorization;
using TransportSchedule.Repo;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User CurrentUser;

        //public DataRepository<Station> _stations = new StationRepository();
        //DataRepository<Route> _routes = new RouteRepository();
        public DbRepository<Station> _stations = new DbStations();
        DbRepository<Route> _routes = new DbRoutes();

        public MainWindow(User _currentUser)
        {
            CurrentUser = _currentUser;
            InitializeComponent();
            Routes_loaded();
            Stations_loaded();
            SelectStation.SelectionChanged += new SelectionChangedEventHandler(Results_Update);
            SelectRoute.SelectionChanged += new SelectionChangedEventHandler(Results_Update);
        }

        private void Routes_loaded()
        {
            SelectRoute.ItemsSource = null;
            SelectRoute.ItemsSource = _routes.Items;

        }

        private void Stations_loaded()
        {
            SelectStation.ItemsSource = null;
            SelectStation.ItemsSource = _stations.Items;
        }

        private void Results_Update(object sender, SelectionChangedEventArgs e)
        {
            ResultList.ItemsSource = null;
            Station current_station = SelectStation.SelectedItem as Station;
            try
            {
                if (SelectRoute.SelectedItem == null)
                {
                    ResultList.ItemsSource = Program.ProgramResult(current_station);
                }
                else
                {
                    ResultList.ItemsSource = null;
                    ResultList.ItemsSource = Program.Sort(Program.ProgramResult(current_station), SelectRoute.SelectedItem as Route);
                }
            }
            catch
            {
                MessageBox.Show("Please, enter the station so as to get schedule", "NOTICE", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Open_Fav(object sender, RoutedEventArgs e)
        {
            AddFavourites w = new AddFavourites(CurrentUser, _stations.Items);
            if (w.ShowDialog() == true)
            {
                SelectStation.Text = w.SelectedFavouriteStation;
                SelectRoute.Text = null;
            }
            else
            {
                CurrentUser = w.CurrentUser;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
