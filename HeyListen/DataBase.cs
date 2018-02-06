using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HeyListen
{
    public class DataBase : DbContext
    {
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Channel> Channels { get; set; }

        private string _connection;

        public DataBase(string connection) : base()
        {
            _connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"{_connection}");
        }
    }
}
