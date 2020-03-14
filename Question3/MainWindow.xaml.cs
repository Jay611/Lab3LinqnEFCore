using BookClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Question3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BooksContext dbContext = new BooksContext();
        System.Windows.Data.CollectionViewSource titlesViewSource;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dbContext.Titles.Load();
            dbContext.Authors.Load();
            dbContext.AuthorIsbn.Load();
            titlesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("titlesViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            titlesViewSource.Source = dbContext.Titles.Local.ToObservableCollection();
        }

        private void SortByTitleButton_Click(object sender, RoutedEventArgs e)
        {
            var title = from t in dbContext.Titles.Local
                        select new
                        {
                            Title = t.Title,
                            ISBN = t.Isbn
                        };
            var authorIsbn = from ai in dbContext.AuthorIsbn.Local
                             select new
                             {
                                 ISBN = ai.Isbn,
                                 AuthorId = ai.AuthorId
                             };
            var author = from a in dbContext.Authors.Local
                         select new
                         {
                             AuthorId = a.AuthorId,
                             FirstName = a.FirstName,
                             LastName = a.LastName
                         };
            var TitleAndAuthor = from t in title
                                 join ai in authorIsbn on t.ISBN equals ai.ISBN
                                 join a in author on ai.AuthorId equals a.AuthorId
                                 orderby t.Title
                                 select new
                                 {
                                     Title = t.Title,
                                     FirstName = a.FirstName,
                                     LastName = a.LastName
                                 };

            titlesViewSource.Source = TitleAndAuthor;
        }

        private void SortByTitleAndAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var title = from t in dbContext.Titles.Local
                        select new
                        {
                            Title = t.Title,
                            ISBN = t.Isbn
                        };
            var authorIsbn = from ai in dbContext.AuthorIsbn.Local
                             select new
                             {
                                 ISBN = ai.Isbn,
                                 AuthorId = ai.AuthorId
                             };
            var author = from a in dbContext.Authors.Local
                         select new
                         {
                             AuthorId = a.AuthorId,
                             FirstName = a.FirstName,
                             LastName = a.LastName
                         };
            var TitleAndAuthor = from t in title
                                 join ai in authorIsbn on t.ISBN equals ai.ISBN
                                 join a in author on ai.AuthorId equals a.AuthorId
                                 orderby t.Title, a.LastName, a.FirstName
                                 select new
                                 {
                                     Title = t.Title,
                                     FirstName = a.FirstName,
                                     LastName = a.LastName
                                 };

            titlesViewSource.Source = TitleAndAuthor;
        }

        private void GroupByTitleAndAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var title = from t in dbContext.Titles.Local
                        select new
                        {
                            Title = t.Title,
                            ISBN = t.Isbn
                        };
            var authorIsbn = from ai in dbContext.AuthorIsbn.Local
                             select new
                             {
                                 ISBN = ai.Isbn,
                                 AuthorId = ai.AuthorId
                             };
            var author = from a in dbContext.Authors.Local
                         select new
                         {
                             AuthorId = a.AuthorId,
                             FirstName = a.FirstName,
                             LastName = a.LastName
                         };
            var TitleAndAuthor = from t in title
                                 join ai in authorIsbn on t.ISBN equals ai.ISBN
                                 join a in author on ai.AuthorId equals a.AuthorId
                                 select new
                                 {
                                     Title = t.Title,
                                     FirstName = a.FirstName,
                                     LastName = a.LastName
                                 };
            var grouped = TitleAndAuthor.GroupBy(item => item.Title).OrderBy(g => g.Key)
                .Select(g => new { key = g.Key, Authores = g.OrderBy(x=>x.LastName).ThenBy(g1=>g1.FirstName)})
                .SelectMany(g=>g.Authores);

            titlesViewSource.Source = grouped;
        }

        // not for assignment
        private void GroupingDataGridButton_Click(object sender, RoutedEventArgs e)
        {
            var title = from t in dbContext.Titles.Local
                        select new
                        {
                            Title = t.Title,
                            ISBN = t.Isbn
                        };
            var authorIsbn = from ai in dbContext.AuthorIsbn.Local
                             select new
                             {
                                 ISBN = ai.Isbn,
                                 AuthorId = ai.AuthorId
                             };
            var author = from a in dbContext.Authors.Local
                         select new
                         {
                             AuthorId = a.AuthorId,
                             FirstName = a.FirstName,
                             LastName = a.LastName
                         };
            var TitleAndAuthor = from t in title
                                 join ai in authorIsbn on t.ISBN equals ai.ISBN
                                 join a in author on ai.AuthorId equals a.AuthorId
                                 select new
                                 {
                                     Title = t.Title,
                                     FirstName = a.FirstName,
                                     LastName = a.LastName
                                 };

            Window1 GroupByTitleWindow = new Window1(TitleAndAuthor);
            GroupByTitleWindow.Show();
        }        
    }
}
