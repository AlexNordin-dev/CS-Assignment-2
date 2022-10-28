using AddressBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace AddressBook.Services
{
    internal interface IFileManager
    {
        public ObservableCollection<ContactPerson> Read();
        public void Save(ObservableCollection<ContactPerson> contacts);
    }
    internal class FileManager : IFileManager
    {

        //Contact kommer sparas lokalt till en JSON fil på följande Path.
        private string _filePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\ContactPath2.Json";



        public ObservableCollection<ContactPerson> Read() //Läs funktion
        {
            
                var contacts = new ObservableCollection<ContactPerson>();

                using var sr = new StreamReader(_filePath);
                contacts = JsonConvert.DeserializeObject<ObservableCollection<ContactPerson>>(sr.ReadToEnd());
                return contacts;
                     
        }

        public void Save(ObservableCollection<ContactPerson> contacts) //Sparar funktion
        {
           
                using StreamWriter sw = new StreamWriter(_filePath);
                sw.WriteLine(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
                       
        }
    }
}
