using BdCoin.Models;
using BdCoin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Service
{
    public class transactionsService : ItransactionsService
    {
        private readonly ItransactionsRepository _transactionsRepository;

        public transactionsService(ItransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        public bool Add(transactions transactionsobj)
        {
            return _transactionsRepository.Add(transactionsobj);
        }
        public bool Delete(int id)
        {
            return _transactionsRepository.Delete(id);
        }
        public List<transactions> GetAll()
        {
            return _transactionsRepository.GetAll();
        }
        public transactions GetById(int id)
        {
            return _transactionsRepository.GetById(id);
        }
    }
}
