using BdCoin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Repository
{
    public interface IchainRepository
    {
        bool Add(chain chainobj);
        bool Delete(int id);
        List<chain> GetAll();
        chain GetById(int id);
    }
}
