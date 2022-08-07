using BdCoin.Data;
using BdCoin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Repository
{
    public class chainRepository : IchainRepository
    {
        private readonly ApplicationDbContext _db;

        public chainRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Add(chain chainobj)
        {
            _db.chains.Add(chainobj);
            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            chain chainobj = _db.chains.FirstOrDefault((c => c.Id == id));
            _db.chains.Remove(chainobj);
            return _db.SaveChanges() > 0;
        }

        public List<chain> GetAll()
        {
            return _db.chains.ToList();
        }
        public chain GetById(int id)
        {
            return _db.chains.FirstOrDefault((c => c.Id == id));
        }
    }
}
