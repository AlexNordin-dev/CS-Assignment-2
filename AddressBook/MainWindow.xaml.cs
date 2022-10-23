﻿using AddressBook.Models;
using AddressBook.Services;
using Newtonsoft.Json;
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
        private ObservableCollection<ContactPerson> contacts;
        private string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ContactPath2.Json";
        private List<ContactPerson> _contacts = new();
        private IFileManager _fileManager = new FileManager();

        public MainWindow()
        {
            InitializeComponent();
            contacts = new ObservableCollection<ContactPerson>();
            lv_Contacts.ItemsSource = contacts;

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            _contacts = JsonConvert.DeserializeObject<List<ContactPerson>>(_fileManager.Read(_filePath))!;

            var contact = contacts.FirstOrDefault(x => x.Email == tb_Email.Text);
            if (contact == null)
            {
                contacts.Add(new ContactPerson
                {
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    Email = tb_Email.Text,
                    StreetName = tb_StreetName.Text,
                    PostalCode = tb_PostalCode.Text,
                    City = tb_City.Text
                });
                
                _fileManager.Save(_filePath, JsonConvert.SerializeObject(_contacts, Formatting.Indented));

            }
            else
            {
                MessageBox.Show("Det finns redan en kontakt med samma e-postadress.");
            }

            ClearFields();
        }

        private void ClearFields()
        {
            tb_FirstName.Text = "";
            tb_LastName.Text = "";
            tb_Email.Text = "";
            tb_StreetName.Text = "";
            tb_PostalCode.Text = "";
            tb_City.Text = "";
        }

        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var contact = (ContactPerson)button!.DataContext;

            contacts.Remove(contact);
        }

        private void lv_Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var contact = (ContactPerson)lv_Contacts.SelectedItems[0]!;
            tb_FirstName.Text = contact.FirstName;
            tb_LastName.Text = contact.LastName;
            tb_Email.Text = contact.Email;
            tb_StreetName.Text = contact.StreetName;
            tb_PostalCode.Text = contact.PostalCode;
            tb_City.Text = contact.City;

        }

        private void tb_PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
