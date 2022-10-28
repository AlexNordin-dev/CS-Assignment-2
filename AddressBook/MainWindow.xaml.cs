using AddressBook.Models;
using AddressBook.Services;
using Microsoft.Win32;
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

namespace AddressBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ContactPerson> _contacts;


        private IFileManager _fileManager;


        public MainWindow()
        {
            InitializeComponent();

            _contacts = new ObservableCollection<ContactPerson>();

            _fileManager = new FileManager();
            {
                _contacts = _fileManager.Read();
            }

            lv_Contacts.ItemsSource = _contacts;

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {


            var contact = _contacts.FirstOrDefault(x => x.Email == tb_Email.Text);
            if (contact == null)
            {
                _contacts.Add(new ContactPerson
                {
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    Email = tb_Email.Text,
                    StreetName = tb_StreetName.Text,
                    PostalCode = tb_PostalCode.Text,
                    City = tb_City.Text
                });
                _fileManager.Save(_contacts);
            }
            else
            {
                MessageBox.Show("Det finns redan en kontakt med samma e-postadress.");
            }

            ClearFields();
        }

        private void ClearFields() //Ta bort allt i Formuläret.
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_StreetName.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e) //Raderar den valde kontakten.
        {
            var button = sender as Button;
            var contact = (ContactPerson)button!.DataContext;

            _contacts.Remove(contact); //Spara till listan
            _fileManager.Save(_contacts); //Spara lokalt till en JSON fil.
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btn_Add.Visibility = Visibility.Collapsed;
                btn_Update.Visibility = Visibility.Visible;
                var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
                tb_FirstName.Text = contact.FirstName;
                tb_LastName.Text = contact.LastName;
                tb_Email.Text = contact.Email;
                tb_StreetName.Text = contact.StreetName;
                tb_PostalCode.Text = contact.PostalCode;
                tb_City.Text = contact.City;
            }
            catch { }

        }


        private void tb_PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        { }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {

            var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
            var index = _contacts.IndexOf(contact);

            _contacts[index].FirstName = tb_FirstName.Text;
            _contacts[index].LastName = tb_LastName.Text;
            _contacts[index].Email = tb_Email.Text;
            _contacts[index].StreetName = tb_StreetName.Text;
            _contacts[index].PostalCode = tb_PostalCode.Text;
            _contacts[index].City = tb_City.Text;

            lv_Contacts.Items.Refresh();

            btn_Update.Visibility = Visibility.Collapsed;
            btn_Add.Visibility = Visibility.Visible;

            _fileManager.Save(_contacts); //Spara lokalt till en JSON fil.

            ClearFields();
        }

        private void btn_clean_Click(object sender, RoutedEventArgs e)
        {
            ClearFields(); //Ta bort allt i Formuläret.
            btn_Add.Visibility = Visibility.Visible; //Visa knappen Lägg till.
            btn_Update.Visibility = Visibility.Collapsed; //Visa inte knappen uppdatera och ta emot inte något värde.
        }
    }
}
