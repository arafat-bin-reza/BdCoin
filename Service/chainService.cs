using BdCoin.Models;
using BdCoin.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Service
{
    public class chainService : IchainService
    {
        private readonly IchainRepository _chainRepository;

        public chainService(IchainRepository chainRepository)
        {
            _chainRepository = chainRepository;
        }

        public bool Add(chain chainobj)
        {
            return _chainRepository.Add(chainobj);
        }
        public bool Delete(int id)
        {
            return _chainRepository.Delete(id);
        }
        public List<chain> GetAll()
        {
            return _chainRepository.GetAll();
        }
        public chain GetById(int id)
        {
            return _chainRepository.GetById(id);
        }
    }
}
