﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using SimpleTrader.Domain.Models;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AssetTransaction> AssetTransactions { get; set; }

        public SimpleTraderDbContext(DbContextOptions options) : base(options) { }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetTransaction>().OwnsOne(a => a.Asset);
            base.OnModelCreating(modelBuilder);
        }

    }
}
