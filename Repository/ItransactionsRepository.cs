using BdCoin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Repository
{
    public interface ItransactionsRepository
    {
        bool Add(transactions transactionsobj);
        bool Delete(int id);
        List<transactions> GetAll();
        transactions GetById(int id);
    }
}
