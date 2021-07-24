using Entity;
using Microsoft.EntityFrameworkCore;
using MyInfrastructure;
using System;

namespace Repository
{
    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationHelper.GetConnectionString("SQLServerCon"));

        }

        public DbSet<UserInfo> userInfos { get; set; }
        public DbSet<School>  schools { get; set; }
    }
}
