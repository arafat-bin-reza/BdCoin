using BdCoin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.Service
{
    public interface IBlockchainService
    {
        bool Add(Blockchain blockchainobj);
        bool Delete(int id);
        List<Blockchain> GetAll();
        Blockchain GetById(int id);
    }
}
