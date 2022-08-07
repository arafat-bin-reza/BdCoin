using BdCoin.Models;
using BdCoin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Service
{
    public class BlockchainService : IBlockchainService
    {
        private readonly IBlockchainRepository _blockchainRepository;

        public BlockchainService(IBlockchainRepository blockchainRepository)
        {
            _blockchainRepository = blockchainRepository;
        }

        public bool Add(Blockchain blockchainobj)
        {
            return _blockchainRepository.Add(blockchainobj);
        }
        public bool Delete(int id)
        {
            return _blockchainRepository.Delete(id);
        }
        public List<Blockchain> GetAll()
        {
            return _blockchainRepository.GetAll();
        }
        public Blockchain GetById(int id)
        {
            return _blockchainRepository.GetById(id);
        }
    }
}
