using Microsoft.EntityFrameworkCore;
using NetCoreAI.Project1_ApiDemo.Entities;
using System.Collections.Generic;

namespace NetCoreAI.Project1_ApiDemo.Context
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database=ApiAIDb;Trusted_Connection=True;TrustServerCertificate=true;");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
