using Microsoft.EntityFrameworkCore;
using PlayerClassLibrary.Models;
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

namespace Question2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BaseballDBContext dbContext = new BaseballDBContext();
        System.Windows.Data.CollectionViewSource playersViewSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dbContext.Players.Load();
            playersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("playersViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            playersViewSource.Source = dbContext.Players.Local.ToObservableCollection();
        }

        // filter by last name
        // using Linq
        //private void SearchButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var lName = SearchTermTextBox.Text.Trim();
        //    var result = from p in dbContext.Players.Local
        //                 where p.LastName.Contains(lName)
        //                 select p;
        //    playersViewSource.Source = result; // bad idea!
        //}

        // using delegate
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            playersViewSource.Filter += playersViewSource_Filter;
        }

        void playersViewSource_Filter(object sender, FilterEventArgs e)
        {
            var lName = SearchTermTextBox.Text.Trim();
            Players p = e.Item as Players;

            if (p.LastName.Contains(lName))
                e.Accepted = true;
            else
                e.Accepted = false;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Players newPlayer = new Players()
            {
                FirstName = firstNameTextBox.Text.ToString(),
                LastName = lastNameTextBox.Text.ToString(),
                BattingAverage = decimal.Parse(battingAverageTextBox.Text.ToString())
            };
            firstNameTextBox.Clear();
            lastNameTextBox.Clear();
            battingAverageTextBox.Clear();
            dbContext.Players.Add(newPlayer);
            dbContext.SaveChanges();
        }

        private void PlayersDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            dbContext.SaveChanges();
        }

        private void AllButton_Click(object sender, RoutedEventArgs e)
        {
            playersViewSource.Filter += playersViewSource_All;
            SearchTermTextBox.Clear();
        }
        void playersViewSource_All(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
        }
    }
}
