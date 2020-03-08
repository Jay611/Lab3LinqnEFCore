using BookClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
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
            //titlesViewSource.Source = dbContext.Titles.Local.ToObservableCollection();
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
                             Firstname = a.FirstName,
                             Lastname = a.LastName
                         };
            var TitleAndAuthor = from t in title
                                 join ai in authorIsbn on t.ISBN equals ai.ISBN
                                 join a in author on ai.AuthorId equals a.AuthorId
                                 orderby t.Title
                                 select new
                                 {
                                     Title = t.Title,
                                     AuthorFirstname = a.Firstname,
                                     AuthorLastname = a.Lastname
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
                             Firstname = a.FirstName,
                             Lastname = a.LastName
                         };
            var TitleAndAuthor = from t in title
                                 join ai in authorIsbn on t.ISBN equals ai.ISBN
                                 join a in author on ai.AuthorId equals a.AuthorId
                                 orderby t.Title, a.Lastname, a.Firstname
                                 select new
                                 {
                                     Title = t.Title,
                                     AuthorFirstname = a.Firstname,
                                     AuthorLastname = a.Lastname
                                 };
            titlesViewSource.Source = TitleAndAuthor;
        }
        
    }
}
