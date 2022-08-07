using AutoMapper;
using BdCoin.Models;
using BdCoin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdCoin.AutoMapperConfig
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<BlockchainVM, Blockchain>();
            CreateMap<Blockchain, BlockchainVM>();

            CreateMap<chainVM, chain>();
            CreateMap<chain, chainVM>();

            CreateMap<transactionsVM, transactions>();
            CreateMap<transactions, transactionsVM>();

            CreateMap<add_transactionVM, add_transaction>();
            CreateMap<add_transaction, add_transactionVM>();


            CreateMap<connect_nodeVM, connect_node>();
            CreateMap<connect_node, connect_nodeVM>();


        }
    }
}
