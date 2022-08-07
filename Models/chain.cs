using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Models
{
    public class chain
    {
        public int Id { get; set; }
        public string message { get; set; }
        public int index { get; set; }
        public string previous_hash { get; set; }
        public int proof { get; set; }
        public string timestamp { get; set; }
        public List<transactions> transactions { get; set; } = new List<transactions>();
        public int blockchainId { get; set; }
    }
}
