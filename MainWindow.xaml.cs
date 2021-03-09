using LibraryBookManagementApp.src;
using System;
using System.Collections.Generic;
using System.IO;
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
        IList<Rent> rents = new List<Rent>();
        IList<Rent> outdatedRents = new List<Rent>();
        IList<Rent> memberRents = new List<Rent>();

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
            rentDg.ItemsSource = rents;
            rentDg.AutoGenerateColumns = false;
            outdatedDg.ItemsSource = outdatedRents;
            outdatedDg.AutoGenerateColumns = false;
            memberRentsDg.ItemsSource = memberRents;
            memberRentsDg.AutoGenerateColumns = false;
            
            DataLoader dlb = new DataLoader("D:\\dev\\LibraryBookManagementApp\\docs\\konyvek.txt");
            while (true)
            {
                string line = dlb.ReadLine();
                if (line == null)
                    break;
                books.Add(new Book(line));
            }
            DataLoader dlm = new DataLoader("D:\\dev\\LibraryBookManagementApp\\docs\\tagok.txt");
            while (true)
            {
                string line = dlm.ReadLine();
                if (line == null)
                    break;
                members.Add(new Member(line));
            }

            DataLoader dlr = new DataLoader("D:\\dev\\LibraryBookManagementApp\\docs\\kolcsonzesek.txt");
            while (true)
            {
                string line = dlr.ReadLine();
                if (line == null)
                    break;
                rents.Add(new Rent(line));
            }

            DataLoader dlo = new DataLoader("D:\\dev\\LibraryBookManagementApp\\docs\\lejartkolcsonzesek.txt");
            while (true)
            {
                string line = dlo.ReadLine();
                if (line == null)
                    break;
                outdatedRents.Add(new Rent(line));
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

        private void booksDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book selected = (Book)booksDg.SelectedItem;
            if (selected != null)
            {
                isRentableCb.IsChecked = selected.IsRentable;
                bookAuthorTb.Text = selected.BookAuthor;
                bookTitleTb.Text = selected.BookTitle;
                bookReleaseDateTb.Text = selected.BookReleaseDate;
                bookPublisherTb.Text = selected.BookPublisher;
                rentBookIdTb.Text = selected.BookId.ToString();
            }
        }

        private void Book_Save_Changes(object sender, RoutedEventArgs e)
        {
            Book selected = (Book)booksDg.SelectedItem;
            try
            {
                if (selected != null)
                {
                    selected.IsRentable = (bool)isRentableCb.IsChecked;
                    selected.BookAuthor = bookAuthorTb.Text;
                    selected.BookTitle = bookTitleTb.Text;
                    selected.BookReleaseDate = bookReleaseDateTb.Text;
                    selected.BookPublisher = bookPublisherTb.Text;
                    booksDg.Items.Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba az adatok mentésében");
            }
        }

        private void membersDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Member selected = (Member)membersDg.SelectedItem;
            if (selected != null)
            {
                memberRents.Clear();
                memberNameTb.Text = selected.MemberName;
                memberBirthTb.Text = selected.MemberBirth;
                memberZipTb.Text = selected.MemberZip.ToString();
                memberCityTb.Text = selected.MemberCity;
                memberStreetTb.Text = selected.MemberStreet;
                var tmp = rents.Where(x => x.RentMemberId == selected.MemberId);
                foreach (var item in tmp)
                {
                    memberRents.Add(item);
                }
                memberRentsDg.Items.Refresh();
            }
        }

        private void Member_Save_Changes(object sender, RoutedEventArgs e)
        {
            Member selected = (Member)membersDg.SelectedItem;
            try
            {
                if (selected != null)
                {
                    selected.MemberName = memberNameTb.Text;
                    selected.MemberBirth = memberBirthTb.Text;
                    selected.MemberZip = int.Parse(memberZipTb.Text);
                    selected.MemberCity = memberCityTb.Text;
                    selected.MemberStreet = memberStreetTb.Text;
                    membersDg.Items.Refresh();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba az adatok mentésében");
            }
        }

        private void Book_Delete(object sender, RoutedEventArgs e)
        {
            Book selected = (Book)booksDg.SelectedItem;
            books.Remove(selected);
            booksDg.SelectedItem = null;
            booksDg.Items.Refresh();
        }

        private void Member_Delete(object sender, RoutedEventArgs e)
        {
            Member selected = (Member)membersDg.SelectedItem;
            members.Remove(selected);
            membersDg.SelectedItem = null;
            membersDg.Items.Refresh();
        }

        private void Add_Book(object sender, RoutedEventArgs e)
        {
            try
            {
                books.Add(new Book(books.Count + 1, bookAuthorTb.Text, bookTitleTb.Text, bookReleaseDateTb.Text, bookPublisherTb.Text, (bool)isRentableCb.IsChecked));
                booksDg.Items.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba tortent.");
            }

        }
        private void Add_Member(object sender, RoutedEventArgs e)
        {
            try
            {
                members.Add(new Member(members.Count + 1, memberNameTb.Text, memberBirthTb.Text, int.Parse(memberZipTb.Text), memberCityTb.Text, memberStreetTb.Text));
                membersDg.Items.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba tortent.");
            }

        }

        private void Add_Rent(object sender, RoutedEventArgs e)
        {
            try
            {
                rents.Add(new Rent(rents.Count + 1, int.Parse(rentMemberIdTb.Text), int.Parse(rentBookIdTb.Text), DateTime.Today, int.Parse(rentTimeTb.Text)));
                var selected = books.Where(x => x.BookId == int.Parse(rentBookIdTb.Text));
                foreach (var item in selected)
                {
                    item.IsRentable = false;
                }
                booksDg.Items.Refresh();
                rentDg.Items.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Hiba tortent.");
            }

        }
        private void Rent_Delete(object sender, RoutedEventArgs e)
        {
            Rent selected = (Rent)rentDg.SelectedItem;
            if (selected != null)
            {
                int selectedBookId = selected.RentBookId;
                var tmp = books.Where(x => x.BookId == selectedBookId);
                foreach (var item in tmp)
                {
                    item.IsRentable = true;
                }
                rents.Remove(selected);
                rentDg.SelectedItem = null;
                rentDg.Items.Refresh();
                booksDg.Items.Refresh();
            }
        }

        private void OutdatedRent_Delete(object sender, RoutedEventArgs e)
        {
            Rent selected = (Rent)outdatedDg.SelectedItem;
            if (selected != null)
            {
                int selectedBookId = selected.RentBookId;
                var tmp = books.Where(x => x.BookId == selectedBookId);
                foreach (var item in tmp)
                {
                    item.IsRentable = true;
                }
                outdatedRents.Remove(selected);
                outdatedDg.SelectedItem = null;
                outdatedDg.Items.Refresh();
                booksDg.Items.Refresh();
            }
        }

        private void Save_Data(object sender, RoutedEventArgs e)
        {
            DataSaver dsb = new DataSaver("D:\\dev\\LibraryBookManagementApp\\docs\\konyvek.txt");
            foreach (var item in books)
            {
                dsb.WriteLine($"{item.BookId};{item.BookAuthor};{item.BookTitle};{item.BookReleaseDate};{item.BookPublisher};{item.IsRentable}");
            }
            dsb.Close();

            DataSaver dsm = new DataSaver("D:\\dev\\LibraryBookManagementApp\\docs\\tagok.txt");
            foreach (var item in members)
            {
                dsm.WriteLine($"{item.MemberId};{item.MemberName};{item.MemberBirth};{item.MemberZip};{item.MemberCity};{item.MemberStreet}");
            }
            dsm.Close();

            DataSaver dsr = new DataSaver("D:\\dev\\LibraryBookManagementApp\\docs\\kolcsonzesek.txt");
            foreach (var item in rents)
            {
                dsr.WriteLine($"{item.RentId};{item.RentMemberId};{item.RentBookId};{item.RentDate};{item.RentEndDate}");
            }
            dsr.Close();

            DataSaver dso = new DataSaver("D:\\dev\\LibraryBookManagementApp\\docs\\lejartkolcsonzesek.txt");
            foreach (var item in outdatedRents)
            {
                dso.WriteLine($"{item.RentId};{item.RentMemberId};{item.RentBookId};{item.RentDate};{item.RentEndDate}");
            }
            dso.Close();
        }
    }
}
