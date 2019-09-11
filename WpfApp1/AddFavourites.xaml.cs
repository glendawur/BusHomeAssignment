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
using TransportSchedule.Repo;
using TransportSchedule.Schedule.Modules;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddFavourites.xaml
    /// </summary>
    ///    //ADD, REMOVE AND CHOOSE BUTTONS
    public partial class AddFavourites : Window
    {
        public User CurrentUser;
        public List<Station> Stations;
        public DbUsers Users = new DbUsers();
        public string SelectedFavouriteStation { get; set; } 

        public AddFavourites(User _currentUser, List<Station> _stations)
        {
            CurrentUser = _currentUser;
            Stations = _stations;
            InitializeComponent();
            LoadAllStations();
            LoadAllFavourites();
            Favourites.SelectionChanged += new SelectionChangedEventHandler(UpdateComment);

        }

        private void LoadAllStations()
        {
            var w = new MainWindow(CurrentUser);
            AllStations.ItemsSource = null;
            AllStations.ItemsSource = w._stations.Items.Select(x => x.Name).ToList(); 
            
        }

        private void LoadAllFavourites()
        {
            var w = new MainWindow(CurrentUser);
            Favourites.ItemsSource = null;
            Favourites.ItemsSource = CurrentUser.FavouriteStations.Select(x => x.Station.Name).ToList();
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            var tempStation = Stations.FirstOrDefault(st => st.Name == AllStations.SelectedItem as string);
            if(!CurrentUser.FavouriteStations.Contains(CurrentUser.FavouriteStations.SingleOrDefault(x => x.Station.Name == tempStation.Name)))
            {
                //CurrentUser = Users.AddFavourite(CurrentUser, tempStation);
                //Users.Update();
                CurrentUser = CurrentUser.AddFavourite(tempStation);
            }
            Favourites.ItemsSource = null;
            Favourites.ItemsSource = CurrentUser.FavouriteStations.Select(x => x.Station.Name).ToList();

        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            var tempStation = Stations.FirstOrDefault(st => st.Name == Favourites.SelectedItem as string);
            if (CurrentUser.FavouriteStations.Contains(CurrentUser.FavouriteStations.SingleOrDefault(x => x.Station.Name == tempStation.Name)))
            {
                //CurrentUser = Users.RemoveFavourite(CurrentUser, tempStation);
                //Users.Update();
                CurrentUser = CurrentUser.RemoveFavourite(tempStation);
            }
            Favourites.SelectedItem = null;
            Favourites.ItemsSource = null;
            Favourites.ItemsSource = CurrentUser.FavouriteStations.Select(x => x.Station.Name).ToList();
            Comment.Text = "";
        }

        private void Button_Choose(object sender, RoutedEventArgs e)
        {
            SelectedFavouriteStation = Favourites.SelectedItem as string;
            if(SelectedFavouriteStation != null)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Select a station from the Favourites", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void UpdateComment(object sender, RoutedEventArgs e)
        {
            Comment.Text = "";
            var temp_st = CurrentUser.FavouriteStations
                .FirstOrDefault(st => st.Station.Name == Favourites.SelectedItem as string);
            if (temp_st != null)
            {
                if (temp_st.Comment != null)
                    Comment.Text = temp_st.Comment;
            }
        }


        private void Button_SaveComment(object sender, RoutedEventArgs e)
        {
            var tempStation = CurrentUser.FavouriteStations.FirstOrDefault(st => st.Station.Name == Favourites.SelectedItem as string);
            if (CurrentUser.FavouriteStations.Contains(tempStation) && tempStation != null)
            {
                //CurrentUser = Users.EditComment(CurrentUser, tempStation, Comment.Text);
                //Users.Update();
                CurrentUser = CurrentUser.EditComment(tempStation, Comment.Text);
            }

            Favourites.ItemsSource = null;
            Favourites.ItemsSource = CurrentUser.FavouriteStations.Select(x => x.Station.Name).ToList();
        }
    }
}
