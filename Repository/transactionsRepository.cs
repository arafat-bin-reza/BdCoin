using BdCoin.Data;
using BdCoin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Repository
{
    public class transactionsRepository : ItransactionsRepository
    {
        private readonly ApplicationDbContext _db;

        public transactionsRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Add(transactions transactionsobj)
        {
            _db.transactions.Add(transactionsobj);
            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            transactions transactionsobj = _db.transactions.FirstOrDefault((c => c.Id == id));
            _db.transactions.Remove(transactionsobj);
            return _db.SaveChanges() > 0;
        }

        public List<transactions> GetAll()
        {
            return _db.transactions.ToList();
        }
        public transactions GetById(int id)
        {
            return _db.transactions.FirstOrDefault((c => c.Id == id));
        }
    }
}
