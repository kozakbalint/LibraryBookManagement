using LibraryBookManagementApp.src;
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

namespace LibraryBookManagementApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IList<Book> books = new List<Book>();
        IList<Member> members = new List<Member>();
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            booksDg.ItemsSource = books;
            booksDg.AutoGenerateColumns = false;
            membersDg.ItemsSource = members;
            membersDg.AutoGenerateColumns = false;
            DataLoader dlb = new DataLoader(".\\docs\\konyvek.txt");
            while (true)
            {
                string line = dlb.ReadLine();
                if (line == null)
                    break;
                books.Add(new Book(line));
            }
            DataLoader dlm = new DataLoader(".\\docs\\tagok.txt");
            while (true)
            {
                string line = dlm.ReadLine();
                if (line == null)
                    break;
                members.Add(new Member(line));
            }
        }

        private void BookSearchBox_Changed(object sender, TextChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)bookSearchTypeCb.SelectedItem;
            if (bookSearchTb.Text == "")
                booksDg.ItemsSource = books;
            else if (selected.Content.ToString() == "Szerző")
                booksDg.ItemsSource = books.Where(x => x.BookAuthor.Contains(bookSearchTb.Text));
            else if (selected.Content.ToString() == "Cím")
                booksDg.ItemsSource = books.Where(x => x.BookTitle.Contains(bookSearchTb.Text));
            else if (selected.Content.ToString() == "Kiadási év")
                booksDg.ItemsSource = books.Where(x => x.BookReleaseDate.Contains(bookSearchTb.Text));
            else
                booksDg.ItemsSource = books.Where(x => x.BookPublisher.Contains(bookSearchTb.Text));
        }

        private void MemberSearchBox_Changed(object sender, TextChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)memberSearchTypeCb.SelectedItem;
            if (memberSearchTb.Text == "")
                membersDg.ItemsSource = members;
            else if (selected.Content.ToString() == "Név")
                membersDg.ItemsSource = members.Where(x => x.MemberName.Contains(memberSearchTb.Text));
            else if (selected.Content.ToString() == "Születésnap")
                membersDg.ItemsSource = members.Where(x => x.MemberBirth.Contains(memberSearchTb.Text));
            else if (selected.Content.ToString() == "Település")
                membersDg.ItemsSource = members.Where(x => x.MemberCity.Contains(memberSearchTb.Text));
            else
                membersDg.ItemsSource = members.Where(x => x.MemberStreet.Contains(memberSearchTb.Text));
        }
    }
}
