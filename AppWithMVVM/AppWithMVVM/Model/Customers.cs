using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithMVVM.Model
{
    class CustomerCollection
    {
        private readonly ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
        }

        public Customer GetCustomerByID(int id)
        {
            return _customers[id];
        }

        public CustomerCollection()
        {
            GenerateCustomers(50);
        }

        private void GenerateCustomers(int howMany)
        {
            var firsts = new List<String>
                         {
                             "Abe",
                             "Alice",
                             "Barry",
                             "Basha",
                             "Charlie",
                             "Colette",
                             "David",
                             "Davida",
                             "Edgar",
                             "Elizabeth",
                             "Frank",
                             "Fran",
                             "George",
                             "Gary",
                             "Harry",
                             "Isaac",
                             "Jesse",
                             "Jessica",
                             "Kevin",
                             "Katrina",
                             "Larry",
                             "Linda",
                             "Mark",
                             "Melinda",
                             "Nick",
                             "Nancy",
                             "Oscar",
                             "Ophilia",
                             "Peter",
                             "Patricia",
                             "Quince",
                             "Quintina",
                             "Robert",
                             "Roberta",
                             "Shy",
                             "Sarah",
                             "Tom",
                             "Teri",
                             "Uberto",
                             "Uma",
                             "Victor",
                             "Victoria",
                             "Walter",
                             "Wendy",
                             "Xerxes",
                             "Xena",
                             "Yaakov",
                             "Yakira",
                             "Zach",
                             "Zahara"
                         };
            var lasts = new List<String>
                        {
                            "Anderson",
                            "Baker",
                            "Connors",
                            "Davidson",
                            "Edgecumbe",
                            "Franklin",
                            "Gregory",
                            "Hines",
                            "Isaacson",
                            "Johnson",
                            "Kennedy",
                            "Liberty",
                            "Mann",
                            "Nickerson",
                            "O'Dwyer",
                            "Patterson",
                            "Quimby",
                            "Richardson",
                            "Stevenson",
                            "Tino",
                            "Usher",
                            "Van Dam",
                            "Walker",
                            "Xenason",
                            "Yager",
                            "Zachery"
                        };

            var random = new Random();
            for (var i = 0; i < howMany; i++)
            {
                var first = firsts[random.Next(0, firsts.Count)];
                var last = lasts[random.Next(0, lasts.Count)];
                _customers.Add(new Customer(i, first, last));
            }
        }
    }

    public class Customer
    {
        public int ID { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        public string Name { get { return First + " " + Last; } }

        public Customer()
        { }

        public Customer(int id, string first, string last)
        {
            ID = id;
            First = first;
            Last = last;
        }
    }
}