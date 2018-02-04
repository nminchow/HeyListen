using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HeyListen
{
    public class DataBase : DbContext
    {
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Channel> Channels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HeyListen;Trusted_Connection=True;");
        }
    }
}
