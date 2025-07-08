using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountApp
{
    public class BankAccount
    {
        public string Owner { get; set; } // gets and sets the string (public encapsulation so it allows for obtaining data outside and inside of class) of the owner
        public Guid AccountNumber { get; set; } // Guid generates random "key" for user
        public decimal Balance { get; set; }

    }
}
