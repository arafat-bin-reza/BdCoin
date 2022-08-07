using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.ViewModel
{
    public class chainVM
    {
        public int Id { get; set; }
        public string message { get; set; }
        public int index { get; set; }
        public string previous_hash { get; set; }
        public int proof { get; set; }
        public string timestamp { get; set; }
        public List<transactionsVM> transactionsVMs { get; set; } = new List<transactionsVM>();
        public int blockchainId { get; set; }
    }
}
