using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.ViewModel
{
    public class add_transactionVM
    {
        public int Id { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public double amount { get; set; }
    }
}
