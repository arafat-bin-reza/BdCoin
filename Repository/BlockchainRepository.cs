using BdCoin.Data;
using BdCoin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Repository
{
    public class BlockchainRepository : IBlockchainRepository
    {
        private readonly ApplicationDbContext _db;

        public BlockchainRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Add(Blockchain blockchainobj)
        {
            _db.blockchains.Add(blockchainobj);
            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            Blockchain blockchainobj = _db.blockchains.FirstOrDefault((c => c.Id == id));
            _db.blockchains.Remove(blockchainobj);
            return _db.SaveChanges() > 0;
        }

        public List<Blockchain> GetAll()
        {
            return _db.blockchains.ToList();
        }
        public Blockchain GetById(int id)
        {
            return _db.blockchains.FirstOrDefault((c => c.Id == id));
        }
    }
}
