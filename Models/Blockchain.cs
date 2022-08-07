using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Models
{
    public class Blockchain
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string message { get; set; }
        public List<chain> chain { get; set; }
        public int length { get; set; }

        public int number_of_transactions { get; set; }
        public int number_of_mine { get; set; }
        public Double balance { get; set; }
    }
}
