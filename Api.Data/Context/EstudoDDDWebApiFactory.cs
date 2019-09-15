using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Data.Context
{
    public class EstudoDDDWebApiFactory : IDesignTimeDbContextFactory<EstudoDDDWebApiContext>
    {
        public EstudoDDDWebApiContext CreateDbContext(string[] args)
        {            
            var connectionString = "Server=DEVLEOPERLINK;Database=dbAPI;User Id=sa;Password=leoq15/*";
            var optionsBuilder = new DbContextOptionsBuilder<EstudoDDDWebApiContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new EstudoDDDWebApiContext(optionsBuilder.Options);
        }
    }
}
