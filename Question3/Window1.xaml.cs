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

namespace Question3
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private IEnumerable<object> titleAndAuthor;

        public Window1(IEnumerable<object> titleAndAuthor)
        {
            InitializeComponent();
            this.titleAndAuthor = titleAndAuthor;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource authorsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("authorsViewSource")));
            // Load data by setting the CollectionViewSource.Source property:

            ListCollectionView collection = new ListCollectionView(titleAndAuthor.ToList());
            collection.GroupDescriptions.Add(new PropertyGroupDescription("Title"));
            authorsDataGrid.ItemsSource = collection;
        }
    }
}
