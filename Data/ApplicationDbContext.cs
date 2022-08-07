using BdCoin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BdCoin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Blockchain> blockchains { get; set; }
        public DbSet<chain> chains { get; set; }
        public DbSet<transactions> transactions { get; set; }
        public DbSet<add_transaction> add_Transactions { get; set; }
        public DbSet<connect_node> connect_Nodes { get; set; }
    }
}
