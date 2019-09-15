using Api.Data.Configurations;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Context
{
    public class EstudoDDDWebApiContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public EstudoDDDWebApiContext(DbContextOptions<EstudoDDDWebApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
