using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.ViewModel
{
    public class BlockchainVM
    {
        public int Id { get; set; }
        public string message { get; set; }
        public List<chainVM> chainVMs { get; set; }
        public int length { get; set; }

        public int number_of_transactions { get; set; }
        public int number_of_mine { get; set; }
        public Double balance { get; set; }
    }
}
